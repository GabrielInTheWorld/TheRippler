using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TheRippler.Source.BaseClasses;
using TheRippler.Source.Data;
using TheRippler.Source.Models;

namespace TheRippler.Source.Controls {
    /// <summary>
    /// Interaction logic for Plane.xaml
    /// </summary>
    public partial class Plane : UserControl, INotifyPropertyChanged {

        /**
         * @deprecated
         */
        public static readonly DependencyProperty SelectedToolProperty = DependencyProperty.Register("SelectedTool", typeof(DrawShape), typeof(Plane));

        /**
         * @deprecated
         */
        public DrawShape SelectedTool {
            get { return (DrawShape)GetValue(SelectedToolProperty); }
            set { Console.WriteLine(value); SetValue(SelectedToolProperty, value); OnPropertyChanged("SelectedTool"); }
        }

        /**
         * @deprecated
         */
        public event PropertyChangedEventHandler PropertyChanged;
        public string planeName = "Hello World";
        public string PlaneName { get { return planeName; } private set { planeName = value; } }

        private Point startPointer;
        private Point movePointer;

        //private Polyline drawingLine = null;
        //private Rectangle rect = null;
        //private Line line = null;
        //private Stroke currentStroke = null;
        private Shape element = null;

        private readonly SharedModel model = null;
        private DrawShape selectedShape;

        private readonly List<IDisposable> subscriptions = new List<IDisposable>();

        public Plane() {
            InitializeComponent();
            model = SharedModel.GetInstance();
            subscriptions.Add(model.GetShape().Subscribe((value) => { ChangeShape(value); return value; }));
        }

        ~Plane() {
            ClearSubscriptions();
        }

        public virtual void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        override
        public string ToString() {
            return planeName;
        }

        private void ChangeShape(DrawShape nextShape) {
            Console.WriteLine(nextShape);
            selectedShape = nextShape;
            switch (selectedShape) {
                case DrawShape.Pen:
                    HidePreview();
                    CanvasDraw.EditingMode = InkCanvasEditingMode.Ink;
                    break;
                case DrawShape.Erase:
                    HidePreview();
                    CanvasDraw.EditingMode = InkCanvasEditingMode.EraseByPoint;
                    break;
                default:
                    ShowPreview();
                    CanvasDraw.EditingMode = InkCanvasEditingMode.None;
                    //PreviewCanvasDraw.EditingMode = InkCanvasEditingMode.None;
                    break;
            }
        }

        private void StartDrawing() {
            //currentStroke = null;
            switch (selectedShape) {
                //case DrawShape.Pen:
                //    drawingLine = DrawPencil();
                //    ToCanvas(drawingLine);
                //    break;
                case DrawShape.Line:
                    element = DrawLine();
                    ToPreview(element);

                    //StylusPointCollection stylusPoints = new StylusPointCollection {
                    //    new StylusPoint(10, 10),
                    //    new StylusPoint(10, 100),
                    //    new StylusPoint(100, 100),
                    //    new StylusPoint(100, 10),
                    //    new StylusPoint(10, 10)
                    //};
                    //Stroke stroke = new Stroke(stylusPoints);
                    //stroke.DrawingAttributes.Color = Colors.Red;
                    //CanvasDraw.Strokes.Add(stroke);
                    break;
                case DrawShape.Rectangle:
                    element = DrawRectangle();
                    //Console.WriteLine("Draw rect");
                    //Console.WriteLine(startPointer);

                    //Canvas.SetLeft(element, startPointer.X);
                    //Canvas.SetTop(element, startPointer.Y);
                    ToPreview(element);
                    break;
            }
        }

