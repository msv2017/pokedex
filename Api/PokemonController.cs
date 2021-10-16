﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pokedex.Application;
using pokedex.Domain.Dtos;

namespace pokedex.Controllers
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
