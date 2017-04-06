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
 

        public ObservableCollectioFileInfoAsync(Dispatcher dispatcher)
        {
            DispatcherObject = dispatcher;
           
            
        }

     

        async void AddItemAsync(FileInfo item)
        {
            try
            { 
            await Task.Run(() =>
                {


                    DispatcherObject.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                    {

                        this.Add(item);

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
