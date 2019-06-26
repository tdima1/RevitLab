using RevitLab.ViewModels;
using System.Windows;

namespace LabRevit
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      private Lab5ViewModel ViewModel { get; set; }
      public MainWindow()
      {
         InitializeComponent();
         ViewModel = new Lab5ViewModel();
         DataContext = ViewModel;
      }
   }
}
