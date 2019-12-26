using System;
using System.Collections.Generic;

namespace GameEngine
{
    public interface IObservable<T>
    {
        void RegisterObserver(
            IEnumerable<T> whatToObserve, IObserver<T> observer);
        void RemoveObserver(T whatToObserve, IObserver<T> observer);
        void RemoveObserver(IObserver<T> observer);
    }
}
