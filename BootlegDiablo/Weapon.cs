namespace BootlegDiablo
{
    /// <summary>
    /// Abstract class weapon to be a template for all the weapons
    /// </summary>
    public abstract class Weapon
    {
        /// <summary>
        /// Max Damage of weapon can make
        /// </summary>
        public int MaxDamage { get; set; }

        /// <summary>
        /// Minimum damage weapon can make
        /// </summary>
        public int MinDamage { get; set; }

        /// <summary>
        /// Durability of weapon
        /// </summary>
        public int Durability { get; set; }

        /// <summary>
        /// Name of weapon
        /// </summary>
        public string Name { get; set; }
    }
}
