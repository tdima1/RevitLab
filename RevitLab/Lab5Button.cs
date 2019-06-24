using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;

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
         string selections = "";
         using (Transaction trans = new Transaction(doc, "Display selected elements category")){
            trans.Start();
            try {
               IList<Reference> refs = sel.PickObjects(ObjectType.Element, "Please select some elements");
               sel.Dispose();

               foreach (var s in refs) {
                  selections += doc.GetElement(s.ElementId).Category.Name + "\n";
               }

            TaskDialog.Show("title", selections);

            } catch (Exception e) {
               TaskDialog.Show("Exception", e.Message);
            }
            trans.Commit();
            return Result.Succeeded;
         }
      }
   }
}
