using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection.Metadata;
using RM_STO_DIPLOM.Views;
using Avalonia.Controls;

namespace RM_STO_DIPLOM.ViewModels
{
    public class PodtverjdModel
    {
        MainViewModel mainView;
        private bool p_0;
        private bool p_1;
        private bool p_2_1;
        private bool p_2_2;
        private bool p_3;
        private bool p_4_1;
        private bool p_4_2;
        private bool p_5;
        private bool p_6_1;
        private bool p_6_2;
        private bool p_7;

        PodtverjdWindow podtverjdWindow;
        public PodtverjdModel(
        MainViewModel _mainView, PodtverjdWindow _podtverjdWindow,
            bool _p_0, bool _p_1,bool _p_2_1, bool _p_2_2,bool _p_3, bool _p_4_1, bool _p_4_2, bool _p_5, bool _p_6_1, bool _p_6_2, bool _p_7)
        {
            podtverjdWindow = _podtverjdWindow;

            mainView = _mainView;
            p_0 = _p_0;
            p_1 = _p_1;
            p_2_1 = _p_2_1;
            p_2_2 = _p_2_2;
            p_3 = _p_3;
            p_4_1 = _p_4_1;
            p_4_2 = _p_4_2;
            p_5 = _p_5;
            p_6_1 = _p_6_1;
            p_6_2 = _p_6_2;
            p_7 = _p_7;
        }

        public void CloseWin()
        {
            podtverjdWindow.Close();
        }

        public void EndCheking()
        {
            string content = null;
            content += $"P_0={p_0}\n";
            content += $"P_1={p_1}\n";
            content += $"P_2_1={p_2_1}\n";
            content += $"P_2_2={p_2_2}\n";
            content += $"P_3={p_3}\n";
            content += $"P_4_1={p_4_1}\n";
            content += $"P_4_2={p_4_2}\n";
            content += $"P_5={p_5}\n";
            content += $"P_6_1={p_6_1}\n";
            content += $"P_6_2={p_6_2}\n";
            content += $"P_7={p_7}\n";

            if(p_0 == false)
            {
                File.WriteAllText(DeclChekerViewModel.declaration.Placement + "\\stage.txt", "Otkaz_Reg");
            }
            else
            {
                File.WriteAllText(DeclChekerViewModel.declaration.Placement + "\\stage.txt", "V_Oform");
            }

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
                }
                    var data_answer = new Dictionary<string, string>();

                    foreach (var row in File.ReadAllLines($"{path}\\answer.properties"))
                    {
                        if (row.Length > 0)
                            if (row[0] != '#')
                            {
                                try
                                {
                                    data_answer.Add(row.Split('=')[0], string.Join("=", row.Split('=').Skip(1).ToArray()).ToLower().Trim());
                                }
                                catch
                                {
                                    continue;
                                }
                            }
                    }

                try
                {
                    if (data_answer["FLC"].Trim() == null)
                    {
                        content += "FLC=False\n";
                    }
                }
                catch
                {
                    content += "FLC=False\n";
                }

                try
                {
                    if (data_answer["IsSendMesNalich"].Trim() == null)
                    {
                        content += "IsSendMesNalich=False\n";
                    }
                }
                catch
                {
                    content += "IsSendMesNalich=False\n";
                }

                File.AppendAllText($@"{dataFolder}\\answer.properties", content);
            }
            catch
            {

            }

            mainView.ResultOpen.Execute(null);
            podtverjdWindow.Close();
        }
    }
}
