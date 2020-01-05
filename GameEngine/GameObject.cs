using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine
{
    /// <summary>
    /// General class to instantiate gameObjects
    /// </summary>
    public class GameObject : IGameObject, IEnumerable<Component>, IDisposable
    {
        /// <summary>
        /// The scene were the game object is
        /// </summary>
        public Scene ParentScene { get; set; }

        /// <summary>
        /// The name of the game object
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Bool to check if a game object is renderable by 
        /// ConsoleRenderer class
        /// </summary>
        public bool IsRenderable =>
            containsRenderableComponent && containsTransform;

        /// <summary>
        /// Bool to check if a game object can be collided with
        /// </summary>
        public bool IsCollidable => containsTransform && containsCollider;

        // Helper variables for the IsRenderable and IsCollidable properties

        /// <summary>
        /// Check if game object has a renderable component
        /// </summary>
        private bool containsRenderableComponent;
        /// <summary>
        /// Check if game object has a transform component
        /// </summary>
        private bool containsTransform;
        /// <summary>
        /// Check if game object has a sprite collider component
        /// </summary>
        private bool containsCollider;

        /// <summary>
        /// Components which a game object can only have one of
        /// </summary>
        private static readonly Type[] oneOfAKind = new Type[]
        {
            typeof(Transform),
            typeof(KeyObserver),
            typeof(RenderableComponent),
            typeof(AbstractCollider)
        };

        /// <summary>
        /// Collection of components in this game object
        /// </summary>
        private readonly ICollection<Component> components;

        /// <summary>
        /// GameObject constructor, accepts nothing
        /// </summary>
        public GameObject()
        {
            components = new List<Component>();
            containsRenderableComponent = false;
            containsTransform = false;
            containsCollider = false;
        }

        /// <summary>
        /// GameObject constructor overload
        /// </summary>
        /// <param name="name"> Accepts a name </param>
        public GameObject(string name)
        {
            components = new List<Component>();
            containsRenderableComponent = false;
            containsTransform = false;
            containsCollider = false;
            Name = name;
        }


        /// <summary>
        /// Add a component to this game object
        /// </summary>
        /// <param name="component"> The component to be added </param>
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
                containsTransform = true;
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

        /// <summary>
        /// Generic method to get a component of desired type
        /// </summary>
        /// <typeparam name="T"> Type of component </typeparam>
        /// <returns> Chosen component </returns>
        public T GetComponent<T>() where T : Component
        {
            return components.FirstOrDefault(component => component is T) as T;
        }

        /// <summary>
        /// Method used within GameObject class 
        /// to check if a component already exists in the
        /// component collection
        /// </summary>
        /// <param name="type"> Type of component </param>
        /// <returns> The chosen component of said type </returns>
        private Component GetComponent(Type type)
        {
            return components.FirstOrDefault(
                component => type.IsInstanceOfType(component));
        }

        /// <summary>
        /// Method to be used in conditions to try get a specific component
        /// </summary>
        /// <typeparam name="T">Type of the desired component</typeparam>
        /// <param name="component"> out component to be used in case of true
        /// </param>
        /// <returns> Returns true if desired Component
        /// from component collection exists, if not, returns false and 
        /// a null object
        /// </returns>
        public bool TryGetComponent<T>(out T component) where T : Component
        {
            if (components.Any(aux => aux is T))
            {
                component = components.FirstOrDefault(comp => comp is T) as T;

                return true;
            }

            component = null;

            return false;
        }

        /// <summary>
        /// Initialize all components in this game object
        /// </summary>
        public virtual void Start()
        {
            foreach (Component component in components)
            {
                component.Start();
            }
        }

        /// <summary>
        /// Update all components in this game object
        /// </summary>
        public virtual void Update()
        {
            foreach (Component component in components)
            {
                component.Update();
            }
        }

        /// <summary>
        /// Tear down all components in this game object
        /// </summary>
        public void Finish()
        {
            foreach (Component component in components)
            {
                component.Finish();
            }

            Dispose();
        }

        // The methods below are required for implementing the IEnumerable<T>
        // interface

        /// <summary>
        /// Go through all components in this game object
        /// </summary>
        /// <returns> The enumerator for each component </returns>
        public IEnumerator<Component> GetEnumerator()
        {
            return components.GetEnumerator();
        }

        /// <summary>
        /// Required method for IEnumerable<T> implementation
        /// </summary>
        /// <returns> Enumerator </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Method to implement IDisposable

        /// <summary>
        /// Required method for IDisposable implementation. 
        /// Clears collection of the game object's components and 
        /// set bools to false so the ConsoleRenderer wont crash
        /// </summary>
        public virtual void Dispose()
        {
            components.Clear();
            containsRenderableComponent = false;
            containsTransform = false;
            containsCollider = false;
        }
    }
}
