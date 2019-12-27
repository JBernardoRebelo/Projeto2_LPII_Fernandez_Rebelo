namespace GameEngine
{
    /// <summary>
    /// Interface that all gameObjects must implement
    /// </summary>
    interface IGameObject
    {
        /// <summary>
        /// Method to be used at the beginning of the gameLoop
        /// </summary>
        void Start();

        /// <summary>
        /// Method to be called at the end of each frame
        /// </summary>
        void Update();

        /// <summary>
        /// Method to be called when game loop is terminated
        /// </summary>
        void Finish();
    }
}
