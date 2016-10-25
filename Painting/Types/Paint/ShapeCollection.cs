﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;

namespace Painting.Types.Paint
{
    public class ShapeCollection : Shape
    {
        private Coordinate _position;

        private Coordinate _size;

        public ShapeCollection(List<Shape> shapes)
            : base(
                shapes.Select(shape => shape.Position).Min(),
                shapes.Select(shape => shape.Size).Max(),
                shapes.FirstOrDefault(shape => shape.MainColour.Visible)?.MainColour)

        {
            Shapes = new ObservableCollection<Shape>(shapes);
        }

        public ObservableCollection<Shape> Shapes { get; set; }

        public override Coordinate Position
        {
            get { return _position; }
            set
            {
                if ((Shapes != null) && (_position != null))
                    foreach (var shape in Shapes)
                        shape.Position = shape.Position.Add(value.Sub(_position));
                _position = value;
            }
        }

        public override Coordinate Size
        {
            get { return _size; }
            set
            {
                if ((Shapes != null) && (_size != null))
                    foreach (var shape in Shapes)
                        shape.Size = shape.Size.Add(value.Sub(_size));
                _size = value;
            }
        }

        public void Paint(Graphics p)
        {
            foreach (var shape in Shapes.Reverse())
            {
                (shape as Ellipse)?.Paint(p);
                (shape as DefinedPolygon)?.Paint(p);
                (shape as DefinedShape)?.Paint(p);
                (shape as Line)?.Paint(p);
                (shape as Polygon)?.Paint(p);
                (shape as Rectangle)?.Paint(p);
            }
        }
    }
}