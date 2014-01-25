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

namespace Queuer
{
    class QueueSystemDrawer
    {
        public QueueSystemDrawer(List<MachineDescription> machineDescriptions, ref Canvas canvasMQueueSystem)
        {
            DrawQueueSystem(machineDescriptions, ref canvasMQueueSystem);
        }

        // draw an entire quueueing system based on given parametres.
        private void DrawQueueSystem(List<MachineDescription> machineDescriptions, ref Canvas canvasMQueueSystem)
        {
            List<Rectangle> rectangles = new List<Rectangle>();
            foreach (MachineDescription desc in machineDescriptions)
            {
                Rectangle myRect = CreateRectangle(200, 100, desc.CoordinateX, desc.CoordinateY, 255, 255, 255, 0, Brushes.Black);
                rectangles.Add(myRect);
            }
            foreach (Rectangle rect in rectangles)
            {
                canvasMQueueSystem.Children.Add(rect);
            }
        }

        private Rectangle CreateRectangle(double width, double height, double xCentre, double yCentre, byte a, byte r, byte g, byte b, Brush outlineBrush)
        {
            // parse centre position coordinates to left top corner position
            Rectangle rect = new Rectangle { Width = width, Height = height};
            double xLeft = xCentre - (width / 2);
            double yTop = yCentre - (height / 2);
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
