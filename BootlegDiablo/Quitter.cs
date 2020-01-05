using System;
using GameEngine;

namespace BootlegDiablo
{
    /// <summary>
    /// Simple component which listens for the escape key and terminates the
    /// parent scene
    /// </summary>
    public class Quitter : Component
    {
        /// <summary>
        /// Variable to make this class an observer of user input
        /// </summary>
        private KeyObserver keyObserver;

        /// <summary>
        /// Override to the component Start()
        /// </summary>
        public override void Start()
        {
            keyObserver = ParentGameObject.GetComponent<KeyObserver>();
        }

        /// <summary>
        /// Override to component Update(), check if specific observed key
        /// is pressed
        /// </summary>
        public override void Update()
        {
            foreach (ConsoleKey key in keyObserver.GetCurrentKeys())
            {
                if (key == ConsoleKey.Escape)
                {
                    Console.WriteLine("Bye :(");
                    ParentScene.Terminate();
                }
            }
        }
    }
}
