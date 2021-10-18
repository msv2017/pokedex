using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using FluentAssertions;
using NSubstitute;
using Pokedex.Application;
using Pokedex.Domain.Dtos;
using Pokedex.Proxies;
using Xunit;

namespace UnitTests
{
    public class PokemonServiceTests
    {
        private readonly Fixture _fixture;
        private readonly IPokeApiProxy _pokeApiProxy;
        private readonly IFunTranslationsApiProxy _funTranslationsApiProxy;

        public PokemonServiceTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoNSubstituteCustomization());

            _pokeApiProxy = _fixture.Freeze<IPokeApiProxy>();
            _funTranslationsApiProxy = _fixture.Freeze<IFunTranslationsApiProxy>();
        }

        /// <summary>
        /// We are simply testing that we call IPokeApiProxy
        /// </summary>
        /// <returns>Any pokemon that exists</returns>
        [Fact]
        public async Task GetPokemonByNameAsync_AnyState_ShouldReturnPokemon()
        {
            // Arrange
            var expected = _fixture.Create<PokemonDto>();
            _pokeApiProxy.GetPokemonByNameAsync(expected.Name).Returns(expected);

            var sut = _fixture.Create<PokemonService>();

            // Act
            var actual = await sut.GetPokemonByNameAsync(expected.Name);

            // Assert
            actual.Should().Be(expected);
            await _pokeApiProxy.Received(1).GetPokemonByNameAsync(expected.Name);
        }

        /// <summary>
        /// Case 1 - pokemon's habitat is cave
        /// </summary>
        /// <returns>Pokemon with yoda trasnlation as description</returns>
        [Fact]
        public async Task GetTranslatedPokemonByNameAsync_Case1_ShouldReturnYodaTranslation()
        {
            // Arrange
            var pokemon = _fixture.Create<PokemonDto>();
            pokemon.Habitat = "cave";
            pokemon.IsLegendary = false;

            var description = _fixture.Create<string>();
            var translator = _fixture.Create<YodaTranslator>();

            _fixture.Inject<IEnumerable<IPokemonTranslator>>(new[] { translator });

            _pokeApiProxy.GetPokemonByNameAsync(pokemon.Name).Returns(pokemon);
            _funTranslationsApiProxy.TranslateAsync(pokemon.Description, translator.Translator)
                .Returns(description);

            var sut = _fixture.Create<PokemonService>();

            // Act
            var translatedPokemon = await sut.GetTranslatedPokemonByNameAsync(pokemon.Name);

            // Assert
            translatedPokemon.Description.Should().Be(description);
        }

        /// <summary>
        /// Case 2 - pokemon is legendary
        /// </summary>
        /// <returns>Pokemon with yoda trasnlation as description</returns>
        [Fact]
        public async Task GetTranslatedPokemonByNameAsync_Case2_ShouldReturnYodaTranslation()
        {
            // Arrange
            var pokemon = _fixture.Create<PokemonDto>();
            pokemon.IsLegendary = true;

            var description = _fixture.Create<string>();
            var translator = _fixture.Create<YodaTranslator>();

            _fixture.Inject<IEnumerable<IPokemonTranslator>>(new[] { translator });

            _pokeApiProxy.GetPokemonByNameAsync(pokemon.Name).Returns(pokemon);
            _funTranslationsApiProxy.TranslateAsync(pokemon.Description, translator.Translator)
                .Returns(description);

            var sut = _fixture.Create<PokemonService>();

            // Act
            var translatedPokemon = await sut.GetTranslatedPokemonByNameAsync(pokemon.Name);

            // Assert
            translatedPokemon.Description.Should().Be(description);
        }

        /// <summary>
        /// Case 3 - pokemon's habitat is not cave and he's not legendary
        /// </summary>
        /// <returns>Pokemon with shakespear trasnlation as description</returns>
        [Fact]
        public async Task GetTranslatedPokemonByNameAsync_Case3_ShouldReturnShakespearTranslation()
        {
            // Arrange
            var pokemon = _fixture.Create<PokemonDto>();
            _pokeApiProxy.GetPokemonByNameAsync(pokemon.Name).Returns(pokemon);

            var description = _fixture.Create<string>();
            var translator = _fixture.Create<ShakespearTranslator>();

            _fixture.Inject<IEnumerable<IPokemonTranslator>>(new[] { translator });

            _pokeApiProxy.GetPokemonByNameAsync(pokemon.Name).Returns(pokemon);
            _funTranslationsApiProxy
                .TranslateAsync(pokemon.Description, translator.Translator)
                .Returns(description);

            var sut = _fixture.Create<PokemonService>();

            // Act
            var translatedPokemon = await sut.GetTranslatedPokemonByNameAsync(pokemon.Name);

            // Assert
            translatedPokemon.Description.Should().Be(description);
        }
    }
}
