using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace RM_STO_DIPLOM.ViewModels
{
    public class PribitModel
    {
        public string DateNow { get; set; }
        public string Declar { get; set; }
        public string Transp { get; set; }
        public string TimeNow { get; set; }
        public string FIO { get; set; }
        PribitWindow pribitWindow;
        private Declaration declaration;
        public PribitModel(PribitWindow _pribitWindow)
        {
            declaration = DeclChekerViewModel.declaration;
            DateNow = DateTime.Now.ToShortDateString().ToString();
            TimeNow = DateTime.Now.ToShortTimeString().ToString();
            Declar = GetGraf("G_14_NAM");
            Transp = $"Код вида ТС: {GetGraf("G_25_1")}\nКол-во ТС: {GetGraf("G_21_0")}\nНомер ТС: {GetGraf("G_21")}\nСтрана регистрации: {GetGraf("G_21_2")}";
            FIO = MainViewModel.FIO;
            CommoditiesPR = new ObservableCollection<Commodity>();
            pribitWindow = _pribitWindow;
        }

        public ObservableCollection<Commodity> CommoditiesPR { get; }

        public void AddCom()
        {
            foreach(var com in declaration.Commodities)
            {
                CommoditiesPR.Add(com);
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

        public void CloseWin()
        {
            pribitWindow.Close();
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
                    File.AppendAllText($@"{dataFolder}\\answer.properties", "IsSendMesPribit=True\n");
                }
                else
                {
                    File.AppendAllText($@"{dataFolder}\\answer.properties", "IsSendMesPribit=True\n");
                }
            }
            catch
            {

            }
            MesSendedWindow _mesSendedWindow = new MesSendedWindow("0");
            _mesSendedWindow.Show();

            pribitWindow.Close();
        }
    }
}
