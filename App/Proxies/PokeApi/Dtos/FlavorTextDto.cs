using Newtonsoft.Json;

namespace Pokedex.Proxies.Dtos
{
    public class FlavorTextDto
    {
        [JsonProperty("flavor_text")]
        public string FlavorText { get; set; }

        public LanguageDto Language { get; set; }

        public VersionDto Version { get; set; }
    }
}
