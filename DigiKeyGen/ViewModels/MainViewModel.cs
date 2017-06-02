using DigiKeyGen.Models;
using DigiKeyGen.ViewModels.Class;
using DigiKeyGen.ViewModels.Commands;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace DigiKeyGen.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private MainModel mainModel;
        private KeyGenerator keyGenerator;

        public MainViewModel()
        {
            mainModel = new MainModel();
            keyGenerator = new KeyGenerator();
        }

        public string ClinicCode
        {
            get { return mainModel.ClinicCode; }
            set
            {
                mainModel.ClinicCode = value;
                OnPropertyChanged("ClinicCode");
                OnPropertyChanged("ClincCodeEncode");
            }
        }

        public string ClincCodeEncode
        {
            get { return string.IsNullOrEmpty(mainModel.ClinicCode) ? string.Empty : keyGenerator.SHA384Encode(mainModel.ClinicCode); }
        }

        private RelayCommand _copyKey;
        public ICommand CopyKeyToClipboard
        {
            get
            {
                if (_copyKey == null)
                {
                    _copyKey = new RelayCommand(CopyKey, CanCopyKey);
                }
                return _copyKey;
            }
        }

        private void CopyKey()
        {
            Clipboard.SetText(ClincCodeEncode);
        }

        private bool CanCopyKey()
        {
            if (string.IsNullOrEmpty(ClincCodeEncode))
                return false;
            else
                return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
