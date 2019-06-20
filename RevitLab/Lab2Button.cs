using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitLab
{
   [Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
   class Lab2Button : IExternalCommand
   {
      public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
      {
         UIApplication app = commandData.Application;
         Document document = app.ActiveUIDocument.Document;
         string familyPath = @"C:\Users\Student\source\repos\RevitLab\RevitLab\Resources\families\Lab3_Test_Family.rfa";

         Transaction trans = new Transaction(document, "Loading");

         trans.Start();
         if (document.LoadFamily(familyPath, out Family family)) {
            string name = family.Name;
            TaskDialog.Show("Revit", $"Family file has been loaded. Its name is {name}");
         } else {
            TaskDialog.Show("Revit", $"Can't load the family file at {familyPath}.");
         }
         trans.Commit();
         return Result.Succeeded;
      }
   }
}
