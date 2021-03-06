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
        private string inputFileName;
        private bool isAnyInput;


        public MainWindow()
        {
            InitializeComponent();
            inputFileName = "";
            inputFileName = "";
            isAnyInput = false;
        }

        private void ButtonOpenFile_Click(object sender, RoutedEventArgs e)
        {
            inputFileName = getFileName();
            RichTextBoxInput.Document.Blocks.Clear();
            RichTextBoxInput.AppendText(File.ReadAllText(inputFileName));
            TextBoxInputFile.Text = inputFileName;
        }

        private void ButtonSaveAs_Click(object sender, RoutedEventArgs e)
        {
            //outputFileName = getFileName();
            List<string> list = new List<string>();
            list.Add(TextBoxInputFile.Text);
            //outputFileName = SaveAs(list);
        }

        private void ButtonParse_Click(object sender, RoutedEventArgs e)
        {
            //Regex r = new Regex("jabadabadu");
            //string regex = @"-+(\r\n|\n|\r)[\w /]*(\r\n|\n|\r)-+";
            string regex = TextBoxRegex.Text;
            Regex r = new Regex(regex);
            RichTextBoxOutput.AppendText(StringFromRichTextBox(RichTextBoxInput));
            //parseInput();
            //parseFile(inputFileName, r);
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
        private List<string> ReadFile(string fileName, Regex reg)
        {
            List<string> list = new List<string>();
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

        private string SaveAs(IEnumerable<string> contents)
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


        /// <summary>
        /// Handles the LostFocus event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void TextFieldLostFocus(object sender, RoutedEventArgs e)
        {
            if (isRichTextBoxEmpty(RichTextBoxInput))
            {
                Brush brush = new SolidColorBrush(Colors.Black);
                brush.Opacity = 0.50;
                RichTextBoxInput.Document.Foreground = brush;
                RichTextBoxInput.AppendText("Input text");
                isAnyInput = false;
            }
            else
            {
                isAnyInput = true;
            }
        }

        /// <summary>
        /// Handles the GotFocus event of the RichTextBoxInput control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void RichTextBoxInput_GotFocus (object sender, RoutedEventArgs e)
        {
            if(!isAnyInput)
                RichTextBoxInput.Document.Blocks.Clear();
                Brush brush = new SolidColorBrush(Colors.Black);
                brush.Opacity = 1.00;
                RichTextBoxInput.Document.Foreground = brush;
        }

        /// <summary>
        /// Determines whether the specified rich text box is empty.
        /// </summary>
        /// <param name="rtb">The RichTextBox.</param>
        /// <returns></returns>
        private bool isRichTextBoxEmpty(RichTextBox rtb)
        {
            if (rtb.Document.Blocks.Count == 0) 
                return true;
            TextPointer startPointer = rtb.Document.ContentStart.GetNextInsertionPosition(LogicalDirection.Forward);
            TextPointer endPointer = rtb.Document.ContentEnd.GetNextInsertionPosition(LogicalDirection.Backward);
            return startPointer.CompareTo(endPointer) == 0;
        }

        /// <summary>
        /// String from rich text box.
        /// </summary>
        /// <param name="rtb">The RTB.</param>
        /// <returns></returns>
        string StringFromRichTextBox(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(
                // TextPointer to the start of content in the RichTextBox.
                rtb.Document.ContentStart, 
                // TextPointer to the end of content in the RichTextBox.
                rtb.Document.ContentEnd
            );

            // The Text property on a TextRange object returns a string 
            // representing the plain text content of the TextRange. 
            return textRange.Text;
        }

    }
}
