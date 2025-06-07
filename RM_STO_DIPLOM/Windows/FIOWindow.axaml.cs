using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using RM_STO_DIPLOM.ViewModels;

namespace RM_STO_DIPLOM;

public partial class FIOWindow : Window
{
    public FIOWindow()
    {
        InitializeComponent();
        DataContext = new FIOWindowModel(this);
    }
}