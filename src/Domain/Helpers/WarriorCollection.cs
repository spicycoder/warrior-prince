namespace Domain.Helpers
{
    using Models;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="WarriorCollection" />
    /// </summary>
    public static class WarriorCollection
    {
        /// <summary>
        /// Gets or sets the Warriors
        /// </summary>
        public static List<Warrior> Warriors { get; set; } = new List<Warrior>();
    }
}
