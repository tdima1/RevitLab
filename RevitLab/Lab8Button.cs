using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitLab
{
   [Transaction(TransactionMode.Manual)]
   //PLACE PIPE INBETWEEN TWO POINTS
   class Lab8Button : IExternalCommand
   {
      public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
      {
         UIDocument uidoc = commandData.Application.ActiveUIDocument;
         Document doc = uidoc.Document;

         var mepSystemTypes = new FilteredElementCollector(doc)
                              .OfClass(typeof(PipingSystemType))
                              .OfType<PipingSystemType>()
                              .ToList();

         var domesticHotWaterSystemType = mepSystemTypes.FirstOrDefault(
                                          st => st.SystemClassification ==
       MEPSystemClassification.DomesticHotWater);

         if (domesticHotWaterSystemType == null) {
            message = "Could not found Domestic Hot Water System Type";
            return Result.Failed;
         }

         var pipeTypes = new FilteredElementCollector(doc)
                        .OfClass(typeof(PipeType))
                        .OfType<PipeType>()
                        .ToList();

         var firstPipeType = pipeTypes.FirstOrDefault();

         if (firstPipeType == null) {
            TaskDialog.Show("Oops!", "Could not found Pipe Type");
            return Result.Failed;
         }

         var level = uidoc.ActiveView.GenLevel;

         if (level == null) {
            TaskDialog.Show("Oops!", "Wrong Active View");
            return Result.Failed;
         }

         var startPoint = XYZ.Zero;

         var endPoint = new XYZ(10, 0, 0);

         using (Transaction t = new Transaction(doc, "Create pipe using Pipe.Create")) {
            try {
               t.Start();
               var pipe = Pipe.Create(doc,
                 domesticHotWaterSystemType.Id,
                 firstPipeType.Id,
                 level.Id,
                 startPoint,
                 endPoint);

               t.Commit();
            } catch (Exception e) {
               TaskDialog.Show("Error", e.Message);
               t.RollBack();
               return Result.Failed;
            }
            return Result.Succeeded;
         }
      }
   }
}