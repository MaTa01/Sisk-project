using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows;
using System.Reflection;

namespace Queuer
{
    class QueueSystemDrawer
    {
        public QueueSystemDrawer() { }
        public QueueSystemDrawer(List<MachineDescription> machineDescriptions, ref Canvas canvasMQueueSystem, bool inputError)
        {
            DrawQueueSystem(machineDescriptions, ref canvasMQueueSystem, inputError);
        }

        // draw an entire quueueing system based on given parametres.
        private void DrawQueueSystem(List<MachineDescription> machineDescriptions, ref Canvas canvasMQueueSystem, bool inputError)
        {
            if (inputError)
            {
                ShowErrorMessage(ref canvasMQueueSystem);
            }
            else
            {
                int i = 0;
                foreach (MachineDescription desc in machineDescriptions)
                {
                    // could have used borders instead of rectangles alternatively
                    double rectWidth = 150;
                    double rectHeight = 140;
                    for (int j = 0; j < desc.SlotsNumber; j++)
                        rectHeight += 16;

                    Rectangle rect = CreateRectangle(rectWidth, rectHeight, desc.CoordinateX,
                        desc.CoordinateY, 255, 255, 255, 0, Brushes.Black);
                    Grid grid = new Grid();
                    StringBuilder nodeProperties = GetNodeProperties(desc);
                    StringBuilder nodeState = new StringBuilder();
                    int servicedJobsNumber = 0; // TODO get actual data from QueueSimulator
                    int queueSize = 0; // TODO get actual data from QueueSimulator
                    nodeState.AppendLine("Queue size: " + queueSize);
                    for (int j = 0; j < desc.SlotsNumber; j++)
                    {
                        nodeState.AppendLine("Submachine " + (j + 1) + ": " + servicedJobsNumber);
                    }
                    string nodeMessage = nodeProperties.ToString() +
                        "Current node state:\n" +
                        nodeState.ToString();
                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = nodeMessage;
                    textBlock.Margin = new Thickness(desc.CoordinateX + 5,
                        desc.CoordinateY + 5, 0, 0);
                    foreach (Route route in desc.Routes)
                    {
                        TextBlock textBlockProbability = new TextBlock();
                        Line line = CreateLineWithCentalCoordinates(
                            desc.CoordinateX, desc.CoordinateY,
                            machineDescriptions[route.Destination - 1].CoordinateX,
                            machineDescriptions[route.Destination - 1].CoordinateY,
                            rectWidth, rectHeight);
                        canvasMQueueSystem.Children.Insert(0, line);
                        textBlockProbability.Margin = new Thickness(desc.CoordinateX +
                            (machineDescriptions[route.Destination - 1].CoordinateX - desc.CoordinateX) / 2 + rectWidth / 2 + 10,
                            desc.CoordinateY + (machineDescriptions[route.Destination - 1].CoordinateY - desc.CoordinateY) / 2 + rectHeight / 2 + 10,
                            0, 0);
                        textBlockProbability.Text = route.Probability.ToString();
                        canvasMQueueSystem.Children.Add(textBlockProbability);
                    }
                    canvasMQueueSystem.Children.Add(rect);
                    canvasMQueueSystem.Children.Add(textBlock);
                    i++;
                }
            }
        }

        private StringBuilder GetNodeProperties(MachineDescription desc)
        {
            StringBuilder slotProperties = new StringBuilder();
            foreach (PropertyInfo property in desc.GetType().GetProperties())
            {
                string line;
                if (property.PropertyType == typeof(int))
                {
                    if (property.Name == "CoordinateX" || 
                        property.Name == "CoordinateY" ||
                        property.Name == "NodeType")
                        continue;
                    line = Convert.ToString(property.GetValue(desc));
                }
                else if (property.PropertyType == typeof(double))
                    line = Convert.ToString(property.GetValue(desc, null));
                else if (property.PropertyType == typeof(string))
                    line = Convert.ToString(property.GetValue(desc));
                else
                    line = "";
                if (property.Name == "Id")
                    slotProperties.Append(property.Name + ": ");
                else if (property.Name == "Id")
                    slotProperties.Append(property.Name + ": ");
                else if (property.Name == "SlotsNumber")
                    slotProperties.Append("Slots number" + ": ");
                else if (property.Name == "QueueSize")
                    slotProperties.Append("Queue size" + ": ");
                else if (property.Name == "ServiceDiscipline")
                    slotProperties.Append("Service discipline" + ": ");
                else if (property.Name == "ServiceType")
                    slotProperties.Append("Service type" + ": ");
                slotProperties.AppendLine(line);
            }
            return slotProperties;
        }

        private Rectangle CreateRectangle(double width, double height, double xLeft, double yTop, byte a, byte r, byte g, byte b, Brush outlineBrush)
        {
            Rectangle rect = new Rectangle { Width = width, Height = height };
            rect.Margin = new Thickness(xLeft, yTop, 0, 0);
            // set colours
            SolidColorBrush myRectangleBrush = new SolidColorBrush();
            myRectangleBrush.Color = Color.FromArgb(a, r, g, b);
            rect.Fill = myRectangleBrush;
            rect.StrokeThickness = 2;
            rect.Stroke = outlineBrush;
            return rect;
        }

        public Line CreateLine(double centreX, double centreY, double centreXDest, double centreYDest)
        {
            Line line = new Line();
            line.X1 = centreX;
            line.X2 = centreXDest;
            line.Y1 = centreY;
            line.Y2 = centreYDest;
            line.HorizontalAlignment = HorizontalAlignment.Left;
            line.VerticalAlignment = VerticalAlignment.Center;
            line.StrokeThickness = 2;
            line.Stroke = new SolidColorBrush(Colors.Black);
            return line;
        }

        public Line CreateLineWithCentalCoordinates(double coordinateX, double coordinateY, double coordinateXDest, double coordinateYDest, double rectWidth, double rectHeight)
        {
            double centreX = coordinateX + rectWidth / 2.0;
            double centreY = coordinateY + rectHeight / 2.0;
            double centreXDest = coordinateXDest + rectWidth / 2.0;
            double centreYDest = coordinateYDest + rectHeight / 2.0;
            return CreateLine(centreX, centreY, centreXDest, centreYDest);
        }
        private void ShowErrorMessage(ref Canvas canvasMQueueSystem)
        {

            TextBlock textBlock = new TextBlock();
            textBlock.Text = "Parsing a queue network configuration has failed.\nPlease validate the network configuration.";
            canvasMQueueSystem.Children.Add(textBlock);
            Canvas.SetLeft(textBlock, canvasMQueueSystem.Width / 2 - 150); 
            Canvas.SetTop(textBlock, canvasMQueueSystem.Height / 2); 
        }
    }
}