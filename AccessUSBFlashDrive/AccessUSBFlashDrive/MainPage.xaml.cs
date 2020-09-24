using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tizen.System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AccessUSBFlashDrive
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        string INTERNAL_ROOT_DIR;
        string EXTERNAL_ROOT_DIR;
        public MainPage()
        {
            InitializeComponent();
        }

        private void CreateFileBtn_Clicked(object sender, EventArgs e)
        {
            INTERNAL_ROOT_DIR = StorageManager.Storages.First(m => m.StorageType == StorageArea.Internal).RootDirectory;
            //Console.Error.WriteLine("INTERNAL_ROOT_DIR : " + INTERNAL_ROOT_DIR);
            status.Text += "\n\nINTERNAL_ROOT_DIR : " + INTERNAL_ROOT_DIR;
            var pathToImageFolder = StorageManager.Storages.First(m => m.StorageType == StorageArea.Internal).GetAbsolutePath(DirectoryType.Images);
            //Console.Error.WriteLine("Images Dir : " + pathToImageFolder);
            status.Text += "\nImages Dir : " + pathToImageFolder;
            EXTERNAL_ROOT_DIR = StorageManager.Storages.First(m => m.StorageType == StorageArea.External)?.RootDirectory;
            //Console.Error.WriteLine("EXTERNAL_ROOT_DIR : " + EXTERNAL_ROOT_DIR);
            status.Text += "\n\nExternal Dir : " + EXTERNAL_ROOT_DIR;
            
            var fileName = Path.Combine(EXTERNAL_ROOT_DIR, "hello.txt");
            status.Text += "\nfileName : " + fileName +"\n\n";
            try
            {
                // Check if file already exists. If yes, delete it.     
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                // Create a new file     
                using (FileStream fs = File.Create(fileName))
                {
                    // Add some text to file    
                    Byte[] title = new UTF8Encoding(true).GetBytes("New Text File\n\n");
                    fs.Write(title, 0, title.Length);
                    byte[] author = new UTF8Encoding(true).GetBytes("Hello Tizen TV!!\n1234567890\n안녕 타이즌 티비 @(^^)@");
                    fs.Write(author, 0, author.Length);
                }
                status.Text += "\n Let's read the file from USB flash drive..\n\n";
                // Open the stream and read it back.    
                using (StreamReader sr = File.OpenText(fileName))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        //Console.Error.WriteLine(s);
                        status.Text += s +"\n";
                    }
                }
            }
            catch (Exception Ex)
            {
                //Console.Error.WriteLine(Ex.ToString());
                status.Text += "\n Exception occurs";
                status.Text += "\n " + Ex.ToString() + ", " + Ex.Message + ", " + Ex.StackTrace;
            }
        }
    }
}