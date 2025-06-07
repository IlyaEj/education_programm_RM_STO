using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using RM_STO_DIPLOM.ViewModels;

namespace RM_STO_DIPLOM;

public partial class PodtverjdWindow : Window
{
    public PodtverjdWindow(MainViewModel mainView, bool _p_0, bool _p_1, bool _p_2_1, bool _p_2_2, bool _p_3, bool _p_4_1, bool _p_4_2, bool _p_5, bool _p_6_1, bool _p_6_2, bool _p_7)
    {
        InitializeComponent();
        DataContext = new PodtverjdModel(mainView, this, _p_0, _p_1, _p_2_1, _p_2_2, _p_3, _p_4_1, _p_4_2, _p_5, _p_6_1, _p_6_2, _p_7);
    }
}