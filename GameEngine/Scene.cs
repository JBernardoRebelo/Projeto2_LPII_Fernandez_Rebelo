using System;
using System.Threading;
using System.Collections.Generic;

namespace GameEngine
{
    /// <summary>
    /// Class where game scenes are created and managed
    /// </summary>
    public class Scene
    {
        /// <summary>
        /// Scene dimensions
        /// </summary>
        public readonly int xdim, ydim;

        /// <summary>
        /// Input handler for this scene
        /// </summary>
        public readonly InputHandler inputHandler;

        /// <summary>
        /// Collision handler for this scene
        /// </summary>
        public readonly CollisionHandler collisionHandler;

        /// <summary>
        /// Game objects in this scene
        /// </summary>
        private Dictionary<string, GameObject> gameObjects;
        
        /// <summary>
        /// Is the scene terminated?
        /// </summary>
        private bool terminate;

        /// <summary>
        /// Renderer for this scene
        /// </summary>
        private readonly ConsoleRenderer renderer;

        /// <summary>
        /// Create a new scene
        /// </summary>
        /// <param name="xdim"> Scene collums size </param>
        /// <param name="ydim"> Scene rows size </param>
        /// <param name="inputHandler"> Class to handle user inputs </param>
        /// <param name="renderer"> Class to handle game objects renderization
        /// </param>
        /// <param name="collisionHandler"> Class to handle colisions
        /// between game objects
        /// </param>
        public Scene(int xdim, int ydim, InputHandler inputHandler,
            ConsoleRenderer renderer, CollisionHandler collisionHandler)
        {
            this.xdim = xdim;
            this.ydim = ydim;
            this.inputHandler = inputHandler;
            this.renderer = renderer;
            this.collisionHandler = collisionHandler;
            terminate = false;
            gameObjects = new Dictionary<string, GameObject>();
        }

        /// <summary>
        /// Add a game object to this scene
        /// </summary>
        /// <param name="gameObject"> Game object to be added </param>
        public void AddGameObject(GameObject gameObject)
        {
            gameObject.ParentScene = this;
            gameObjects.Add(gameObject.Name, gameObject);
        }

        /// <summary>
        /// Find a game object by name in this scene
        /// </summary>
        /// <param name="name"> The name of the game object </param>
        /// <returns> Game object with specific name </returns>
        public GameObject FindGameObjectByName(string name)
        {
            return gameObjects[name];
        }

        /// <summary>
        /// Terminate scene
        /// </summary>
        public void Terminate()
        {
            terminate = true;
        }

        /// <summary>
        /// Game loop
        /// </summary>
        /// <param name="msFramesPerSecond"> Frame length untill new frame
        /// is rendered
        /// </param>
        public void GameLoop(int msFramesPerSecond)
        {
            // Initialize all game objects
            foreach (GameObject gameObject in gameObjects.Values)
            {
                gameObject.Start();
            }

            // Initialize renderer
            renderer?.Start();

            // Start reading input
            inputHandler.StartReadingInput();

            // Perform the game loop until the scene is terminated
            while (!terminate)
            {
                // Time to wait until next frame
                int timeToWait;

                // Get real time in ticks (10000 ticks = 1 milisecond)
                long start = DateTime.Now.Ticks;

                // Update game objects
                foreach (GameObject gameObject in gameObjects.Values)
                {
                    gameObject.Update();
                }

                // Update collision information
                collisionHandler.Update(gameObjects.Values);

                // Render current frame
                renderer?.Render(gameObjects.Values);

                // Time to wait until next frame
                timeToWait = (int)(start / TimeSpan.TicksPerMillisecond
                    + msFramesPerSecond
                    - DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);

                // Handle robustly
                timeToWait = timeToWait > 0 ? timeToWait : 0;

                // Wait until next frame
                Thread.Sleep(timeToWait);
            }

            // Teardown the game objects in this scene
            foreach (GameObject gameObject in gameObjects.Values)
            {
                gameObject.Finish();
            }

            // Teardown renderer
            renderer?.Finish();
        }
    }
}
