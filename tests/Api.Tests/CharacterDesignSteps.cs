namespace Api.Tests
{
    using Domain.Helpers;
    using Domain.Models;
    using FluentAssertions;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using RestSharp;
    using System;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Defines the <see cref="CharacterDesignSteps" />
    /// </summary>
    [Binding]
    public class CharacterDesignSteps
    {
        /// <summary>
        /// Defines the _warrior
        /// </summary>
        private Warrior _warrior;

        /// <summary>
        /// Base uri
        /// </summary>
        private static string _baseUri;

        /// <summary>
        /// The Initialize
        /// </summary>
        [BeforeFeature]
        public static void Initialize()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            _baseUri = config.GetSection("ApiSettings")["BaseUri"];
        }

        /// <summary>
        /// Given Create A New Ogre
        /// </summary>
        /// <param name="character">The character<see cref="CharacterTypes"/></param>
        [Given(@"Create a new (.*)")]
        public void GivenCreateANewOgre(CharacterTypes character)
        {
            var client = new RestClient($"{_baseUri}/api/Warriors/create?character={character}");
            var request = new RestRequest(Method.POST);
            var response = client.Execute(request);
            _warrior = JsonConvert.DeserializeObject<Warrior>(response.Content);
        }

        /// <summary>
        /// Then Ogre Health Must Be
        /// </summary>
        /// <param name="health">The health<see cref="int"/></param>
        [Then(@"Warrior Health must be (.*)")]
        public void ThenOgreHealthMustBe(int health)
        {
            _warrior.Health.Should().Be(health);
        }

        /// <summary>
        /// Then Ogre Magic Must Be
        /// </summary>
        /// <param name="magic">The magic<see cref="int"/></param>
        [Then(@"Warrior Magic must be (.*)")]
        public void ThenOgreMagicMustBe(int magic)
        {
            _warrior.Magic.Should().Be(magic);
        }
    }
}
