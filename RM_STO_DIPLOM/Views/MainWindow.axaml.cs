using Avalonia.Controls;
using RM_STO_DIPLOM.ViewModels;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace RM_STO_DIPLOM.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        CustomsDeclLoader customsDeclLoader = new CustomsDeclLoader();
    }

    private void ListBox_DoubleTapped(object? sender, Avalonia.Input.TappedEventArgs e)
    {
        ListBox listBox = sender as ListBox;

        if(listBox == null)
        {
            return;
        }

        Declaration dec = (Declaration)listBox.SelectedItem;

        if (DataContext is MainViewModel vm)
        {
            vm.DeclCheck.Execute(dec);
        }

    }
}
