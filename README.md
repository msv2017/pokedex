# Prerequisites
- Install [Docker Desktop](https://docs.docker.com/desktop/)
- Install [Docker Compose](https://docs.docker.com/compose/install/)
- Install [.NET Core](https://dotnet.microsoft.com/learn/dotnet/hello-world-tutorial/install)

# Build
In the solution root directory, run in terminal:
```sh
dotnet build
```

# Run locally
In the solution directory, run in terminal:
```sh
dotnet run
```
Application will be available at http://localhost:5000

To shut it down press `CTRL + C` on Windows or `Control + C` on Mac.

# Run as docker container
Navigate to the solution directory first, then follow these steps:
1. Build the docker image:
    ```sh
    docker build -t pokedex .
    ```
2. Start the application container:
    ```sh
    docker run -d -p 5000:80 --name pokedex pokedex
    ```
3. At this point, the application should sit at http://localhost:5000, and when we are done to shut it down:
    ```sh
    docker stop pokedex
    ```
    
# Run as docker container (docker-compose)
Navigate to the solution directory first, then follow these steps:
1. Build our service(s) and run them using docker-compose:
    ```sh
    docker-compose up -d
    ```
2. At this point, the application should sit at http://localhost:5000, and when we are done to shut it down:
    ```sh
    docker-compose down
    ```
    
# API
| Endpoint | Description |
| -------- | ----------- |
| `GET /pokemon/{name}` | Returns [PokemonDto](#pokemondto) |
| `GET /pokemon/translated/{name}` | Returns [PokemonDto](#pokemondto), where `description` is translated using [these rules](#translation-rules) |

#### Try it out
When the application is up and running at http://localhost:5000, try to hit two endpoints whatever tool available (`curl`, Postman etc.). We'll use `curl`:

| Command | Response |
| ------- | -------- |
|`curl -X GET http://localhost:5000/pokemon/mewtwo`|`{"id":150,"name":"mewtwo","description":"It was created by a scientist after years of horrific gene-splicing and DNA-engineering experiments.","habitat":"rare","isLegendary":true}`|
|`curl -X GET http://localhost:5000/pokemon/translated/mewtwo`|`{"id":150,"name":"mewtwo","description":"Created by a scientist after years of horrific gene-splicing and dna-engineering experiments,  it was.","habitat":"rare","isLegendary":true}`|

#### PokemonDto
```json
{
    "id": number,
    "name": "string",
    "description": "string",
    "habitat": "string",
    "isLegendary": bool
}
```

#### Translation rules

1. If the Pokemon's habitat is `cave` or it's legendary Pokemon then apply the [Yoda translation](#yoda-translation)
2. For all other Pokemon, apply the [Shakespear translation](#shakespear-translation)
3. If translation is not available (for any reason), the default description will be used

#### Yoda translation
https://api.funtranslations.com/translate/yoda
#### Shakespear translation
https://api.funtranslations.com/translate/shakespear

