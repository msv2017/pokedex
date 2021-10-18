using AutoFixture;
using AutoFixture.AutoNSubstitute;
using FluentAssertions;
using Pokedex.Application;
using Pokedex.Domain.Dtos;
using Xunit;

namespace UnitTests
{
    public class YodaTranslatorTest
    {
        private readonly Fixture _fixture;

        public YodaTranslatorTest()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoNSubstituteCustomization());
        }

        [Fact]
        public void CanTranslate_HabitatIsCave_ShouldReturnTrue()
        {
            // Arrange
            var pokemon = _fixture.Create<PokemonDto>();
            pokemon.Habitat = "cave";
            pokemon.IsLegendary = false;

            var sut = _fixture.Create<YodaTranslator>();

            // Act
            var result = sut.CanTranslate(pokemon);


            // Assert
            result.Should().Be(true);
        }

        [Fact]
        public void CanTranslate_PokemonIsLegendary_ShouldReturnTrue()
        {
            // Arrange
            var pokemon = _fixture.Create<PokemonDto>();
            pokemon.IsLegendary = true;

            var sut = _fixture.Create<YodaTranslator>();

            // Act
            var result = sut.CanTranslate(pokemon);


            // Assert
            result.Should().Be(true);
        }

        [Fact]
        public void CanTranslate_HabitatIsCaveAndPokemonIsLegendary_ShouldReturnTrue()
        {
            // Arrange
            var pokemon = _fixture.Create<PokemonDto>();
            pokemon.Habitat = "cave";
            pokemon.IsLegendary = true;

            var sut = _fixture.Create<YodaTranslator>();

            // Act
            var result = sut.CanTranslate(pokemon);


            // Assert
            result.Should().Be(true);
        }
    }
}
