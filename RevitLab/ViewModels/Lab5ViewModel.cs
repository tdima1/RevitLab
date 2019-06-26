using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RevitLab.ViewModels
{
   class Lab5ViewModel
   {
      private IList<MyListViewItem> itemsList;

      public Lab5ViewModel()
      {
         itemsList = new List<MyListViewItem>();
      }

      public IList<MyListViewItem> Items
      {
         get { return itemsList; }
         set { itemsList = value; }
      }

      public bool CanExecute()
      {
         return true;
      }

      public void Execute()
      {
         TaskDialog.Show("test", "command test");
      }

      private ICommand _doSomething;
      public ICommand DoSomethingCommand
      {
         get
         {
            if (_doSomething == null) {
               _doSomething = new RelayCommand(
                   p => this.CanExecute(),
                   p => this.Execute());
               ;
            }
            return _doSomething;
         }
      }
   }
}
