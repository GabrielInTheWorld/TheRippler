using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRippler.Source.BaseClasses {
    public abstract class BaseService<T> where T: class, new() {

        private static T instance;

        protected BaseService() { }

        public static T GetInstance() {
            //if (instance == null) {
            //    instance = new T();
            //}

            return instance ?? (instance = new T());
        }
    }
}
