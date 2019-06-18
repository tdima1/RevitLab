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
   class Lab2Button : IExternalCommand
   {
      public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
      {
         UIApplication app = commandData.Application;
         Document FamilyDocument = app.ActiveUIDocument.Document;
         string FamilyPath = @"C:\Users\Student\source\repos\RevitLab\RevitLab\Resources\families\Lab3_Test_Family.rfa";

         Transaction trans = new Transaction(
            FamilyDocument, "FakeLoading");

         trans.Start();
         Family family;
         if (FamilyDocument.LoadFamily(FamilyPath, out family)) {
            string name = family.Name;
            TaskDialog.Show("Revit", "Family file has been loaded. Its name is " + name);
         } else {
            TaskDialog.Show("Revit", "Can't load the family file.");
         }
         trans.Commit();
         return Result.Succeeded;
      }
   }
}
