using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RevitLab
{
   class LenghtCommand : ICommand
   {

      public bool CanExecute(object parameter)
      {
         return parameter != null;
      }

      public void Execute(object parameter)
      {
         TaskDialog.Show("Title", "Test");
      }

      public event EventHandler CanExecuteChanged;
   }
}
