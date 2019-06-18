using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitLab
{
   [Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
   class Lab3Button : IExternalCommand
   {
      public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
      {
         UIApplication app = commandData.Application;
         Document doc = app.ActiveUIDocument.Document;
         string FamilyPath = @"C:\Users\Student\source\repos\RevitLab\RevitLab\Resources\families\Lab3_Test_Family.rfa";

         Transaction trans = new Transaction(
            doc, "FakeLoading");

         trans.Start();

         Family family;

         if (doc.LoadFamily(FamilyPath, out family)) {
            String name = family.Name;
            TaskDialog.Show("Revit", "Family file has been loaded. Its name is " + name);
         } else {
            TaskDialog.Show("Revit", "Can't load the family file.");
         }
         trans.Commit();
         Document familyDoc = doc.EditFamily(family);
         MakeNewFamilyType(familyDoc, 60, 100);

         GetFamilyTypesInFamily(familyDoc);

         familyDoc.LoadFamily(doc, new LoadOpts());

         return Result.Succeeded;
      }

      public void MakeNewFamilyType(Document familyDoc, int D1, int L1)
      {
         FamilyManager familyManager = familyDoc.FamilyManager;
         using (Transaction newFamilyTypeTransaction = new Transaction(familyDoc, "Add Type to Family")) {

            int changesMade = 0;
            newFamilyTypeTransaction.Start();

            FamilyType newFamilyType = familyManager.NewType("60 X 100");

            if (newFamilyType != null) {

               FamilyParameter familyParam = familyManager.get_Parameter("D 1");
               if (null != familyParam) {
                  familyManager.Set(familyParam, D1);
                  changesMade += 1;
               }

               familyParam = familyManager.get_Parameter("L 1");
               if (null != familyParam) {
                  familyManager.Set(familyParam, L1);
                  changesMade += 1;
               }
            }



            if (2 == changesMade)   
            {
               newFamilyTypeTransaction.Commit();
            } else   
              {
               newFamilyTypeTransaction.RollBack();
            }
         }
      }

      public void GetFamilyTypesInFamily(Document familyDoc)
      {
         if (familyDoc.IsFamilyDocument) {
            FamilyManager familyManager = familyDoc.FamilyManager;
            // get types in family
            string types = "Family Types: ";
            FamilyTypeSet familyTypes = familyManager.Types;
            FamilyTypeSetIterator familyTypesItor = familyTypes.ForwardIterator();
            familyTypesItor.Reset();

            while (familyTypesItor.MoveNext()) {
               FamilyType familyType = familyTypesItor.Current as FamilyType;
               types += "\n" + familyType.Name;
            }
            TaskDialog.Show("Revit", types);
         }
      }
   }
}
