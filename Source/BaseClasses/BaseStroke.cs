using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;

namespace TheRippler.Source.BaseClasses {
    public class BaseStroke : Stroke {

        public BaseStroke(StylusPointCollection collection) : base(collection) {
            this.StylusPoints = collection;
        }

        protected override void DrawCore(DrawingContext context, DrawingAttributes attributes) {
            if (context == null) {

            }
            if (attributes == null) {

            }
            DrawingAttributes originalData = attributes.Clone();
            SolidColorBrush brush = new SolidColorBrush(attributes.Color);
            brush.Freeze();

            StylusPoint firstPoint = StylusPoints[0];
            StylusPoint secondPoint = StylusPoints[1];
            double radius = System.Math.Sqrt(System.Math.Pow((double)secondPoint.X - firstPoint.X, 2) - Math.Pow((double)secondPoint.Y - firstPoint.Y, 2)) / 2.0;
            context.DrawEllipse(brush, null, new System.Windows.Point((secondPoint.X + firstPoint.X) / 2.0, (secondPoint.Y + firstPoint.Y) / 2.0), radius, radius);
        }
    }
}
