using Pokedex.Domain.Dtos;
using Pokedex.Proxies;

namespace Pokedex.Application
{
    public class ShakespearTranslator : IPokemonTranslator
    {
        public Translator Translator => Translator.Shakespeare;

        public int Index => 1;

        public bool CanTranslate(PokemonDto pokemon) => true;
    }
}
