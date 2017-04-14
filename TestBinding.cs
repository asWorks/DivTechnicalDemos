using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleFoldersFilesSort
{
   public class TestBinding:INotifyPropertyChanged
    {



        private int _FileCounter;
        public int FileCounter
        {
            get { return _FileCounter; }
            set
            {
                if (value != _FileCounter)
                {
                    _FileCounter = value;
                    OnPropertyChanged("FileCounter");
                    //  isDirty = true;
                }
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}
