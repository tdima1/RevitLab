using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace RevitLab
{
   [Transaction(Autodesk.Revit.Attributes.TransactionMode.ReadOnly)]
   public class Lab1 : IExternalApplication
   {
      public Result OnShutdown(UIControlledApplication application)
      {
         throw new NotImplementedException();
      }

      public Result OnStartup(UIControlledApplication app)
      {
         RibbonPanel panel = app.CreateRibbonPanel("New Ribbon Panel");
         panel.AddSeparator();

         PushButton Lab1Button = panel.AddItem(new PushButtonData("Lab 1",
        "Press to display textbox", @"RevitLab.dll", "RevitLab.Lab1Button")) as PushButton;
         Lab1Button.ToolTip = "Open text box";
         BitmapImage Lab1ButtonImage = new BitmapImage(new Uri(@"C:\Users\Student\source\repos\RevitLab\RevitLab\Resources\Lab1Button.png")) {
            DecodePixelHeight = 10,
            DecodePixelWidth = 10
         };
         Lab1Button.LargeImage = Lab1ButtonImage;
             
         //TaskDialog.Show("Lab1", "Done");

         return Result.Succeeded;
      }
   }
}
