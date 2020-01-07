using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace GameEngine
{
    /// <summary>
    /// Class to handle keyboard input, where 
    /// other objects can register themselves
    /// as observers and listen to specific keys stored in a .
    /// </summary>
    public class InputHandler : IObservable<ConsoleKey>
    {
        /// <summary>
        /// Dictionary of ConsoleKeys and collection of Observers for 
        /// specific keys.
        /// </summary>
        private Dictionary<ConsoleKey, ICollection<IObserver<ConsoleKey>>>
            observers;

        /// <summary>
        /// The input thread
        /// </summary>
        private Thread inputThread;

        /// <summary>
        /// A list of keys which cause the input handler to terminate
        /// </summary>
        private IEnumerable<ConsoleKey> _keysToObserver;

        /// <summary>
        /// Create a new input handler
        /// </summary>
        /// <param name="keysToObserve"> IEnumerable of keys to observe 
        /// </param>
        public InputHandler(IEnumerable<ConsoleKey> keysToObserve)
        {
            _keysToObserver = keysToObserve;
            observers = new
                Dictionary<ConsoleKey, ICollection<IObserver<ConsoleKey>>>();
            inputThread = new Thread(ReadInput);
        }

        /// <summary>
        /// Method which will run in a thread reading keys
        /// </summary>
        private void ReadInput()
        {
            ConsoleKey key;

            // Listen keys until a quit key is pressed
            do
            {
                // Read key
                key = Console.ReadKey(true).Key;

                // Notify observers listening for this key
                if (observers.ContainsKey(key))
                {
                    foreach (IObserver<ConsoleKey> observer in observers[key])
                    {
                        observer.Notify(key);
                    }
                }
            } while (!_keysToObserver.Contains(key));
        }

        /// <summary>
        /// Start thread which will read the input
        /// </summary>
        public void StartReadingInput()
        {
            inputThread.Start();
        }

        /// <summary>
        /// Wait for thread reading the input to terminate
        /// </summary>
        public void StopReadingInput()
        {
            inputThread.Join();
        }

        /// <summary>
        /// Method to register an observer of ConsoleKey
        /// </summary>
        /// <param name="whatToObserve"> IEnumerable of ConsoleKey </param>
        /// <param name="observer"> An observer that implements IObserver
        /// of ConsoleKey </param>
        public void RegisterObserver(
            IEnumerable<ConsoleKey> whatToObserve,
            IObserver<ConsoleKey> observer)
        {
            foreach (ConsoleKey key in whatToObserve)
            {
                if (!observers.ContainsKey(key))
                {
                    observers[key] = new List<IObserver<ConsoleKey>>();
                }
                observers[key].Add(observer);
            }
        }

        /// <summary>
        /// Method to stop obsrvation of a T from an observer
        /// </summary>
        /// <param name="whatToObserve"> ConsoleKey that was being observed
        /// </param>
        /// <param name="observer"> An observer that implements IObserver
        /// of ConsoleKey
        /// </param>
        public void RemoveObserver(
            ConsoleKey whatToObserve, IObserver<ConsoleKey> observer)
        {
            if (observers.ContainsKey(whatToObserve))
            {
                observers[whatToObserve].Remove(observer);
            }
        }

        /// <summary>
        /// Method to remove an observer of ConsoleKey
        /// </summary>
        /// <param name="observer"> Observer of ConsoleKey </param>
        public void RemoveObserver(IObserver<ConsoleKey> observer)
        {
            foreach (ICollection<IObserver<ConsoleKey>> theseObservers
                        in observers.Values)
            {
                theseObservers.Remove(observer);
            }
        }
    }
}
