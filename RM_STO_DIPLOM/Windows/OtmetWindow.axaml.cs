using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using RM_STO_DIPLOM.ViewModels;

namespace RM_STO_DIPLOM;

public partial class OtmetWindow : Window
{
    public OtmetWindow()
    {
        InitializeComponent();
        DataContext = new OtmetViewModel();
    }
}