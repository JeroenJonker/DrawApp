using DrawApp.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawApp
{
    public class SaveVisitor : IVisitor
    {
        public StringBuilder stringBuilder = new StringBuilder();
        public string Spaces { get; set; }

        public void Visit(ShapeGroup group)
        {
            string line = string.Format("{0}group {1}", Spaces, group.Shapes.Count());
            stringBuilder.AppendLine(line);
        }

        public void Visit(InternalShape shape)
        {
            string line = string.Format("{0}{1} {2} {3} {4} {5}", Spaces, shape.GetName(), shape.Location.X, shape.Location.Y, shape.Width, shape.Height);
            stringBuilder.AppendLine(line);
        }

        public void Visit(TextDecorator textDecorator)
        {
            string line = ""; 
            for (int x = 0; x < textDecorator.Texts.Count; x++)
            {
                if (!string.IsNullOrEmpty(textDecorator.Texts[x]))
                {
                    string position = "";
                    switch (x)
                    {
                        case 0:
                            {
                                position = "top";
                                break;
                            }
                        case 1:
                            {
                                position = "right";
                                break;
                            }
                        case 2:
                            {
                                position = "bottom";
                                break;
                            }
                        case 3:
                            {
                                position = "left";
                                break;
                            }
                    }
                    line = string.Format("{0}{1} {2} \"{3}\"", Spaces, "ornament", position, textDecorator.Texts[x]);
                    stringBuilder.AppendLine(line);
                }
            }
        }
    }
}
