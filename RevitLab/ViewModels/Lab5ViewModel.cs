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
      //public ICommand listViewClick;

      public Lab5ViewModel()
      {
         itemsList = new List<MyListViewItem>();
         //this.listViewClick = new CommandBinding(listViewClick)
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

      
   }
}
