﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        private void ButtonInput_Click(object sender, RoutedEventArgs e)
        {
            TextBoxInputFile.Text = getFileName();
        }

        private void ButtonOutput_Click(object sender, RoutedEventArgs e)
        {
            TextBoxOutputFile.Text = getFileName();
        }

        private void ButtonParse_Click(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <returns>a file name</returns>
        private string getFileName()
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.DefaultExt = ".txt";
            fileDialog.Filter = "TXT Files (*.txt)|*.txt";
            Nullable<bool> selectedFile = fileDialog.ShowDialog();

            if (selectedFile == true)
            {
                return fileDialog.FileName;
            }
            else 
            {
                return "";
            }
        }

        private List<string> parseFile(string fileName, Regex reg)
        {
            List<String> list = new List<String>();
            using (StreamReader r = new StreamReader(fileName))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {

                    list.Add(line);
                }

            }
        }
    }
}
