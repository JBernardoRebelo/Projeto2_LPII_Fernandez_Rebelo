namespace GameEngine
{
    /// <summary>
    /// Interface to be implemented by observers of T
    /// </summary>
    /// <typeparam name="T"> Chosen type or class </typeparam>
    public interface IObserver<T>
    {
        /// <summary>
        /// Method to notify observers of changes of T
        /// </summary>
        /// <param name="notification"> Message to be notified </param>
        void Notify(T notification);
    }
}
