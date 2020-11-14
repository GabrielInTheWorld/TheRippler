using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace TheRippler.Source.Controls {
    /// <summary>
    /// Interaction logic for PlaneControl.xaml
    /// </summary>
    public partial class PlaneControl : UserControl {
        //public DataVector<Plane> Planes { get; } = new DataVector<Plane>();
        public ObservableCollection<Plane> Planes { get; } = new ObservableCollection<Plane>();

        public PlaneControl() {
            InitializeComponent();
            planeListView.ItemsSource = Planes;
        }

        private void Add_Click(object sender, RoutedEventArgs e) {
            Plane next = new Plane();
            Planes.Add(next);
        }

        private void Remove_Click(object sender, RoutedEventArgs e) {

        }
    }
}
