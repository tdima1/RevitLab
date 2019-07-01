using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using RevitLab;
using System.Collections.Generic;
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
         ListViewItem item = sender as ListViewItem;
         //Selection selection;
         //FilteredElementCollector instances
         //      = new FilteredElementCollector(_doc, _doc.ActiveView.Id);
         ICollection<ElementId> selectedElems = new List<ElementId>();

         if (item != null && item.IsSelected) {
            TaskDialog.Show("title", (item.Content as MyListViewItem).CategoryName);
            if (_doc.GetElement((item.Content as MyListViewItem).Id.ToString()) != null) {
               selectedElems.Add(_doc.GetElement((item.Content as MyListViewItem).Id.ToString()).Id);
            }

            Lab5Button btn = new Lab5Button();
            btn.MakeSelection(_uidoc, _doc, selectedElems);
            
         }
      }

   }


}
