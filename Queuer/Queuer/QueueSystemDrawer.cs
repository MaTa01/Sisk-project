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
            Grid mainGrid = new Grid();
            int i = 0;
            foreach (MachineDescription desc in machineDescriptions)
            {
                // could have used borders instead of rectangles alternatively
                double rectWidth = 150;
                double rectHeight = 120;
                Rectangle rect = CreateRectangle(rectWidth, rectHeight, desc.CoordinateX,
                    desc.CoordinateY, 255, 255, 255, 0, Brushes.Black);
                Grid grid = new Grid();
                StringBuilder slotProperties = new StringBuilder();
                TextBlock textBlock = new TextBlock();
                foreach (PropertyInfo property in desc.GetType().GetProperties())
                {
                    string line;
                    if (property.PropertyType == typeof(int))
                        line = Convert.ToString(property.GetValue(desc));
                    else if (property.PropertyType == typeof(double))
                        line = Convert.ToString(property.GetValue(desc, null));
                    else
                        line = Convert.ToString(property.GetValue(desc));
                    if (property.Name == "Id")
                        slotProperties.Append(property.Name + ": ");
                    else if (property.Name == "Id")
                        slotProperties.Append(property.Name + ": ");
                    else if (property.Name == "SlotsNumber")
                        slotProperties.Append("Slots number" + ": ");
                    else if (property.Name == "QueueSize")
                        slotProperties.Append("Queue size" + ": ");
                    else if (property.Name == "QueueDiscipline")
                        slotProperties.Append("Queue discipline" + ": ");
                    else if (property.Name == "StreamType")
                        slotProperties.Append("Stream type" + ": ");
                    else if (property.Name == "CoordinateX")
                        slotProperties.Append("Position x" + ": ");
                    else if (property.Name == "CoordinateY")
                        slotProperties.Append("Position y" + ": ");
                    slotProperties.AppendLine(line);
                }
                textBlock.Text = slotProperties.ToString();
                textBlock.Margin = new Thickness(desc.CoordinateX + 5,
                    desc.CoordinateY + 5, 0, 0);
                canvasMQueueSystem.Children.Add(rect);
                canvasMQueueSystem.Children.Add(textBlock);
                i++;
            }
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
    }
}
