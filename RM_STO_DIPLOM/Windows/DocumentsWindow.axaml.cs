using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using RM_STO_DIPLOM.ViewModels;

namespace RM_STO_DIPLOM;

public partial class DocumentsWindow : Window
{
    public DocumentsWindow()
    {
        InitializeComponent();
    }

    public DocumentsWindow(DeclChekerViewModel declChekerViewModel)
    {
        InitializeComponent();
        DataContext = declChekerViewModel;
    }
}