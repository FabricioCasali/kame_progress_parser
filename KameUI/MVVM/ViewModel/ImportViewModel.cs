using KameUI.Core;
using Microsoft.Win32;
using System;

namespace KameUI.MVVM.ViewModel
{
    public delegate void StartProcessingFile (object sender, string path);

    class ImportViewModel : ObservableObject
    {
        private string _filePath;

        public event StartProcessingFile StartProcessingFile;

        public RelayCommand OpenFileCommand { get; set; }
        public RelayCommand ProcessCommand { get; set; }

        public ImportViewModel()
        {
            OpenFileCommand = new RelayCommand(x => { HandleOpenFileClick(); });
            ProcessCommand = new RelayCommand(x => { HandleProcessClick(); });
        }

        private void HandleProcessClick()
        {
            var mce = StartProcessingFile;
            if (mce != null)
            {
                mce(this, FilePath);
            }
        }

        private void HandleOpenFileClick()
        {
            var fd = new OpenFileDialog();
            if (fd.ShowDialog() == true)
            {
                FilePath = fd.FileName;
            }
        }

        public string FilePath
        {
            get { return _filePath; }
            set
            {
                _filePath = value;
                OnPropertyChanged();
            }
        }

        private Boolean _processed;

        public Boolean Processed
        {
            get { return _processed; }
            set
            {
                _processed = value;
                OnPropertyChanged();
            }
        }
    }
}
