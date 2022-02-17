using KameProgressParser;
using KameUI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KameUI.MVVM.ViewModel
{
    class ProgressObjectViewModel : ObservableObject
    {

        public ProgressObjectViewModel(ProgressInstruction instrucntion)
        {
            _instruction = instrucntion; 
        }

        public ProgressObjectViewModel()
        {

        }

        private ProgressObject  _obj;
        private ProgressInstruction _instruction;

        public ProgressObject ProgressObject
        {
            get { return _obj; }
            set { _obj = value; }
        }

    }
}
