using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MultipleFoldersFilesSort
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ListFiles liste;
        public MainWindow()
        {
            InitializeComponent();
            liste = new ListFiles();
            this.DataContext = liste;

        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            liste.Clear();
        }

        private void LookUp_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                DirectoryInfo dir = new DirectoryInfo(dialog.SelectedPath.ToString());
                liste.StartWatch();
                liste.SearchFoldersSyncronous(dir);

            }

            // liste.CopyData();
            
        }

        private void LookUpAsync_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                DirectoryInfo dir = new DirectoryInfo(dialog.SelectedPath.ToString());
                liste.CopyData(dir);

            }
        }




        private void LookUpAsyncOld_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                DirectoryInfo dir = new DirectoryInfo(dialog.SelectedPath.ToString());
                liste.StartWatch();
                liste.SearchFoldersAsync(dir);

            }
        }

        private void TestAnonymus_Click(object sender, RoutedEventArgs e)
        {
            string[] TheNames = { "Fritz", "Kurt", "Hans", "Holger", "Frank", "Klaus" };

            textBlock.Text = AnonymusTest.ShowNamesNew<string>(TheNames, "Horst", "Gibt es den Namen?", (f, c, k) => {

                string buffer = "";
                foreach (var item in f)
                {
                    buffer += item;
                }
                c = buffer;
                return buffer; });


            int res = AnonymusTest.CalcGenT1<string, int>("Barmixer", "Bargeld", (p1, p2) =>
            {
                int i = 0;
                while (p1[i] == p2[i])
                    i++;
                return i;
            });


            textBlock.Text += res.ToString();

            string res1 = AnonymusTest.CalcGenT1<string,string>("Barmixer", "Bargeld",(p1,p2)=>
            {
                int i = 0;
                while (p1[i] == p2[i])
                    i++;
                
                return p1.Substring(0,i);
            });

            textBlock.Text += res1;
        }

        private void LookUpAsyncYield_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                DirectoryInfo dir = new DirectoryInfo(dialog.SelectedPath.ToString());
                liste.StartWatch();
                liste.SearchFoldersAsync(dir);

            }
        }
    }
}
