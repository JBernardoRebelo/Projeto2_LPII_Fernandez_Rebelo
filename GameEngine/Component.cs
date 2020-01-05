namespace GameEngine
{
    /// <summary>
    /// Class that all gameObject Components must inherit from
    /// </summary>
    public abstract class Component : BaseGameObject
    {
        /// <summary>
        /// 
        /// </summary>
        public GameObject ParentGameObject { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Scene ParentScene => ParentGameObject.ParentScene;
    }
}
