using System;
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
using System.Windows.Shapes;

namespace TheRippler.Source.Windows {
    /// <summary>
    /// Interaction logic for CreateDialog.xaml
    /// </summary>
    public partial class CreateDialog : Window {
        public string Message { private set; get; }
        
        public CreateDialog() {
            InitializeComponent();
            DataContext = this;
        }

        private void OnConfirm(object sender, RoutedEventArgs e) {
            Console.WriteLine(Message);
            DialogResult = true;
        }

        private void OnCancel(object sender, RoutedEventArgs e) {
            DialogResult = false;
        }
    }
}
