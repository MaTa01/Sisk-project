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

namespace Regexer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //// Create OpenFileDialog
            //Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            //fileDialog.DefaultExt = ".txt";
            //fileDialog.Filter = "TXT Files (*.txt)|*.txt";
            //Nullable<bool> selectedFile = fileDialog.ShowDialog();

            //if (selectedFile == true)
            //{
            //    string fileName = fileDialog.FileName;
            //    TextBoxInputFile.Text = fileName;
                
            //}
            setFileName();



        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            setFileName();
        }
        private bool setFileName()
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.DefaultExt = ".txt";
            fileDialog.Filter = "TXT Files (*.txt)|*.txt";
            Nullable<bool> selectedFile = fileDialog.ShowDialog();

            if (selectedFile == true)
            {
                string fileName = fileDialog.FileName;
                TextBoxInputFile.Text = fileName;
                return true;
            }
            else 
            {
                return false;
            }
        }
    }
}
