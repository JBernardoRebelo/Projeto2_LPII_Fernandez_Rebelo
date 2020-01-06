namespace GameEngine
{
    /// <summary>
    /// Class that all gameObject Components must inherit from
    /// </summary>
    public abstract class Component : BaseGameObject
    {
        /// <summary>
        /// Property that contains component parent game object
        /// </summary>
        public GameObject ParentGameObject { get; set; }

        /// <summary>
        /// Property that contains component parent game object scene
        /// </summary>
        public Scene ParentScene => ParentGameObject.ParentScene;
    }
}
