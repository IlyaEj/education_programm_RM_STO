using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using RM_STO_DIPLOM.ViewModels;
using System;

namespace RM_STO_DIPLOM;

public partial class ControlFLC : UserControl
{
    public ControlFLC()
    {
        InitializeComponent();
        DataContext = new ControlFLCModel();
    }

}