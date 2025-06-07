using Avalonia;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.IO;

namespace RM_STO_DIPLOM.ViewModels
{
    public class EducationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DeclChekerViewModel declChekerViewModel;

        private int _index;
        public int Index
        {
            get => _index;
            set
            {
                if (_index != value)
                {
                    _index = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isVisibleLast;
        public bool IsVisibleLast
        {
            get => _isVisibleLast;
            set
            {
                if (_isVisibleLast != value)
                {
                    _isVisibleLast = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isVisibleNext;
        public bool IsVisibleNext
        {
            get => _isVisibleNext;
            set
            {
                if (_isVisibleNext != value)
                {
                    _isVisibleNext = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _xPosition;
        public int XPosition
        {
            get => _xPosition;
            set
            {
                if (_xPosition != value)
                {
                    _xPosition = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _yPosition;
        public int YPosition
        {
            get => _yPosition;
            set
            {
                if (_yPosition != value)
                {
                    _yPosition = value;
                    OnPropertyChanged();
                }
            }
        }

        private Thickness _xyPosition;
        public Thickness XYPosition
        {
            get => _xyPosition;
            set
            {
                if (_xyPosition != value)
                {
                    _xyPosition = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _windowText;
        public string WindowText
        {
            get => _windowText;
            set
            {
                if (_windowText != value)
                {
                    _windowText = value;
                    OnPropertyChanged();
                }
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ObservableCollection<EdWind> VisibleWindows { get; set; }

        public EducationViewModel(DeclChekerViewModel dcvm)
        {
            declChekerViewModel = dcvm;
            Index = 0;
            VisibleWindows = LoadData(DeclChekerViewModel.declaration.Placement + "\\EducationWindows.json");
            LoadWindow(Index);
        }

        public void NextWindow()
        {
            if(Index < VisibleWindows.Count - 1)
            {
                Index++;
                LoadWindow(Index);
            }
            else
            {
                return;
            }
        }
        public void LastWindow()
        {
            if (Index != 0)
            {
                Index--;
                LoadWindow(Index);
            }
            else
            {
                return;
            }
        }
        private void LoadWindow(int i)
        {
            XPosition = VisibleWindows[i].X;
            YPosition = VisibleWindows[i].Y;
            WindowText = VisibleWindows[i].Text;
            XYPosition = new Thickness(XPosition, YPosition);

            if(VisibleWindows[i].Command == "OpenFLC")
            {
                declChekerViewModel.OpenFLC(true);
            }
            if (VisibleWindows[i].Command == "CloseFLC")
            {
                declChekerViewModel.OpenFLC(false);
            }
            if (VisibleWindows[i].Command == "FLC")
            {
                declChekerViewModel.OpenFLC();
            }

            if (VisibleWindows[i].Command == "DEN")
            {
                declChekerViewModel.OpenOtkaz();
            }

            if (VisibleWindows[i].Command == "DEF")
            {
                declChekerViewModel.IsCheckedFirst = true;
                declChekerViewModel.IsCheckedSecond = false;
                declChekerViewModel.IsCheckedThird = false;
            }
            if (VisibleWindows[i].Command == "LST")
            {
                declChekerViewModel.IsCheckedFirst = false;
                declChekerViewModel.IsCheckedSecond = true;
                declChekerViewModel.IsCheckedThird = false;
            }
            if (VisibleWindows[i].Command == "COM")
            {
                declChekerViewModel.IsCheckedFirst = false;
                declChekerViewModel.IsCheckedSecond = false;
                declChekerViewModel.IsCheckedThird = true;
            }

            if (VisibleWindows[i].Command == "OpenPVH")
            {
                declChekerViewModel.OpenNalich();
            }
            if (VisibleWindows[i].Command == "DOC")
            {
                declChekerViewModel.OpenDoc();
            }
            if (VisibleWindows[i].Command == "SCR")
            {
                declChekerViewModel.ScrollToEnd();
            }
            if (VisibleWindows[i].Command == "END")
            {
                CloseEdRegime();
            }

            if (Index == VisibleWindows.Count - 1)
            {
                IsVisibleNext = false;
            }
            else
            {
                IsVisibleNext = true;
            }

            if (Index == 0)
            {
                IsVisibleLast = false;
            }
            else
            {
                IsVisibleLast = true;
            }
        }

        public void CloseEdRegime()
        {
            declChekerViewModel.CloseDC.Execute(null);
        }

        private ObservableCollection<EdWind> LoadData(string path)
        {
            string json = File.ReadAllText(path);
            var edWindows = JsonSerializer.Deserialize<List<EdWind>>(json);
            return new ObservableCollection<EdWind>(edWindows);
        }
    }

    public class EdWind
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Text { get; set; }
        public string Command { get; set; }
    }
}
