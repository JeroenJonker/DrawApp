using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawApp.classes.Commands
{
    class AddTextCommand : Command
    {
        public bool IsCompleted { get; set; }
        public TextDecorator TextOrnament { get; set; }
        public ShapeComponent OldShapeComponent { get; set; }

        public AddTextCommand()
        {
            IsCompleted = false;
        }

        public void Execute(InternalCanvas canvas)
        {
            foreach (ShapeComponent canvasshape in canvas.Shapes)
            {
                if (canvasshape.IsMouseOver)
                {
                    OldShapeComponent = canvasshape;
                    if (!(canvasshape is TextDecorator))
                    {
                        AddTextDecorator(canvas, canvasshape);
                    }
                    else if (canvasshape is TextDecorator textshape)
                    {
                        ModifyTextDecorator(textshape);
                    }
                    IsCompleted = true;
                    break;
                }
            }
        }

        private static void ModifyTextDecorator(TextDecorator textshape)
        {
            InputDialog inputDialog = new InputDialog(textshape.Texts);
            if (inputDialog.ShowDialog() == true)
            {
                textshape.Texts = inputDialog.Texts;
            }
        }

        private void AddTextDecorator(InternalCanvas canvas, ShapeComponent canvasshape)
        {
            InputDialog inputDialog = new InputDialog();
            if (inputDialog.ShowDialog() == true)
            {
                TextOrnament = new TextDecorator(canvasshape, inputDialog.Texts);

                canvas.Shapes.Remove(canvasshape);
                canvas.Children.Remove(canvasshape);

                canvas.Shapes.Add(TextOrnament);
                canvas.Children.Add(TextOrnament);
                canvas.SetNewShape(TextOrnament, TextOrnament.ShapeComponent.Location.X, TextOrnament.ShapeComponent.Location.Y);
            }
        }


        public void Undo(InternalCanvas canvas)
        {
            canvas.Shapes.Remove(TextOrnament);
            canvas.Children.Remove(TextOrnament);
            canvas.Shapes.Add(OldShapeComponent);
            canvas.Children.Add(OldShapeComponent);
        }

        public void Redo(InternalCanvas canvas)
        {
            canvas.Shapes.Add(TextOrnament);
            canvas.Children.Add(TextOrnament);
            canvas.Shapes.Remove(OldShapeComponent);
            canvas.Children.Remove(OldShapeComponent);
        }
    }
}
