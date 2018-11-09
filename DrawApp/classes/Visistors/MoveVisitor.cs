using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DrawApp.classes.Visistors
{
    public class MoveVisitor : IVisitor
    {
        public Point DifferenceInPosition { get; set; }

        public MoveVisitor(Point differenceInPosition)
        {
            this.DifferenceInPosition = differenceInPosition;
        }

        public void Visit(InternalShape shape)
        {
            Execute(shape);
        }

        public void Visit(ShapeGroup group)
        {
            Execute(group);
        }

        public void Visit(TextDecorator textDecorator)
        {
            Execute(textDecorator);
        }

        private void Execute(ShapeComponent shapeComponent)
        {
            //InternalCanvas canvas = InternalCanvas.getInstance();
            //Point oldLocation = canvas.GetPositionOfShapeInCanvas(shapeComponent);
            Point oldLocation = shapeComponent.Location;
            Point newlocation = new Point(oldLocation.X + DifferenceInPosition.X, oldLocation.Y + DifferenceInPosition.Y);
            //canvas.SetNewShape(shapeComponent, newlocation.X, newlocation.Y);
            shapeComponent.Location = newlocation;
        }
    }
}
