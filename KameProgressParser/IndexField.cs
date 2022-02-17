namespace KameProgressParser
{
    public class IndexField : Index
    {
        public IndexFieldSort Sort { get; set; }

        public override string ToString()
        {
            return $"IndexField {base.ToString()} {Sort.ToString()}";
        }
    }
}