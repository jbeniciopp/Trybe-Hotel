# Trybe Hotel

## O que foi desenvolvido
- Foi desenvolvido um back-end em C# e ASP.NET Core de um software de booking de várias redes de hotéis.

## Habilidades trabalhadas
- Entender do funcionamento do ASP.NET e como ele se integra ao C#.
- Entender do funcionamento do banco de dados SQL Server.
- Criar operações de manipulação de banco de dados em uma API.

## Como instalar
1. Clone o repositório

  - Use o comando:
    - `git clone git@github.com:jbeniciopp/Trybe-Hotel.git`.
  - Entre na pasta do repositório que você acabou de clonar:
    - `cd Trybe-Hotel`

2. Instale as dependências

  - Execute o comando: `dotnet restore`.

## Como rodar o projeto
- Use o comando para iniciar o Banco de Dados:
  - `docker compose up -d --build`
- Use o comando para o dotnet fazer as migrations:
  - `dotnet ef migrations add InitialCreate`
- Use o comando para criar as tabelas do Banco de Dados:
  - `dotnet ef database update`
- Use o comando para iniciar a API:
  - `dotnet run`

## Endpoints

### GET /
- Retorno:
  - status: `200`
```json
{
  "message": "online"
}
```

### GET /city
- Retorno:
  - status: `200`
```json
[
  {
    "cityId": 1,
    "name": "Rio Branco",
    "state": "AC"
  },
  /*...*/
]
```

### POST /city
- O corpo da requisição deve seguir o padrão abaixo:
```json
{
  "Name": "Rio de Janeiro",
  "State": "RJ"
}
```

- Retorno:
  - status: `201`
```json
{
  "cityId": 2,
  "name": "Rio de Janeiro",
  "state": "RJ"
}
```

### PUT /city
- O corpo da requisição deve seguir o padrão abaixo:
```json
{
  "CityId": 1,
  "Name": "Rio de Janeiro",
  "State": "RJ"
}
```

- Retorno:
  - status: `200`
```json
{
  "cityId": 1,
  "name": "Rio de Janeiro",
  "state": "RJ"
}
```

### GET /hotel
- Retorno:
  - status: `200`
```json
[
  {
    "hotelId": 1,
    "name": "Trybe Hotel SP",
    "address": "Avenida Paulista, 1400",
    "cityId": 1,
    "cityName": "São Paulo",
    "state": "SP"
  },
  /*...*/
]
```

### POST /hotel
- Este endpoint pode ser acessado somente se o token de um admin estiver no Bearer da requisição.

- O corpo da requisição deve seguir o padrão abaixo:
```json
{
  "Name":"Trybe Hotel RJ",
  "Address":"Avenida Atlântica, 1400",
  "CityId": 2
}
```

- Retorno:
  - status: `201`
```json
{
  "hotelId": 2,
  "name": "Trybe Hotel RJ",
  "address": "Avenida Atlântica, 1400",
  "cityId": 2,
  "cityName": "Rio de Janeiro",
  "state": "RJ"
}
```

### GET /room/:hotelId
- O hotelId deve ser o id do Hotel desejado.

- Retorno:
  - status: `200`
```json
[
  {
    "roomId": 1,
    "name": "Suite básica",
    "capacity": 2,
    "image": "image suite",
    "hotel": {
      "hotelId": 1,
      "name": "Trybe Hotel SP",
      "address": "Avenida Paulista, 1400",
      "cityId": 1,
      "cityName": "São Paulo",
      "state": "SP"
    }
  },
  /*...*/
]
```

### POST /room
- Este endpoint pode ser acessado somente se o token de um admin estiver no Bearer da requisição.

- O corpo da requisição deve seguir o padrão abaixo:
```json
{
  "Name":"Suite básica",
  "Capacity":2,
  "Image":"image suite",
  "HotelId": 1
}
```

- Retorno:
  - status: `201`
```json
{
  "roomId": 1,
  "name": "Suite básica",
  "capacity": 2,
  "image": "image suite",
  "hotel": {
    "hotelId": 1,
    "name": "Trybe Hotel SP",
    "address": "Avenida Paulista, 1400",
    "cityId": 1,
    "cityName": "São Paulo",
    "state": "SP"
  }
}
```

### DELETE /room/:roomId
- Este endpoint pode ser acessado somente se o token de um admin estiver no Bearer da requisição.

- O roomId deve ser o id do Quarto desejado para exclusão.

- Caso o roomId seja invalido o Retorno será o status: `400`

- Retorno:
  - status: `204`

### POST /user
- O corpo da requisição deve seguir o padrão abaixo:
```json
{
  "Name":"João Benício",
  "Email": "j.beniciopp@gmail.com",
  "Password": "123456"
}
```

