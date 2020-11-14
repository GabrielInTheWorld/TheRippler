using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRippler.Source.Data {
    class BehaviorSubject<T> : Subject<T> {

        //public T Value { set; get; }

        public BehaviorSubject(T initValue) {
            //Value = initValue;
            Next(initValue);
        }

        //public new IDisposable Subscribe(Func<T, T> observer) {
        //    if (!observers.Contains(observer)) {
        //        observers.Add(observer);
        //    }
        //    observer(Value);
        //    return new Unsubscriber(observers, observer);
        //}

        //public void Next(T value) {
        //    Value = value;
        //    NextValue(value);
        //    //foreach(<T, T> observer in observers) {
        //    //    observer.OnNext(value);
        //    //}
        //}
    }
}
