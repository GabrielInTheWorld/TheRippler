using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using TheRippler.Source.Data;

namespace TheRippler {
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window, INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;

        //private bool isDrawing = false;

        private DrawShape selectedShape;
        //private BehaviorSubject<DrawShape> observableSelectedShape = new BehaviorSubject<DrawShape>(DrawShape.Line);
        //private Point startPointer;
        //private Point movePointer;
        //private Point endPointer;

        //private Polyline drawingLine = null;

        //private DataVector<UIElement> elementStack = new DataVector<UIElement>();

        public MainWindow() {
            InitializeComponent();
            DataContext = this;
            NextShape(DrawShape.Line);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //private Polyline DrawPencil() {
        //    return new Polyline {
        //        Stroke = Brushes.Blue,
        //        StrokeThickness = 2.0
        //    };
        //}

        //private Line DrawLine(double endX, double endY) {
        //    return new Line() {
        //        Stroke = Brushes.Blue,
        //        X1 = startPointer.X,
        //        Y1 = startPointer.Y,
        //        X2 = endX,
        //        Y2 = endY
        //    };
        //}

        //private Rectangle DrawRectangle() {
        //    return new Rectangle() {
        //        Stroke = Brushes.Blue,
        //        StrokeThickness = 4
        //    };
        //}

        private void DrawPencil(object sender, RoutedEventArgs e) {
            NextShape(DrawShape.Pen);
            //this.selectedShape = DrawShape.Pen;
            //this.observableSelectedShape.Next(DrawShape.Pen);
        }

        private void DrawLine(object sender, RoutedEventArgs e) {
            NextShape(DrawShape.Line);
            //this.selectedShape = DrawShape.Line;
            //this.observableSelectedShape.Next(DrawShape.Line);
        }

        private void DrawRectangle(object sender, RoutedEventArgs e) {
            NextShape(DrawShape.Rectangle);
            //this.selectedShape = DrawShape.Rectangle;
            //this.observableSelectedShape.Next(DrawShape.Rectangle);
        }

        //private void Plane_Loaded(object sender, RoutedEventArgs e) {
        //    Console.WriteLine("Loaded");
        //}

        private void NextShape(DrawShape shape) {
            SelectedShape = shape;
            //OnPropertyChanged("selectedShape");
        }

        public DrawShape SelectedShape {
            get { return this.selectedShape; }
            set { if (value != selectedShape) {
                    selectedShape = value;
                    OnPropertyChanged();
                } 
            }
        }
    }
}
