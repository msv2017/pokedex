using System.Threading.Tasks;
using Pokedex.Domain.Dtos;

namespace Pokedex.Application
{
    public interface IPokemonService
    {
        Task<PokemonDto> GetPokemonByNameAsync(string name);
        Task<PokemonDto> GetTranslatedPokemonByNameAsync(string name);
    }
}
