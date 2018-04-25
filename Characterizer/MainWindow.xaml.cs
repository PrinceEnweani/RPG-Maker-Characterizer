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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Characterizer
{

  
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///
    public partial class MainWindow : Window
    {
        //Keeps track of the windows being open to avoid multiple
       // static Boolean isOpen = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow x = new MainWindow();
            Resizer a = new Resizer();
          
            this.Close();

            a.Show();


        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This section is coming soon.");
        }

        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This section is coming soon.");
        }
    }
}
