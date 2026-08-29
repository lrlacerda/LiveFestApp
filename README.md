# 🎉 LiveFestApp

**LiveFestApp** é uma aplicação full stack para descoberta de eventos locais (shows, festas, festivais). O projeto é composto por um app mobile em **React Native (Expo)** e uma **API REST em ASP.NET Core (.NET 8)** com banco de dados SQL Server, responsável por autenticação, cadastro de eventos, categorias, avaliações e favoritos.

O repositório contém dois projetos independentes:

```
LiveFestApp/
├── LiveFest/    # App mobile (React Native + Expo)
└── WebAPI/      # API REST (ASP.NET Core)
```

## Funcionalidades

- **Descoberta de eventos**: listagem de eventos próximos, com busca e navegação por categorias.
- **Mapa interativo**: visualização dos eventos no mapa (Google Maps) e traçado de rota até o local.
- **Detalhes do evento**: página com informações completas de cada evento.
- **Favoritos**: marcar/desmarcar eventos como favoritos.
- **Avaliações**: cadastro de avaliações vinculadas a um evento.
- **Autenticação de usuário**: criação de conta, login (JWT), verificação por código e recuperação de senha por e-mail.
- **Cadastro de eventos**: criação de eventos com upload de imagem para o Azure Blob Storage.

## Tecnologias

### App mobile (`LiveFest/`)

| Tecnologia | Uso |
|---|---|
| React Native + Expo (`~51`) | Base do app mobile |
| React Navigation (bottom-tabs + native-stack) | Navegação entre telas |
| `react-native-maps` / `react-native-maps-directions` | Mapa e rotas |
| `axios` | Consumo da API |
| `expo-location`, `expo-camera`, `expo-image-picker` | Localização, câmera e seleção de imagens |
| `jwt-decode` | Leitura do token de autenticação |
| `styled-components` | Estilização dos componentes |
| TypeScript | Tipagem (parcial) |

### API (`WebAPI/`)

| Tecnologia | Uso |
|---|---|
| ASP.NET Core (.NET 8) | Framework da API |
| Entity Framework Core + SQL Server | Persistência de dados e migrations |
| JWT Bearer Authentication | Autenticação |
| BCrypt.Net-Next | Hash de senhas |
| MailKit / MimeKit | Envio de e-mails (verificação e recuperação de senha) |
| Azure.Storage.Blobs | Upload de imagens dos eventos |
| Swashbuckle (Swagger) | Documentação/exploração da API |

## Estrutura do projeto

**App mobile — telas principais (`LiveFest/src/screens`):**
Home, Login, CreateAccount, EmailVerification, VerificationCode, PasswordRecover, PasswordReset, RegistrationSuccessful, Main, Categories, SelectedCategory, DetailedCard, MapNearby, Favorites, Splash, Onboarding.

**API — controllers (`WebAPI/LiveFest/Controllers`):**
`EventsController`, `CategoriesController`, `AddressController`, `EvaluationsController`, `SaveEventsController`, `UsersController`, `LoginController`, `RecoveryPasswordController`, `SendEmailController`.

## Principais endpoints da API

| Recurso | Endpoint | Descrição |
|---|---|---|
| Eventos | `POST /api/Events` | Cria um evento (com upload de imagem) |
| | `GET /api/Events` | Lista todos os eventos |
| | `GET /api/Events/GetById` | Busca evento por id |
| | `GET /api/Events/GetByCategory` | Lista eventos por categoria |
| | `DELETE /api/Events/{id}` | Remove um evento |
| Categorias | `POST /api/Categories`, `GET /api/Categories`, `GET /api/Categories/GetById` | CRUD básico de categorias |
| Endereços | `POST /api/Address`, `GET /api/Address`, `GET /api/Address/GetById` | CRUD básico de endereços |
| Avaliações | `POST /api/Evaluations`, `GET /api/Evaluations/GetById`, `GET /api/Evaluations/GetByEvent` | Avaliações de eventos |
| Favoritos | `GET /api/SaveEvents/All`, `POST /api/SaveEvents/Create`, `DELETE /api/SaveEvents/Delete` | Gerenciar eventos favoritos |
| Usuários | `POST /api/Users`, `GET /api/Users/GetById`, `PUT /api/Users/UpdatePassword` | Cadastro e atualização de usuário |
| Login | `POST /api/Login` | Autenticação (JWT) |
| Recuperação de senha | `POST /api/RecoveryPassword`, `POST /api/RecoveryPassword/RecoveryPassword` | Fluxo de recuperação de senha |
| E-mail | `POST /api/SendEmail` | Envio de e-mails transacionais |

## Como executar

### Pré-requisitos

- Node.js e npm
- [Expo CLI](https://docs.expo.dev/) (`npx expo`)
- .NET 8 SDK
- SQL Server (local ou remoto)

### App mobile

```bash
cd LiveFest
npm install
npm start        # ou: npx expo start
```

Outros scripts disponíveis: `npm run android`, `npm run ios`, `npm run web`.

### API

```bash
cd WebAPI/LiveFest
dotnet restore
dotnet ef database update   # aplica as migrations no SQL Server
dotnet run
```

Configure a connection string do SQL Server, as credenciais de e-mail (MailKit) e as chaves do Azure Blob Storage em `appsettings.json` / `appsettings.Development.json` antes de subir a API. A URL base consumida pelo app mobile é definida em `LiveFest/src/service/service.js`.

## Contato

Dúvidas ou sugestões: lribeirolacerda@gmail.com
