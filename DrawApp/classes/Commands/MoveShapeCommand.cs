using DrawApp.classes.Visistors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using static DrawApp.classes.ShapeComponent;

namespace DrawApp.classes
{
    class MoveShapeCommand : Command
    {
        private Point oldLocation;
        private double oldHeight;
        private double oldWidth;
        private Point mousePosition;
        private Point newLocation;
        private double newHeight;
        private double newWidth;
        private ShapeComponent shapeComponent;
        public bool IsCompleted { get; set; }

        public MoveShapeCommand()
        {
            IsCompleted = false;
        }

        public void Execute(InternalCanvas canvas)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed && shapeComponent == null)
            {
                ExecuteMouseDown(canvas);
            }
            else if (Mouse.LeftButton == MouseButtonState.Pressed && shapeComponent != null)
            {
                ExecuteMouseMove(canvas);
            }
            else if (Mouse.LeftButton == MouseButtonState.Released && shapeComponent != null)
            {
                ExecuteMouseUp(canvas);
            }
        }

        private void ExecuteMouseDown(InternalCanvas canvas)
        {
            mousePosition = Mouse.GetPosition(canvas);
            foreach (ShapeComponent canvasshape in canvas.Shapes)
            {
                if (canvasshape.IsMouseOver)
                {
                    canvasshape.MousePosition = canvasshape.GetMousePositionType(mousePosition);
                    shapeComponent = canvasshape;
                    oldLocation = canvas.GetPositionOfShapeInCanvas(shapeComponent);
                    oldHeight = shapeComponent.Height;
                    oldWidth = shapeComponent.Width;
                    shapeComponent.Stroke = Brushes.DarkBlue;
                    break;
                }
            }
            newLocation = canvas.GetPositionOfShapeInCanvas(shapeComponent);
        }

        private void ExecuteMouseMove(InternalCanvas canvas)
        {
            // See how much the mouse has moved.
            Point point = Mouse.GetPosition(canvas);
            double offset_x = point.X - mousePosition.X;
            double offset_y = point.Y - mousePosition.Y;

            // Get the rectangle's current position.
            Point newPosition = GetNewLocationAndSize(canvas, offset_x, offset_y);

            // Don't use negative width or height.
            if ((newWidth > 0) && (newHeight > 0))
            {
                // Update the rectangle.
                shapeComponent.Move(new MoveVisitor(new Point(newPosition.X - newLocation.X, newPosition.Y - newLocation.Y)));
                //canvas.SetNewShape(shapeComponent, newPosition.X, newPosition.Y);
                canvas.SetNewShape(shapeComponent, shapeComponent.Location.X, shapeComponent.Location.Y);
                shapeComponent.Resize(new ResizeVisitor(offset_x, offset_y, shapeComponent.MousePosition));
                //shapeComponent.Width = newWidth;
                //shapeComponent.Height = newHeight;

                // Save the mouse's new location.
                mousePosition = point;
            }
            newLocation = canvas.GetPositionOfShapeInCanvas(shapeComponent);
            shapeComponent.Location = newLocation;
            //SetMouseCursor(shape);
        }

        private void ExecuteMouseUp(InternalCanvas canvas)
        {
            //shape.Selected = false;
            shapeComponent.Stroke = null;
            //Cursor = Cursors.Arrow;
        }

        private double SetTopOfShape(InternalCanvas canvas, ShapeComponent shape, double new_y)
        {
            if (new_y < 0)
            {
                return 0;
            }
            else if (new_y > (canvas.ActualHeight - shape.ActualHeight))
            {
                return canvas.ActualHeight - shape.ActualHeight;
            }
            else
            {
                return new_y;
            }
        }

        private double SetLeftOfShape(InternalCanvas canvas, ShapeComponent shape, double new_x)
        {
            if (new_x < 0)
            {
                return 0;
            }
            else if (new_x > (canvas.ActualWidth - shape.ActualWidth))
            {
                return canvas.ActualWidth - shape.ActualWidth;
            }
            else
            {
                return new_x;
            }
        }

        private Point GetNewLocationAndSize(InternalCanvas canvas, double offset_x, double offset_y)
        {
            double new_x = shapeComponent.Location.X;
            double new_y = shapeComponent.Location.Y;
            newWidth = shapeComponent.ActualWidth;
            newHeight = shapeComponent.ActualHeight;

            // Update the rectangle.
            switch (shapeComponent.MousePosition)
            {
                case MousePositionType.Body:
                    new_x += offset_x;
                    new_y += offset_y;
                    break;
                case MousePositionType.UL:
                    new_x += offset_x;
                    new_y += offset_y;
                    newWidth -= offset_x;
                    newHeight -= offset_y;
                    break;
                case MousePositionType.UR:
                    new_y += offset_y;
                    newWidth += offset_x;
                    newHeight -= offset_y;
                    break;
                case MousePositionType.DR:
                    newWidth += offset_x;
                    newHeight += offset_y;
                    break;
                case MousePositionType.DL:
                    new_x += offset_x;
                    newWidth -= offset_x;
                    newHeight += offset_y;
                    break;
                case MousePositionType.L:
                    new_x += offset_x;
                    newWidth -= offset_x;
                    break;
                case MousePositionType.R:
                    newWidth += offset_x;
                    break;
                case MousePositionType.B:
                    newHeight += offset_y;
                    break;
                case MousePositionType.T:
                    new_y += offset_y;
                    newHeight -= offset_y;
                    break;
            }
            return new Point(SetLeftOfShape(canvas, shapeComponent, new_x), SetTopOfShape(canvas, shapeComponent, new_y));
        }

        public void Undo(InternalCanvas canvas)
        {
            shapeComponent.Width = oldWidth;
            shapeComponent.Height = oldHeight;
            shapeComponent.Location = oldLocation;
            canvas.SetNewShape(shapeComponent, oldLocation.X, oldLocation.Y);
        }

        public void Redo(InternalCanvas canvas)
        {
            shapeComponent.Width = newWidth;
            shapeComponent.Height = newHeight;
            shapeComponent.Location = newLocation;
            canvas.SetNewShape(shapeComponent, newLocation.X, newLocation.Y);
        }

        //private void SetMouseCursor(InternalShape shape)
        //{
        //    // See what cursor we should display.
        //    Cursor desired_cursor = Cursors.Arrow;
        //    switch (shape.MousePosition)
        //    {
        //        case MousePositionType.None:
        //            desired_cursor = Cursors.Arrow;
        //            break;
        //        case MousePositionType.Body:
        //            desired_cursor = Cursors.ScrollAll;
        //            break;
        //        case MousePositionType.UL:
        //        case MousePositionType.DR:
        //            desired_cursor = Cursors.SizeNWSE;
        //            break;
        //        case MousePositionType.DL:
        //        case MousePositionType.UR:
        //            desired_cursor = Cursors.SizeNESW;
        //            break;
        //        case MousePositionType.T:
        //        case MousePositionType.B:
        //            desired_cursor = Cursors.SizeNS;
        //            break;
        //        case MousePositionType.L:
        //        case MousePositionType.R:
        //            desired_cursor = Cursors.SizeWE;
        //            break;
        //    }

        //    // Display the desired cursor.
        //    if (Cursor != desired_cursor) Cursor = desired_cursor;
        //}
    }
}
