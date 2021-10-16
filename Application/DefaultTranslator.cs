using Pokedex.Domain.Dtos;
using Pokedex.Proxies;

namespace Pokedex.Application
{
    public class DefaultTranslator : IPokemonTranslator
    {
        public Translator Translator => Translator.Default;

        public int Index => int.MaxValue;

        public bool CanTranslate(PokemonDto pokemon) => true;
    }
}
