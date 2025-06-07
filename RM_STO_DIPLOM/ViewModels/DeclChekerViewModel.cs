using Avalonia.Controls;
using Avalonia.Interactivity;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using System.Xml.Linq;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Windows.Input;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using System.IO;
using System.Reflection.Metadata;
using Avalonia.Controls.Shapes;
using Avalonia;

namespace RM_STO_DIPLOM.ViewModels;

public class DeclChekerViewModel : ViewModelBase, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    public ICommand OpenDTWindows { get; }
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public static MainViewModel MainVm;
    public ICommand CloseDC { get; }

    public static Declaration declaration;
    public static ObservableCollection<DocumentDT> ImportMes { get; set; }
    public static ObservableCollection<DocumentDT> ExportMes { get; set; }
    public ObservableCollection<Commodity> Commodities { get; set; }
    public ObservableCollection<Commodity_B> Commodities_B { get; }

    public ObservableCollection<DocumentDT> Docs { get; set; }

    public DeclChekerViewModel(Declaration dec, MainViewModel _mainVm)
    {
        Docs = new ObservableCollection<DocumentDT>();
        ImportMes = new ObservableCollection<DocumentDT>();
        ExportMes = new ObservableCollection<DocumentDT>();

        FIO = MainViewModel.FIO;
        MainVm = _mainVm;
        CloseDC = new RelayCommand(() => MainVm.DCClose());

        Commodities = new ObservableCollection<Commodity>();
        Commodities_B = new ObservableCollection<Commodity_B>();

        declaration = dec;
        Commodities.Add(dec.Commodities);
        Commodities_B.Add(dec.Commodity_Bs);
        FLCIsVisible = false;
        MainIsVisible = true;
        FLCText = "ФЛК";
        // Инициализируем начальные значения
        ComIndex = 1;
        ChosenCom = Commodities.FirstOrDefault(); // Устанавливаем первый элемент по умолчанию

        if (File.Exists($"{declaration.Placement}\\data\\answer.properties"))
        {
            File.WriteAllText($"{declaration.Placement}\\data\\answer.properties","");
        }

        PushMesIm("0");
        PushMesEx("0");
        OpenDTWindows = new AsyncRelayCommand<string>(OpenWindowAsync);

        IsCheckedFirst = true;
        IsCheckedSecond = false;
        IsCheckedThird = false;

        if (declaration.IsEducation)
            EducationControl = new EducationView(this);


        DescriptionWindow descriptionWindow = new DescriptionWindow(declaration);
        descriptionWindow.Show();
    }

    private Vector _scrollOfset;
    public Vector ScrollOfset
    {
        get => _scrollOfset;
        set
        {
            if (_scrollOfset != value)
            {
                _scrollOfset = value;
                OnPropertyChanged();
            }
        }
    }

    public string FIO { get; set; }

    private ControlFLC _controlFLC;
    public ControlFLC ControlFLC
    {
        get => _controlFLC;
        set
        {
            if (_controlFLC != value)
            {
                _controlFLC = value;
                OnPropertyChanged();
            }
        }
    }

    private EducationView _educationControl;
    public EducationView EducationControl
    {
        get => _educationControl;
        set
        {
            if (_educationControl != value)
            {
                _educationControl = value;
                OnPropertyChanged();
            }
        }
    }

    private bool _isCheckedFirst;
    public bool IsCheckedFirst
    {
        get => _isCheckedFirst;
        set
        {
            if (_isCheckedFirst != value)
            {
                _isCheckedFirst = value;
                ScrollOfset = new Vector(0, 0);
                OnPropertyChanged();
            }
        }
    }
    private bool _isCheckedSecond;
    public bool IsCheckedSecond
    {
        get => _isCheckedSecond;
        set
        {
            if (_isCheckedSecond != value)
            {
                _isCheckedSecond = value;
                ScrollOfset = new Vector(0, 0);
                OnPropertyChanged();
            }
        }
    }
    private bool _isCheckedThird;
    public bool IsCheckedThird
    {
        get => _isCheckedThird;
        set
        {
            if (_isCheckedThird != value)
            {
                _isCheckedThird = value;
                ScrollOfset = new Vector(0, 0);
                OnPropertyChanged();
            }
        }
    }

    private Commodity _chosenCom;
    public Commodity ChosenCom
    {
        get => _chosenCom;
        set
        {
            if (_chosenCom != value)
            {
                _chosenCom = value;
                Docs.Clear();
                Docs.Add(ChosenCom.DocumentDTs);
                OnPropertyChanged();
            }
        }
    }

    private int _comIndex;
    public int ComIndex
    {
        get => _comIndex;
        set
        {
            if (_comIndex != value)
            {
                _comIndex = value;
                try
                {
                    ChosenCom = Commodities[_comIndex - 1]; // Обновляем выбранный товар
                }
                catch
                {
                    ChosenCom = Commodities[0]; // Обновляем выбранный товар
                }
                OnPropertyChanged();
            }
        }
    }
    private bool _flcIsVisible;
    public bool FLCIsVisible
    {
        get => _flcIsVisible;
        set
        {
            if(_flcIsVisible != value)
            {
                _flcIsVisible = value;
                OnPropertyChanged();
            }
        }
    }
    private bool _mainIsVisible;
    public bool MainIsVisible
    {
        get => _mainIsVisible;
        set
        {
            if (_mainIsVisible != value)
            {
                _mainIsVisible = value;
                OnPropertyChanged();
            }
        }
    }
    private string _flcText;
    public string FLCText
    {
        get => _flcText;
        set
        {
            if (_flcText != value)
            {
                _flcText = value;
                OnPropertyChanged();
            }
        }
    }

    private string G_1_1
    {
        get
        {
            return GetGraf("G_1_1");
        }
    }
    private string G_1_2
    {
        get
        {
            return GetGraf("G_1_2");
        }
    }
    private string G_1_31
    {
        get
        {
            return GetGraf("G_1_31");
        }
    }
    private string G_7_1_NUM
    {
        get
        {
            return GetGraf("G_7_1","NUM");
        }
    }
    private string G_3_1
    {
        get
        {
            return GetGraf("G_3_1");
        }
    }
    private string G_5_1
    {
        get
        {
            return GetGraf("G_5_1");
        }
    }
    private string G_6_1
    {
        get
        {
            return GetGraf("G_6_1");
        }
    }
    private string G_4_1
    {
        get
        {
            return GetGraf("G_4_1");
        }
    }
    private string G_4_2
    {
        get
        {
            return GetGraf("G_4_2");
        }
    }
    private string G_7
    {
        get
        {
            return GetGraf("G_7");
        }
    }
    private string G_12_1
    {
        get
        {
            return GetGraf("G_12_1");
        }
    }
    private string G_12_2
    {
        get
        {
            return GetGraf("G_12_2");
        }
    }
    private string G_16_1
    {
        get
        {
            return GetGraf("G_16_1");
        }
    }
    private string G_2
    {
        get
        {
            return GetGraf("G_2", ["NAME", "ADDRESS"]);
        }
    }
    private string G_8
    {
        get
        {
            return GetGraf("G_8", ["NAME", "ADDRESS"]);
        }
    }
    private string G_9
    {
        get
        {
            return GetGraf("G_9", ["NAME", "ADDRESS"]);
        }
    }
    private string G_14
    {
        get
        {
            return GetDeclar();
        }
    }
    private string G_30
    {
        get
        {
            return "код местонахождения " + GetGraf("G_30_0") + $"; " +
                "код ТО " + GetGraf("G_30_12") + $"; " +
                GetGraf("G_30_CC") + $";\n" + GetGraf("G_30_CIT") + ";";
        }
    }
    private string G_54
    {
        get
        {
            return "Лицо, заполнившее документ\n" + GetGraf("G_54_3") + " " + GetGraf("G_54_3NM") + " " + GetGraf("G_54_3MD") + "\n" +
                GetGraf("G_54_4") + " от " + GeTDate("G_54_60") + " № " + GetGraf("G_54_5");
        }
    }
    private string G_USL
    {
        get
        {
            return GetGraf("G_20_20") + " " + GetGraf("G_20_21") + ";\n" +
                "курс валюты " + GetGraf("G_23_1") + "; " +
                GetGraf("G_22_3") + "; " + "стоимость по счету " +
                GetGraf("G_22_2") + "; " + "код особенности сделка " +
                GetGraf("G_28_12") + "; " + "код характера сделка " +
                GetGraf("G_28_21") + "; " + GetGraf("G_11_1") + ";";
        }
    }
    private string G_15_1
    {
        get
        {
            return GetGraf("G_15_1");
        }
    }
    private string G_17_1
    {
        get
        {
            return GetGraf("G_17_1");
        }
    }
    private bool G_19_1
    {
        get
        {
            return GetCheck("G_19_1");
        }
    }
    private string G_29_1
    {
        get
        {
            return GetGraf("G_29_1") + " " + GetGraf("G_29_2");
        }
    }
    private string G_PRIB
    {
        get
        {
            return "код ТС " + GetGraf("G_26_1") + "; " + "количество ТС " + GetGraf("G_18_0") + ";\n" +
                GetGraf("G_18_2") + ";\n" +
                "Номер(а) ТС " + GetGraf("G_18");
        }
    }
    private string G_GRAN
    {
        get
        {
            return "код ТС " + GetGraf("G_25_1") + "; " + "количество ТС " + GetGraf("G_21_0") + ";\n" +
                GetGraf("G_21_2") + ";\n" +
                "Номер(а) ТС " + GetGraf("G_21");
        }
    }

    /*public Declaration GetDec(int index)
    {
        return CustomsDeclLoader.DTs[index];
    }

    public static ObservableCollection<Commodity> GetCom(int index)
    {
        return new ObservableCollection<Commodity>(CustomsDeclLoader.DTs[index].Commodities);
    }

    public ObservableCollection<Commodity_B> GetB(int index)
    {
        return new ObservableCollection<Commodity_B>(CustomsDeclLoader.DTs[index].Commodity_Bs);
    }*/

    public string GetGraf(string key)
    {
        if(declaration != null)
        {
            string grafTitle = "";
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

    public string GetGraf(string key,string subkey)
    {
        if(declaration != null)
        {
            string grafTitle = "";
            var elements = declaration.Decl.Descendants();

            foreach (var element in elements)
            {
                if (element.Name == key)
                {
                    var nodes = element.Elements();
                    foreach (var node in nodes)
                        if(node.Name == subkey)
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

    public string GetGraf(string key, string[] subkeys)
    {
        if (declaration != null)
        {
            string grafTitle = "";
            var elements = declaration.Decl.Descendants();

            foreach (var element in elements)
            {
                if (element.Name == key)
                {
                    var nodes = element.Elements();
                    foreach (var node in nodes)
                    {
                        for (int i = 0; i < subkeys.Length; i++)
                        {
                            if (node.Name == subkeys[i])
                            {
                                grafTitle += node.Value + $"\n";
                            }
                        }
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

    public string GetDeclar()
    {
        string grafName = "";
        string grafINNKPP = "";
        string grafPOST = "";
        string grafREG = "";
        string grafCIT = "";
        string grafADRESS = "";

        var elements = declaration.Decl.Descendants();

        foreach (var element in elements)
        {
            if (element.Name == "G_14_NAM")
            {
                grafName = element.Value + "; ";

            }
            if (element.Name == "G_14_POS")
            {
                grafPOST = element.Value + ", ";

            }
            if (element.Name == "G_14_SUB")
            {
                grafREG = element.Value + ", ";

            }
            if (element.Name == "G_14_CIT")
            {
                grafCIT = element.Value + ", ";

            }
            if (element.Name == "G_14_STR")
            {
                grafADRESS = element.Value;

            }
        }
        grafINNKPP = GetINNKPP();

        return grafName + grafINNKPP + grafPOST + grafREG + grafCIT + grafADRESS;
    }

    public string GetINNKPP()
    {
        var elements = declaration.Decl.Descendants();
        string[] innkpp = new string[2];
        foreach (var element in elements)
        {
            if (element.Name == "G_14_4")
            {
                innkpp = element.Value.Split('/');
                break;
            }
        }
        return "ИНН " + innkpp[0] + "; " + "КПП " + innkpp[1] + $";\n";
    }

    public string GeTDate(string key)
    {
        string date = GetGraf(key);
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

    public bool GetCheck(string key)
    {
        string check = GetGraf(key);
        if(check == "0")
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void OpenOtkaz()
    {
        OpenWindowAsync("0");
    }
    public void OpenNalich()
    {
        OpenWindowAsync("1");
    }
    public void OpenPribit()
    {
        OpenWindowAsync("2");
    }
    public void OpenFLC()
    {
        if (!FLCIsVisible)
        {
            if(ControlFLC == null)
                ControlFLC = new ControlFLC();

            FLCIsVisible = true;
            MainIsVisible = false;
            FLCText = "Завершить ФЛК";
        }
        else
        {
            FLCIsVisible = false;
            MainIsVisible = true;
            FLCText = "ФЛК";
            CloseFLC();
        }
    }

    public void OpenFLC(bool open) //Для режима обучения
    {
        if (open)
        {
            if (ControlFLC == null)
                ControlFLC = new ControlFLC();

            FLCIsVisible = true;
            MainIsVisible = false;
            FLCText = "Завершить ФЛК";
        }
        else
        {
            FLCIsVisible = false;
            MainIsVisible = true;
            FLCText = "ФЛК";
        }
    }

    public void OpenOtmet()
    {
        OtmetWindow ow = new OtmetWindow();
        ow.Show();
    }

    public void CloseFLC()
    {
        try
        {
            bool FLCIsClosed = true;

            foreach (var err in ControlFLCModel.DTErrs)
            {
                if (!err.IsClosed)
                {
                    FLCIsClosed = false;
                }
            }

            if (FLCIsClosed)
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
                        File.AppendAllText($@"{dataFolder}\\answer.properties", "FLC=True\n");
                    }
                    else
                    {
                        File.AppendAllText($@"{dataFolder}\\answer.properties", "FLC=True\n");
                    }
                }
                catch
                {

                }
            }
        }
        catch
        {
            return;
        }
    }

    public void OpenPodtverjd()
    {
        var windPodv = new PodtverjdWindow(DeclChekerViewModel.MainVm, true, false, false, false, false, false, false, false, false, false, false);
        windPodv.Show();
    }

    private async Task OpenWindowAsync(string index)
    {
        switch (index)
        {
            case "0":
                var windowOtkaz = new WindowOtkaz();
                windowOtkaz.Show();
                break;
            case "1":
                var windowNalich = new NalichWindow();
                windowNalich.Show();
                break;

            case "2":
                var windowPribit = new PribitWindow();
                windowPribit.Show();
                break;

            default:
                var windowOtkaz_ = new WindowOtkaz();
                windowOtkaz_.Show();
                break;
        }
    }

    public void PushMesIm(string i)
    {
        string path = $"{declaration.Placement}\\data";

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

            foreach (var row in data)
            {
                if (row.Key == "IsMesPribit")
                {
                    if (data["IsMesPribit"].ToLower().Trim() == "true")
                    {
                        if (i == "0")
                        {
                            if (data["IsPribit"].Trim() == "True" || data["IsPribit"].Trim() == "true")
                            {
                                ImportMes.Add(new DocumentDT("Информация о прибытии товара", "Товар прибыл"));
                            }
                            else
                            {
                                ImportMes.Add(new DocumentDT("Информация о прибытии товара", "Товар не прибыл"));
                            }
                        }
                    }
                }

                if (row.Key == "IsMesNalich")
                {
                    if (data["IsMesNalich"].ToLower().Trim() == "true")
                    {
                        if (i == "1")
                        {
                            if (data["IsNalich"].Trim() == "True" || data["IsNalich"].Trim() == "true")
                            {
                                ImportMes.Add(new DocumentDT("Информация о наличии товара", "Товар в наличии"));
                            }
                            else
                            {
                                ImportMes.Add(new DocumentDT("Информация о наличии товара", "Товара нет в наличии"));
                            }

                        }
                    }
                }
            }
        }
        else
        {
            ImportMes.Add(new DocumentDT("ОТСУТСТВУЕТ ФАЙЛ С ОТВЕТОМ", "ДОБАВЬТЕ ФАЙЛ"));
        }
    }
    public void PushMesEx(string i)
    {
        if (i == "0")
        {
            ExportMes.Add(new DocumentDT("Запрос о прибытии", "Направлен запрос о прибытии"));
        }
        if (i == "1")
        {
            ExportMes.Add(new DocumentDT("Запрос о наличии", "Направлен запрос о наличии товаров"));
        }
    }

    public void OpenDoc()
    {
        var windDoc = new DocumentsWindow(this);
        windDoc.Show();
    }

    public void ScrollToEnd()
    {
        ScrollOfset = new Vector(0, 9999);
    }
}
