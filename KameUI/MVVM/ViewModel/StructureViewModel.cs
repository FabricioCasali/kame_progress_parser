using KameProgressParser;
using KameUI.Core;
using KameUI.MVVM.View;
using System;
using System.Collections.ObjectModel;

namespace KameUI.MVVM.ViewModel
{
    class StructureViewModel : ObservableObject
    {
        public StructureViewModel(ProgressScript progressScript)
        {
            _progressScript = progressScript;
            Components = new ObservableCollection<ProgressObjectViewModel>();
            Load();
        }

        private void Load()
        {
            foreach(var instrucntion in _progressScript.Instructions)
            {
                var cc = new ProgressObjectViewModel(instrucntion);
                AddComp(cc);
            }
        }

        private ObservableCollection<ProgressObjectViewModel> _components;

        public ObservableCollection<ProgressObjectViewModel> Components
        {
            get { return _components; }
            set { _components = value; }
        }

        public void AddComp (ProgressObjectViewModel c)
        {
            Components.Add(c);
            OnPropertyChanged();
        }


        private ProgressScript _progressScript;

    }
}
