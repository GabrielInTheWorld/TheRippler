using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRippler.Source.Data {
    class Observable<T> : IObservable<T> {
        protected readonly List<IObserver<T>> observers;

        public Observable() {
            observers = new List<IObserver<T>>();
        }

        public IDisposable Subscribe(IObserver<T> observer) {
            if (!observers.Contains(observer)) {
                observers.Add(observer);
            }
            return new Unsubscriber(observers, observer);
        }

        //public void Next(T value) {
        //    foreach (Observer<T> observer in observers) {
        //        observer.OnNext(value);
        //    }
        //}

        protected class Unsubscriber : IDisposable {
            private readonly List<IObserver<T>> observers;
            private readonly IObserver<T> observer;
            
            public Unsubscriber(List<IObserver<T>> observers, IObserver<T> item) {
                this.observers = observers;
                this.observer = item;
            }

            public void Dispose() {
                if (observer != null && observers.Contains(observer)) {
                    observers.Remove(observer);
                }
            }
        }
    }
}
