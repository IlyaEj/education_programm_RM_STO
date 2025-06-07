using System.Collections.Generic;
using System.Xml.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;

namespace RM_STO_DIPLOM.ViewModels
{
    public class CustomsDeclLoader
    {
        string searchKey = "___NUM";
        public ObservableCollection<Declaration> DTs;

        public ObservableCollection<Declaration> GetDeclarations(string path)
        {
            DTs = new ObservableCollection<Declaration> { };

            foreach (string folder in Directory.GetDirectories(path))
            {
                string decltTitle = "";
                string cur = null;
                string curex = null;

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                if (!File.Exists(folder + "\\DT.xml"))
                {
                    continue;
                }

                XDocument doc = XDocument.Load(folder + "\\DT.xml");

                var elements = doc.Descendants();

                foreach (var element in elements)
                {
                    if (element.Name == searchKey)
                    {
                        decltTitle = element.Value;
                        break;
                    }
                }

                foreach (var element in elements)
                {
                    if (element.Name == "G_22_3")
                    {
                        cur = element.Value;
                    }
                    if (element.Name == "G_23_1")
                    {
                        curex = element.Value;
                    }

                    if(cur != null && curex != null)
                    {
                        break;
                    }
                }

                if (!File.Exists(folder + "\\stage.txt"))
                {
                    FileStream fs = File.Create(folder + "\\stage.txt");
                    fs.Dispose();
                    File.WriteAllText(folder + "\\stage.txt", "Registration");
                }

                string stage = File.ReadAllText(folder + "\\stage.txt");

                if(stage == null || stage.Trim() == "")
                {
                    File.WriteAllText(folder + "\\stage.txt", "Registration");
                    stage = File.ReadAllText(folder + "\\stage.txt");
                }

                DTs.Add(new Declaration(decltTitle, folder,
                    doc, GetCommodities(doc), GetGraf(doc, "B_"), stage.Trim(),
                    GetEd(folder, "Title_Ed"), GetEd(folder, "Desc_Ed"), GetEd(folder),
                    cur, curex));
            }

            return DTs;
        }

