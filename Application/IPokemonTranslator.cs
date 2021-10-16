using Pokedex.Domain.Dtos;
using Pokedex.Proxies;

namespace Pokedex.Application
{
    public interface IPokemonTranslator
    {
        bool CanTranslate(PokemonDto pokemon);

        Translator Translator { get; }

        int Index { get; }
    }
}
