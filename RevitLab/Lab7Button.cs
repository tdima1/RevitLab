using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
namespace RevitLab
{
   //LAB13
   [Transaction(TransactionMode.Manual)]
   public class Lab7Button : IExternalCommand
   {
      public static ExternalCommandData _commData = null;
      public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
      {
         _commData = commandData;
         UIDocument uidoc = commandData.Application.ActiveUIDocument;
         Document doc = uidoc.Document;

         using (Transaction trans = new Transaction(doc, "Draw Cube")) {
            trans.Start();

            LenghtInputWindow lenghtWindow = new LenghtInputWindow();
            lenghtWindow.ShowDialog();
            trans.Commit();
         }


         return Result.Succeeded;
      }
      //fsa
      public static Result DrawCube(int size)
      {
         if (_commData != null) {
            UIDocument uidoc = _commData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            List<XYZ> cubePoints = new List<XYZ> {
               new XYZ(),
               new XYZ(size, 0, 0),
               new XYZ(size, size, 0),
               new XYZ(0, size, 0)
            };
            //XYZ testPoint = new XYZ(cubeStart.X + size, cubeStart.Y, cubeStart.Z);
            for (int i = 0; i < cubePoints.Count -1; i++) {
               Line line = Line.CreateBound(cubePoints[i], cubePoints[i+1]);
               //Application app = _commData.Application.Application.Create;
            }

            return Result.Succeeded;
         } else {
            return Result.Failed;
         }
      }


   }
}
