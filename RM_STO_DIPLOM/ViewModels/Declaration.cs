using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RM_STO_DIPLOM.ViewModels
{
    public class Declaration
    {
        string Title { get; }
        string Title_Ed { get; }
        string Desc_Ed { get; }
        public bool IsEducation { get; }

        public string Cur { get; }
        public string CurEx { get; }

        public string Stage { get; set; }
        public string Placement { get; }

        public XDocument Decl { get; }

        public List<Commodity> Commodities { get; }
        public List<Commodity_B> Commodity_Bs { get; }

        public Declaration(string title, string placement, 
            XDocument xDocument, List<Commodity> commodities, 
            List<Commodity_B> commodity_Bs, string stage,
            string title_ed, string desc_ed, bool isEducation,
            string cur, string curex)
        {
            Commodities = commodities;
            Title = title;
            Stage = stage;
            Placement = placement;
            Decl = xDocument;
            Commodity_Bs = commodity_Bs;
            Title_Ed = title_ed;
            Desc_Ed = desc_ed;
            IsEducation = isEducation;

            Cur = cur;
            CurEx = curex;
        }
    }
}
