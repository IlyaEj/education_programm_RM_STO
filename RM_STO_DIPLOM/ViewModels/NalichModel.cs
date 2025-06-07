using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.IO;

namespace RM_STO_DIPLOM.ViewModels
{
    public class NalichModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Declaration declaration;

        bool addded_1;
        bool addded_2;
        bool addded_3;
        bool addded_4;
        NalichWindow nalichWindow;

        public NalichModel(NalichWindow _nalichWindow)
        {
            declaration = DeclChekerViewModel.declaration;
            FIO = MainViewModel.FIO;
            DateNow = DateTime.Now.ToShortDateString().ToString();
            TimeNow = DateTime.Now.ToShortTimeString().ToString();
            Country = GetGraf("G_15A_1");
            CountryFull = GetGraf("G_15_1");
            Otpr = GetGraf("G_2_NAM");
            Poluch = GetGraf("G_8_NAM");
            Declar = GetGraf("G_14_NAM");
            TORasp = GetGraf("G_30_12");
            AddedDocs = new ObservableCollection<DocumentDT>();
            addded_1 = false;
            addded_2 = false;
            addded_3 = false;
            addded_4 = false;
            nalichWindow = _nalichWindow;

            GetDocMDP();
            GetDocTD();
            GetDoc();
        }

        private string _fio;
        public string FIO
        {
            get => _fio;
            set
            {
                if (_fio != value)
                {
                    _fio = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _dateNow;
        public string DateNow
        {
            get => _dateNow;
            set
            {
                if (_dateNow != value)
                {
                    _dateNow = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _timeNow;
        public string TimeNow
        {
            get => _timeNow;
            set
            {
                if (_timeNow != value)
                {
                    _timeNow = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _country;
        public string Country
        {
            get => _country;
            set
            {
                if (_country != value)
                {
                    _country = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _countryFull;
        public string CountryFull
        {
            get => _countryFull;
            set
            {
                if (_countryFull != value)
                {
                    _countryFull = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _otpr;
        public string Otpr
        {
            get => _otpr;
            set
            {
                if (_otpr != value)
                {
                    _otpr = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _poluch;
        public string Poluch
        {
            get => _poluch;
            set
            {
                if (_poluch != value)
                {
                    _poluch = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _declar;
        public string Declar
        {
            get => _declar;
            set
            {
                if (_declar != value)
                {
                    _declar = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _toRasp;
        public string TORasp
        {
            get => _toRasp;
            set
            {
                if (_toRasp != value)
                {
                    _toRasp = value;
                    OnPropertyChanged();
                }
            }
        }
        private ObservableCollection<DocumentDT> _addedDocs;
        public ObservableCollection<DocumentDT> AddedDocs
        {
            get => _addedDocs;
            set
            {
                if (_addedDocs != value)
                {
                    _addedDocs = value;
                    OnPropertyChanged();
                }
            }
        }

        public void GetDocMDP()
        {
            if (!addded_1)
            {
                foreach (var com in declaration.Commodities)
                {
                    foreach (var doc in com.DocumentDTs)
                    {
                        if (doc.Cod_Vid.Trim() == "02024")
                        {
                            AddedDocs.Add(doc);
                        }
                    }
                }
                addded_1 = true;
            }
        }
        public void GetDoc()
        {
            if (!addded_2)
            {
                foreach (var com in declaration.Commodities)
                {
                    foreach (var doc in com.DocumentDTs)
                    {
                        try
                        {
                            char[] code_items = doc.Cod_Vid.ToCharArray();
                            string code = code_items[0].ToString() + code_items[1].ToString() + code_items[2].ToString();

                            if (code.Trim() == "020" && doc.Cod_Vid != "02024")
                            {
                                AddedDocs.Add(doc);
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                addded_2 = true;
            }
        }

        public void GetDocTD()
        {
            if (!addded_3)
            {
                foreach (var com in declaration.Commodities)
                {
                    foreach (var doc in com.DocumentDTs)
                    {
                        try
                        {
                            if (doc.Cod_Vid.Trim() == "09013")
                            {
                                AddedDocs.Add(doc);
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                addded_3 = true;
            }
        }

        public string GetGraf(string key)
        {
            if (declaration != null)
            {
                string grafTitle = null;
                var elements = declaration.Decl.Descendants();

                foreach (var element in elements)
                {
                    if (element.Name == key)
                    {
                        grafTitle = element.Value;
                        break;
                    }
                }
                return grafTitle;
            }
            else
            {
                return "ошибка";
            }
        }
        public string GetGraf(string key, string subkey)
        {
            if (declaration != null)
            {
                string grafTitle = null;
                var elements = declaration.Decl.Descendants();

                foreach (var element in elements)
                {
                    if (element.Name == key)
                    {
                        var nodes = element.Elements();
                        foreach (var node in nodes)
                            if (node.Name == subkey)
                            {
                                grafTitle = node.Value;
                                break;
                            }
                    }
                }
                return grafTitle;
            }
            else
            {
                return "ошибка";
            }
        }

        public void CloseWin()
        {
            nalichWindow.Close();
        }
        public void SendMes()
        {
            try
            {
                string path = $"{DeclChekerViewModel.declaration.Placement}\\data";
                DirectoryInfo dataFolder = new DirectoryInfo(path);
                if (!dataFolder.Exists)
                {
                    dataFolder.Create();
                }

                if (!File.Exists($@"{dataFolder}\\answer.properties"))
                {
                    FileStream fs = File.Create($@"{dataFolder}\\answer.properties");
                    fs.Dispose();
                    File.AppendAllText($@"{dataFolder}\\answer.properties", "IsSendMesNalich=True\n");
                }
                else
                {
                    File.AppendAllText($@"{dataFolder}\\answer.properties", "IsSendMesNalich=True\n");
                }
            }
            catch
            {

            }
            MesSendedWindow _mesSendedWindow = new MesSendedWindow("1");
            _mesSendedWindow.Show();

            nalichWindow.Close();
        }
    }
}
