namespace KameProgressParser
{
    public abstract class ProgressObject
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }

        public virtual string ObjectType
        {
            get { return GetType().Name; }
        }
    }


}
