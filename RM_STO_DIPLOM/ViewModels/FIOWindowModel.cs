using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RM_STO_DIPLOM.ViewModels
{
    public class FIOWindowModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string _fioInput;
        private string FIOInput
        {
            get => _fioInput;
            set
            {
                if (_fioInput != value)
                {
                    _fioInput = value;
                    OnPropertyChanged();
                }
            }
        }

        FIOWindow FIOWin;

        public FIOWindowModel(FIOWindow fIO)
        {
            FIOWin = fIO;
        }

        public void CloseFIO()
        {
            if (FIOInput != null && FIOInput != "Введите ФИО!")
            {
                MainViewModel.FIO = FIOInput;
                FIOWin.Close();
            }
            else
            {
                FIOInput = "Введите ФИО!";
            }
        }
    }
}
