using System;
using System.Numerics;

namespace GameEngine
{
    /// <summary>
    /// Class responsible for collision detection of a game object, to be used
    /// as a game object component
    /// </summary>
    public class ObjectCollider : AbstractCollider
    {
        /// <summary>
        /// Position of the collider
        /// </summary>
        public override Vector2 ColPos { get; set; }

        /// <summary>
        /// Parent game object transform information
        /// </summary>
        private Transform _transform;

        /// <summary>
        /// Bool to check if ColPos is to be updated
        /// </summary>
        private readonly bool _update;

        /// <summary>
        /// Empty constructor to be used in objects that move
        /// </summary>
        public ObjectCollider()
        {
            _update = true;
        }

        /// <summary>
        /// Constructor used with objects that occupy larger space and/or won't
        /// update their position
        /// </summary>
        /// <param name="pos"> Collider initial position </param>
        /// <param name="update"> Optional: Bool to decide if collider
        /// positon will update or not </param>
        public ObjectCollider(Vector2 pos, bool update = false)
        {
            ColPos = pos;
            _update = update;
        }

        /// <summary>
        /// Method to run at the beginning of a scene
        /// </summary>
        public override void Start()
        {
            _transform = ParentGameObject.GetComponent<Transform>();

            if (_transform == null)
            {
                throw new InvalidOperationException(
                    $"The {nameof(ObjectCollider)} component " +
                    $"requires a {nameof(Transform)} component");
            }
        }

        /// <summary>
        /// Method to update collider postion if bool value is true
        /// </summary>
        public override void Update()
        {
            if (_update)
            {
                ColPos = new Vector2(
                (int)_transform.Pos.X,
                (int)_transform.Pos.Y);
            }
        }
    }
}
