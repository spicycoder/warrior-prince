namespace Api.Http.Controllers
{
    using Domain.Helpers;
    using Domain.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="WarriorsController" />
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class WarriorsController : ControllerBase
    {
        /// <summary>
        /// The Create
        /// </summary>
        /// <param name="character">The character<see cref="CharacterTypes"/></param>
        /// <returns>The <see cref="ActionResult{Warrior}"/></returns>
        [HttpPost("create")]
        public ActionResult<Warrior> Create(CharacterTypes character)
        {
            var warrior = new Warrior(character);
            WarriorCollection.Warriors.Add(warrior);
            return Ok(warrior);
        }

        /// <summary>
        /// The Kill
        /// </summary>
        /// <param name="id">The id<see cref="string"/></param>
        /// <returns>The <see cref="ActionResult{Warrior}"/></returns>
        [HttpDelete("kill")]
        public ActionResult<Warrior> Kill(string id)
        {
            var warrior = WarriorCollection.Warriors.FirstOrDefault(w => w.Id == id);

            if (warrior == null)
            {
                return NotFound();
            }

            WarriorCollection.Warriors.Remove(warrior);
            return Ok(warrior);
        }

        /// <summary>
        /// The KillAll
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpDelete("kill-all")]
        public ActionResult KillAll()
        {
            WarriorCollection.Warriors.RemoveAll(_ => true);
            return Ok();
        }

        /// <summary>
        /// The Eat
        /// </summary>
        /// <param name="id">The id<see cref="string"/></param>
        /// <param name="food">The food<see cref="FoodTypes"/></param>
        /// <returns>The <see cref="ActionResult{Warrior}"/></returns>
        [HttpPut("eat")]
        public ActionResult<Warrior> Eat(string id, FoodTypes food)
        {
            var warrior = WarriorCollection.Warriors.FirstOrDefault(w => w.Id == id);

            if (warrior == null)
            {
                return NotFound();
            }

            warrior.Eat(food);
            return Ok(warrior);
        }

        /// <summary>
        /// The Drink
        /// </summary>
        /// <param name="id">The id<see cref="string"/></param>
        /// <param name="portion">The portion<see cref="PortionTypes"/></param>
        /// <returns>The <see cref="ActionResult{Warrior}"/></returns>
        [HttpPut("drink")]
        public ActionResult<Warrior> Drink(string id, PortionTypes portion)
        {
            var warrior = WarriorCollection.Warriors.FirstOrDefault(w => w.Id == id);

            if (warrior == null)
            {
                return NotFound();
            }

            warrior.Drink(portion);
            return Ok(warrior);
        }

        /// <summary>
        /// The Warrior
        /// </summary>
        /// <param name="id">The id<see cref="string"/></param>
        /// <returns>The <see cref="ActionResult{Warrior}"/></returns>
        [HttpGet("warrior")]
        public ActionResult<Warrior> Warrior(string id)
        {
            var warrior = WarriorCollection.Warriors.FirstOrDefault(w => w.Id == id);

            if (warrior == null)
            {
                return NotFound();
            }

            return Ok(warrior);
        }

        /// <summary>
        /// The AllWarriors
        /// </summary>
        /// <returns>The <see cref="ActionResult{Warrior}"/></returns>
        [HttpGet("all-warriors")]
        public ActionResult<Warrior> AllWarriors()
        {
            return Ok(WarriorCollection.Warriors);
        }
    }
}
