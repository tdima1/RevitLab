using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitLab
{
   [Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
   class Lab4Button : IExternalCommand
   {
      public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
      {
         UIDocument uidoc = commandData.Application.ActiveUIDocument;
         Document doc = uidoc.Document;
         Selection sel = uidoc.Selection;

         IList<Reference> refs = sel.PickObjects(ObjectType.Element, "Please select some elements");

         using (Transaction getParamsTransaction = new Transaction(doc, "Get params")) {
            getParamsTransaction.Start();

            FamilyInstance inst = null;
            getParamsTransaction.Commit();

         }


         return Result.Succeeded;
      }

   }
}
