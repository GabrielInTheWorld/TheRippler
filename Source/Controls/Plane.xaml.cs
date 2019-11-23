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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TheRippler.Source.Data;
using TheRippler.Source.Models;

namespace TheRippler.Source.Controls {
    /// <summary>
    /// Interaction logic for Plane.xaml
    /// </summary>
    public partial class Plane : UserControl, INotifyPropertyChanged {
        public static readonly DependencyProperty SelectedToolProperty = DependencyProperty.Register("SelectedTool", typeof(DrawShape), typeof(Plane));

        public DrawShape SelectedTool {
            get { return (DrawShape)GetValue(SelectedToolProperty); }
            set { Console.WriteLine(value); SetValue(SelectedToolProperty, value); OnPropertyChanged("SelectedTool"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //private bool isDrawing = false;

        private Point startPointer;
        private Point movePointer;
        //private Point endPointer;

        private Polyline drawingLine = null;
        private Rectangle rect = null;
        private Line line = null;

        private SharedModel model = null;
        private DrawShape selectedShape;

        public Plane() {
            InitializeComponent();
            model = SharedModel.GetInstance();
            model.getShape().Subscribe((value) => { selectedShape = value; return value; });
        }

        public virtual void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //private void Draw() {
        //    switch (SelectedTool) {
        //        case DrawShape.Pen:
        //            drawingLine = null;
        //            break;
        //        case DrawShape.Line:
        //            //ToCanvas(DrawLine(endPointer.X, endPointer.Y));
        //            break;
        //        case DrawShape.Rectangle:
        //            //ToCanvas(DrawRectangle(endPointer.X, endPointer.Y));
        //            break;
        //    }
        //}

        private void StartDrawing() {
            switch (selectedShape) {
                case DrawShape.Pen:
                    drawingLine = DrawPencil();
                    ToCanvas(drawingLine);
                    break;
                case DrawShape.Line:
                    line = DrawLine();
                    //line.X1 = startPointer.X;
                    //line.X2 = startPointer.X;
                    //line.Y1 = startPointer.Y;
                    //line.Y2 = startPointer.Y;
                    ToCanvas(line);
                    break;
                case DrawShape.Rectangle:
                    rect = DrawRectangle();
                    Canvas.SetLeft(rect, startPointer.X);
                    Canvas.SetTop(rect, startPointer.Y);
                    ToCanvas(rect);
                    //ToCanvas(DrawRectangle(endPointer.X, endPointer.Y));
                    break;
            }
        }

        private void Draw() {
            switch (selectedShape) {
                case DrawShape.Pen:
                    if (startPointer != movePointer) {
                        drawingLine.Points.Add(movePointer);
                    }
                    break;
                case DrawShape.Line:
                    //ToPreviewCanvas(DrawLine(movePointer.X, movePointer.Y));
                    if (line == null) {
                        return;
                    }
                    line.X2 = movePointer.X;
                    line.Y2 = movePointer.Y;
                    break;
                case DrawShape.Rectangle:
                    if (rect == null) {
                        return;
                    }
                    double x = Math.Min(startPointer.X, movePointer.X);
                    double y = Math.Min(startPointer.Y, movePointer.Y);

                    double width = Math.Max(startPointer.X, movePointer.X) - x;
                    double height = Math.Max(startPointer.Y, movePointer.Y) - y;

                    rect.Width = width;
                    rect.Height = height;

                    Canvas.SetLeft(rect, x);
                    Canvas.SetTop(rect, y);
                    //ToPreviewCanvas(DrawRectangle(movePointer.X, movePointer.Y));
                    break;
            }
        }

        private Polyline DrawPencil() {
            return new Polyline {
                Stroke = Brushes.Blue,
                StrokeThickness = 2.0
            };
        }

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
            //double width = endX > startPointer.X ? endX - startPointer.X : startPointer.X - endX;
            //double height = endY > startPointer.Y ? endY - startPointer.Y : startPointer.Y - endY;
            return new Rectangle() {
                Stroke = Brushes.Blue,
                StrokeThickness = 4,
                //Width = width,
                //Height = height,
            };
        }

        private void ToCanvas(UIElement element) {
            CanvasDraw.Children.Add(element);
        }

        //private void ToPreviewCanvas(UIElement element) {
        //    CanvasPreview.Children.Clear();
        //    CanvasPreview.Children.Add(element);
        //}

        private void CanvasMouseDown(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) {
                startPointer = e.GetPosition(CanvasDraw);
                StartDrawing();
                //isDrawing = true;
                //if (SelectedTool == DrawShape.Pen) {
                //    drawingLine = DrawPencil();
                //    ToCanvas(drawingLine);
                //}
                //switch (SelectedTool) {
                //    case DrawShape.Pen:
                //        drawingLine = DrawPencil();
                //        ToCanvas(drawingLine);
                //        break;
                //    case DrawShape.Line:
                //        line = DrawLine();
                //        //line.X1 = startPointer.X;
                //        //line.X2 = startPointer.X;
                //        //line.Y1 = startPointer.Y;
                //        //line.Y2 = startPointer.Y;
                //        ToCanvas(line);
                //        break;
                //    case DrawShape.Rectangle:
                //        rect = DrawRectangle();
                //        Canvas.SetLeft(rect, startPointer.X);
                //        Canvas.SetTop(rect, startPointer.Y);
                //        ToCanvas(rect);
                //        //ToCanvas(DrawRectangle(endPointer.X, endPointer.Y));
                //        break;
                //}
            }
        }

        public void CanvasMouseUp(object sender, MouseButtonEventArgs e) {
            //Console.WriteLine("mouse up");
            ClearForms();
            //endPointer = e.GetPosition(CanvasDraw);
            //Draw();
            //isDrawing = false;
        }

        private void CanvasMouseMove(object sender, MouseEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) {
                movePointer = e.GetPosition(CanvasDraw);
                Draw();
            //} else if (isDrawing && e.LeftButton == MouseButtonState.Released) {
            //    endPointer = e.GetPosition(CanvasPreview);
            //    Draw();
            //    isDrawing = false;
            }
        }

        private void ClearForms() {
            line = null;
            rect = null;
            drawingLine = null;
        }
    }
}
