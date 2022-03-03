using System.Collections.Generic;

namespace KameProgressParser
{
    public class Index : ProgressObject
    {
        public string Area { get; internal set; }
        public bool IsPrimary { get; internal set; }
        public bool IsUnique { get; internal set; }
        public string ForeignName { get; internal set; }
        public int IndexNum { get; internal set; }
        public string TableName { get; internal set; }
        internal List<IndexField> Fields { get; set; }

        public override string ToString()
        {
            return $"Index {base.ToString()} on {TableName}";
        }
    }
}
