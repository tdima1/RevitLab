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
   [Transaction(TransactionMode.ReadOnly)]
   public class Lab1 : IExternalApplication
   {
      private int numberOfAddins;
      private List<string> buttonNames = new List<string>();

      public Result OnShutdown(UIControlledApplication application)
      {
         return Result.Succeeded;
      }

      public Lab1()
      {
         buttonNames.Add("Display textbox");
         buttonNames.Add("Load Family");
         buttonNames.Add("Duplicate Symbol and place");
         buttonNames.Add("Make Selection of family 4 elem");
         buttonNames.Add("Display selection category");
         numberOfAddins = buttonNames.Count();
      }

      public void Initialize(RibbonPanel panel, int addinNum, string buttonName)
      {
         PushButton button = panel.AddItem(new PushButtonData($"Lab {addinNum}",
         buttonName, @"RevitLab.dll", $"RevitLab.Lab{addinNum}Button")) as PushButton;
         AddImageToButton(button, $@"C:\Users\Student\source\repos\RevitLab\RevitLab\Resources\Lab{addinNum}Button.png");
         panel.AddSeparator();
      }

      public Result OnStartup(UIControlledApplication app)
      {
         RibbonPanel panel = app.CreateRibbonPanel("New Panel");

         for (int addinNum = 1; addinNum <= numberOfAddins; addinNum++) {
            Initialize(panel, addinNum, buttonNames[addinNum - 1]);
         }
         return Result.Succeeded;
      }

      public void AddImageToButton(PushButton btn, string uri)
      {
         BitmapImage Lab1ButtonImage = new BitmapImage(new Uri(uri));
         btn.LargeImage = Lab1ButtonImage;
      }
   }
}
