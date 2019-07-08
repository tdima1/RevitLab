using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;

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

      public static Result DrawCube(int size)
      {
         int offsetX = size / 3;
         int offsetY = size / 5;

         if (_commData != null) {
            UIDocument uidoc = _commData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            UIApplication uiapp = _commData.Application;
            View activeView = doc.ActiveView;
            List<DetailLine> cubeEdgeList = new List<DetailLine>();

            List<XYZ> cubePoints = new List<XYZ> {
               new XYZ(),
               new XYZ(size, 0, 0),
               new XYZ(size, size, 0),
               new XYZ(0, size, 0),

               new XYZ(offsetX, offsetY, 0),
               new XYZ(size + offsetX, offsetY, 0),
               new XYZ(size + offsetX, size+offsetY, 0),
               new XYZ(offsetX, size + offsetY, 0)
            };

            //foreach (XYZ start in cubePoints) {
            //   foreach (XYZ finish in cubePoints) {
            for (int i = 0; i < cubePoints.Count - 1; i++) {
               for (int j = i + 1; j < cubePoints.Count; j++) {
                  if (cubePoints[i] != cubePoints[j] && (cubePoints[i].DistanceTo(cubePoints[j]) == size ||
                     cubePoints[i].DistanceTo(cubePoints[j]).Equals(Math.Sqrt(Math.Pow(offsetX, 2) + Math.Pow(offsetY, 2))))) {

                     Curve geometryCurve = Line.CreateBound(cubePoints[i], cubePoints[j]);

                     cubeEdgeList.Add(doc.Create.NewDetailCurve(activeView, geometryCurve) as DetailLine);
                  }
               }
            }

            return Result.Succeeded;
         } else {
            return Result.Failed;
         }
      }
   }
}
