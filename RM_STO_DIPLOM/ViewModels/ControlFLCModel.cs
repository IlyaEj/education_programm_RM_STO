using CommunityToolkit.Mvvm.Input;
using DynamicData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace RM_STO_DIPLOM.ViewModels
{
    public class ControlFLCModel : INotifyPropertyChanged
    {
        public ICommand CloseFLCCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Declaration declaration;
        public static ObservableCollection<DTErr> DTErrs { get; set; }

        public ControlFLCModel()
        {
            declaration = DeclChekerViewModel.declaration;
            DTErrs = new ObservableCollection<DTErr>();
            CloseFLCCommand = new RelayCommand<DTErr>(CloseFLC);

            if (CheckTheRight())
            {
                DTErrs.Add(new DTErr("ДТ подана неуполномоченным лицом","","гр 54","001","Red"));
            }

            CheckDates();
            CheckGrafes();
        }

        private void CloseFLC(DTErr dTErr)
        {

            if(dTErr == null)
            {
                return;
            }

            try
            {
                dTErr.IsClosed = true;
            }
            catch
            {

            }
        }

        public bool CheckTheRight()
        {
            try
            {
                if (!File.Exists($@"{declaration.Placement}\documents\EP.properties"))
                {
                    return true;
                }
            }
            catch
            {
                return true;
            }

            Dictionary<string, string> _EPproperties = new Dictionary<string, string>();

            foreach (var line in File.ReadAllLines($@"{declaration.Placement}\documents\EP.properties"))
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                    continue;

                var parts = line.Split(new[] { '=' }, 2);

                if (parts.Length == 2)
                {
                    var key = parts[0].Trim();
                    var value = parts[1].Trim();
                    _EPproperties[key] = value;
                }
            }

            if (GetGraf("G_54_3").ToLower() != _EPproperties["SecondName"].ToLower())
                return true;
            if (GetGraf("G_54_3NM").ToLower() != _EPproperties["Name"].ToLower())
                return true;
            if (GetGraf("G_54_3MD").ToLower() != _EPproperties["Patronymic"].ToLower())
                return true;
            if (GetGraf("G_54_8").ToLower() != _EPproperties["CodDoc"].ToLower())
                return true;
            if (GetGraf("G_54_12").ToLower() != _EPproperties["SerDoc"].ToLower())
                return true;
            if (GetGraf("G_54_100").ToLower() != _EPproperties["NumDoc"].ToLower())
                return true;
            if (GetGraf("G_54_101").ToLower() != _EPproperties["DataDoc"].ToLower())
                return true;
            if (GetGraf("G_54_4").ToLower() != _EPproperties["NaimUdDoc"].ToLower())
                return true;
            if (GetGraf("G_54_5").ToLower() != _EPproperties["NamUdDoc"].ToLower())
                return true;
            if (GetGraf("G_54_60").ToLower() != _EPproperties["DataOtUdDoc"].ToLower())
                return true;
            if (GetGraf("G_54_61").ToLower() != _EPproperties["DataDoUdDoc"].ToLower())
                return true;

            return false;
        }

        public void CheckDates()
        {
            ObservableCollection<DTErr> DateErrs = new ObservableCollection<DTErr>();

            foreach(var com in declaration.Commodities)
            {
                foreach(var doc in com.DocumentDTs)
                {
                    try
                    {
                        if (DateTime.Parse(doc.DateDO) < DateTime.Today && doc.DateDO != null)
                        {
                            DateErrs.Add(new DTErr($"Документ с истекшим сроком: {doc.NomDat} \n\n до: {doc.DateDO}", "", "гр 44", "001", "Red"));
                        }
                    }
                    catch
                    {

                    }
                    try
                    {
                        if (DateTime.Parse(doc.DateOT) > DateTime.Today && doc.DateOT != null)
                        {
                            DateErrs.Add(new DTErr($"Документ еще не вступил в силу: {doc.NomDat} \n\n от: {doc.DateOT}", "", "гр 44", "001", "Red"));
                        }
                    }
                    catch
                    {

                    }
                }
            }

            DTErrs.Add(DateErrs);
        }

        public void CheckGrafes()
        {
            ObservableCollection<DTErr> DateErrs = new ObservableCollection<DTErr>();

            if (GetGraf("G_14") == null)
                DateErrs.Add(new DTErr($"Незаполнена графа 14 ДТ", "", "гр 14", "001", "Red"));
            if (GetGraf("G_2") == null)
                DateErrs.Add(new DTErr($"Незаполнена графа 2 ДТ", "", "гр 2", "001", "Red"));
            if (GetGraf("G_8") == null)
                DateErrs.Add(new DTErr($"Незаполнена графа 8 ДТ", "", "гр 8", "001", "Red"));
            if (GetGraf("G_9") == null)
                DateErrs.Add(new DTErr($"Незаполнена графа 9 ДТ", "", "гр 9", "001", "Red"));

            if (GetGraf("G_11_1") == null)
                DateErrs.Add(new DTErr($"Незаполнена графа 11 ДТ", "", "гр 11", "001", "Red"));
            if (GetGraf("G_15A_1") == null)
                DateErrs.Add(new DTErr($"Незаполнена графа 15 ДТ", "", "гр 15", "001", "Red"));
            if (GetGraf("G_16_2") == null)
                DateErrs.Add(new DTErr($"Незаполнена графа 16 ДТ", "", "гр 16", "001", "Red"));
            if (GetGraf("G_17A_1") == null)
                DateErrs.Add(new DTErr($"Незаполнена графа 17 ДТ", "", "гр 17", "001", "Red"));

            if (GetGraf("G_26_1") == null)
                DateErrs.Add(new DTErr($"Код транспортного средства должен быть заполнен в графе 26", "", "гр 18/26", "001", "LightGreen"));
            if (GetGraf("G_25_1") == null)
                DateErrs.Add(new DTErr($"Код транспортного средства должен быть заполнен в графе 25", "", "гр 21/25", "001", "LightGreen"));

            if (GetGraf("G_14_BLD") == null)
                DateErrs.Add(new DTErr($"Не заполнен элемент House декларанта", "", "гр 14", "001", "LightGreen"));
            if (GetGraf("G_14_PHONE") == null)
                DateErrs.Add(new DTErr($"Не заполнены контактные данные декларанта (номер телефона)", "", "гр 14", "001", "LightGreen"));
            if (GetGraf("G_14_EMAIL") == null)
                DateErrs.Add(new DTErr($"Не заполнены контактные данные декларанта (E-mail)", "", "гр 14", "001", "LightGreen"));

            if (GetGraf("G_14_STR") == null)
                DateErrs.Add(new DTErr($"Не заполнен элемент Street декларанта", "", "гр 14", "001", "LightYellow"));
            if (GetGraf("G_14_SUB") == null)
                DateErrs.Add(new DTErr($"Не заполнен элемент Subject декларанта", "", "гр 14", "001", "LightYellow"));
            if (GetGraf("G_14_CC") == null)
                DateErrs.Add(new DTErr($"Не заполнен элемент Country декларанта", "", "гр 14", "001", "LightYellow"));

            int i = 1;
            foreach(var com in declaration.Commodities)
            {
                if (com.ComCO == null)
                    DateErrs.Add(new DTErr($"Незаполнена графа 34 ДТ", i.ToString(), "гр 34", "001", "Red"));
                if (com.ComCode == null)
                    DateErrs.Add(new DTErr($"Незаполнена графа 33 ДТ", i.ToString(), "гр 33", "001", "Red"));
                if(com.ComDesc == null)
                    DateErrs.Add(new DTErr($"Отсутствует описание товара", i.ToString(), "гр 31", "001", "Red"));
                i++;
            }

            if (declaration.Commodities == null)
                DateErrs.Add(new DTErr($"Отсутствуют товары", "", "гр 31", "001", "Red"));

            DTErrs.Add(DateErrs);
        }

        public void CloseFLCAll()
        {
            foreach(var err in DTErrs)
            {
                err.IsClosed = true;
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
    }
}
