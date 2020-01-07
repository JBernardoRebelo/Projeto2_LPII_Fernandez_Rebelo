using System;
using System.Collections.Generic;

namespace GameEngine
{
    /// <summary>
    /// Class responsible to observe user input
    /// </summary>
    public class KeyObserver : Component, IObserver<ConsoleKey>
    {
        /// <summary>
        /// IEnumerble of keys to be observed for input
        /// </summary>
        private IEnumerable<ConsoleKey> keysToObserve;

        /// <summary>
        /// Collection of inputed keys
        /// </summary>
        private Queue<ConsoleKey> observedKeys;

        /// <summary>
        /// Lock used for the thread
        /// </summary>
        private object queueLock;

        /// <summary>
        /// Constructor for KeyObserver class
        /// </summary>
        /// <param name="keysToObserve"> IEnumerable of keys to be observed
        /// </param>
        public KeyObserver(IEnumerable<ConsoleKey> keysToObserve)
        {
            this.keysToObserve = keysToObserve;
            observedKeys = new Queue<ConsoleKey>(2);
            queueLock = new object();
        }

        /// <summary>
        /// Method to be used in the beginning of the scene creation
        /// </summary>
        public override void Start()
        {
            ParentScene.inputHandler.RegisterObserver(keysToObserve, this);
        }


        /// <summary>
        /// This method will be called by the subject when an observed key is
        /// pressed
        /// </summary>
        /// <param name="notification"> ConsoleKey to inform observers of
        /// </param>
        public void Notify(ConsoleKey notification)
        {
            lock (queueLock)
            {
                observedKeys.Enqueue(notification);
            }
        }

        /// <summary>
        /// Return the currently observed keys
        /// </summary>
        /// <returns> IEnumerable of ConsoleKey that were inputed </returns>
        public IEnumerable<ConsoleKey> GetCurrentKeys()
        {
            IEnumerable<ConsoleKey> currentKeys;
            lock (queueLock)
            {
                currentKeys = observedKeys.ToArray();
                observedKeys.Clear();
            }
            return currentKeys;
        }
    }
}
