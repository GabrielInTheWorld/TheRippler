using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRippler.Source.Data {
    class Observer<T> : IObserver<T> {
        private IDisposable unsubscriber;
        private T value;

        public virtual void Subscribe(IObservable<T> observable) {
            if (observable != null) {
                unsubscriber = observable.Subscribe(this);
            }
        }

        public virtual void Unsubscribe() {
            unsubscriber.Dispose();
        }

        public virtual void OnCompleted() {
            Unsubscribe();
        }

        public virtual void OnError(Exception error) {
            throw new NotImplementedException();
        }

        public virtual void OnNext(T value) {
            this.value = value;
        }
    }
}
