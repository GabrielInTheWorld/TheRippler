using System;
using System.Collections;
using System.Collections.Generic;
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
using TheRippler.Source.Collections;

namespace TheRippler {
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window {

        private enum DrawShape {
            Line, Rectangle, Ellipse
        }

        private DrawShape selectedShape = DrawShape.Line;
        private Point startPointer;
        private Point movePointer;
        private Point endPointer;

        private DataVector<UIElement> elementStack = new DataVector<UIElement>();

        public MainWindow() {
            InitializeComponent();
        }

        private void Draw() {
            switch (this.selectedShape) {
                case DrawShape.Line:
                    this.ToCanvas(this.DrawLine(this.endPointer.X, this.endPointer.Y));
                    break;
                case DrawShape.Rectangle:
                    this.ToCanvas(this.DrawRectangle());
                    break;
            }
        }

        private void DrawPreview() {
            switch(this.selectedShape) {
                case DrawShape.Line:
                    this.ToPreviewCanvas(this.DrawLine(this.movePointer.X, this.movePointer.Y));
                    break;
                case DrawShape.Rectangle:
                    this.ToPreviewCanvas(this.DrawRectangle());
                    break;
            }
        }

        private Line DrawLine(double endX, double endY) {
            return new Line() {
                Stroke = Brushes.Blue,
                X1 = this.startPointer.X,
                Y1 = this.startPointer.Y,
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
            CanvasDraw.Children.Add(element);
        }

        private void ToPreviewCanvas(UIElement element) {
            CanvasPreview.Children.Clear();
            CanvasPreview.Children.Add(element);
        }

        private void DrawPencil(object sender, RoutedEventArgs e) {
            this.selectedShape = DrawShape.Line;
        }

        private void DrawRectangle(object sender, RoutedEventArgs e) {
            this.selectedShape = DrawShape.Rectangle;
        }

        private void CanvasMouseDown(object sender, MouseButtonEventArgs e) {
            this.startPointer = e.GetPosition(this);
        }

        private void CanvasMouseUp(object sender, MouseButtonEventArgs e) {
            this.endPointer = e.GetPosition(this);
            this.Draw();
        }

        private void CanvasMouseMove(object sender, MouseEventArgs e) {
            Console.WriteLine("mouse move");
            if (e.LeftButton == MouseButtonState.Pressed) {
                Console.WriteLine("Butten pressed");
                this.movePointer = e.GetPosition(this);
                this.DrawPreview();
            }
        }
    }
}
