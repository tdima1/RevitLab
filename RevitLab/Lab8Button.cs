using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
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
         Selection sel = uidoc.Selection;

         var mepSystemTypes = new FilteredElementCollector(doc)
                              .OfClass(typeof(PipingSystemType))
                              .OfType<PipingSystemType>()
                              .ToList();

         var domesticHotWaterSystemType = mepSystemTypes.FirstOrDefault(
                                          st => st.SystemClassification ==
       MEPSystemClassification.DomesticHotWater);

         if (domesticHotWaterSystemType == null) {
            message = "Could not find Domestic Hot Water System Type";
            return Result.Failed;
         }

         var pipeTypes = new FilteredElementCollector(doc)
                        .OfClass(typeof(PipeType))
                        .OfType<PipeType>()
                        .ToList();

         var firstPipeType = pipeTypes.FirstOrDefault();

         if (firstPipeType == null) {
            TaskDialog.Show("Oops!", "Could not find Pipe Type");
            return Result.Failed;
         }

         var level = uidoc.ActiveView.GenLevel;

         if (level == null) {
            TaskDialog.Show("Oops!", "Wrong Active View");
            return Result.Failed;
         }

         using (Transaction trans = new Transaction(doc, "Create pipe using Pipe.Create")) {
            try {
               trans.Start();

               XYZ startPoint = sel.PickPoint();
               XYZ endPoint = sel.PickPoint();

               var pipe = Pipe.Create(doc,
                 domesticHotWaterSystemType.Id,
                 firstPipeType.Id,
                 level.Id,
                 startPoint,
                 endPoint);

               trans.Commit();
            } catch (Exception e) {
               TaskDialog.Show("Error", e.Message);
               trans.RollBack();
               return Result.Failed;
            }
            return Result.Succeeded;
         }
      }
   }
}