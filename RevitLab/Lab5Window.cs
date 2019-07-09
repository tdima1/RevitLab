using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using RevitLab;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RevitLab
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class Lab5Window : Window
   {
      public UIDocument _uidoc;
      public Document _doc;
      public Selection _sel;

      public Lab5Window(UIDocument uidoc)
      {
         _uidoc = uidoc;
         _doc = uidoc.Document;
         _sel = uidoc.Selection;

         InitializeComponent();
      }

      public void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {
         ICollection<Element> selectedElems = new List<Element>();
         ListViewItem item = sender as ListViewItem;
         
         string elementIdAsString = (item.Content as MyListViewItem).Id;

         TaskDialog.Show("title", $"{(item.Content as MyListViewItem).CategoryName} {elementIdAsString}");
         
         Lab5Button.MakeSelection(_uidoc, elementIdAsString);
      }

   }


}
