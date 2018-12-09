using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using static DrawApp.classes.ShapeComponent;

namespace DrawApp.classes.Visistors
{
    public class ResizeVisitor : IVisitor
    {
        public double DiffenceWidth { get; set; }
        public double DiffenceHeight { get; set; }
        private MousePositionType MousePositionType { get; set; } 

        public ResizeVisitor(double diffenceWidth, double differenceHeight, MousePositionType mousePositionType)
        {
            DiffenceHeight = differenceHeight;
            DiffenceWidth = diffenceWidth;
            MousePositionType = mousePositionType;
        }

        public void Visit(InternalShape shape)
        {
            Execute(shape);
        }

        public void Visit(ShapeGroup group)
        {
            //DiffenceWidth *= 2;
            //DiffenceHeight *= 2;
            Execute(group);
        }

        public void Visit(TextDecorator textDecorator)
        {
            Execute(textDecorator);
        }

        public void Execute(ShapeComponent shapeComponent)
        {
            Rect bounds = shapeComponent.GetGeometry().GetRenderBounds(new Pen(), 0, ToleranceType.Absolute);
            double newwidth = 0;
            double newheight = 0;
            switch (MousePositionType)
            {
                case MousePositionType.UL:
                    newwidth = bounds.Width * DiffenceWidth;
                    newheight = bounds.Height * DiffenceHeight;
                    break;
                case MousePositionType.UR:
                    newwidth = bounds.Width * DiffenceWidth;
                    newheight = bounds.Height * DiffenceHeight;
                    break;
                case MousePositionType.DR:
                    newwidth = bounds.Width * DiffenceWidth;
                    newheight = bounds.Height * DiffenceHeight;
                    break;
                case MousePositionType.DL:
                    newwidth = bounds.Width * DiffenceWidth;
                    newheight = bounds.Height * DiffenceHeight;
                    break;
                case MousePositionType.L:
                    newwidth = bounds.Width * DiffenceWidth;
                    break;
                case MousePositionType.R:
                    newwidth = bounds.Width * DiffenceWidth;
                    break;
                case MousePositionType.B:
                    newheight = bounds.Height * DiffenceHeight;
                    break;
                case MousePositionType.T:
                    newheight = bounds.Height * DiffenceHeight;
                    break;
            }
            if (newheight > 0)
            {
                shapeComponent.Height = newheight;
            }
            if (newwidth > 0)
            {
                shapeComponent.Width = newwidth;
            }

            //if (DiffenceHeight >= 0)
            //{
            //    shapeComponent.Height = shapeComponent.ActualHeight * DiffenceHeight;
            //}
            //if (DiffenceWidth >= 0)
            //{
            //    shapeComponent.Width = shapeComponent.ActualWidth * DiffenceWidth;
            //}
        }
    }
}
