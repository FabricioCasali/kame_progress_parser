using System;
namespace KameProgressParser
{
    public class ProgressInstruction
    {
        public ProgressInstruction()
        {

        }
        public Action Action { get; set; }
        public ProgressObject Object { get; set; }
        public string SourceCode { get; set; }

        public override String ToString()
        {
            return $"Instruction {Action} {Object}";
        }
    }


}
