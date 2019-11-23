using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRippler.Source.Data;

namespace TheRippler.Source.Models {

    class SharedModel {
        private static SharedModel model = null;

        public Color Color { set; get; }
        public DrawShape Shape { set { Console.WriteLine(value);  _shape.Next(value); } get { return _shape.Value; } }
        public double Thickness { set; get; }

        private readonly BehaviorSubject<DrawShape> _shape = new BehaviorSubject<DrawShape>();

        private SharedModel() {}

        public static SharedModel GetInstance() {
            if (model == null) {
                model = new SharedModel();
            }
            return model;
        }

        public BehaviorSubject<DrawShape> getShape() {
            return _shape;
        }
    }
}
