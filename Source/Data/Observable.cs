using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRippler.Source.Data.Interfaces;

namespace TheRippler.Source.Data {
    class Observable<T> : ICustomObservable<T> {
        protected readonly List<Func<T, T>> observers;

        public Observable() {
            observers = new List<Func<T, T>>();
        }

        public IDisposable Subscribe(Func<T, T> observer) {
            if (!observers.Contains(observer)) {
                observers.Add(observer);
            }
            return new Unsubscriber(observers, observer);
        }

        protected void NextValue(T value) {
            foreach (Func<T, T> observer in observers) {
                observer(value);
            }
        }

        //public void Next(T value) {
        //    foreach (Observer<T> observer in observers) {
        //        observer.OnNext(value);
        //    }
        //}

        protected class Unsubscriber : IDisposable {
            private readonly List<Func<T, T>> observers;
            private readonly Func<T, T> observer;
            
            public Unsubscriber(List<Func<T, T>> observers, Func<T, T> item) {
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
