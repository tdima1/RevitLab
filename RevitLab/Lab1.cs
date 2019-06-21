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
         return Result.Succeeded;
      }

      public Result OnStartup(UIControlledApplication app)
      {
         RibbonPanel panel = app.CreateRibbonPanel("New Panel");

         Laborator1(panel);
         Laborator2(panel);
         Laborator3(panel);
         Laborator4(panel);

         return Result.Succeeded;
      }

      public void AddImageToButton(PushButton btn, string uri)
      {
         BitmapImage Lab1ButtonImage = new BitmapImage(new Uri(uri));
         btn.LargeImage = Lab1ButtonImage;
      }

      public void Laborator1(RibbonPanel panel)
      {
         PushButton Lab1Button = panel.AddItem(new PushButtonData("Lab 1",
        "Display textbox", @"RevitLab.dll", "RevitLab.Lab1Button")) as PushButton;
         AddImageToButton(Lab1Button, @"C:\Users\Student\source\repos\RevitLab\RevitLab\Resources\Lab1Button.png");
         panel.AddSeparator();
      }

      public void Laborator2(RibbonPanel panel)
      {
         PushButton Lab2Button = panel.AddItem(new PushButtonData("Lab 2",
        "Load Family", @"RevitLab.dll", "RevitLab.Lab2Button")) as PushButton;
         AddImageToButton(Lab2Button, @"C:\Users\Student\source\repos\RevitLab\RevitLab\Resources\Lab2Button.png");
         panel.AddSeparator();
         
      }
      public void Laborator3(RibbonPanel panel)
      {
         PushButton Lab3Button = panel.AddItem(new PushButtonData("Lab 3",
        "Duplicate Symbol and place", @"RevitLab.dll", "RevitLab.Lab3Button")) as PushButton;
         AddImageToButton(Lab3Button, @"C:\Users\Student\source\repos\RevitLab\RevitLab\Resources\Lab3Button.png");
         panel.AddSeparator();
      }
      public void Laborator4(RibbonPanel panel)
      {
         PushButton Lab4Button = panel.AddItem(new PushButtonData("Lab 4",
        "Make Selection", @"RevitLab.dll", "RevitLab.Lab4Button")) as PushButton;
         AddImageToButton(Lab4Button, @"C:\Users\Student\source\repos\RevitLab\RevitLab\Resources\Lab3Button.png");
         panel.AddSeparator();
      }

   }
}
