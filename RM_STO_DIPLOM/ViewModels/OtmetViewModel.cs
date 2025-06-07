using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RM_STO_DIPLOM.ViewModels
{
    public class OtmetViewModel
    {
        public ObservableCollection<Otmet> Otmets { get; set; }
        public OtmetViewModel()
        {
            Otmets = new ObservableCollection<Otmet>();
            Otmets.Add(new Otmet("E2", "1.00", "", DateTime.Now.ToString(), "АВТОМАТ АВТОМАТ (ЛНП 000)","ПОДАНА В Т/О","A"));
            Otmets.Add(new Otmet("E2", "1.01", "", DateTime.Now.ToString(), "АВТОМАТ АВТОМАТ (ЛНП 000)", "Получение ДТ в месте фактического оформления", "A"));
            Otmets.Add(new Otmet("E2", "1.98", "", DateTime.Now.ToString(), "АВТОМАТ АВТОМАТ (ЛНП 000)", "НЕ СОБЛЮДЕН КРИТЕРИЙ АВТОРЕГИСТРАЦИИ № 100 020", "A"));
            Otmets.Add(new Otmet("E2", "5.02", "История обработки процедуры", DateTime.Now.ToString(), MainViewModel.FIO, "ДТ на регистрации", "A"));
            Otmets.Add(new Otmet("E2", "5.06", "", DateTime.Now.ToString(), "АВТОМАТ АВТОМАТ (ЛНП 000)", "Уровень сложности ДТ при регистрации", "A"));
        }
    }
    public class Otmet
    {
        public string Graf { get; set; }
        public string Num { get; set; }
        public string Naim { get; set; }
        public string Date { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
        public string Prizn { get; set; }

        public Otmet(string graf, string num, string naim, string date,
            string author, string text, string prizn)
        {
            Graf = graf;
            Num = num;
            Naim = naim;
            Date = date;
            Author = author;
            Text = text;
            Prizn = prizn;
        }
    }
}
