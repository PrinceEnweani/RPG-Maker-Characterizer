using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Windows.Forms;
using System.Windows.Navigation;

namespace Characterizer
{
    /// <summary>
    /// Interaction logic for Resizer.xaml
    /// </summary>
    public partial class Resizer : Window, IDisposable
    {
        
        public static string randomName()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[5];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            var res = new string(stringChars);
            return res;
        }
        public Resizer()
        {
            InitializeComponent();
            inputDirectory.Text = "";
            outputDirectory.Text = "";
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow x = new MainWindow();
            x.Show();
            this.Close();
        }

        private void SingleFile_Checked(object sender, RoutedEventArgs e)
        {
            if (MultipleFile.IsChecked == true)
            {
                MultipleFile.IsChecked = false;
                inputDirectory.Text = "";
                outputDirectory.Text = "";
            }
        }

        private void MultipleFile_Checked(object sender, RoutedEventArgs e)
        {
            if (SingleFile.IsChecked == true)
            {
                SingleFile.IsChecked = false;
                inputDirectory.Text = "";
                outputDirectory.Text = "";
            }
        }

        private void browse_Click(object sender, RoutedEventArgs e)
        {
            if (SingleFile.IsChecked == false && MultipleFile.IsChecked == false)
            {
                System.Windows.MessageBox.Show("Hey bud, check either single or multiple files.");
            }
            else if (SingleFile.IsChecked == true)
            {

                //This creates the openfile dialog
                Microsoft.Win32.OpenFileDialog fileSearch = new Microsoft.Win32.OpenFileDialog();

                fileSearch.Title = "Choose Image to Convert";
                //Here we set the filet
                fileSearch.DefaultExt = ".png";
                fileSearch.Filter = "PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg";

                //Display the opendialog by using the show dialog
                Nullable<bool> result = fileSearch.ShowDialog();

                //Here we get the selected name
                if (result == true)
                {
                    //open the file
                    string name = fileSearch.FileName;
                    inputDirectory.Text = name;
                }
                // Uri x = new Uri(outputDirectory.Text);
                ImageSource ims = new BitmapImage(new Uri(inputDirectory.Text, UriKind.RelativeOrAbsolute));
                previewBox.Source = ims;
            }else if(MultipleFile.IsChecked == true)
            {
                using (var folderDlg = new FolderBrowserDialog())
                {
                    if(folderDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        inputDirectory.Text = folderDlg.SelectedPath;
                    }
                }
            }
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void outputDirectory_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void browseOutput_Click(object sender, RoutedEventArgs e)
        {
            using (var folderDlg = new FolderBrowserDialog())
            {
                if (folderDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string dir = folderDlg.SelectedPath;
                    outputDirectory.Text = dir;
                }
            }
        }

        private void ResizeBtn_Click(object sender, RoutedEventArgs e)
        {
           
            if (SingleFile.IsChecked == false && MultipleFile.IsChecked == false)
            {
                System.Windows.MessageBox.Show("Hey bud, check either single or multiple files", "Characterizer");
            }
            else if(SingleFile.IsChecked == true)
            {
                ImageCrop(inputDirectory.Text, outputDirectory.Text);
            }else if (MultipleFile.IsChecked == true)
            {
             ImageCropMultiple(inputDirectory.Text , outputDirectory.Text);
            }
        }

        public void ImageCrop(string path , string outputPath)
        {
            double multiplier = 1.5;
            // int wid = 96;
            // int hei = 192;
            //Use a rectangle to make the new image the appropriate size
            Bitmap srcImg = System.Drawing.Image.FromFile(inputDirectory.Text) as Bitmap;
            int wid = (int)(srcImg.Width - (srcImg.Width * 0.25));
            int hei = srcImg.Height;
            System.Drawing.Rectangle cropRect = new System.Drawing.Rectangle(32 , 0, wid , hei);
            Bitmap target = new Bitmap(cropRect.Width , cropRect.Height);
            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(srcImg, new System.Drawing.Rectangle(0 , 0, target.Width , target.Height), cropRect, GraphicsUnit.Pixel);
                Bitmap resized = new Bitmap(target , new System.Drawing.Size((int)(target.Width * multiplier) , (int)(target.Height * multiplier)));
                resized.Save(outputPath+"\\" + "$" + randomName() + ".png",ImageFormat.Png);
            }
            
        }
        public void ImageCropMultiple(string path, string outputPath)
        {
            path = inputDirectory.Text;
            string[] files = Directory.GetFiles(path);
            double multiplier = 1.5;
            // int wid = 96;
            // int hei = 192;
            //Use a rectangle to make the new image the appropriate size
            for (int i = 0; i < files.Length; i++)
            {
                Bitmap srcImg = System.Drawing.Image.FromFile(files[i]) as Bitmap;
                int wid = (int)(srcImg.Width - (srcImg.Width * 0.25));
                int hei = srcImg.Height;
                System.Drawing.Rectangle cropRect = new System.Drawing.Rectangle(32, 0, wid, hei);
                Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);
                using (Graphics g = Graphics.FromImage(target))
                {
                    g.DrawImage(srcImg, new System.Drawing.Rectangle(0, 0, target.Width, target.Height), cropRect, GraphicsUnit.Pixel);
                    Bitmap resized = new Bitmap(target, new System.Drawing.Size((int)(target.Width * multiplier), (int)(target.Height * multiplier)));
                    resized.Save(outputPath + "\\" + "$" + randomName() + ".png", ImageFormat.Png);
                }

            }
           

        }

        void IDisposable.Dispose()
        {
            
        }
    }
}
