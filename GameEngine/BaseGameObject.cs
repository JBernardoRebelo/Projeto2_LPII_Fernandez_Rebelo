namespace GameEngine
{
    /// <summary>
    /// Abstract class to be used by game object's components
    /// </summary>
    public abstract class BaseGameObject : IGameObject
    {
        /// <summary>
        /// Method to be used at the beginning of the gameLoop, 
        /// can be overriden by the components.
        /// </summary>
        public virtual void Start() { }

        /// <summary>
        /// Method to be called at the end of each frame, can be overriden 
        /// by the components.
        /// </summary>
        public virtual void Update() { }

        /// <summary>
        /// Method to be called when game loop is terminated, can be overriden 
        /// by the components,
        /// </summary>
        public virtual void Finish() { }
    }
}
