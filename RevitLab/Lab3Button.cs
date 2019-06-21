using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
/*FilteredElementCollector symbols
            = new FilteredElementCollector(doc)
               .OfClass(typeof(FamilySymbol));

         FamilySymbol toCloneSymbol = null;

         for (int i = 0; i < symbols.Count(); i++) {
            if (symbols.ElementAt(i).Name == "Ball Valve") {
               toCloneSymbol = symbols.ElementAt(i) as FamilySymbol;
            }
         }
         TaskDialog.Show("Symbol to clone", toCloneSymbol.Name);*/
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
         Family family = null;
         FamilySymbol newSymbol = null;

         using (Transaction trans = new Transaction(doc, "Load family")) {

            trans.Start();

            if (doc.LoadFamily(FamilyPath, out family)) {
               string name = family.Name;
               TaskDialog.Show("Revit", "Family file has been loaded. Its name is " + name);
            } else {
               TaskDialog.Show("Revit", "Family file already loaded.");
            }
            trans.Commit();
         }

         var firstSymbolId = family.GetFamilySymbolIds().First();
         FamilySymbol toCloneSymbol = (FamilySymbol)doc.GetElement(firstSymbolId);

         using (Transaction trans = new Transaction(doc, "Duplicate object and set params")) {
            trans.Start();

            double d1FeetValue = UnitUtils.ConvertToInternalUnits(60, DisplayUnitType.DUT_MILLIMETERS);
            double l1FeetValue = UnitUtils.ConvertToInternalUnits(100, DisplayUnitType.DUT_MILLIMETERS);

            try {
               newSymbol = toCloneSymbol.Duplicate("Ball Valve 2") as FamilySymbol;
               Parameter d1Param = newSymbol.LookupParameter("D 1");
               Parameter l1Param = newSymbol.LookupParameter("L 1");

               if (null != d1Param) {
                  d1Param.Set(d1FeetValue);
               }
               if (null != l1Param) {
                  l1Param.Set(l1FeetValue);
               }

            } catch (Exception e) {
               TaskDialog.Show("Exception: ", e.Message);
            }
            trans.Commit();
         }

         using (Transaction trans = new Transaction(doc, "Insert Family Instance")) {
            trans.Start();
            FamilyInstance instance = doc.Create.NewFamilyInstance(new XYZ(), newSymbol, StructuralType.UnknownFraming);
            trans.Commit();
         }



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

            if (2 == changesMade) {
               newFamilyTypeTransaction.Commit();
            } else {
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
