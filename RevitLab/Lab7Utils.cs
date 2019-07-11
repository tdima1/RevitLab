using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RevitLab
{
   public class Lab7Utils
   {

      ICommand _getCubeSize = new DelegateCommand((param) => Lab7Button.DrawCube(Convert.ToInt32(param)));

      ICommand _refreshList = new DelegateCommand( x => Lab5Button.PopulateList());

      public ICommand GetCubeSize { get { return _getCubeSize; } }

      public ICommand RefreshList { get { return _refreshList; } }

   }
}