namespace BootlegDiablo
{
    /// <summary>
    /// Class shortSword who inherits from weapon
    /// </summary>
    public class ShortSword : Weapon
    {
        /// <summary>
        /// Constructor to instantiate with preset values
        /// </summary>
        public ShortSword()
        {
            MinDamage = 2;
            MaxDamage = 6;
            Durability = 24;
            Name = "Short Sword";
        }
    }
}
