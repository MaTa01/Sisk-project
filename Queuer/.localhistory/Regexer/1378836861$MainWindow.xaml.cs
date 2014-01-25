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
        private String inputFileName;
        private String outputFileName;

        public MainWindow()
        {
            InitializeComponent();
            inputFileName = "";
            inputFileName = "";
        }

        private void ButtonInput_Click(object sender, RoutedEventArgs e)
        {
            inputFileName = getFileName();
            TextBoxInputFile.Text = inputFileName;
        }

        private void ButtonSaveAs_Click(object sender, RoutedEventArgs e)
        {
            //outputFileName = getFileName();
            List<String> list = new List<string>();
            list.Add(TextBoxInput.Text);
            outputFileName = SaveAs(list);
        }

        private void ButtonParse_Click(object sender, RoutedEventArgs e)
        {
            //Regex r = new Regex("jabadabadu");
            Regex r = new Regex(@"-+(\r\n|\n|\r)[\w /]*(\r\n|\n|\r)-+");
            parseFile(inputFileName, r);
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

        /// <summary>
        /// Parses the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="reg">The regular expression.</param>
        /// <returns></returns>
        private List<string> parseFile(string fileName, Regex reg)
        {
            List<String> list = new List<String>();
            using (StreamReader r = new StreamReader(fileName))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    Match m = reg.Match(line);
                    if (m.Success)
                    {
                        list.Add(line);
                    }
                }
            }
            return list;
        }

        private String SaveAs(IEnumerable<string> contents)
        {
            Microsoft.Win32.SaveFileDialog fileDialog = new Microsoft.Win32.SaveFileDialog();
            fileDialog.DefaultExt = ".txt";
            fileDialog.Filter = "TXT Files (*.txt)|*.txt";

            Nullable<bool> selectedFile = fileDialog.ShowDialog();

            if (selectedFile == true)
            {
                string name = fileDialog.FileName;
                File.WriteAllLines(name, contents);
                return name;
            }
            else 
            {
                return "";
            }
        }

    }
}
