using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace RevitLab
{
   [Transaction(TransactionMode.Manual)]
   class Lab5Button : IExternalCommand
   {
      public static ObservableCollection<MyListViewItem> _myListViewItems = new ObservableCollection<MyListViewItem>();

      public static Lab5Window _window;

      public PropertyChangedEventHandler PropertyChanged;

      public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
      {
         
         //commandData.Application.ActiveUIDocument.RefreshActiveView();
         UIDocument uidoc = commandData.Application.ActiveUIDocument;
         Document doc = uidoc.Document;
         commandData.Application.ViewActivated += new EventHandler<ViewActivatedEventArgs>(OnViewActivated);

         using (Transaction trans = new Transaction(doc, "Display selected elements category")) {
            trans.Start();
            try {

               _window = new Lab5Window(uidoc);
               PopulateList();
               _window.Show();

               trans.Commit();

            } catch (Exception e) {
               TaskDialog.Show("Exception", e.Message);
               trans.RollBack();
            }
            return Result.Succeeded;
         }
      }

      void OnViewActivated(object sender, ViewActivatedEventArgs e){
         try {
            View vPrevious = e.PreviousActiveView;
            View vCurrent = e.CurrentActiveView;
            //TaskDialog.Show("test", (sender as UIApplication).ActiveUIDocument.Document.Title);
            //_window.Close();
            _window = new Lab5Window((sender as UIApplication).ActiveUIDocument);
            //PopulateList();

         } catch (Exception ex) {
            throw ex;
         }

      }

      public static void PopulateList()
      {
         FilteredElementCollector instances
                       = new FilteredElementCollector(_window._doc, _window._doc.ActiveView.Id)
                       .OfClass(typeof(FamilyInstance));

         _myListViewItems.Clear();

         foreach (Element inst in instances.ToElements()) {
            _myListViewItems.Add(new MyListViewItem() {
               Id = inst.UniqueId,
               CategoryName = inst.Name
            });
         }

         _window.CategoryList.ItemsSource = _myListViewItems;
         _window.CategoryList.Items.Refresh();
         // (_window as Lab5Window).Show();
      }

      public static Result MakeSelection(UIDocument _uidoc, string selectedElemId)
      {
         try {
            List<ElementId> selectedElemIdList = new List<ElementId>() { _uidoc.Document.GetElement(selectedElemId).Id };
            _uidoc.Selection.SetElementIds(selectedElemIdList);
            return Result.Succeeded;
         } catch (Exception e) {
            TaskDialog.Show("Error", e.Message);
            return Result.Failed;
         }
      }

      public void RaisePropertyChanged(string propertyName)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
   }
}
