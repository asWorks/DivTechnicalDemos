using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleFoldersFilesSort
{
    public class GetFilesAsync
    {
        public ObservableCollection<FileInfo> Filelist { get; set; }
        Stopwatch sWatch;
        private int _FileCount;
        public int FileCount
        {
            get { return _FileCount; }
            set
            {
                if (value != _FileCount)
                {
                    _FileCount = value;

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

                    //  isDirty = true;
                }
            }
        }

        public GetFilesAsync()
        {
            sWatch = new Stopwatch();

            Clear();
        }


        public async Task<GetFilesAsync> RunSearch(DirectoryInfo dir)
        {
            sWatch.Start();
            var x = await Task.Factory.StartNew((p) => SearchFoldersAsync((DirectoryInfo)p), dir);
            sWatch.Stop();
            return this;
        }


        public async Task SearchFoldersAsync(DirectoryInfo dir)
        {



            var files = dir.GetFiles();
            if (files.Length > 0)
            {
                //await Task.Run(() =>
                //{
                foreach (var file in files)
                {
                    //for (double i = 0; i < 100000; i++)
                    //{
                    //    var res = Math.Sqrt(i);
                    //    // Debug.Print(res.ToString());
                    //}

                    FileCount++;
                    Filelist.Add(file);
                    FileLengthTotal += file.Length;
                    if (MaxFileLength < file.Length)
                    {
                        MaxFileLength = file.Length;
                    }
                    TimeDone = sWatch.ElapsedMilliseconds;
                };


            }




            var dirs = dir.GetDirectories();
            if (dirs.Length > 0)
            {
                foreach (var item in dirs)
                {
                    await SearchFoldersAsync(item);
                }
            }


        }

        public void Clear()
        {
            if (Filelist == null)
            {
                Filelist = new ObservableCollection<FileInfo>();
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
    }
}
