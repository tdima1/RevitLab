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

         Laborator1(app, panel);
         Laborator2(app, panel);
         Laborator3(app, panel);

         return Result.Succeeded;
      }

      public void AddImageToButton(PushButton btn, string uri)
      {
         BitmapImage Lab1ButtonImage = new BitmapImage(new Uri(uri)) {
            DecodePixelHeight = 16,
            DecodePixelWidth = 16
         };
         btn.LargeImage = Lab1ButtonImage;
      }

      public void Laborator1(UIControlledApplication app, RibbonPanel panel)
      {
         PushButton Lab1Button = panel.AddItem(new PushButtonData("Lab 1",
        "Display textbox", @"RevitLab.dll", "RevitLab.Lab1Button")) as PushButton;
         AddImageToButton(Lab1Button, @"C:\Users\Student\source\repos\RevitLab\RevitLab\Resources\Lab1Button.png");
         panel.AddSeparator();
      }

      public void Laborator2(UIControlledApplication app, RibbonPanel panel)
      {
         PushButton Lab2Button = panel.AddItem(new PushButtonData("Lab 2",
        "Load Family", @"RevitLab.dll", "RevitLab.Lab2Button")) as PushButton;
         AddImageToButton(Lab2Button, @"C:\Users\Student\source\repos\RevitLab\RevitLab\Resources\Lab2Button.png");
         panel.AddSeparator();
         
      }
      public void Laborator3(UIControlledApplication app, RibbonPanel panel)
      {
         PushButton Lab3Button = panel.AddItem(new PushButtonData("Lab 3",
        "Load Family", @"RevitLab.dll", "RevitLab.Lab3Button")) as PushButton;
         AddImageToButton(Lab3Button, @"C:\Users\Student\source\repos\RevitLab\RevitLab\Resources\Lab2Button.png");
         panel.AddSeparator();

      }


   }
}
