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
   [Transaction(Autodesk.Revit.Attributes.TransactionMode.ReadOnly)]
   class Lab2Button : IExternalCommand
   {
      public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
      {
         UIApplication app = commandData.Application;
         Document FamilyDoctument = app.ActiveUIDocument.Document;

         LoadFam(FamilyDoctument);
         return Result.Succeeded;
      }

      void LoadFam(Autodesk.Revit.DB.Document document)
      {
         string FamilyPath = "C:\\Users\\Student\\source\\repos\\RevitLab\\RevitLab\\Resources\\families\\Lab3_Test_Family.rfa";

         Family family = null;
         if (!document.LoadFamily(FamilyPath, out family)) {
            throw new Exception("Unable to load " + FamilyPath);
         }
      }
   }
}
