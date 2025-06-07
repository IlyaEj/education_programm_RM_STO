using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using RM_STO_DIPLOM.ViewModels;

namespace RM_STO_DIPLOM;

public partial class MesSendedWindow : Window
{
    public MesSendedWindow(string i)
    {
        InitializeComponent();
        DataContext = new MesWinModel(i,this);
    }
}