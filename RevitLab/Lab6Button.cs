using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Collections.Generic;
using System.Linq;

namespace RevitLab
{
   [Transaction(TransactionMode.Manual)]
   class Lab6Button : IExternalCommand
   {
      //LAB 12

      public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
      {
         UIDocument uidoc = commandData.Application.ActiveUIDocument;
         Document doc = uidoc.Document;
         Selection sel = uidoc.Selection;
         IList<Reference> refs = sel.PickObjects(ObjectType.Element, "Please select some elements");

         if (!refs.Count().Equals(1)) {
            TaskDialog.Show("Error", "Please select only one element from the drawing");
         } else {
            if (refs.Count.Equals(1)) {
               Element selectedElement = doc.GetElement(refs.First().ElementId);
               using (Transaction trans = new Transaction(doc, "Translate Element")) {
                  trans.Start();
                  ElementTransformUtils.MoveElement(doc, selectedElement.Id, new XYZ(20, 0, 0));
                  trans.Commit();
               }
            } else {
               return Result.Failed;
            }
         }

         return Result.Succeeded;
      }
   }
}
