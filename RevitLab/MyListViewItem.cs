using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitLab
{
   class MyListViewItem : INotifyPropertyChanged
   {
      public string Id { get; set; }
      public string CategoryName { get; set; }

      public event PropertyChangedEventHandler PropertyChanged;

      public void RaisePropertyChanged(string propertyName)
      {
         PropertyChangedEventHandler handler = PropertyChanged;
         if (handler != null) {
            handler(this, new PropertyChangedEventArgs(propertyName));
         }
      }
   }
}
