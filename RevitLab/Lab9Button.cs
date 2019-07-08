using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;

namespace RevitLab
{
   //ROTATE SELECTION RIGHT
   [Transaction(TransactionMode.Manual)]
   class Lab9Button : IExternalCommand
   {
      public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
      {
         UIDocument uidoc = commandData.Application.ActiveUIDocument;
         Document doc = uidoc.Document;
         Selection sel = uidoc.Selection;

         IList<Reference> refs = sel.PickObjects(ObjectType.Element, "Select items you wish to rotate");

         using (Transaction trans = new Transaction(doc, "Rotate Left by 45 degrees")) {
            try {
               trans.Start();
               if (refs != null) {
                  foreach (var r in refs) {
                     ElementTransformUtils.RotateElement(doc, r.ElementId, Line.CreateBound(new XYZ(0, 0, 0), new XYZ(0, 0, 30)), Math.PI / 4.0);
                  }
                  trans.Commit();
                  return Result.Succeeded;
               } else {
                  TaskDialog.Show("Error", "You must select some elements");
                  trans.RollBack();
                  return Result.Failed;
               }
            }catch (Exception e) {
               TaskDialog.Show("Error", e.Message);
               trans.RollBack();
               return Result.Failed;
            }
         }
      }
   }
}
