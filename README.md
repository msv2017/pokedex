

# Content
1. [Prerequisites](#prerequisites)
2. [Build](#build)
3. [Test](#test)
4. [Run locally](#run-locally)
5. [Run using docker cli](#run-the-app-using-docker-cli)
6. [Run using docker-compose](#run-the-app-using-docker-compose)
7. [API](#api)
8. [Try it out](#try-it-out)
9. [Troubleshooting](#troubleshooting)
10. [Make it production ready](#production-ready)

# Prerequisites
- Install [Docker Desktop](https://docs.docker.com/desktop/)
- Install [Docker Compose](https://docs.docker.com/compose/install/)
- Install [.NET Core](https://dotnet.microsoft.com/learn/dotnet/hello-world-tutorial/install)

# Build
In the solution root directory, run in terminal:
```sh
dotnet restore
dotnet build
```

# Test
In the solution root directory run in terminal:
```sh
dotnet test
```
At the end of this command we'll see this:
```
Passed!  - Failed:     0, Passed:     8, Skipped:     0, Total:     8, Duration: 150 ms
```
Which means all good and we can continue with the next steps.

# Run locally
In the solution directory do
1. Start redis container
    ```sh
    docker run -d p 6379:6379 --name redis redis
    ```
    The application expects to find redis at http://localhost:6379
2. In the terminal:
    ```sh
    dotnet run --project App
    ```
Application will be available at http://localhost:5000

To shut it down press `CTRL + C` on Windows or `Control + C` on Mac.
Then stop redis container and remove it:
```sh
docker stop redis
docker rm redis
```

# Run using docker cli
Navigate to the solution directory first, then follow these steps:
1. Build the docker image:
    ```sh
    docker build -t pokedex .
    ```
2. Start redis container
    ```sh
    docker run -d p 6379:6379 --name redis redis
    ```
    The application expects to find redis at http://localhost:6379
3. Start the application container:
    ```sh
    docker run -d -p 5000:80 --name pokedex pokedex
    ```
3. At this point, the application should sit at http://localhost:5000, and when we are done to shut it down:
    ```sh
    docker stop pokedex
    ```
Then stop redis container and remove it:
```sh
docker stop redis
docker rm redis
```
    
# Run using docker-compose
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

# Troubleshooting
If for some reason `docker-compose up -d` produces an error, it's probably because container named `pokedex` or `redis` already exist.
Following commands might help fix it:
```sh
docker rm pokedex
docker rm redis
```

# Make it production ready
Things to improve before going to production:
1. Add redirect from HTTP to HTTPS (plus signed certificates)
2. Add authentication and better authorization
3. Add swagger interface with proper examples
4. Add integration tests
5. Add terraform configuration

From infrastructure point of view we need to set up:
 1. VCS
 2. Issue & Project tracking
 3. CI/CD platform pipeline
 5. Cloud