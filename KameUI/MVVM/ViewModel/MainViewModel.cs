using Antlr4.Runtime;
using KameProgressParser;
using KameUI.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KameUI.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        private object _currentView;
        private bool _fileLoaded;
        public ImportViewModel ImportVM { get; set; }
        public StructureViewModel StructureVM { get; set; }
        public RelayCommand ImportViewCommand { get; set; }
        public RelayCommand StructureViewCommand { get; set; }

        private ProgressScript _progressScript;
        public MainViewModel()
        {
            ImportVM = new ImportViewModel();
            ImportVM.StartProcessingFile += HandleProgressFile;
            _currentView = ImportVM;
            ImportViewCommand = new RelayCommand(x => { CurrentView = ImportVM; });
            StructureViewCommand = new RelayCommand(x => { CurrentView = StructureVM; });
        }

        private void HandleProgressFile(object sender, string path)
        {
            _progressScript = Load(path);
            StructureVM = new StructureViewModel(_progressScript);
            CurrentView = StructureVM;
        }

        private ProgressScript Load(string path)
        {
            var inputStream = new AntlrFileStream(path);
            var lexer = new ABLProgressLexer(inputStream);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new ABLProgressParser(tokenStream);
            var visitor = new ABLProgressCommandVisitor(tokenStream);
            parser.BuildParseTree = true;
            var s = parser.script();
            var script = visitor.Visit(s);
            FileLoaded = true;
            return script;
        }

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        

        public bool FileLoaded
        {
            get { return _fileLoaded; }
            set
            {
                _fileLoaded = value;
                OnPropertyChanged("FileLoaded");
            }
        }

    }
}
