using GameEngine;
using System.Numerics;

namespace BootlegDiablo
{
    /// <summary>
    /// General enemy class, to be used as base class to all enemies
    /// </summary>
    public abstract class Enemy : GameObject
    {
        /// <summary>
        /// Enemy's hit points
        /// </summary>
        public int HP { get; set; }

        /// <summary>
        /// Enemy's damage values
        /// </summary>
        public int Damage { get; set; }

        /// <summary>
        /// Variable to get player's Transform information
        /// </summary>
        private Transform _plrTrans;

        /// <summary>
        /// Variable to store instance Tranform information
        /// </summary>
        private Transform _selfTrans;

        /// <summary>
        /// Variable to store an instance of Player that exists in the 
        /// game scene to access player's life when attacking
        /// </summary>
        public Player plr;

        /// <summary>
        /// Instance left side position to be used in the attack method
        /// </summary>
        public Vector2 leftSide;

        /// <summary>
        /// Instance right side position to be used in the attack method
        /// </summary>
        public Vector2 rightSide;

        /// <summary>
        /// Instance up side position to be used in the attack method
        /// </summary>
        public Vector2 upSide;

        /// <summary>
        /// Instance down side position to be used in the attack method
        /// </summary>
        public Vector2 downSide;

        /// <summary>
        /// Instance actual position to be used in the attack method
        /// </summary>
        public Vector2 actPos;

        /// <summary>
        /// Player's position to be used in the attack method
        /// </summary>
        public Vector2 plrPos;

        /// <summary>
        /// Override method of base class Start()
        /// </summary>
        public override void Start()
        {
            base.Start();

            _selfTrans = GetComponent<Transform>();
            plr = ParentScene.FindGameObjectByName("Player") as Player;
            _plrTrans = plr.GetComponent<Transform>();
        }

        /// <summary>
        /// Enemy's override to Update method
        /// </summary>
        public override void Update()
        {
            base.Update();

            // Gets right side values
            rightSide = new Vector2(
                (int)_selfTrans.Pos.X + 1,
                (int)_selfTrans.Pos.Y);
            // Gets left side values
            leftSide = new Vector2(
                (int)_selfTrans.Pos.X - 1,
                (int)_selfTrans.Pos.Y);
            // Gets up side values
            upSide = new Vector2(
                (int)_selfTrans.Pos.X,
                (int)_selfTrans.Pos.Y - 1);
            // Gets down side values
            downSide = new Vector2(
                (int)_selfTrans.Pos.X,
                (int)_selfTrans.Pos.Y + 1);
            // Gets instance actual position values
            actPos = new Vector2(
                (int)_selfTrans.Pos.X,
                (int)_selfTrans.Pos.Y);
            // Gets player position values
            plrPos = new Vector2(
                (int)_plrTrans.Pos.X,
                (int)_plrTrans.Pos.Y);

            // Enemy death
            if (HP <= 0)
            {
                Finish();
            }
        }

        /// <summary>
        /// Virtual method that contains bbasic attack procedure for enemies
        /// </summary>
        public virtual void Attack()
        {
            // Check to stop enemies from hitting in the first frame
            if (actPos != new Vector2(0,0))
            {
                // If player is the same position
                if (actPos == plrPos)
                {
                    plr.Life -= Damage;
                }
                // If player is to the left
                if (leftSide == plrPos)
                {
                    plr.Life -= Damage;
                }
                // If player is to the right
                if (rightSide == plrPos)
                {
                    plr.Life -= Damage;
                }
                // If player is on top
                if (upSide == plrPos)
                {
                    plr.Life -= Damage;
                }
                // If player is bellow
                if (downSide == plrPos)
                {
                    plr.Life -= Damage;
                } 
            }
        }
    }
}