- Caso o e-mail seja repetido, a pessoa usuária não será cadastrada.
  - A resposta será o status `409`.
```json
{
  "message": "User email already exists"
}
```

- Retorno:
  - status: `201`
```json
{
  "userId": 1,
  "name":"João Benício",
  "email": "j.beniciopp@gmail.com",
  "userType": "client"
}
```

### GET /user
- Este endpoint pode ser acessado somente se o token de um admin estiver no Bearer da requisição.

- Retorno:
  - status: `200`
```json
[
  {
    "userId": 1,
    "name":"João Benício",
    "email": "j.beniciopp@gmail.com",
    "userType": "client"
  }, 
  /*...*/
]
```

### POST /login
- O corpo da requisição deve seguir o padrão abaixo:
```json
{
  "Email": "j.beniciopp@gmail.com",
  "Password": "123456"
}
```

- Caso a combinação e-mail e senha não existam:
  - A resposta será o status `401`.
```json
{
  "message": "Incorrect e-mail or password"
}
```

- Retorno:
  - status: `200`
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.   eyJyb2xlIjoiYWRtaW4iLCJlbWFpbCI6ImRhbmlsby5zaWx2YUBiZXRyeWJlLmNvbSIsIm5iZiI6MTY4ODQxMTIxMiwiZXhwIjoxNjg4NDk3NjEyLCJpYXQiOjE2ODg0MTEyMTJ9. q1cNj2_xspeQC6Uz1maV79P95hVtWH4Z7auZgOen-Qo",
}
```

### POST /booking
- O corpo da requisição deve seguir o padrão abaixo:
```json
{
  "CheckIn":"2030-08-27",
  "CheckOut":"2030-08-28",
  "GuestQuant":"1",
  "RoomId":1
}
```

- Caso GuestQuant for maior que a capacidade do quarto:
  - A resposta será o status `400`.
```json
{
  "message": "Guest quantity over room capacity"
}
```

- Retorno:
  - status: `201`
```json
{
  "bookingId": 1,
  "checkIn": "2030-08-27T00:00:00",
  "checkOut": "2030-08-28T00:00:00",
  "guestQuant": 1,
  "room": {
    "roomId": 1,
    "name": "Suite básica",
    "capacity": 2,
    "image": "image suite",
    "hotel": {
      "hotelId": 1,
      "name": "Trybe Hotel RJ",
      "address": "Avenida Atlântica, 1400",
      "cityId": 1,
      "cityName": "Rio de Janeiro",
      "state": "RJ"
    }
  }
}
```

### GET /booking
- Este endpoint pode ser acesado somente se o token de quem fez a reserva estiver no Bearer da requisição.

- Retorno:
  - status: `200`
```json
{
  "bookingId": 1002,
  "checkIn": "2023-08-27T00:00:00",
  "checkOut": "2023-08-28T00:00:00",
  "guestQuant": 1,
  "room": {
    "roomId": 1,
    "name": "Suite básica",
    "capacity": 2,
    "image": "image suite",
    "hotel": {
      "hotelId": 1,
      "name": "Trybe Hotel RJ",
      "address": "Avenida Atlântica, 1400",
      "cityId": 1,
      "cityName": "Rio de Janeiro",
      "state": "RJ"
    }
  }
}
```

### GET /geo/status
- Este endpoint é responsável por conferir o status da api externa responsável pela geolocalização.

- Retorno:
  - status: `200`
```json
{
  "status": 0,
  "message": "OK",
  "data_updated": "2020-05-04T14:47:00+00:00",
  "software_version": "3.6.0-0",
  "database_version": "3.6.0-0"
}
```

### GET /geo/address
- Este endpoint é responsável por trazer os hotéis ordenados por distância de um endereço (ordem crescente de distância).

- O corpo da requisição deve seguir o padrão abaixo:
```json
{
  "Address":"Rua Arnaldo Barreto",
  "City":"Campinas",
  "State":"SP"
}
```

- Retorno:
  - status: `200`
```json
[
  {
    "hotelId": 2,
    "name": "Trybe Hotel SP",
    "address": "Avenida Paulista, 2000",
    "cityName": "São Paulo",
    "state": "SP",
    "distance": 82
  },
  {
    "hotelId": 1,
    "name": "Trybe Hotel RJ",
    "address": "Avenida Atlântica, 1400",
    "cityName": "Rio de Janeiro",
    "state": "RJ",
    "distance": 399
  },
  /* ... */
]
```

## OBS
- Este projeto também está pronto para deploy, pois inclui um arquivo Dockerfile que está totalmente configurado.
- Também deve ser feito os passos descritos nos arquivos:
  - `Program.cs`
  - `docker-compose.yml`
  - `Repository/TrybeHotelContext.cs`