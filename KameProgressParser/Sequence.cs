namespace KameProgressParser
{
    public class Sequence : ProgressObject
    {
        public int Initial { get; set; }
        public int Increment { get; set; }
        public bool CycleOnLimit { get; set; }
        public int? MinVal { get; set; }
        public string ForeigName { get; internal set; }
        public string ForeigOwner { get; internal set; }
    }


}
