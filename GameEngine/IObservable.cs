using System.Collections.Generic;

namespace GameEngine
{
    /// <summary>
    /// Generic interface to set T to be observable
    /// </summary>
    /// <typeparam name="T"> Type or Class </typeparam>
    public interface IObservable<T>
    {
        /// <summary>
        /// Method to register an observer to selected 
        /// </summary>
        /// <param name="whatToObserve"> IEnumerable of T to be observed
        /// </param>
        /// <param name="observer"> An observer that implements IObserver
        /// of the same T
        /// </param>
        void RegisterObserver(
            IEnumerable<T> whatToObserve, IObserver<T> observer);

        /// <summary>
        /// Method to stop obsrvation of a T from an observer
        /// </summary>
        /// <param name="whatToObserve"> T that was being observed </param>
        /// <param name="observer"> The observer in question </param>
        void RemoveObserver(T whatToObserve, IObserver<T> observer);

        /// <summary>
        /// Method to remove an observer of T
        /// </summary>
        /// <param name="observer"> The observer in question </param>
        void RemoveObserver(IObserver<T> observer);
    }
}
