using System.ComponentModel;

namespace RevitLab
{
   class MyListViewItem : INotifyPropertyChanged
   {
      public int Id
      {
         get { return Id; }
         set
         {
            if (Id != value) {
               Id = value;
               OnPropertyChange("Id");
            }
         }
      }
      public string CategoryName
      {
         get { return CategoryName; }
         set
         {
            if (CategoryName != value) {
               CategoryName = value;
               OnPropertyChange("CategoryName");
            }
         }
      }

      public event PropertyChangedEventHandler PropertyChanged;

      protected void OnPropertyChange(string propertyName)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
   }
}
