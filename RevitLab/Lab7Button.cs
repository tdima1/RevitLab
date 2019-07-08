using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
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

      public static Result DrawCube(int size)
      {
         int offsetX = size / 3;
         int offsetY = size / 5;

         if (_commData != null) {
            UIDocument uidoc = _commData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            UIApplication uiapp = _commData.Application;
            View activeView = doc.ActiveView;
            //Autodesk.Revit.ApplicationServices.Application app = _commData.Application.Application;

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

            //Curve geometryCurve = Line.CreateBound(cubePoints[0], cubePoints[1]);
            //DetailLine line = doc.Create.NewDetailCurve(activeView, geometryCurve) as DetailLine;
            //geometryCurve = Line.CreateBound(cubePoints[1], cubePoints[2]);
            //line = doc.Create.NewDetailCurve(activeView, geometryCurve) as DetailLine;
            //geometryCurve = Line.CreateBound(cubePoints[2], cubePoints[3]);
            //line = doc.Create.NewDetailCurve(activeView, geometryCurve) as DetailLine;
            //geometryCurve = Line.CreateBound(cubePoints[3], cubePoints[0]);
            //line = doc.Create.NewDetailCurve(activeView, geometryCurve) as DetailLine;

            foreach (XYZ start in cubePoints) {
               foreach (XYZ finish in cubePoints) {
                  if (start != finish && (start.DistanceTo(finish) == size || 
                     start.DistanceTo(finish).Equals(Math.Sqrt(Math.Pow(offsetX, 2) + Math.Pow(offsetY, 2))) )) {
                     Curve geometryCurve = Line.CreateBound(start, finish);
                     //Curve geometryCurveZ = geometryCurve.CreateTransformed(Transform.CreateTranslation(new XYZ(size/2,size/3,size)));
                     //Curve geometryCurveY = geometryCurve.CreateTransformed(Transform.CreateRotation(new XYZ(0, 0, 1), 90));

                     DetailLine line = doc.Create.NewDetailCurve(activeView, geometryCurve) as DetailLine;
                     //DetailLine lineZ = doc.Create.NewDetailCurve(activeView, geometryCurveZ) as DetailLine;
                     //DetailLine lineY = doc.Create.NewDetailCurve(activeView, geometryCurveY) as DetailLine;
                  }
               }
            }

            //XYZ testPoint = new XYZ(cubeStart.X + size, cubeStart.Y, cubeStart.Z);
            //for (int i = 0; i < cubePoints.Count - 1; i++) {
            //   Curve geometryCurve = Line.CreateBound(cubePoints[i], cubePoints[i + 1]);
            //   Plane geometryPlane = Plane.CreateByNormalAndOrigin(new XYZ(1,1,0), new XYZ());
            //   SketchPlane sketch = SketchPlane.Create(doc, geometryPlane);
            //   doc.Create.NewModelCurve(geometryCurve, sketch);
            //   //DrawLine(_commData.Application, cubePoints[i], cubePoints[i + 1], new XYZ(1, 1, 1));
            //}

            //XYZ startPoint = new XYZ(0, 0, 0);
            //XYZ endPoint = new XYZ(10, 10, 0);
            //Line geomLine = Line.CreateBound(startPoint, endPoint);
            //
            //XYZ end0 = new XYZ(1, 0, 0);
            //XYZ end1 = new XYZ(10, 10, 10);
            //XYZ pointOnCurve = new XYZ(10, 0, 0);
            //Arc geomArc = Arc.Create(end0, end1, pointOnCurve);
            //
            //XYZ origin = new XYZ(0, 0, 0);
            //XYZ normal = new XYZ(1, 1, 0);
            //Plane geomPlane = Plane.CreateByNormalAndOrigin(normal, origin);
            //
            //SketchPlane sketch = SketchPlane.Create(doc, geomPlane);
            //
            //ModelLine line = doc.Create.NewModelCurve(geomLine, sketch) as ModelLine;
            //
            //ModelArc arc = doc.Create.NewModelCurve(geomArc, sketch) as ModelArc;

            return Result.Succeeded;
         } else {
            return Result.Failed;
         }
      }

      public static void DrawLine(UIApplication app, XYZ start, XYZ finish, XYZ direction)
      {
         if (start.DistanceTo(finish) < 0) {
            throw new Exception("Distance must be greater than 0.");
         }

         //try {
         Line line = Line.CreateBound(start, finish);
         XYZ rotatedDirection = XYZ.BasisX;
         if (!direction.IsAlmostEqualTo(XYZ.BasisZ) && !direction.IsAlmostEqualTo(-XYZ.BasisZ)) {
            rotatedDirection = direction.Normalize().CrossProduct(XYZ.BasisZ);
         }

         Plane geometryPlane = Plane.CreateByOriginAndBasis(direction, rotatedDirection, start);
         SketchPlane skplane = SketchPlane.Create(app.ActiveUIDocument.Document, geometryPlane);
         ModelCurve mcurve = app.ActiveUIDocument.Document.Create.NewModelCurve(line, skplane);

         //} catch (Exception e) {
         //   TaskDialog.Show("Error", e.Message);
         //}

      }
   }
}
