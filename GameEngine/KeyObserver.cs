using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine
{
    public class KeyObserver : Component
    {
        // Return the currently observed keys
        public IEnumerable<ConsoleKey> GetCurrentKeys()
        {
            IEnumerable<ConsoleKey> currentKeys;
            //lock (queueLock)
            //{
            //    currentKeys = observedKeys.ToArray();
            //    observedKeys.Clear();
            //}

            currentKeys = new List<ConsoleKey>();

            return currentKeys;

        }
    }
}
