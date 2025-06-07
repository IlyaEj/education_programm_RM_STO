using Avalonia.Controls;
using Avalonia.Interactivity;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using RM_STO_DIPLOM.Views;
using Avalonia.Input;
using System.Linq;
using System.Reactive.Linq;
using System.Net;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Reflection;

namespace RM_STO_DIPLOM.ViewModels;

public class MainViewModel : ViewModelBase, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
    public ICommand DeclCheck { get; }
    public ICommand ResultOpen { get; set; }

    CustomsDeclLoader customsDeclLoader;

    string path = $@".\tasks\practice";
    string path_2 = $@".\tasks\education";

    public static string FIO { get; set; }

    public bool _declChekerIsVisible;
    private bool DeclChekerIsVisible
    {
        get => _declChekerIsVisible;
        set
        {
            if (_declChekerIsVisible != value)
            {
                _declChekerIsVisible = value;
                OnPropertyChanged();
            }
        }
    }
    public bool _mainWinIsVisible;
    private bool MainWinIsVisible
    {
        get => _mainWinIsVisible;
        set
        {
            if (_mainWinIsVisible != value)
            {
                _mainWinIsVisible = value;
                OnPropertyChanged();
            }
        }
    }

    public bool _isPractice;
    private bool IsPractice
    {
        get => _isPractice;
        set
        {
            if (_isPractice != value)
            {
                _isPractice = value;
                OnPropertyChanged();
            }
        }
    }

    public bool _isEducation;
    private bool IsEducation
    {
        get => _isEducation;
        set
        {
            if (_isEducation != value)
            {
                _isEducation = value;
                OnPropertyChanged();
            }
        }
    }

    public bool _practiceRegime;
    private bool PracticeRegime
    {
        get => _practiceRegime;
        set
        {
            if (_practiceRegime != value)
            {
                _practiceRegime = value;
                ChangeRegime();
                OnPropertyChanged();
            }
        }
    }

    public bool _educationRegime;
    private bool EducationRegime
    {
        get => _educationRegime;
        set
        {
            if (_educationRegime != value)
            {
                _educationRegime = value;
                ChangeRegime();
                OnPropertyChanged();
            }
        }
    }

    private DeclChekerView _declCheker;
    public DeclChekerView DeclCheker
    {
        get => _declCheker;
        set
        {
            if (_declCheker != value)
            {
                _declCheker = value;
                OnPropertyChanged();
            }
        }
    }

    private ResultView _resultView;
    public ResultView ResultViewer
    {
        get => _resultView;
        set
        {
            if (_resultView != value)
            {
                _resultView = value;
                OnPropertyChanged();
            }
        }
    }

    public bool _resultIsVisible;
    private bool ResultIsVisible
    {
        get => _resultIsVisible;
        set
        {
            if (_resultIsVisible != value)
            {
                _resultIsVisible = value;
                OnPropertyChanged();
            }
        }
    }

    public ObservableCollection<Declaration> DTFolders_Registration { get; set; }
    public ObservableCollection<Declaration> DTFolders_Otkaz_v_Reg { get; set; }
    public ObservableCollection<Declaration> DTFolders_v_Oform { get; set; }
    public ObservableCollection<Declaration> EducationDTs { get; set; }

    public MainViewModel()
    {
        MainWinIsVisible = true;
        DeclChekerIsVisible = false;
        ResultIsVisible = false;

        PracticeRegime = true;
        EducationRegime = false;
        IsPractice = true;
        IsEducation = false;

        customsDeclLoader = new CustomsDeclLoader();

        FIOWindow fIOWindow = new FIOWindow();
        fIOWindow.Show();

        DeclCheck = ReactiveCommand.Create((Declaration dec) =>
        {
            if (!DeclChekerIsVisible)
            {
                DeclCheker = new DeclChekerView(dec, this);

                DeclChekerIsVisible = true;
                MainWinIsVisible = false;
            }
            else
            {
                DeclChekerIsVisible = false;
                MainWinIsVisible = true;
                customsDeclLoader = new CustomsDeclLoader();
                ClearCollections();
                LoadStages();
            }
        });

        ResultOpen = ReactiveCommand.Create((int index) =>
        {
            if (!ResultIsVisible)
            {
                ResultViewer = new ResultView();

                ResultIsVisible = true; 
                DeclChekerIsVisible = false;
                MainWinIsVisible = false;
            }
            else
            {
                ResultIsVisible = false;
                MainWinIsVisible = true;
                customsDeclLoader = new CustomsDeclLoader();
                ClearCollections();
                LoadStages();
            }
        });

        DTFolders_Registration = new ObservableCollection<Declaration>();
        DTFolders_Otkaz_v_Reg = new ObservableCollection<Declaration>();
        DTFolders_v_Oform = new ObservableCollection<Declaration>();
        EducationDTs = new ObservableCollection<Declaration>();

        EducationDTs = customsDeclLoader.GetDeclarations(path_2);

        LoadStages();
    }

    public void LoadStages()
    {
        foreach (Declaration dec in customsDeclLoader.GetDeclarations(path))
        {
            if (dec.Stage.ToLower() == "Registration".ToLower())
                DTFolders_Registration.Add(dec);
            if (dec.Stage.ToLower() == "Otkaz_Reg".ToLower())
                DTFolders_Otkaz_v_Reg.Add(dec);
            if (dec.Stage.ToLower() == "V_Oform".ToLower())
                DTFolders_v_Oform.Add(dec);
        }
    }

    private void ClearCollections()
    {
        DTFolders_Registration.Clear();
        DTFolders_Otkaz_v_Reg.Clear();
        DTFolders_v_Oform.Clear();
    }

    public void DCClose()
    {
        DeclChekerIsVisible = false;
        MainWinIsVisible = true;
    }

    public void ChangeRegime()
    {
        if (PracticeRegime)
        {
            IsPractice = true;
            IsEducation = false;
        }
        if (EducationRegime)
        {
            IsPractice = false;
            IsEducation = true;
        }
    }
}
