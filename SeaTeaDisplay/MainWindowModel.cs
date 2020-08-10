using SeaTeaImageApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SeaTeaDisplay
{
    class MainWindowModel : WithNotification
    {
        public MainWindowModel()
        {
            _ImportCommand = new DelegateCommand(ImportPointClouds, CanExecute);
            BottomMessage = "SeaTea Display initialized.";
        }

        /// <summary>
        /// Import point clouds
        /// </summary>
        /// <param name="param"></param>
        public void ImportPointClouds(object param)
        {

        }

        private bool CanExecute(object param)
        {
            return true;
        }

        #region Commands
        private DelegateCommand _ImportCommand;

        public ICommand ImportCommand
        {
            get { return _ImportCommand; }
        }
        #endregion

        #region Attritubes
        private string _BottomMessage;
        public string BottomMessage
        {
            get { return _BottomMessage; }
            set
            {
                _BottomMessage = value;
                RaisedPropertyChanged(nameof(BottomMessage));
            }
        }
        #endregion
    }
}
