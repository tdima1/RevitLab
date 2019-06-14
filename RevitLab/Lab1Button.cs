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
   class Lab1Button : IExternalCommand
   {
      public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
      {
         TaskDialog.Show("Laborator 1", "Done");
         return Result.Succeeded;
      }
   }
}
