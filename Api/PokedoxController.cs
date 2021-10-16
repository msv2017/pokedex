using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pokedex.Domain.Models;

namespace pokedex.Controllers
{
    [ApiController]
    public class PokemonController : ControllerBase
    {
        public PokemonController()
        {
        }

        [HttpGet("pokemon/{name}")]
        public async Task<PokemonDto> GetPokemon(string name)
        {
            throw new NotImplementedException();
        }

        [HttpGet("pokemon/translated/{name}")]
        public async Task<PokemonDto> GetTranslatedPokemon(string name)
        {
            throw new NotImplementedException();
        }
    }
}
