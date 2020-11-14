using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRippler.Source.Data {
    class Subject<T> : Observable<T>{
        public T Value { set; get; }

        public Subject() { Value = default; }

        public new IDisposable Subscribe(Func<T, T> observer) {
            if (!observers.Contains(observer)) {
                observers.Add(observer);
            }
            observer(Value);
            return new Unsubscriber(observers, observer);
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
