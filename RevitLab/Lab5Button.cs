using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using LabRevit;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace RevitLab
{
   [Transaction(TransactionMode.Manual)]
   class Lab5Button : IExternalCommand
   {
      public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
      {
         UIDocument uidoc = commandData.Application.ActiveUIDocument;
         Document doc = uidoc.Document;
         Selection sel = uidoc.Selection;
         Lab5Window window = new Lab5Window();

         using (Transaction trans = new Transaction(doc, "Display selected elements category")){
            trans.Start();
            try {
               IList<Reference> refs = sel.PickObjects(ObjectType.Element, "Please select some elements");
               sel.Dispose();
               GridView view = new GridView();

               GridViewColumn col2 = new GridViewColumn {
                  Header = "Category Name"
               };
               view.Columns.Add(col2);

               window.CategoryList.View = view;

               foreach (var s in refs) {
                  window.CategoryList.Items.Add(doc.GetElement(s.ElementId).Category.Name);
               }

               window.Show();

            } catch (Exception e) {
               TaskDialog.Show("Exception", e.Message);
            }
            trans.Commit();
            return Result.Succeeded;
         }
      }
   }
}
