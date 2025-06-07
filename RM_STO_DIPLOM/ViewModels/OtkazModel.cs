using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RM_STO_DIPLOM.ViewModels
{
    public class OtkazModel : ViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        WindowOtkaz windowOtkaz;

        public OtkazModel(WindowOtkaz _windowOtkaz)
        {
            windowOtkaz = _windowOtkaz;
            CanSenRes = false;
        }

        public void CloseWin()
        {
            windowOtkaz.Close();
        }

        public void OpenPodtverjd()
        {
            var windPodv = new PodtverjdWindow(DeclChekerViewModel.MainVm, false, P_1, P_2_1, P_2_2, P_3, P_4_1, P_4_2, P_5, P_6_1, P_6_2, P_7);
            windPodv.Show();
            windowOtkaz.Close();
        }

        private bool _canSenRes;
        public bool CanSenRes
        {
            get => _canSenRes;
            set
            {
                if (_canSenRes != value)
                {
                    _canSenRes = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _p_1;
        public bool P_1
        {
            get => _p_1;
            set
            {
                if (_p_1 != value)
                {
                    _p_1 = value;
                    ChekPossibilityToSendRes();
                    OnPropertyChanged();
                }
            }
        }
        private bool _p_2_1;
        public bool P_2_1
        {
            get => _p_2_1;
            set
            {
                if (_p_2_1 != value)
                {
                    _p_2_1 = value;
                    ChekPossibilityToSendRes();
                    OnPropertyChanged();
                }
            }
        }
        private bool _p_2_2;
        public bool P_2_2
        {
            get => _p_2_2;
            set
            {
                if (_p_2_2 != value)
                {
                    _p_2_2 = value;
                    ChekPossibilityToSendRes();
                    OnPropertyChanged();
                }
            }
        }
        private bool _p_3;
        public bool P_3
        {
            get => _p_3;
            set
            {
                if (_p_3 != value)
                {
                    _p_3 = value;
                    ChekPossibilityToSendRes();
                    OnPropertyChanged();
                }
            }
        }
        private bool _p_4_1;
        public bool P_4_1
        {
            get => _p_4_1;
            set
            {
                if (_p_4_1 != value)
                {
                    _p_4_1 = value;
                    ChekPossibilityToSendRes();
                    OnPropertyChanged();
                }
            }
        }
        private bool _p_4_2;
        public bool P_4_2
        {
            get => _p_4_2;
            set
            {
                if (_p_4_2 != value)
                {
                    _p_4_2 = value;
                    ChekPossibilityToSendRes();
                    OnPropertyChanged();
                }
            }
        }
        private bool _p_5;
        public bool P_5
        {
            get => _p_5;
            set
            {
                if (_p_5 != value)
                {
                    _p_5 = value;
                    ChekPossibilityToSendRes();
                    OnPropertyChanged();
                }
            }
        }
        private bool _p_6_1;
        public bool P_6_1
        {
            get => _p_6_1;
            set
            {
                if (_p_6_1 != value)
                {
                    _p_6_1 = value;
                    ChekPossibilityToSendRes();
                    OnPropertyChanged();
                }
            }
        }
        private bool _p_6_2;
        public bool P_6_2
        {
            get => _p_6_2;
            set
            {
                if (_p_6_2 != value)
                {
                    _p_6_2 = value;
                    ChekPossibilityToSendRes();
                    OnPropertyChanged();
                }
            }
        }
        private bool _p_7;
        public bool P_7
        {
            get => _p_7;
            set
            {
                if (_p_7 != value)
                {
                    _p_7 = value;
                    ChekPossibilityToSendRes();
                    OnPropertyChanged();
                }
            }
        }

        public void ChekPossibilityToSendRes()
        {
            if (P_1 || P_2_1 || P_2_2 || P_3 || P_4_1 || P_4_2 || P_5 || P_6_1 || P_6_2 || P_7)
            {
                CanSenRes = true;
            }
            else
            {
                CanSenRes = false;
            }
        }
    }
}
