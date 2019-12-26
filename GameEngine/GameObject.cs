using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine
{
    /// <summary>
    /// Generic class to instantiate gameObjects
    /// </summary>
    public class GameObject : IGameObject, IEnumerable<Component>
    {
        /// <summary>
        /// The scene were the game object is
        /// </summary>
        public Scene ParentScene { get; protected set; }

        /// <summary>
        /// The name of the game object
        /// </summary>
        public string Name { get; protected set; }

        //Is this game object renderable?
        public bool IsRenderable =>
            containsRenderableComponent && containsPosition;

        // Is the game object collidable?
        public bool IsCollidable => containsPosition && containsCollider;

        // Components which a game object can only have one of
        private static readonly Type[] oneOfAKind = new Type[]
        {
            typeof(Transform),
            typeof(KeyObserver),
            typeof(RenderableComponent),
            typeof(AbstractCollider)
        };

        // Helper variables for the IsRenderable property
        private bool
            containsRenderableComponent, containsPosition, containsCollider;

        // The components in this game object
        private readonly ICollection<Component> components;

        public GameObject()
        {
            components = new List<Component>();
            containsRenderableComponent = false;
            containsPosition = false;
            containsCollider = false;
        }

        // Add a component to this game object
        public void AddComponent(Component component)
        {

            // Check for one of a kind components
            foreach (Type componentType in oneOfAKind)
            {
                if (componentType.IsInstanceOfType(component)
                    && GetComponent(componentType) != null)
                {
                    throw new InvalidOperationException(
                        $"Game objects can only have one {componentType.Name} "
                        + "component");
                }
            }

            // Is this component a position component or a renderable component?
            if (component is Transform)
                containsPosition = true;
            else if (component is RenderableComponent)
                containsRenderableComponent = true;
            else if (component is AbstractCollider)
                containsCollider = true;

            // Specify reference to this game object in the component
            component.ParentGameObject = this;

            // Add component to this game object
            components.Add(component);
        }

        // The following methods provide several ways of getting components
        // from this game object
        public T GetComponent<T>() where T : Component
        {
            // TODO: Use dictionary for one of a kind game objects
            // to speed up this search

            return components.FirstOrDefault(component => component is T) as T;
        }

        public Component GetComponent(Type type)
        {
            // TODO: Use dictionary for one of a kind game objects
            // to speed up this search

            return components.FirstOrDefault(
                component => type.IsInstanceOfType(component));
        }

        public IEnumerator<Component> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