        private bool GetEd(string path)
        {
            if (!File.Exists(path + "\\Education.properties"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private string GetEd(string path, string keyEd)
        {
            string _ed;

            if (!File.Exists(path + "\\Education.properties"))
            {
                return null;
            }

            Dictionary<string, string> _EDproperties = new Dictionary<string, string>();

            foreach (var line in File.ReadAllLines(path + "\\Education.properties"))
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                    continue;

                var parts = line.Split(new[] { '=' }, 2);

                if (parts.Length == 2)
                {
                    var key = parts[0].Trim();
                    var value = parts[1].Trim();
                    _EDproperties[key] = value;
                }
            }

            try
            {
                _ed = _EDproperties[keyEd];
            }
            catch
            {
                _ed = null;
            }

            return _ed;
        }

        public List<Commodity> GetCommodities(XDocument doc)
        {
            List<Commodity> commodities = new List<Commodity>();

            var elements = doc.Descendants();

            foreach (var element in elements)
            {
                if (element.Name == "BLOCK")
                {
                    commodities.Add(new Commodity(GetGraf(element, "G_32_1"),
                        GetGraf(element, "G_31", "NAME"), GetGraf(element, "G_33_1"),
                        GetGraf(element, "G_35_1") + "\n" + GetGraf(element, "G_38_1"),
                        GetGraf(element, "G_42_1"), GetGraf(element, "G_34_1"),
                        GetGraf(element, "G_45_0"), GetGraf(element, "TOVG", "G31_1"),
                        GetGraf(element, "G_31_8"), GetGraf(element, "G_41_2"),
                        GetGraf(element, "TOVG", "G31_12"), GetGraf(element, "TOVG", "G31_A"), GetGraf_47(element,"G_47"),
                        GetGraf(element, "G_32_2"), GetGraf(element, "G_33_3"), GetGraf(element, "G_33_4"), GetGraf(element, "G_33_5"), GetGraf(element, "G_33_3"),
                        GetGraf(element, "G_33_6"), GetGraf(element, "G_33_6"), GetGraf(element, "G_35_1"), GetGraf(element, "G_38_1"),
                        GetGraf(element, "G_37_1"), GetGraf(element, "G_31", "VSEGO"), GetGraf(element, "G_31", "VSEGO2"), GetGraf(element, "G_31", "POWER"), GetGraf(element, "G_31", "POWER2"),
                        GetGraf(element, "G_43_1"), GetGraf(element, "G_45_1"), GetGraf(element, "G_46_1"),
                        $"количество грузовых мест {GetGraf(element, "G_31", "PLACE")}; {GetGraf(element, "G_31", "PLACE2")}",
                        GetGraf(element, "G_40_1"), GetGraf(element, "G_31", "KONT2"), GetDocuments(element, "G44"), GetGraf(element, "G_33_2"),
                        GetGraf(element, "G_36_2")));
                }
            }
            return commodities;
        }

        public string GetGraf(XContainer elem, string key)
        {
            string grafTitle = null;
            var nodes = elem.Elements();
            foreach (var node in nodes)
                if (node.Name == key)
                {
                    grafTitle = node.Value;
                    break;
                }
            return grafTitle;
        }
        public string GetGraf(XContainer elem, string key,string subkey)
        {
            string grafTitle = null;
            var nodes = elem.Elements();
            foreach (var node in nodes)
                if (node.Name == key)
                {
                    foreach (var subnode in node.Elements())
                        if (subnode.Name == subkey)
                        {
                            grafTitle = subnode.Value;
                            break;
                        }
                }
            return grafTitle;
        }

        public List<Commodity_47> GetGraf_47(XContainer elem, string key)
        {
            List<Commodity_47> commodity_47s = new List<Commodity_47>();

            string com_VP = null;
            string com_Osn = null;
            string com_Stav = null;
            string com_Summ = null;
            string com_SP = null;

            string[] grafs = new string[] {com_VP, com_Osn, com_Stav, com_Summ, com_SP };

            int i = 1;
            int j = 1;
            int n = 0;
            var nodes = elem.Elements();

            foreach (var node in nodes)
            {
                if(node.Name == $"{key}_{i}_{j}")
                {
                    grafs[n] = node.Value;
                    j++;
                    n++;

                    if(j > 5)
                    {
                        try
                        {
                            commodity_47s.Add(new Commodity_47(grafs[0], grafs[1], grafs[2], grafs[3], grafs[4]));
                        }
                        catch
                        {
                            continue;
                        }

                        i++;
                        j = 1;
                        n = 0;
                    }
                }
                else
                {
                    continue;
                }
            }
            return commodity_47s;
        }

        public List<DocumentDT> GetDocuments(XContainer elem, string key)
        {
            List<DocumentDT> documentDTs = new List<DocumentDT>();

            string NAIM = null;
            string KOD = null;

            string cod_Vid = null;
            string nomDat = null;
            string doc = null;
            string dateOT = null;
            string dateDO = null;
            string getDoc = null;

            var nodes = elem.Elements();
            int i = 1;
            foreach (var node in nodes)
            {
                if (node.Name == $"{key}")
                {
                    foreach(var node_ in node.Elements())
                    {
                        if(node_.Name == "G441")
                        {
                            cod_Vid = node_.Value;
                        }
                        if (node_.Name == "G442")
                        {
                            KOD = node_.Value;
                            nomDat = node_.Value;
                            doc = node_.Value;
                        }
                        if (node_.Name == "G443")
                        {
                            nomDat += $" от {node_.Value}\n";
                            doc += $" от {GeTDate(node_.Value)}";
                        }
                        if (node_.Name == "G444")
                        {
                            NAIM = node_.Value;
                            nomDat += node_.Value.ToLower();
                        }
                        if (node_.Name == "BACK")
                        {
                            getDoc = GeTDate(node_.Value);
                        }
                        if (node_.Name == "G446")
                        {
                            dateOT = GeTDate(node_.Value);
                        }
                        if (node_.Name == "G447")
                        {
                            dateDO = GeTDate(node_.Value);
                        }
                        if (node_.Name == "FACE" && node_.Value == "true")
                        {
                            documentDTs.Add(new DocumentDT(NAIM, KOD, i.ToString(), cod_Vid, nomDat, doc, "", dateOT, dateDO, getDoc));
                            cod_Vid = "";
                            nomDat = "";
                            doc = "";
                            dateOT = "";
                            dateDO = "";
                            getDoc = "";
                            i++;
                        }
                    }
                }
                else
                {
                    continue;
                }
            }


            return documentDTs;
        }

        public string GeTDate(string date)
        {
            try
            {
                string[] dates = new string[3];
                dates = date.Split('-');
                return dates[2] + "." + dates[1] + "." + dates[0];
            }
            catch
            {
                return date;
            }
        }

        public List<Commodity_B> GetGraf(XDocument doc, string key)
        {
            List<Commodity_B> commodity_Bs = new List<Commodity_B>();
            List<string> commodity_Bs_string = new List<string>();

            var elements = doc.Descendants();

            int i = 1;

            foreach (var element in elements)
            {

                if (element.Name == $"{key}{i}")
                {
                    commodity_Bs_string.Add(element.Value);
                    i++;
                }
            }

            foreach(string element in commodity_Bs_string)
            {
                string[] elems = new string[4];
                elems = element.Split('-');

                try
                {
                    commodity_Bs.Add(new Commodity_B(elems[0], elems[1], elems[3]));
                }
                catch
                {
                    continue;
                }
                
            }

            return commodity_Bs;
        }
    }
}
