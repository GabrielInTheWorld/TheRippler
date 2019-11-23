using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRippler.Source.Data.Interfaces {
    interface ICustomObservable<T> {
        IDisposable Subscribe(Func<T, T> func);
    }
}
