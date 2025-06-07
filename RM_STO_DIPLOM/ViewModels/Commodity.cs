using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.Sqlite;
using SQLitePCL;

namespace RM_STO_DIPLOM.ViewModels
{
    public class Commodity
    {
        public string ComDocs { get; }
        public string ComNum { get; }
        public string ComDesc { get; }
        public string ComCode { get; }
        public string ComWeight { get; }
        public string ComCen { get; }
        public string ComCO { get; }
        public string ComITS { get; }
        public string ComDescFull { get; }
        public string ComKoll { get; }
        public string ComEI { get; }
        public string ComTM { get; }
        public string ComArtic { get; }
        public string G_32_2 { get; }
        public string G_33_3 { get; }
        public string G_S { get; }
        public string G_I { get; }
        public string G_O { get; }
        public string G_M { get; }
        public string G_P { get; }
        public string G_Kod_Osob { get; }
        string G_37 { get; }
        public string G_37_1 { get; }
        public string G_37_2 { get; }
        public string G_37_3 { get; }
        public string G_35 { get; }
        public string G_38 { get; }
        public string G_31_1_1 { get; }
        public string G_31_1_2 { get; }
        public string G_31_1_3 { get; }
        public string G_31_1_4 { get; }
        public string G_43 { get; }
        public string G_45 { get; }
        public string G_46 { get; }
        public string G_31_2 { get; }
        public string G_40_1 { get; }
        public string G_31_3 { get; }

        public string G_36 { get; }
        public string G_36_1 { get; }
        public string G_36_2 { get; }
        public string G_36_3 { get; }
        public string G_36_4 { get; }

        public string InEd_1 { get; }
        public string InEd_2 { get; }
        public string EdIs { get; }

        public List<DocumentDT> DocumentDTs { get; }

        public List<Commodity_47> Commodity_47s { get; }
        public Commodity(string desc)
        {
            ComDesc = desc;
        }
        public Commodity(string comNum, string comDesc, string comCode, string comWeight,
            string comCen, string comCO, string comITS, string comDescFull,
            string comKoll, string comEI, string comTM, string comArtic, List<Commodity_47> commodity_47s,
            string g_32_2, string g_33_3, string g_s, string g_i, string g_o, string g_m, string g_p, string g_35, string g_38, string g_37,
            string g_31_1_1, string g_31_1_2, string g_31_1_3, string g_31_1_4,
            string g_43, string g_45, string g_46, string g_31_2,
            string g_40_1, string g_31_3, List<DocumentDT> documentDTs, string g_Kod_Osob,
            string g_36)
        {
            ComNum = comNum;
            ComDesc = comDesc;
            ComCode = comCode;
            ComWeight = comWeight;
            ComCen = comCen;
            ComCO = comCO;
            ComITS = comITS;
            ComDescFull = comDescFull;
            ComKoll = comKoll;
            ComEI = comEI;
            ComTM = comTM;
            ComArtic = comArtic;
            Commodity_47s = commodity_47s;
            G_32_2 = g_32_2;
            G_33_3 = g_33_3;
            G_S = g_s;
            G_I = g_i;
            G_O = g_o;
            G_M = g_m;
            G_P = g_p;
            G_35 = g_35;
            G_38 = g_38;
            G_37 = g_37;

            try
            {
                G_37_1 = GetPart(G_37, [0, 1]);
                G_37_2 = GetPart(G_37, [2, 3]);
                G_37_3 = GetPart(G_37, [4, 5, 6]);
            }
            catch
            {
                G_37_1 = "--";
                G_37_2 = "--";
                G_37_3 = "---";
            }

            G_31_1_1 = g_31_1_1;
            G_31_1_2 = g_31_1_2;
            G_31_1_3 = g_31_1_3;
            G_31_1_4 = g_31_1_4;
            G_43 = g_43;
            G_45 = g_45;
            G_46 = g_46;
            G_31_2 = g_31_2;
            G_40_1 = g_40_1;
            G_31_3 = g_31_3;
            DocumentDTs = documentDTs;

            G_36 = g_36;

            try
            {
                G_36_1 = GetPart(G_36, [0, 1]);
                G_36_2 = GetPart(G_36, [2, 3]);
                G_36_3 = GetPart(G_36, [4]);
                G_36_4 = GetPart(G_36, [5, 6]);
            }
            catch
            {
                G_36_1 = "--";
                G_36_2 = "--";
                G_36_3 = "-";
                G_36_4 = "--";
            }

            try
            {
                if (G_31_1_2 != null)
                    InEd_1 = GetDopEd(G_31_1_2.Replace(" ", "").Trim(), "DopEd");

                if (G_31_1_4 != null)
                    InEd_2 = GetDopEd(G_31_1_4.Replace(" ", "").Trim(), "DopEd");
                if(ComEI != null)
                    EdIs = GetDopEd(ComEI.Replace(" ", "").Trim(), "DopEd");
            }
            catch
            {

            }

            G_Kod_Osob = g_Kod_Osob;

            ComDocs = null;

            foreach (var doc in documentDTs)
            {
                char[] doc_chares = doc.Cod_Vid.Trim().ToCharArray();

                try
                {
                    string doc_kode = doc_chares[0].ToString() + doc_chares[1].ToString() + doc_chares[2].ToString();

                    if (doc_kode == "020")
                        ComDocs += doc.NAIM + ";\n";
                }
                catch
                {
                    continue;
                }
            }
        }
        public string GetPart(string Graf, int[] positions)
        {
            if (Graf == null)
            {
                return null;
            }

            string part = null;
            foreach (int i in positions)
            {
                try
                {
                    part += Graf[i];
                }
                catch
                {
                    continue;
                }
            }
            return part;
        }

        public string GetDopEd(string _key,string _classificator) //SQLite
        {
            object dopEd;
            Batteries.Init();

            try
            {
                //Варинт с использованием .properties

                /*if (!File.Exists($".\\tasks\\classificators\\{_classificator}.properties"))
                {
                    return null;
                }

                Dictionary<string, string> _DopEd = new Dictionary<string, string>();

                foreach (var line in File.ReadAllLines($".\\tasks\\classificators\\{_classificator}.properties"))
                {
                    if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                        continue;

                    var parts = line.Split(new[] { '=' }, 2);

                    if (parts.Length == 2)
                    {
                        var key = parts[0].Trim();
                        var value = parts[1].Trim();
                        _DopEd[key] = value;
                    }
                }*/

                if (!File.Exists($".\\tasks\\classificators\\{_classificator}.db"))
                {
                    return null;
                }

                string fullPath = Path.GetFullPath($".\\tasks\\classificators\\{_classificator}.db");

                string connectionString = $"Data Source={fullPath};";
                string sql = $"SELECT KOD FROM {_classificator} WHERE NAME = @key";

                using (SqliteConnection con = new SqliteConnection(connectionString))
                {
                    con.Open();
                    SqliteCommand cmd = new SqliteCommand(sql, con);
                    SqliteParameter key = new SqliteParameter("@key", _key); //параметр столбца NAME
                    cmd.Parameters.Add(key);
                    dopEd = cmd.ExecuteScalar();
                    con.Close();
                }

                return dopEd?.ToString(); //если null, то и вернет null

            }
            catch (Exception ex)
            {
                string a = ex.ToString();
                return null;
            }
        }
    }
}
