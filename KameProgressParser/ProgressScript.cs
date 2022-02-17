using System.Collections.Generic;

namespace KameProgressParser
{
    public class ProgressScript
    {
        public ProgressScript()
        {

        }

        public IList<ProgressInstruction> Instructions { get; set; }

        public override string ToString()
        {
            var sb = new System.Text.StringBuilder();
            foreach (var i in Instructions)
            {
                sb.AppendLine(i.ToString());
            }
            return sb.ToString();
        }
    }


}
