using DrawApp.classes.Visistors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrawApp.classes
{
    public abstract class ShapeComponent : Shape
    {
        //public abstract void Add(ShapeComponent component);
        //public abstract void Remove(ShapeComponent component);
        //public abstract bool Contains(ShapeComponent component);
        public abstract void Save(SaveVisitor visitor);
        public abstract void Move(MoveVisitor visitor);
        public abstract void Resize(ResizeVisitor visitor);
        public abstract Geometry GetGeometry(double x = 0, double y = 0, double width = 5, double height = 5);
        public enum MousePositionType
        {
            None, Body, UL, UR, DR, DL, R, L, T, B
        };

        public abstract Geometry GetGeometry();

        public MousePositionType MousePosition { get; set; }
        public Point Location { get; set; }

        public MousePositionType GetMousePositionType(Point point)
        {
            double left = Location.X;
            double top = Location.Y;
            double right = left + ActualWidth;
            double bottom = top + ActualHeight;
            if (point.X < left || point.X > right || point.Y < top || point.Y > bottom) return MousePositionType.None;

            const double GAP = 10;
            if (point.X - left < GAP)
            {
                // Left edge.
                if (point.Y - top < GAP) return MousePositionType.UL;
                if (bottom - point.Y < GAP) return MousePositionType.DL;
                return MousePositionType.L;
            }
            else if (right - point.X < GAP)
            {
                // Right edge.
                if (point.Y - top < GAP) return MousePositionType.UR;
                if (bottom - point.Y < GAP) return MousePositionType.DR;
                return MousePositionType.R;
            }
            if (point.Y - top < GAP) return MousePositionType.T;
            if (bottom - point.Y < GAP) return MousePositionType.B;
            return MousePositionType.Body;
        }
    }
}
