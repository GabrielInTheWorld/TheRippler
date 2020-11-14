using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TheRippler.Source.BaseClasses;
using TheRippler.Source.Windows;

namespace TheRippler.Source.Services {
    public class ToolBarService : BaseService<ToolBarService> {

        public void OpenCreateWindow() {
            var dialog = new CreateDialog();
            if (dialog.ShowDialog() == true) {
                Console.WriteLine("On confirm");
            }
        }
    }
}
