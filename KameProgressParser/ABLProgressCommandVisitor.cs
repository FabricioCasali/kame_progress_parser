using System;
using System.Collections.Generic;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime;
using static ABLProgressParser;

namespace KameProgressParser
{
    public class ABLProgressCommandVisitor : ABLProgressBaseVisitor<ProgressScript>
    {
        private ProgressScript _script = new ProgressScript();
        private Action? _action = null;
        private ProgressInstruction _currentInstruction;
        private CommonTokenStream _tokens;
        public override ProgressScript VisitIndexObject([NotNull] ABLProgressParser.IndexObjectContext context)
        {
            var index = new Index();
            index.Name = context.objectName().GetText().Replace("\"", "");
            index.TableName = context.indexOnValue().GetText().Replace("\"", "");

            var args = context.indexOptions();
            if (args != null)
            {
                foreach (IndexOptionsContext arg in args)
                {
                    if (arg.KW_AREA() != null)
                    {
                        foreach (var v in _tokens.Get(arg.indexAreavalue().Start.TokenIndex, arg.indexAreavalue().Stop.TokenIndex))
                            index.Area += v.Text.Replace("\"", "");
                    }
                    else if (arg.KW_PRIMARY() != null)
                    {
                        index.IsPrimary = true;
                    }
                    else if (arg.KW_UNIQUE() != null)
                    {
                        index.IsUnique = true;
                    }
                    else if (arg.KW_FOREIGN_NAME() != null)
                    {
                        index.ForeignName = arg.indexForeignNameValue().GetText().Replace("\"", "");
                    }
                    else if (arg.KW_INDEX_NUM() != null)
                    {
                        index.IndexNum = Int32.Parse(arg.indexNumValue().GetText());
                    }
                }

                var fields = context.indexFields();
                if (fields != null)
                {
                    index.Fields = new List<IndexField>();
                    foreach (var field in fields)
                    {
                        var indexField = new IndexField();
                        index.Fields.Add(indexField);
                        indexField.Name = field.indexFieldValue().GetText().Replace("\"", "");
                        if (field.KW_INDEX_SORT_ASC() == null && field.KW_INDEX_SORT_DESC() == null)
                        {
                            indexField.Sort = IndexFieldSort.Ascending;
                        }
                        else if (field.KW_INDEX_SORT_ASC() != null)
                        {
                            indexField.Sort = IndexFieldSort.Ascending;
                        }
                        else
                        {
                            indexField.Sort = IndexFieldSort.Descending;
                        }
                    }
                }

                _currentInstruction.Object = index;
                _currentInstruction.Action = _action.Value;
            }
            return base.VisitIndexObject(context);
        }
        public override ProgressScript VisitFieldObject([NotNull] ABLProgressParser.FieldObjectContext context)
        {
            var field = new Field();
            field.Name = context.objectName().GetText().Replace("\"", "");
            field.TableName = context.fieldOfValue().GetText().Replace("\"", "");
            field.Datatype = context.fieldDataTypeValue().GetText().Replace("\"", "");
            var args = context.fieldOptions();
            if (args != null)
            {
                foreach (var arg in args)
                {
                    if (arg.KW_FORMAT() != null)
                    {
                        field.Format = arg.fieldFormatValue().GetText().Replace("\"", "");
                    }
                    else if (arg.KW_INITIAL() != null)
                    {
                        field.Initial = arg.fieldInitialValue().GetText().Replace("\"", "");
                    }
                    else if (arg.KW_ORDER() != null)
                    {
                        field.Order = Int32.Parse(arg.fieldOrderValue().GetText());
                    }
                    else if (arg.KW_POSITION() != null)
                    {
                        field.Position = Int32.Parse(arg.fieldPositionValue().GetText());
                    }
                    else if (arg.KW_FOREIGN_NAME() != null)
                    {
                        field.ForeignName = arg.fieldForeignNameValue().GetText().Replace("\"", "");
                    }
                    else if (arg.KW_FOREIGN_POS() != null)
                    {
                        field.ForeignPosition = Int32.Parse(arg.fieldForeignPosValue().GetText());
                    }
                    else if (arg.KW_FOREIGN_TYPE() != null)
                    {
                        field.ForeignType = arg.foreignTypeValues().GetText().Replace("\"", "");
                    }
                    else if (arg.KW_SHADOW_COL() != null)
                    {
                        field.ShadowCol = arg.fieldShadowColValue().GetText().Replace("\"", "");
                    }
                    else if (arg.KW_FIELD_MISC13() != null)
                    {
                        field.FieldMisc13 = Int32.Parse(arg.fieldMisc13Value().GetText());
                    }
                    else if (arg.KW_FIELD_MISC14() != null)
                    {
                        field.FieldMisc14 = Int32.Parse(arg.fieldMisc14Value().GetText());
                    }
                    else if (arg.KW_FIELD_MISC15() != null)
                    {
                        field.FieldMisc15 = Int32.Parse(arg.fieldMisc15Value().GetText());
                    }
                    else if (arg.KW_FOREIGN_CODE() != null)
                    {
                        field.ForeignCode = Int32.Parse(arg.fieldForeignCodeValue().GetText());
                    }
                    else if (arg.KW_CLOB_CODEPAGE() != null)
                    {
                        field.ClobCodePage = arg.fieldClobCodePageValue().GetText().Replace("\"", "");
                    }
                    else if (arg.KW_CLOB_TYPE() != null)
                    {
                        field.ClobType = Int32.Parse(arg.fieldClobTypeValue().GetText());
                    }
                    else if (arg.KW_CLOB_COLLATION() != null)
                    {
                        field.ClobCollation = arg.fieldClobCollationValue().GetText().Replace("\"", "");
                    }
                }
            }

            _currentInstruction.Object = field;
            _currentInstruction.Action = _action.Value;
            return base.VisitFieldObject(context);
        }

