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
using TheRippler.Source.Models;
using TheRippler.Source.Services;

namespace TheRippler {
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window, INotifyPropertyChanged {

        // Public members
        public event PropertyChangedEventHandler PropertyChanged;

        // Private members
        private DrawShape selectedShape;

        private readonly SharedModel model;
        private readonly ToolBarService toolBar;

        public MainWindow() {
            InitializeComponent();
            model = SharedModel.GetInstance();
            toolBar = ToolBarService.GetInstance();
            DataContext = this;
            NextShape(DrawShape.Line);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void DrawPencil(object sender, RoutedEventArgs e) {
            NextShape(DrawShape.Pen);
        }

        private void DrawLine(object sender, RoutedEventArgs e) {
            NextShape(DrawShape.Line);
        }

        private void DrawRectangle(object sender, RoutedEventArgs e) {
            NextShape(DrawShape.Rectangle);
        }

        private void DrawCircle(object sender, RoutedEventArgs e) {
            NextShape(DrawShape.Circle);
        }

        private void NextShape(DrawShape shape) {
            //SelectedShape = shape;
            model.Shape = shape;
        }

        private void OnNew(object sender, RoutedEventArgs e) {
            toolBar.OpenCreateWindow();
        }

        public DrawShape SelectedShape {
            get { return this.selectedShape; }
            set { if (value != selectedShape) {
                    selectedShape = value;
                    OnPropertyChanged();
                } 
            }
        }

        private void Erase(object sender, RoutedEventArgs e) {
            NextShape(DrawShape.Erase);
        }
    }
}
