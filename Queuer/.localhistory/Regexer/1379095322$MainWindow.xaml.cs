#region Imports (17)

using Microsoft.Win32;
using System;
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

#endregion Imports (17)

namespace Regemon
{


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Enums of MainWindow (6)

        SaveFileDialog fileDialog;
        private string inputFileName;
        private bool isAnyInput;
        private bool isAnyOutput;
        private bool isAnyRegex;
        Nullable<bool> selectedFile;

        #endregion Enums of MainWindow (6)

        #region Constructors of MainWindow (1)

        public MainWindow()
        {
            InitializeComponent();
            inputFileName = "";
            isAnyRegex = false;
            isAnyInput = false;
            isAnyOutput = false;
            selectedFile = false;
            fileDialog = null;
        }

        #endregion Constructors of MainWindow (1)

        #region Methods of MainWindow (20)

        private void ButtonOpenFile_Click(object sender, RoutedEventArgs e)
        {
            inputFileName = getFileName();
            RichTextBoxInput.Document.Blocks.Clear();
            RichTextBoxInput.AppendText(File.ReadAllText(inputFileName));
            isAnyInput = true;
        }

        /// <summary>
        /// Handles the Click event of the ButtonParse control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonParse_Click(object sender, RoutedEventArgs e)
        {
            //string regex = @"-+(\r\n|\n|\r)[\w /]*(\r\n|\n|\r)-+";

            Regex regex = new Regex(TextBoxRegex.Text);
            foreach (Match match in regex.Matches(StringFromRichTextBox(RichTextBoxInput)))
                RichTextBoxOutput.AppendText(match.Value + "\n");
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (selectedFile == true)
                Save(fileDialog);
            else
                SaveAs();
        }

        private void ButtonSaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveAs();
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

        private void RichTextBoxGotFocus(RichTextBox rtb, ref bool isAnyContent)
        {
            if (!isAnyContent)
                rtb.Document.Blocks.Clear();
            Brush brush = new SolidColorBrush(Colors.Black);
            brush.Opacity = 1.00;
            rtb.Document.Foreground = brush;
        }

        /// <summary>
        /// Handles the GotFocus event of the RichTextBoxInput control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void RichTextBoxInput_GotFocus(object sender, RoutedEventArgs e)
        {
            RichTextBoxGotFocus(RichTextBoxInput, ref isAnyInput);
        }

        /// <summary>
        /// Handles the LostFocus event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void RichTextBoxInput_LostFocus(object sender, RoutedEventArgs e)
        {
            RichTextBoxLostFocus(RichTextBoxInput, out isAnyInput, "Text input");
        }

        private void RichTextBoxLostFocus(RichTextBox rtb, out bool isAnyContent, string msg)
        {
            if (isRichTextBoxEmpty(rtb))
            {
                Brush brush = new SolidColorBrush(Colors.Black);
                brush.Opacity = 0.50;
                rtb.Document.Foreground = brush;
                rtb.AppendText(msg);
                isAnyContent = false;
            }
            else
            {
                isAnyContent = true;
            }
        }

        /// <summary>
        /// Handles the GotFocus event of the RichTextBoxOutput control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void RichTextBoxOutput_GotFocus(object sender, RoutedEventArgs e)
        {
            RichTextBoxGotFocus(RichTextBoxOutput, ref isAnyOutput);
        }

        /// <summary>
        /// Handles the LostFocus event of the RichTextBoxOutput control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void RichTextBoxOutput_LostFocus(object sender, RoutedEventArgs e)
        {
            RichTextBoxLostFocus(RichTextBoxOutput, out isAnyOutput, "Text output");
        }

        private void Save(SaveFileDialog fileDialog)
        {
            string name = fileDialog.FileName;
            File.WriteAllText(name, StringFromRichTextBox(RichTextBoxOutput));
        }

        private bool SaveAs()
        {
            List<string> list = new List<string>();
            fileDialog = new SaveFileDialog();
            fileDialog.DefaultExt = ".txt";
            fileDialog.Filter = "TXT Files (*.txt)|*.txt";

            selectedFile = fileDialog.ShowDialog();
            if (selectedFile == true)
            {
                Save(fileDialog);
                return true;
            }
            else
            {
                return false;
            }
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

        private void TextBoxGotFocus(TextBox tb, ref bool isAnyContent)
        {
            if (!isAnyContent)
                tb.Clear();
            Brush brush = new SolidColorBrush(Colors.Black);
            brush.Opacity = 1.00;
            tb.Foreground = brush;
        }

        private void TextBoxLostFocus(TextBox tb, out bool isAnyContent, string msg)
        {
            if (string.IsNullOrEmpty(tb.Text))
            {
                Brush brush = new SolidColorBrush(Colors.Black);
                brush.Opacity = 0.50;
                tb.Foreground = brush;
                tb.AppendText(msg);
                isAnyContent = false;
            }
            else
            {
                isAnyContent = true;
            }
        }

        private void TextBoxRegex_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxGotFocus(TextBoxRegex, ref isAnyRegex);
        }

        private void TextBoxRegex_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBoxLostFocus(TextBoxRegex, out isAnyRegex, "Enter a regular expression");
        }

        #endregion Methods of MainWindow (20)
    }
}
