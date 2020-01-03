namespace GameEngine
{
    /// <summary>
    /// Class that all gameObject Components must inherit from
    /// </summary>
    public abstract class Component : BaseGameObject
    {
        public GameObject ParentGameObject { get; set; }

        public Scene ParentScene => ParentGameObject.ParentScene;
    }
}
