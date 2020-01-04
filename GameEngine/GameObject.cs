﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine
{
    /// <summary>
    /// Generic class to instantiate gameObjects
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

        //Is this game object renderable?
        public bool IsRenderable =>
            containsRenderableComponent && containsPosition;

        // Is the game object collidable?
        public bool IsCollidable => containsPosition && containsCollider;

        // Helper variables for the IsRenderable property
        private bool
            containsRenderableComponent, containsPosition, containsCollider;

        // Components which a game object can only have one of
        private static readonly Type[] oneOfAKind = new Type[]
        {
            typeof(Transform),
            typeof(KeyObserver),
            typeof(RenderableComponent),
            typeof(AbstractCollider)
        };

        // The components in this game object
        private readonly ICollection<Component> components;

        /// <summary>
        /// GameObject constructor, accepts nothing
        /// </summary>
        public GameObject()
        {
            components = new List<Component>();
            containsRenderableComponent = false;
            containsPosition = false;
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
            containsPosition = false;
            containsCollider = false;
            Name = name;
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

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetComponent<T>() where T : Component
        {
            // TODO: Use dictionary for one of a kind game objects
            // to speed up this search

            return components.FirstOrDefault(component => component is T) as T;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private Component GetComponent(Type type)
        {
            // TODO: Use dictionary for one of a kind game objects
            // to speed up this search

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

        // Initialize all components in this game object
        public virtual void Start()
        {
            foreach (Component component in components)
            {
                component.Start();
            }
        }

        // Update all components in this game object
        public virtual void Update()
        {
            foreach (Component component in components)
            {
                component.Update();
            }
        }

        // Tear down all components in this game object
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

        // Go through all components in this game object
        public IEnumerator<Component> GetEnumerator()
        {
            return components.GetEnumerator();
        }

        // Required for IEnumerable<T> implementation
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Method to implement IDisposable
        public virtual void Dispose()
        {
            components.Clear();
            containsRenderableComponent = false;
            containsPosition = false;
            containsCollider = false;
        }
    }
}
