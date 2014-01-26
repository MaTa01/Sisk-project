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
        public QueueSystemDrawer() {} 
        public QueueSystemDrawer(List<MachineDescription> machineDescriptions, ref Canvas canvasMQueueSystem)
        {
            DrawQueueSystem(machineDescriptions, ref canvasMQueueSystem);
        }

        // draw an entire quueueing system based on given parametres.
        private void DrawQueueSystem(List<MachineDescription> machineDescriptions, ref Canvas canvasMQueueSystem)
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
                TextBlock textBlock = new TextBlock();
                StringBuilder nodeProperties = GetNodeProperties(desc);
                StringBuilder nodeState = new StringBuilder();
                int servicedJobsNumber = 0; // TODO get actual data from QueueSimulator
                int queueSize = 0; // TODO get actual data from QueueSimulator
                nodeState.AppendLine("Queue size: " + queueSize);
                for (int j = 0; j < desc.SlotsNumber; j++)
                {
                    nodeState.AppendLine("Submachine " + (j + 1) + ": " + servicedJobsNumber);
                }
                //nodeState.AppendLine("
                string nodeMessage = nodeProperties.ToString() +
                    "Current node state:\n" + 
                    nodeState.ToString();
                textBlock.Text = nodeMessage;
                textBlock.Margin = new Thickness(desc.CoordinateX + 5,
                    desc.CoordinateY + 5, 0, 0);
                foreach (int route in desc.Routes)
                {
                    Line line = CreateLineWithCentalCoordinates(
                        desc.CoordinateX, desc.CoordinateY,
                        machineDescriptions[route - 1].CoordinateX,
                        machineDescriptions[route - 1].CoordinateY,
                        rectWidth, rectHeight);
                    canvasMQueueSystem.Children.Insert(0, line);
                }
                canvasMQueueSystem.Children.Add(rect);
                canvasMQueueSystem.Children.Add(textBlock);
                i++;
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
                    if (property.Name == "CoordinateX" || property.Name == "CoordinateY")
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
    }
}
