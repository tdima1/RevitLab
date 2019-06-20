using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Linq;

namespace RevitLab
{
   [Transaction(TransactionMode.Manual)]
   class Lab3Button : IExternalCommand
   {
      public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
      {
         UIApplication app = commandData.Application;
         Document doc = app.ActiveUIDocument.Document;
         string FamilyPath = @"C:\Users\Student\source\repos\RevitLab\RevitLab\Resources\families\Lab3_Test_Family.rfa";

         Transaction trans = new Transaction(
            doc, "Make new type");

         trans.Start();

         if (doc.LoadFamily(FamilyPath, out Family family)) {
            string name = family.Name;
            TaskDialog.Show("Revit", "Family file has been loaded. Its name is " + name);
         } else {
            TaskDialog.Show("Revit", "Can't load the family file.");
         }
         trans.Commit();
         //FamilyManager famManager = doc.FamilyManager;
         //famManager.NewType("60x100");

         //IList<Parameter> paramList = firstSymbol.GetParameters("D 1");
         //
         //for (int p = 0; p < paramList.Count; p++) {
         //   newSymbol.GetParameters("D 1").ElementAt(p).Set(60);
         //}

         FilteredElementCollector symbols
            = new FilteredElementCollector(doc)
               .OfClass(typeof(FamilySymbol));

         //string str = "";
         //foreach (FamilySymbol s in symbols) {
         //   str = str + s.Name + "\n";
         //}
         //TaskDialog.Show("sfsa", str);

         FamilySymbol toCloneSymbol = null;

         for (int i = 0; i < symbols.Count(); i++) {
            if (symbols.ElementAt(i).Name == "Ball Valve") {
               toCloneSymbol = symbols.ElementAt(i) as FamilySymbol;
            }
         }

         trans.Start();
         FamilySymbol newSymbol = toCloneSymbol.Duplicate("test") as FamilySymbol;
         newSymbol.LookupParameter("D 1").Set(60);
         trans.Commit();
         //Can't make type "Lab3_Test_Family : test".




         // trans.Start();
         // FamilySymbol newSymbol = firstSymbol.Duplicate("test") as FamilySymbol;
         // newSymbol.LookupParameter("D 1").Set(60);
         // trans.Commit();



         //Document familyDoc = doc.EditFamily(family);

         //MakeNewFamilyType(familyDoc, 60, 100);
         //
         //GetFamilyTypesInFamily(familyDoc);
         //
         //familyDoc.LoadFamily(doc, new LoadOpts());

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
