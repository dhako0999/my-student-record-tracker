using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using HelloWorld.Avalonia.ViewModels;


namespace HelloWorld.Avalonia.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }
    
    public void AddStudent_Click(object? sender, RoutedEventArgs e)
    {
        (DataContext as MainWindowViewModel)?.AddStudent();
    }

    public void Refresh_Click(object? sender, RoutedEventArgs e)
    {
        Console.WriteLine("Refresh clicked!");
        (DataContext as MainWindowViewModel)?.Refresh();
    }
}