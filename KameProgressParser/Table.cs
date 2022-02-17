using System;

namespace KameProgressParser
{
    public class Table : ProgressObject
    {
        public string TableType { get; set; }
        public string Area { get; set; }
        public String DumpName { get; set; }
        public string ForeignName { get; set; }
        public string ForeignOwner { get; set; }
        public string ForeignType { get; set; }
        public int? ProgressRecid { get; internal set; }

        public override string ToString()
        {
            return $"Table {base.ToString()}";
        }
    }


}
