using System.Collections.Generic;
using Newtonsoft.Json;

namespace Pokedex.Proxies.Dtos
{
    public class PokemonSpecimenDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonProperty("is_legendary")]
        public bool IsLegendary { get; set; }

        public HabitatDto Habitat { get; set; }

        [JsonProperty("flavor_text_entries")]
        public List<FlavorTextDto> FlavorTextEntries { get; set; }
    }
}
