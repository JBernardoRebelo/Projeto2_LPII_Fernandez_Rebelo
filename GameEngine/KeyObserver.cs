using System;
using System.Collections.Generic;

namespace GameEngine
{
    public class KeyObserver : Component, IObserver<ConsoleKey>
    {
        private IEnumerable<ConsoleKey> keysToObserve;
        private Queue<ConsoleKey> observedKeys;
        private object queueLock;

        public KeyObserver(IEnumerable<ConsoleKey> keysToObserve)
        {
            this.keysToObserve = keysToObserve;
            observedKeys = new Queue<ConsoleKey>(2);
            queueLock = new object();
        }

        public override void Start()
        {
            ParentScene.inputHandler.RegisterObserver(keysToObserve, this);
        }

        // This method will be called by the subject when an observed key is
        // pressed
        public void Notify(ConsoleKey notification)
        {
            lock (queueLock)
            {
                observedKeys.Enqueue(notification);
            }
        }

        // Return the currently observed keys
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
