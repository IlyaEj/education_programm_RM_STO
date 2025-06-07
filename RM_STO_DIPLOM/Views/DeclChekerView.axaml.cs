using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using RM_STO_DIPLOM.ViewModels;
using System;
using System.Reflection;

namespace RM_STO_DIPLOM.Views;

public partial class DeclChekerView : UserControl
{
    public DeclChekerView()
    {
        InitializeComponent();
    }
    public DeclChekerView(Declaration dec, MainViewModel mainViewModel)
    {
        InitializeComponent();
        DataContext = new DeclChekerViewModel(dec, mainViewModel);
    }
}