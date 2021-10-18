using AutoFixture;
using AutoFixture.AutoNSubstitute;
using FluentAssertions;
using Pokedex.Application;
using Pokedex.Domain.Dtos;
using Xunit;

namespace UnitTests
{
    public class ShakespearTranslatorTest
    {
        private readonly Fixture _fixture;

        public ShakespearTranslatorTest()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoNSubstituteCustomization());
        }

        [Fact]
        public void CanTranslate_AnyPokemon_ShouldReturnTrue()
        {
            // Arrange
            var pokemon = _fixture.Create<PokemonDto>();
            var sut = _fixture.Create<ShakespearTranslator>();

            // Act
            var result = sut.CanTranslate(pokemon);


            // Assert
            result.Should().Be(true);
        }
    }
}
