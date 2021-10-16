using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pokedex.Application;
using Pokedex.Domain.Dtos;

namespace Pokedex.Controllers
{
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;

        public PokemonController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        [HttpGet("pokemon/{name}")]
        public async Task<PokemonDto> GetPokemonByName(string name)
        {
            return await _pokemonService.GetPokemonByNameAsync(name);
        }

        [HttpGet("pokemon/translated/{name}")]
        public async Task<PokemonDto> GetTranslatedPokemonByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
