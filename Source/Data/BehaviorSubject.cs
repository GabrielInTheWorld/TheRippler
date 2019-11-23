using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRippler.Source.Data {
    class BehaviorSubject<T> : Observable<T> {

        public T Value { set; get; }

        public BehaviorSubject(T initValue = default) {
            //Value = initValue;
            Next(initValue);
        }

        public void Next(T value) {
            Value = value;
            NextValue(value);
            //foreach(<T, T> observer in observers) {
            //    observer.OnNext(value);
            //}
        }
    }
}
