using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RM_STO_DIPLOM.ViewModels
{
    public class DTErr : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DTErr(string desc, string nomCom, string nomGraf, string nomErr, string crit)
        {
            Description = desc;
            NomCom = nomCom;
            NomGraf = nomGraf;
            NomErr = nomErr;
            Crit = crit;
            IsClosed = false;
        }
        public string Description { get;}
        public string NomCom { get; }
        public string NomGraf { get; }
        public string NomErr { get; }
        public string _crit;
        public string Crit
        {
            get => _crit;
            set
            {
                if (_crit != value)
                {
                    _crit = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool _isClosed;
        public bool IsClosed
        {
            get => _isClosed;
            set
            {
                if (_isClosed != value)
                {
                    _isClosed = value;
                    OnPropertyChanged();
                    ChangeCrit();
                }
            }
        }
        public void ChangeCrit()
        {
            Crit = "LightGray";
        }
    }
}
