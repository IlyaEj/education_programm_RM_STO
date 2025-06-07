using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using RM_STO_DIPLOM.ViewModels;

namespace RM_STO_DIPLOM;

public partial class EducationView : UserControl
{
    public EducationView()
    {
        InitializeComponent();
    }

    public EducationView(DeclChekerViewModel dcvm)
    {
        InitializeComponent();
        DataContext = new EducationViewModel(dcvm);
    }
}