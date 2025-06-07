using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using RM_STO_DIPLOM.ViewModels;

namespace RM_STO_DIPLOM;

public partial class ResultView : UserControl
{
    public ResultView()
    {
        InitializeComponent();
        DataContext = new ResultModel();
    }
}