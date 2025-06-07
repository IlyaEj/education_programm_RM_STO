using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace RM_STO_DIPLOM.ViewModels
{
    public class ResultModel
    {
        public string Ocenka { get; set; }
        public string ResultPath { get; set; }
        public Declaration declaration;
        public ObservableCollection<Mistake> Mistakes { get; set; }
        public ResultModel()
        {
            Mistakes = new ObservableCollection<Mistake>();
            declaration = DeclChekerViewModel.declaration;
            Ocenka = CheckOc();
            CreateRes();
        }

        public void CreateRes()
        {
            string date = DateTime.Now.ToString().Replace(' ', '_').Replace('.', '_').Replace(':', '_');
            string path = $"{MainViewModel.FIO}_{date}.pdf";

            if (!File.Exists($"{path}"))
            {
                FileStream fs = File.Create($"{path}");
                ResultPath = fs.Name;
                fs.Dispose();
            }

            BaseFont baseFont = BaseFont.CreateFont("c:/windows/fonts/arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font font = new Font(baseFont, 12f);
            Font font__ = new Font(baseFont, 34f);

            Document doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));

            doc.Open();

            try
            {
                if (MainViewModel.FIO != null)
                {
                    Paragraph p1 = new Paragraph(MainViewModel.FIO, font);
                    doc.Add(p1);
                }
                else
                {
                    Paragraph p1 = new Paragraph("ФИО не указано!", font);
                    doc.Add(p1);
                }

                Paragraph p2 = new Paragraph(Ocenka, font__);
                Paragraph p3 = new Paragraph("", font);

                foreach (Mistake mist in Mistakes)
                {
                    p3.Add($"\n{mist.Text}");
                }

                Paragraph p4 = new Paragraph($"\n\n{date}", font);

                doc.Add(p2);
                doc.Add(p3);
                doc.Add(p4);
            }
            catch
            {

            }

            doc.Close();

        }

        public void CloseRes()
        {

            MainViewModel mainView = DeclChekerViewModel.MainVm;
            mainView.ResultOpen.Execute(null);
        }
        private string CheckOc()
        {
            string path = $"{declaration.Placement}\\data";
            double sum = 110;
            double score = 0;

            if (!File.Exists($"{path}\\correct.properties"))
            {
                return "Нет файла ответа!";
            }

                var data_correct = new Dictionary<string, string>();
            foreach (var row in File.ReadAllLines($"{path}\\correct.properties"))
            {
                if(row.Length > 0)
                    if (row[0] != '#')
                    {
                        try
                        {
                            data_correct.Add(row.Split('=')[0], string.Join("=", row.Split('=').Skip(1).ToArray()).ToLower().Trim());
                        }
                        catch
                        {
                            continue;
                        }
                    }
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
                if (data_correct["P_0"] != data_answer["P_0"])
                {
                    if(data_correct["P_0"] == "true")
                    {
                        Mistakes.Add(new Mistake("Декларация должна была быть зарегистрирована!"));
                        score -= 50;
                    }
                    else if(data_correct["P_0"] == "false")
                    {
                        Mistakes.Add(new Mistake("Предполагается отказ в регистрации ДТ!"));
                        score -= 50;
                    }
                }
            }
            catch
            {

            }

            try
            {
                if (data_correct["P_0"] == "true")
                {
                    if (data_answer["FLC"] == "false")
                    {
                        Mistakes.Add(new Mistake("Ошибки ФЛК не закрыты!"));
                        score -= 35;
                    }
                }
            }
            catch
            {

            }

            try
            {
                if (data_correct["IsMesNalich"] == "false")
                {
                    if (data_answer["IsSendMesNalich"] == "false")
                    {
                        if (data_correct["P_5"] == "false" || data_correct["P_0"] == "true")
                        {
                            Mistakes.Add(new Mistake("Не запрошена информация о наличии товаров на ТПФК!"));
                            score -= 30;
                        }
                        else if(data_correct["P_5"] == "true")
                        {
                            Mistakes.Add(new Mistake("Не запрошена информация о наличии товаров на ТПФК!"));
                            score -= 50;
                        }
                    }
                }
            }
            catch
            {

            }

            try
            {
                if (data_correct["P_0"] == data_answer["P_0"])
                {
                    score += 10;
                }

                if (data_correct["P_1"] == data_answer["P_1"])
                {
                    score += 10;
                }
                else
                {
                    Mistakes.Add(new Mistake("Проверьте правомочность таможенного органа!"));
                    score -= 40;
                }

                if (data_correct["P_2_1"] == data_answer["P_2_1"])
                {
                    score += 10;
                }
                else
                {
                    Mistakes.Add(new Mistake("Проверьте полномочия лица!"));
                    score -= 40;
                }

                if (data_correct["P_2_2"] == data_answer["P_2_2"])
                {
                    score += 10;
                }
                else
                {
                    Mistakes.Add(new Mistake("Проверьте электронную подпись ДТ!"));
                    score -= 40;
                }

                if (data_correct["P_3"] == data_answer["P_3"])
                {
                    score += 10;
                }
                else
                {
                    Mistakes.Add(new Mistake("Проверьте соблюдение формы таможенного декларирования!"));
                    score -= 40;
                }

                if (data_correct["P_4_1"] == data_answer["P_4_1"])
                {
                    score += 10;
                }
                else
                {
                    Mistakes.Add(new Mistake("Проверьте наличие необходимых сведений в ДТ!"));
                    score -= 40;
                }

                if (data_correct["P_4_2"] == data_answer["P_4_2"])
                {
                    score += 10;
                }
                else
                {
                    Mistakes.Add(new Mistake("Проверьте заполнение ДТ!"));
                    score -= 40;
                }

                if (data_correct["P_5"] == data_answer["P_5"])
                {
                    score += 10;
                }
                else
                {
                    Mistakes.Add(new Mistake("Проверьте наличие товаров на территории РФ!"));
                    score -= 40;
                }

                if (data_correct["P_6_1"] == data_answer["P_6_1"])
                {
                    score += 10;
                }
                else
                {
                    Mistakes.Add(new Mistake("Проверьте совершенность действий, предшествующих по зак. ЕАЭС подаче ДТ!"));
                    score -= 40;
                }

                if (data_correct["P_6_2"] == data_answer["P_6_2"])
                {
                    score += 10;
                }
                else
                {
                    Mistakes.Add(new Mistake("Проверьте совершенность действий, предшествующих по зак. РФ подаче ДТ!"));
                    score -= 40;
                }

                if (data_correct["P_7"] == data_answer["P_7"])
                {
                    score += 10;
                }
                else
                {
                    Mistakes.Add(new Mistake("Проверьте соблюдение особенностей таможенного декларирования товаров!"));
                    score -= 40;
                }


            }
            catch
            {

            }

            /*foreach(var cor in data_correct)
            {
                foreach(var ans in data_answer)
                {
                    if(cor.Key.ToLower().Trim() == ans.Key.ToLower().Trim())
                    {
                        if(cor.Value.ToLower().Trim() == ans.Value.ToLower().Trim())
                            score += 10;
                    }
                }
            }*/

            double result = score / sum * 100;

                if (result < 60)
                    return "2";
                if (result >= 60 && result < 76)
                    return "3";
                if (result >= 76 && result < 90)
                    return "4";
                if (result >= 90)
                    return "5";
                else
                    return "ошибка";
        }
    }

    public class Mistake
    {
        public string Text { get; set; }
        public Mistake(string text)
        {
            Text = text;
        }
    }
}