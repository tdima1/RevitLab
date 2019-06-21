using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
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
         string FamilyPath = @"C:\Users\Student\source\repos\RevitLab\RevitLab\Resources\families\Lab4_Test_Family.rfa";
         Family family = null;
         List<FamilySymbol> familySymbols = new List<FamilySymbol>();
         XYZ placementPoint = new XYZ(15, 10, 0);
         XYZ position = new XYZ();


         using (Transaction trans = new Transaction(doc, "Load family")) {

            trans.Start();

            if (doc.LoadFamily(FamilyPath, out family)) {
               string name = family.Name;
            } else {
               FilteredElementCollector families
                  = new FilteredElementCollector(doc)
                     .OfClass(typeof(Family));

               family = (from f in families
                         where f.Name == "Lab4_Test_Family"
                         select f as Family).First();

               if (null == family) {
                  throw new Exception("Couldn't load family. Path could be damaged");
               }
            }

            FilteredElementCollector symbols
               = new FilteredElementCollector(doc)
                  .OfClass(typeof(FamilySymbol));

            foreach (ElementId fsId in family.GetFamilySymbolIds()) {
               familySymbols.Add(doc.GetElement(fsId) as FamilySymbol);
            }

            trans.Commit();
         }

         using (Transaction trans = new Transaction(doc, "Insert Family Instances")) {
            trans.Start();
            Random rand = new Random();

            if (familySymbols.Count > 0) {
               foreach (FamilySymbol fs in familySymbols) {
                  fs.Activate();

                  FamilyInstance instance = doc.Create.NewFamilyInstance
                     (placementPoint, fs, StructuralType.UnknownFraming);

                  position = instance.GetTransform().OfPoint(placementPoint);


               }
            }
            trans.Commit();
         }

         using (Transaction getParamsTransaction = new Transaction(doc, "Select items in view")) {
            getParamsTransaction.Start();
            string selections = "";
            try {
               IList<Reference> refs = sel.PickObjects(ObjectType.Element, "Please select some elements");
               sel.Dispose();

               foreach (var s in refs) {
                  selections += $" {position.X} {position.Y} \n";
                  //var parameters = doc.GetElement(s).Parameters;

               }

               TaskDialog.Show("Elements", selections);

            } catch (Exception e) {
               TaskDialog.Show("Exception", e.Message);
            }

            getParamsTransaction.Commit();

         }


         return Result.Succeeded;
      }

   }
}
