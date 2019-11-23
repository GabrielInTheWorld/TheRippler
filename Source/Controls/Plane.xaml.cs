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

        private bool isDrawing = false;

        private DrawShape selectedShape = DrawShape.Line;
        private Point startPointer;
        private Point movePointer;
        private Point endPointer;

        private Polyline drawingLine = null;

        public Plane() {
            InitializeComponent();

            Loaded += AfterLoading;
        }

        public virtual void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AfterLoading(object sender, RoutedEventArgs e) {
            Console.WriteLine(SelectedTool);
        }

        private void Draw() {
            //Console.WriteLine("draw");
            switch (SelectedTool) {
                case DrawShape.Pen:
                    drawingLine = null;
                    break;
                case DrawShape.Line:
                    ToCanvas(DrawLine(endPointer.X, endPointer.Y));
                    break;
                case DrawShape.Rectangle:
                    ToCanvas(DrawRectangle());
                    break;
            }
        }

        private void DrawPreview() {
            switch (SelectedTool) {
                case DrawShape.Pen:
                    if (startPointer != movePointer) {
                        drawingLine.Points.Add(movePointer);
                    }
                    break;
                case DrawShape.Line:
                    ToPreviewCanvas(DrawLine(movePointer.X, movePointer.Y));
                    break;
                case DrawShape.Rectangle:
                    ToPreviewCanvas(DrawRectangle());
                    break;
            }
        }

        private Polyline DrawPencil() {
            return new Polyline {
                Stroke = Brushes.Blue,
                StrokeThickness = 2.0
            };
        }

        private Line DrawLine(double endX, double endY) {
            return new Line() {
                Stroke = Brushes.Blue,
                X1 = startPointer.X,
                Y1 = startPointer.Y,
                X2 = endX,
                Y2 = endY
            };
        }

        private Rectangle DrawRectangle() {
            return new Rectangle() {
                Stroke = Brushes.Blue,
                StrokeThickness = 4
            };
        }

        private void ToCanvas(UIElement element) {
            //Console.WriteLine("canvas", element, CanvasDraw.Height, CanvasDraw.Width);
            CanvasDraw.Children.Add(element);
        }

        private void ToPreviewCanvas(UIElement element) {
            CanvasPreview.Children.Clear();
            CanvasPreview.Children.Add(element);
        }

        private void DrawPencil(object sender, RoutedEventArgs e) {
            this.selectedShape = DrawShape.Pen;
        }

        private void DrawLine(object sender, RoutedEventArgs e) {
            this.selectedShape = DrawShape.Line;
        }

        private void DrawRectangle(object sender, RoutedEventArgs e) {
            this.selectedShape = DrawShape.Rectangle;
        }

        private void CanvasMouseDown(object sender, MouseButtonEventArgs e) {
            Console.WriteLine("Mouse down");
            if (e.LeftButton == MouseButtonState.Pressed) {
                startPointer = e.GetPosition(CanvasDraw);
                isDrawing = true;
                //((Control)sender).CaptureMouse = true;
                if (SelectedTool == DrawShape.Pen) {
                    drawingLine = DrawPencil();
                    ToCanvas(drawingLine);
                }
            }
        }

        public void CanvasMouseUp(object sender, MouseButtonEventArgs e) {
            //Console.WriteLine("Mouse up", sender);
            endPointer = e.GetPosition(CanvasDraw);
            Draw();
            isDrawing = false;
        }

        private void CanvasMouseMove(object sender, MouseEventArgs e) {
            //Console.WriteLine("move");
            //Control control = sender as Control;
            if (e.LeftButton == MouseButtonState.Pressed) {
                movePointer = e.GetPosition(CanvasDraw);
                DrawPreview();
            } else if (isDrawing && e.LeftButton == MouseButtonState.Released) {
                endPointer = e.GetPosition(CanvasPreview);
                Draw();
                isDrawing = false;
            }
            //if (e.LeftButton == MouseButtonState.Released) {
            //    Console.WriteLine("move released");
            //}
        }
    }
}
