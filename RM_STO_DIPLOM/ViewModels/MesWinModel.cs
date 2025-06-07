using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RM_STO_DIPLOM.ViewModels
{
    public class MesWinModel
    {
        string path = null;
        MesSendedWindow mesSendedWindow;
        public MesWinModel(string i, MesSendedWindow _mesSendedWindow)
        {
            mesSendedWindow = _mesSendedWindow;
            path = $"{DeclChekerViewModel.declaration.Placement}\\data";
            PushMesIm(i);
            PushMesEx(i);
        }

        public void CloseWin()
        {
            mesSendedWindow.Close();
        }
        
        public void PushMesIm(string i)
        {
            if (File.Exists($@"{path}\\correct.properties"))
            {
                var data = new Dictionary<string, string>();
                foreach (var row in File.ReadAllLines($"{path}\\correct.properties"))
                {
                    try
                    {
                        if (row.Length > 0)
                            if (row[0] != '#')
                                data.Add(row.Split('=')[0], string.Join("=", row.Split('=').Skip(1).ToArray()));
                    }
                    catch
                    {
                        continue;
                    }
                }

                if (i == "0")
                {
                    try
                    {
                        if (data["IsPribit"] == "True" || data["IsPribit"] == "true")
                        {
                            DeclChekerViewModel.ImportMes.Add(new DocumentDT("Информация о прибытии товара", "Товар прибыл"));
                        }
                        else
                        {
                            DeclChekerViewModel.ImportMes.Add(new DocumentDT("Информация о прибытии товара", "Товар не прибыл"));
                        }
                    }
                    catch
                    {

                    }
                }

                if (i == "1")
                {
                    try
                    {
                        if (data["IsNalich"] == "True" || data["IsNalich"] == "true")
                        {
                            DeclChekerViewModel.ImportMes.Add(new DocumentDT("Информация о наличии товара", "Товар в наличии"));
                        }
                        else
                        {
                            DeclChekerViewModel.ImportMes.Add(new DocumentDT("Информация о наличии товара", "Товара нет в наличии"));
                        }
                    }
                    catch
                    {

                    }

                }
            }
            else
            {
                DeclChekerViewModel.ImportMes.Add(new DocumentDT("ОТСУТСТВУЕТ ФАЙЛ С ОТВЕТОМ", "ДОБАВЬТЕ ФАЙЛ"));
            }
        }
        public void PushMesEx(string i)
        {
            if(i == "0")
            {
                DeclChekerViewModel.ExportMes.Add(new DocumentDT("Запрос о прибытии", "Направлен запрос о прибытии"));
            }
            if (i == "1")
            {
                DeclChekerViewModel.ExportMes.Add(new DocumentDT("Запрос о наличии", "Направлен запрос о наличии товаров"));
            }
        }

    }
}
