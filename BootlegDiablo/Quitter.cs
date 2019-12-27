using System;
using GameEngine;

namespace BootlegDiablo
{
    // Simple component which listens for the escape key and terminates the
    // parent scene
    public class Quitter : Component
    {
        private KeyObserver keyObserver;
        public override void Start()
        {
            keyObserver = ParentGameObject.GetComponent<KeyObserver>();
        }
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