        private void Draw() {
            //PreviewCanvasDraw.Strokes.Clear();
            StylusPointCollection stylus;
            switch (selectedShape) {
                //case DrawShape.Pen:
                //    if (startPointer != movePointer) {
                //        drawingLine.Points.Add(movePointer);
                //    }
                //    break;
                case DrawShape.Line:
                    if (element == null) {
                        return;
                    }
                    ((Line)element).X2 = movePointer.X;
                    ((Line)element).Y2 = movePointer.Y;

                    //stylus = new StylusPointCollection {
                    //    new StylusPoint(startPointer.X, startPointer.Y),
                    //    new StylusPoint(movePointer.X, movePointer.Y)
                    //};
                    //currentStroke = new Stroke(stylus);
                    //currentStroke.DrawingAttributes.Color = Colors.Blue;
                    //PreviewCanvasDraw.Strokes.Add(currentStroke);
                    break;
                case DrawShape.Rectangle:
                    //stylus = new StylusPointCollection {
                    //    new StylusPoint(startPointer.X, startPointer.Y),
                    //    new StylusPoint(movePointer.X, startPointer.Y),
                    //    new StylusPoint(movePointer.X, movePointer.Y),
                    //    new StylusPoint(startPointer.X, movePointer.Y),
                    //    new StylusPoint(startPointer.X, startPointer.Y)
                    //};
                    //currentStroke = new Stroke(stylus);
                    //currentStroke.DrawingAttributes.Color = Colors.Blue;
                    //PreviewCanvasDraw.Strokes.Add(currentStroke);
                    if (element == null) {
                        return;
                    }
                    //Console.WriteLine("draw rect on move");
                    //Console.WriteLine(movePointer);
                    double x = Math.Min(startPointer.X, movePointer.X);
                    double y = Math.Min(startPointer.Y, movePointer.Y);

                    double width = Math.Max(startPointer.X, movePointer.X) - x;
                    double height = Math.Max(startPointer.Y, movePointer.Y) - y;

                    ((Rectangle)element).Width = width;
                    ((Rectangle)element).Height = height;

                    Canvas.SetLeft(element, x);
                    Canvas.SetTop(element, y);
                    break;
                case DrawShape.Circle:
                    //Ellipse ellipse = new Ellipse();
                    //ellipse.Stroke = Brushes.Blue;
                    //stylus = new StylusPointCollection {
                    //    new StylusPoint(startPointer.X, startPointer.Y),
                    //    new StylusPoint(movePointer.X, movePointer.Y)
                    //};
                    //if (currentStroke != null) {
                    //    //PreviewCanvasDraw.Strokes.Remove(currentStroke);
                    //}
                    //currentStroke = new BaseStroke(stylus);
                    //currentStroke.DrawingAttributes.Color = Colors.Blue;
                    //PreviewCanvasDraw.Strokes.Add(currentStroke);

                    break;
            }
        }

        //private Polyline DrawPencil() {
        //    return new Polyline {
        //        Stroke = Brushes.Blue,
        //        StrokeThickness = 2.0
        //    };
        //}

        private Line DrawLine() {
            return new Line() {
                Stroke = Brushes.Blue,
                X1 = startPointer.X,
                Y1 = startPointer.Y,
                X2 = startPointer.X,
                Y2 = startPointer.Y
            };
        }

        private Rectangle DrawRectangle() {
            Rectangle rect = new Rectangle() {
                Stroke = Brushes.Blue,
                StrokeThickness = 4
            };

            double x = Math.Min(startPointer.X, movePointer.X);
            double y = Math.Min(startPointer.Y, movePointer.Y);

            double width = Math.Max(startPointer.X, movePointer.X) - x;
            double height = Math.Max(startPointer.Y, movePointer.Y) - y;

            rect.Width = width;
            rect.Height = height;

            Canvas.SetLeft(rect, x);
            Canvas.SetTop(rect, y);

            return rect;
            //return new Rectangle() {
            //    Stroke = Brushes.Blue,
            //    StrokeThickness = 4
            //};
        }

        private void ToPreview(UIElement element) {
            PreviewCanvasDraw.Children.Add(element);
            //if (stroke == null) {
            //    return;
            //}
            //stroke.DrawingAttributes.Color = Colors.Black;
            //CanvasDraw.Strokes.Add(stroke);
            //HidePreview();
        }

        private void ToCanvas() {
            //element.Stroke = Brushes.Black;
            CanvasDraw.Children.Add(DrawRectangle());
        }

        private void CanvasMouseDown(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) {
                startPointer = e.GetPosition(PreviewCanvasDraw);
                movePointer = e.GetPosition(PreviewCanvasDraw);
                //Console.WriteLine("startPointer:");
                //Console.WriteLine(startPointer);
                StartDrawing();
            }
        }

        public void CanvasMouseUp(object sender, MouseButtonEventArgs e) {
            Console.WriteLine("mouseup");
            ToCanvas();
            ClearForms();
            //if (IsIllegalShape()) {
            //    return;
            //}
            //ToCanvas(currentStroke);
            //ClearPreview();
        }

        private void CanvasMouseMove(object sender, MouseEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) {
                movePointer = e.GetPosition(PreviewCanvasDraw);
                Draw();
            }
        }

        private void ChangeCursor() {
            Cursor cursor;
            switch (selectedShape) {
                //case DrawShape.Ellipse:
                //    cursor = Cursors.ArrowCD;
                //    break;
                //case DrawShape.Line:
                //    cursor = Cursors.Wait;
                //    break;
                //case DrawShape.Pen:
                //    cursor = Cursors.Pen;
                //    break;
                default:
                    cursor = Cursors.Arrow;
                    break;

            }
            this.Cursor = cursor;
        }

        private void ClearForms() {
            //line = null;
            //rect = null;
            //drawingLine = null;
            element = null;
        }

        private bool IsIllegalShape() {
            return selectedShape == DrawShape.Pen || selectedShape == DrawShape.Erase;
        }

        private void HidePreview() {
            PreviewCanvasDraw.Visibility = Visibility.Hidden;
        }

        private void ShowPreview() {
            PreviewCanvasDraw.Visibility = Visibility.Visible;
        }

        //private void ClearPreview() {
        //    PreviewCanvasDraw.Strokes.Clear();
        //}

        private void ClearSubscriptions() {
            foreach (IDisposable subscription in subscriptions) {
                subscription.Dispose();
            }
        }
    }
}