        public override ProgressScript VisitQuotedString([NotNull] ABLProgressParser.QuotedStringContext context)
        {

            return base.VisitQuotedString(context);
        }

        public override ProgressScript VisitAction([NotNull] ABLProgressParser.ActionContext context)
        {
            if (context.Start.Text.Equals("ADD")) _action = Action.ADD;
            else if (context.Start.Text.Equals("ALTER")) _action = Action.ALTER;
            else if (context.Start.Text.Equals("DROP")) _action = Action.DROP;
            return base.VisitAction(context);
        }


        public override ProgressScript VisitScript([NotNull] ABLProgressParser.ScriptContext context)
        {
            base.VisitScript(context);
            return _script;
        }


        public ABLProgressCommandVisitor(CommonTokenStream tokens)
        {
            _tokens = tokens;
        }

        public override ProgressScript VisitTableObject([NotNull] ABLProgressParser.TableObjectContext context)
        {
            var table = new Table();
            table.Name = context.objectName().GetText().Replace("\"", "");
            if (context.KW_TYPE() != null)
                table.TableType = context.tableType().GetText();

            var args = context.tableOptions();
            if (args != null)
            {
                foreach (var arg in args)
                {
                    if (arg.KW_AREA() != null)
                    {
                        foreach (var v in _tokens.Get(arg.tableAreaValue().Start.TokenIndex, arg.tableAreaValue().Stop.TokenIndex))
                            table.Area += v.Text.Replace("\"", "");

                    }
                    else if (arg.KW_DUMP_NAME() != null)
                    {
                        table.DumpName = arg.tableDumpNameValue().GetText().Replace("\"", "");
                    }
                    else if (arg.KW_FOREIGN_NAME() != null)
                    {
                        table.ForeignName = arg.tableForeignNameValue().GetText().Replace("\"", "");
                    }
                    else if (arg.KW_FOREIGN_OWNER() != null)
                    {
                        table.ForeignOwner = arg.tableForeignOwnerValue().GetText().Replace("\"", "");
                    }
                    else if (arg.KW_FOREIGN_TYPE() != null)
                    {
                        table.ForeignType = arg.foreignTypeValues().GetText().Replace("\"", "");
                    }
                    else if (arg.KW_PROGRESS_RECID() != null)
                    {
                        table.ProgressRecid = Int32.Parse(arg.tableProgressRecidValue().GetText());
                    }
                }
            }

            _currentInstruction.Object = table;
            _currentInstruction.Action = _action.Value;
            return base.VisitTableObject(context);
        }


        public override ProgressScript VisitSequenceObject([NotNull] ABLProgressParser.SequenceObjectContext context)
        {
            var seq = new Sequence();
            seq.Name = context.objectName().GetText().Replace("\"", "");

            var args = context.sequenceOptions();
            if (args != null)
            {
                foreach (var arg in args)
                {
                    if (arg.KW_INITIAL() != null)
                    {
                        seq.Initial = Int32.Parse(arg.sequenceInitialValue().GetText());
                    }
                    else if (arg.KW_INCREMENT() != null)
                    {
                        seq.Increment = Int32.Parse(arg.sequenceIncrementValue().GetText());
                    }
                    else if (arg.KW_CYCLE_ON_LIMIT() != null)
                    {
                        seq.CycleOnLimit = arg.sequenceCycleOnLimitValue().GetText().Equals("yes", StringComparison.CurrentCultureIgnoreCase);
                    }
                    else if (arg.KW_MIN_VAL() != null)
                    {
                        var minVal = arg.sequenceMinValue().GetText();
                        if (!string.IsNullOrWhiteSpace(minVal) && !minVal.Equals("?"))
                        {
                            seq.MinVal = Int32.Parse(minVal);
                        }
                    }
                    else if (arg.KW_FOREIGN_NAME() != null)
                    {
                        seq.ForeigName = arg.sequenceForeignName().GetText().Replace("\"", "");
                    }
                    else if (arg.KW_FOREIGN_OWNER() != null)
                    {
                        seq.ForeigOwner = arg.sequenceForeigOwner().GetText().Replace("\"", "");
                    }
                }
            }

            _currentInstruction.Object = seq;
            _currentInstruction.Action = _action.Value;
            return base.VisitSequenceObject(context);
        }

        public override ProgressScript VisitInstruction([NotNull] ABLProgressParser.InstructionContext context)
        {
            if (_script.Instructions == null)
                _script.Instructions = new List<ProgressInstruction>();

            _currentInstruction = new ProgressInstruction();
            _script.Instructions.Add(_currentInstruction);
            return base.VisitInstruction(context);
        }

    }


}
