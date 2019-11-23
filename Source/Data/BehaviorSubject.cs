using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRippler.Source.Data {
    class BehaviorSubject<T> : Observable<T> {

        public T Value { set; get; }

        public BehaviorSubject(T initValue) {
            Value = initValue;
        }

        public void Next(T value) {
            Value = value;
            foreach(Observer<T> observer in observers) {
                observer.OnNext(value);
            }
        }
    }
}
