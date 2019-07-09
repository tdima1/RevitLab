using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RevitLab
{
   [Transaction(TransactionMode.Manual)]
   class Lab5Button : IExternalCommand
   {
      public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
      {
         UIDocument uidoc = commandData.Application.ActiveUIDocument;
         Document doc = uidoc.Document;

         using (Transaction trans = new Transaction(doc, "Display selected elements category")){
            trans.Start();
            try {

               FilteredElementCollector instances
                        = new FilteredElementCollector(doc, doc.ActiveView.Id)
                        .OfClass(typeof(FamilyInstance));

            List<MyListViewItem> myListViewItems = new List<MyListViewItem>();

               foreach (Element inst in instances.ToElements()) {
                  myListViewItems.Add(new MyListViewItem() {
                     Id = inst.UniqueId,
                     CategoryName = inst.Name });
               }

               Lab5Window window = new Lab5Window(uidoc);
               
               window.CategoryList.ItemsSource = myListViewItems;
               window.Show();

               trans.Commit();

            } catch (Exception e) {
               TaskDialog.Show("Exception", e.Message);
               trans.RollBack();
            }
            return Result.Succeeded;
         }
      }

      public static Result MakeSelection(UIDocument _uidoc, string selectedElemId)
      {
         List<ElementId> selectedElemIdList = new List<ElementId>() { _uidoc.Document.GetElement(selectedElemId).Id };
         _uidoc.Selection.SetElementIds(selectedElemIdList);
         return Result.Succeeded;
      }
   }
}
