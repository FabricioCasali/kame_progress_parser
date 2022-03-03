using System;
using System.Collections.Generic;
using System.Text;

namespace KameProgressParser
{
    public class OracleGenerator
    {
        private int _numberOfTables;
        private StringBuilder _sb;

        public int MinimumLengthOfToken { get; set; }

        public OracleGenerator()
        {
            MinimumLengthOfToken = 3;
        }
        public string Generate(ProgressScript _script)
        {
            _sb = new StringBuilder();

            foreach (var i in _script.Instructions)
            {
                if (i.Object.ObjectType.Equals("TABLE", StringComparison.InvariantCultureIgnoreCase))
                {
                    // create the default sequence, for recid
                    GenerateSequence(i.Action, new Sequence() { Name = i.Object.Name + "_SEQ", Initial = 1, Increment = 1 }, true);
                    GenerateTable(Action.ADD, i.Object as Table);
                    GenerateIndex(Action.ADD, new Index() { Name = "REC", TableName = i.Object.Name, Fields = new List<IndexField>() { new IndexField() { Name = "PROGRESS_RECID", Sort = IndexFieldSort.Ascending } } });
                }
                else if (i.Object.ObjectType.Equals("SEQUENCE", StringComparison.InvariantCultureIgnoreCase))
                {
                    GenerateSequence(i.Action, i.Object as Sequence);
                }
                else if (i.Object.ObjectType.Equals("FIELD", StringComparison.InvariantCultureIgnoreCase))
                {
                    GenerateField(i.Action, i.Object as Field);
                }
                else if (i.Object.ObjectType.Equals("INDEX", StringComparison.InvariantCultureIgnoreCase))
                {
                    GenerateIndex(i.Action, i.Object as Index);
                }
            }
            return _sb.ToString();
        }

        private void GenerateIndex(Action action, Index index)
        {
            var unique = index.IsUnique ? "UNIQUE " : "";
            _sb.Append($"CREATE {unique}INDEX {ToOracleValidName(index.TableName + "##" + index.Name)} ON {ToOracleValidName(index.TableName)}");
            for (int i = 0; i < index.Fields.Count; i++)
            {
                var field = index.Fields[i];
                if (i == 0)

                    _sb.Append("(");
                else
                    _sb.Append(",");
                _sb.Append($"{ToOracleValidName(field.Name)}");
                var order = field.Sort == IndexFieldSort.Ascending ? "ASC" : "DESC";
                _sb.Append($" {order}");
            }
            _sb.Append(");\n");
        }

        private void GenerateField(Action action, Field field)
        {
            _sb.Append($"ALTER TABLE {ToOracleValidName(field.TableName)} ADD ({ToOracleValidName(field.Name)} {ConvertDataType(field.Datatype, field.Format)}");
            if (!string.IsNullOrEmpty(field.Initial) && !field.Initial.Equals("?"))
            {
                _sb.Append($" DEFAULT {FormatInitialValue(field.Initial, field.Datatype)}");
            }
            _sb.Append(");\n");
        }

        private object FormatInitialValue(string initial, string datatype)
        {
            if (datatype.StartsWith("CHAR", StringComparison.InvariantCultureIgnoreCase))
            {
                return $"\"{initial}\"";
            }
            else return initial;
        }

        private void GenerateSequence(Action action, Sequence seq, bool autoSequence = false)
        {
            var initial = seq.Initial == 0 ? 1 : seq.Initial;
            var increment = seq.Increment == 0 ? 1 : seq.Increment;
            _sb.Append($"CREATE SEQUENCE { ToOracleValidName(seq.Name, autoSequence)} START WITH {initial} INCREMENT BY {increment};\n");
        }

        private void GenerateTable(Action action, Table table)
        {
            _sb.Append($"CREATE TABLE {ToOracleValidName(table.Name)} (PROGRESS_RECID NUMBER NULL);\n");
        }

        public string ConvertDataType(string datatype, string format)
        {
            datatype = datatype.Trim();
            if (datatype.Equals("INTEGER", StringComparison.InvariantCultureIgnoreCase) ||
                datatype.Equals("INT", StringComparison.InvariantCultureIgnoreCase))
                return "NUMBER";

            if (datatype.Equals("DECIMAL", StringComparison.InvariantCultureIgnoreCase) ||
                datatype.Equals("DEC", StringComparison.InvariantCultureIgnoreCase))
                return "NUMBER";

            if (datatype.Equals("LOGICAL", StringComparison.InvariantCultureIgnoreCase) ||
                datatype.Equals("LOG", StringComparison.InvariantCultureIgnoreCase))
                return "NUMBER";

            if (datatype.Equals("CHARACTER", StringComparison.InvariantCultureIgnoreCase) ||
                datatype.Equals("CHAR", StringComparison.InvariantCultureIgnoreCase))
            {
                var converted = "VARCHAR2";
                if (!string.IsNullOrEmpty(format))
                {
                    if (format.StartsWith("x("))
                    {
                        var size = Convert.ToInt32(format.Substring(2).Replace(")", ""));
                        converted += $"({size})";
                    }
                    else
                    {
                        var size = Convert.ToInt32(format.Length);
                        converted += $"({size})";
                    }
                }
                return converted;
            }

            if (datatype.Equals("DATE", StringComparison.InvariantCultureIgnoreCase) ||
                datatype.Equals("DATETIME", StringComparison.InvariantCultureIgnoreCase) ||
                datatype.Equals("DATETIME-TZ", StringComparison.InvariantCultureIgnoreCase))
                return "DATE";


            if (datatype.Equals("CLOB", StringComparison.InvariantCultureIgnoreCase))
                return "CLOB";


            throw new Exception($"datatype unknown: {datatype}");
        }

        public string ToOracleValidName(string name, bool avoidLast = false)
        {
            var finalName = name.Replace(" ", "_").Replace("-", "_").Trim().ToUpper();
            var totalLength = finalName.Length;
            while (totalLength > 30)
            {
                var tokens = finalName.Split('_');
                var index = 0;
                var biggest = 0;
                for (var i = 0; i < tokens.Length; i++)
                {
                    if (i == 0)
                        continue;
                    if (tokens[i].Length > biggest && (avoidLast && i < tokens.Length - 1) == false)
                    {
                        biggest = tokens[i].Length;
                        index = i;
                    }
                }

                var larger = tokens[index];
                var reduce = finalName.Length - 30;
                if (reduce > larger.Length)
                {
                    reduce = larger.Length - MinimumLengthOfToken;
                }
                var reduced = larger.Substring(0, larger.Length - reduce);
                tokens[index] = reduced;
                finalName = string.Join("_", tokens);
                totalLength = finalName.Length;
            }
            return finalName;
        }
    }
}