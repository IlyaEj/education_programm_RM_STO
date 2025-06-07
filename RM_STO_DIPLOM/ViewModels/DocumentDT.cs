using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RM_STO_DIPLOM.ViewModels
{
    public class DocumentDT
    {
        public string NAIM { get; }
        public string KOD { get; }

        public string PP { get; }
        public string Cod_Vid { get; }
        public string NomDat { get; }
        public string DateOT { get; }
        public string DateDO { get; }
        public string Doc { get; }
        public string Prim { get; }
        public string PredostavDoc { get; }

        public DocumentDT(string naim, string pp)
        {
            NAIM = naim;
            PP = pp;
        }

        public DocumentDT(string naim, string kod, string pp, string cod_vid, string nomdat, string doc, string prim, string dateOT, string dateDO, string getDoc)
        {
            NAIM = naim;
            KOD = kod;

            PP = pp;
            Cod_Vid = cod_vid;
            NomDat = nomdat;
            Doc = doc;
            Prim = prim;
            DateOT = dateOT;
            DateDO = dateDO;

            switch (getDoc)
            {
                case "0":
                    PredostavDoc = "документ не представлен при подаче ДТ";
                    break;
                case "1":
                    PredostavDoc = "документ представлен при подаче ДТ";
                    break;
                case "2":
                    PredostavDoc = "документ не представлен в соответствии с пунктом 10 статьи 109 ТК ЕАЭС";
                    break;
                case "3":
                    PredostavDoc = "документ представлен (будет представлен) после выпуска товаров";
                    break;
                default:
                    PredostavDoc = "документ не представлен при подаче ДТ";
                    break;
            }
        }
    }
}
