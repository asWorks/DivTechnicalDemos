using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MultipleFoldersFilesSort
{
   public class ObservableCollectioFileInfoAsync:ObservableCollection<FileInfo>
    {




        public Dispatcher DispatcherObject { get; protected set; }



        private int _FileCount;
        public int FileCount
        {
            get { return _FileCount; }
            set
            {
                if (value != _FileCount)
                {
                    _FileCount = value;
                    OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("FileCount"));
                    
                }
            }
        }


        public ObservableCollectioFileInfoAsync(Dispatcher dispatcher,IEnumerable<FileInfo> collection):base(collection)
        {
            DispatcherObject = dispatcher;
            FileCount = 0;



        }

        public ObservableCollectioFileInfoAsync(Dispatcher dispatcher)
        {
            DispatcherObject = dispatcher;
            FileCount = 0;
        }

     

      public  async void AddItemAsync(FileInfo item)
        {
            try
            { 
            await Task.Run(() =>
                {


                    DispatcherObject.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                    {

                        this.Add(item);

                        FileCount++;

                    }));


                }

            );
        }
            catch (UnauthorizedAccessException)
            {
                var x = new FileInfo(item + "_UnauthorizedAccessException");
        await Task.Run(() =>
                {
            DispatcherObject.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                this.Add(x);
            }));
        });


            }



}
        

       
    }
}
