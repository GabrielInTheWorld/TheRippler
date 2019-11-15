using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRippler.Source.Collections {
    class DataVector<T> {
        private T[] stack;
        private int incrementSize = 0;
        private int _size = 0;

        public int Size { get;  }

        public DataVector() {
            this.stack = new T[2];
        }

        public DataVector(int startSize) {
            this.stack = new T[startSize];
        }

        public DataVector(int startSize, int incrementSize) {
            this.stack = new T[startSize];
            this.incrementSize = incrementSize;
        }

        public void Add(T element) {
            if (this._size == this.stack.Length) {
                this.Resize();
            }
            this.stack[this._size++] = element;
        }

        private void Resize() {
            T[] tmp = new T[this._size];
            if (this.incrementSize == 0) {
                this.stack = new T[this._size * 2];
            } else {
                this.stack = new T[this._size + this.incrementSize];
            }
            for (int i = 0; i < tmp.Length; ++i) {
                this.stack[i] = tmp[i];
            }
        }
    }
}
