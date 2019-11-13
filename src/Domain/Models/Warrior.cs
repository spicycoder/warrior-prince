namespace Domain.Models
{
    using Helpers;
    using System;

    /// <summary>
    /// Defines the <see cref="Warrior" />
    /// </summary>
    public class Warrior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Warrior"/> class.
        /// </summary>
        /// <param name="character">The character<see cref="CharacterTypes"/></param>
        public Warrior(CharacterTypes character)
        {
            Id = Guid.NewGuid().ToString();
            CharacterType = character;

            switch (character)
            {
                case CharacterTypes.Elf:
                    Health = 80;
                    Magic = 100;
                    break;
                case CharacterTypes.Ogre:
                    Health = 100;
                    Magic = 80;
                    break;
            }
        }

        /// <summary>
        /// Gets the Warrior Id
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets the Character Type
        /// </summary>
        public CharacterTypes CharacterType { get; private set; }

        /// <summary>
        /// Gets the Health
        /// </summary>
        public int Health { get; private set; }

        /// <summary>
        /// Gets the Magic
        /// </summary>
        public int Magic { get; private set; }

        /// <summary>
        /// Warrior eats food
        /// </summary>
        /// <param name="food">The food<see cref="FoodTypes"/></param>
        public void Eat(FoodTypes food)
        {
            switch (food)
            {
                case FoodTypes.Carrot:
                    Health += 10;
                    break;
                case FoodTypes.Bread:
                    Health += 20;
                    break;
            }
        }

        /// <summary>
        /// Warrior drinks a portion
        /// </summary>
        /// <param name="portion">The portion<see cref="PortionTypes"/></param>
        public void Drink(PortionTypes portion)
        {
            switch (CharacterType)
            {
                case CharacterTypes.Elf:
                    switch (portion)
                    {
                        case PortionTypes.Wine:
                            Health -= 20;
                            break;
                        case PortionTypes.Poison:
                            Health += 40;
                            break;
                    }
                    break;
                case CharacterTypes.Ogre:
                    switch (portion)
                    {
                        case PortionTypes.Wine:
                            Health += 40;
                            break;
                        case PortionTypes.Poison:
                            Health -= 20;
                            break;
                    }
                    break;
            }
        }
    }
}
