namespace KameProgressParser
{
    public class Field : ProgressObject
    {
        public string TableName { get; set; }
        public string Datatype { get; set; }
        public string Format { get; set; }
        public string Initial { get; set; }
        public int Position { get; set; }
        public int Order { get; set; }
        public int ForeignPosition { get; set; }
        public string ForeignType { get; set; }
        public string ForeignName { get; set; }
        public int FieldMisc15 { get; internal set; }
        public string ShadowCol { get; internal set; }
        public int FieldMisc14 { get; internal set; }
        public int FieldMisc13 { get; internal set; }
        public int ForeignCode { get; internal set; }
        public string ClobCodePage { get; internal set; }
        public int ClobType { get; internal set; }
        public string ClobCollation { get; internal set; }

        public override string ToString()
        {
            return $"Field {base.ToString()}: {Datatype} ({Format})";
        }
    }
}
