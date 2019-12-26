using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace GameEngine
{
    // This class handle keyboard input, other objects can register themselves
    // as observers to listen to specific keys
    public class InputHandler : IObservable<ConsoleKey>
    {
        // Observers for specific keys
        private Dictionary<ConsoleKey, ICollection<IObserver<ConsoleKey>>>
            observers;

        // The input thread
        private Thread inputThread;

        // A list of keys which cause the input handler to terminate
        private IEnumerable<ConsoleKey> quitKeys;

        // Create a new input handler
        public InputHandler(IEnumerable<ConsoleKey> quitKeys)
        {
            this.quitKeys = quitKeys;
            observers = new
                Dictionary<ConsoleKey, ICollection<IObserver<ConsoleKey>>>();
            inputThread = new Thread(ReadInput);
        }

        // Method which will run in a thread reading keys
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
            } while (!quitKeys.Contains(key));
        }

        // Start thread which will read the input
        public void StartReadingInput()
        {
            inputThread.Start();
        }

        // Wait for thread reading the input to terminate
        public void StopReadingInput()
        {
            inputThread.Join();
        }

        // Below are methods for registering and removing observers for this
        // subject

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
        public void RemoveObserver(
            ConsoleKey whatToObserve, IObserver<ConsoleKey> observer)
        {
            if (observers.ContainsKey(whatToObserve))
            {
                observers[whatToObserve].Remove(observer);
            }
        }

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
