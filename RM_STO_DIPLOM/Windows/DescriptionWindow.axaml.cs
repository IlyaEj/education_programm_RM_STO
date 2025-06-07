using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using RM_STO_DIPLOM.ViewModels;

namespace RM_STO_DIPLOM;

public partial class DescriptionWindow : Window
{
    public DescriptionWindow(Declaration declaration)
    {
        InitializeComponent();
        DataContext = new DescriptionWindowModel(declaration, this);
    }
}