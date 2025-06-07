using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using RM_STO_DIPLOM.ViewModels;

namespace RM_STO_DIPLOM;

public partial class WindowOtkaz : Window
{
    public WindowOtkaz()
    {
        InitializeComponent();
        DataContext = new OtkazModel(this);
    }
}