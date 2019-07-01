using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using LabRevit;

namespace RevitLab
{
   //LAB13
   [Transaction(TransactionMode.Manual)]
   class Lab7Button : IExternalCommand
   {


      public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
      {
         UIDocument uidoc = commandData.Application.ActiveUIDocument;
         Document doc = uidoc.Document;
         int userLenghtInput = 0;

         LenghtInputWindow lenghtWindow = new LenghtInputWindow();
         lenghtWindow.ShowDialog();

         return Result.Succeeded;
      }
   }
}
