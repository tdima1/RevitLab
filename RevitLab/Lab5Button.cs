using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using LabRevit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RevitLab
{
   [Transaction(TransactionMode.Manual)]
   class Lab5Button : IExternalCommand
   {
      public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
      {
         UIDocument uidoc = commandData.Application.ActiveUIDocument;
         Document doc = uidoc.Document;
         //Selection sel = uidoc.Selection;
         Lab5Window window = new Lab5Window(uidoc);

         using (Transaction trans = new Transaction(doc, "Display selected elements category")){
            trans.Start();
            try {

            FilteredElementCollector instances
                     = new FilteredElementCollector(doc, doc.ActiveView.Id);

            List<MyListViewItem> myListViewItems = new List<MyListViewItem>();

               foreach (var inst in instances.ToElements()) {
                  myListViewItems.Add(new MyListViewItem() { Id = inst.Id.IntegerValue,
                     CategoryName = inst.Name });
                  
               }

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

      public Result MakeSelection(UIDocument _uidoc, Document _doc, ICollection<ElementId> selectedElems)
      {
         using (Transaction trans = new Transaction(_doc, "Highlight selected element from Window")) {
            trans.Start();
            _uidoc.Selection.SetElementIds(selectedElems);
            trans.Commit();
         }
         return Result.Succeeded;
      }
   }
}
