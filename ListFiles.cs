using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Diagnostics;

namespace MultipleFoldersFilesSort
{
    public class ListFiles : INotifyPropertyChanged
    {

        private long fLength;
        private int fCount;
        private long fLengthTotal;
        DirectoryInfo searchFolder;
        Stopwatch sWatch;


        public virtual Dispatcher DispatcherObject { get; protected set; }


        private List<FileInfo> TempList;

        //public ObservableCollection<FileInfo> Filelist { get; set; }



        private ObservableCollection<FileInfo> _Filelist;
        public ObservableCollection<FileInfo> Filelist
        {
            get { return _Filelist; }
            set
            {
                if (value != _Filelist)
                {
                    _Filelist = value;
                    OnPropertyChanged("Filelist");
                    //  isDirty = true;
                }
            }
        }

        private int _FileCount;
        public int FileCount
        {
            get { return _FileCount; }
            set
            {
                if (value != _FileCount)
                {
                    _FileCount = value;
                    OnPropertyChanged("FileCount");
                    //  isDirty = true;
                }
            }
        }


        private long _MaxFileLength;
        public long MaxFileLength
        {
            get { return _MaxFileLength; }
            set
            {
                if (value != _MaxFileLength)
                {
                    _MaxFileLength = value;
                    OnPropertyChanged("MaxFileLength");
                    //  isDirty = true;
                }
            }
        }


        private long _FileLengthTotal;
        public long FileLengthTotal
        {
            get { return _FileLengthTotal; }
            set
            {
                if (value != _FileLengthTotal)
                {
                    _FileLengthTotal = value;
                    OnPropertyChanged("FileLengthTotal");

                }
            }
        }


        private long _TimeDone;
        public long TimeDone
        {
            get { return _TimeDone; }
            set
            {
                if (value != _TimeDone)
                {
                    _TimeDone = value;
                    OnPropertyChanged("TimeDone");
                    //  isDirty = true;
                }
            }
        }

        public ListFiles()
        {
            DispatcherObject = Dispatcher.CurrentDispatcher;
            sWatch = new Stopwatch();

            Clear();
        }
        public void SearchFoldersSyncronous(DirectoryInfo dir)
        {

            var files = dir.GetFiles();
            if (files.Length > 0)
            {

                foreach (var file in files)
                {


                    FileCount++;
                    Filelist.Add(file);
                    FileLengthTotal += file.Length;
                    if (MaxFileLength < file.Length)
                    {
                        MaxFileLength = file.Length;
                    }

                    TimeDone = sWatch.ElapsedMilliseconds;

                }

            }

            var dirs = dir.GetDirectories();
            if (dirs.Length > 0)
            {
                foreach (var item in dirs)
                {
                    SearchFoldersSyncronous(item);
                }
            }


        }



        public async void SearchFoldersAsync(DirectoryInfo dir)
        {

            try
            {
                var files = dir.GetFiles();
                if (files.Length > 0)
                {
                    await Task.Run(() =>
                    {
                        foreach (var file in files)
                        {

                            DispatcherObject.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                            {
                                FileCount++;
                                Filelist.Add(file);
                                FileLengthTotal += file.Length;
                                if (MaxFileLength < file.Length)
                                {
                                    MaxFileLength = file.Length;
                                }
                                TimeDone = sWatch.ElapsedMilliseconds;
                            }));


                        }
                    }
      );
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                var x = new FileInfo(dir + "_UnauthorizedAccessException");
                await Task.Run(() =>
                {
                    DispatcherObject.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                    {
                        Filelist.Add(x);
                    }));
                });


            }
            catch (Exception)
            {

                throw;
            }


            try
            {
                var dirs = dir.GetDirectories();
                if (dirs.Length > 0)
                {
                    foreach (var item in dirs)
                    {
                        SearchFoldersAsync(item);
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                var x = new FileInfo(dir + "_UnauthorizedAccessException");
                await Task.Run(() =>
                {
                    DispatcherObject.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                    {
                        Filelist.Add(x);
                    }));
                });


            }
            catch (Exception)
            {

                throw;
            }



        }


        public async Task<GetFilesAsync> GetData(DirectoryInfo folder)
        {
            Clear();
            return await new GetFilesAsync().RunSearch(folder);


        }

        public async void CopyData(DirectoryInfo dir)
        {
            //GetFilesAsync res = await GetData(dir);
            Clear();
            GetFilesAsync res = await new GetFilesAsync().RunSearch(dir);


            Filelist = new ObservableCollection<FileInfo>(res.Filelist);
            FileLengthTotal = res.FileLengthTotal;
            FileCount = res.FileCount;
            MaxFileLength = res.MaxFileLength;
            TimeDone = res.TimeDone;

        }


        public void Clear()
        {
            if (Filelist == null)
            {
                Filelist = new ObservableCollection<FileInfo>();
            }
            if (TempList == null)
            {
                TempList = new List<FileInfo>();
            }
            Filelist.Clear();
            FileCount = 0;
            MaxFileLength = 0;
            FileLengthTotal = 0;
            sWatch.Stop();
            sWatch.Reset();
            TimeDone = 0;

        }

        public void StartWatch()
        {
            Clear();
            sWatch.Start();
        }

        //public void CopyData()
        //{
        //    FileLengthTotal = fLengthTotal;
        //    FileCount = fCount;
        //    MaxFileLength = fLength;
        //    Filelist = new ObservableCollection<FileInfo>(TempList.ToList());

        //}


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
