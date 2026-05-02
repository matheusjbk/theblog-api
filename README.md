# TheBlog API

Uma API RESTful moderna construída com .NET 10, para gerenciamento de blog com autenticação JWT, upload de imagens e funcionalidades completas de CRUD para posts e usuários.

## 📋 Características

- **Autenticação JWT**: Sistema seguro de autenticação com tokens JWT
- **Gerenciamento de Usuários**: Criar e gerenciar usuários do blog
- **Gerenciamento de Posts**: Criar, atualizar, listar e deletar posts
- **Upload de Imagens**: Sistema integrado para upload de imagens associadas aos posts
- **Validações**: Validações robustas em todas as requisições
- **Rate Limiting**: Proteção contra abuso com rate limiting
- **CORS Habilitado**: Suporte a requisições de diferentes origens
- **Documentação OpenAPI**: Especificação OpenAPI/JSON disponível
- **Tratamento Global de Erros**: Tratamento centralizado e consistente de exceções

## 🏗️ Arquitetura

A aplicação segue uma arquitetura em camadas bem definida:

```
src/
├── TheBlog.API/              # Projeto principal - Apresentação e Controllers
├── TheBlog.Application/      # Casos de uso (Use Cases) e DTOs
├── TheBlog.Domain/           # Entidades de negócio e regras de domínio
└── TheBlog.Infra/            # Infraestrutura - Banco de dados, autenticação
```

### Camadas

- **TheBlog.API**: Controllers, filtros, middlewares e configuração da API
- **TheBlog.Application**: Lógica de aplicação, casos de uso e comunicação (requests/responses)
- **TheBlog.Domain**: Entidades, valores de domínio e interfaces
- **TheBlog.Infra**: EF Core, autenticação JWT, criptografia BCrypt

## 🛠️ Tecnologias

- **.NET 10**: Framework de desenvolvimento
- **Entity Framework Core 10**: ORM para acesso ao banco de dados
- **SQL Server 2025**: Banco de dados relacional
- **JWT (JSON Web Tokens)**: Autenticação e autorização
- **BCrypt.Net-Next**: Criptografia de senhas
- **Docker & Docker Compose**: Containerização
- **OpenAPI**: Especificação de documentação da API

## 📦 Requisitos

- Docker & Docker Compose instalados
- Porta 5000 disponível (backend)
- Porta 1433 disponível (SQL Server)

## 🚀 Como Iniciar

### Usando Docker Compose (Recomendado)

1. **Clone o repositório**:
   ```bash
   git clone https://github.com/matheusjbk/theblog-api.git
   cd theblog-api
   ```

2. **Configure as variáveis de ambiente** (opcional):

   Abra o arquivo `docker-compose.yml` e atualize as seguintes variáveis se desejar:
   - `MSSQL_SA_PASSWORD`: Senha do SQL Server (padrão: `YourSecretPassword123!`)
   - `ConnectionStrings__SqlServer`: String de conexão (ajuste o nome do banco de dados se necessário)

3. **Inicie os serviços**:
   ```bash
   docker-compose up -d
   ```

   Ou, para ver os logs em tempo real:
   ```bash
   docker-compose up
   ```

4. **Aguarde o inicialização completa**:

   O serviço de saúde do SQL Server aguardará até 50 segundos para que o banco de dados esteja pronto. O backend iniciará após o SQL Server estar operacional.

5. **Acesse a API**:

   - **API Backend**: http://localhost:5000
   - **Especificação OpenAPI**: http://localhost:5000/openapi/v1.json

### Desenvolvimento Local (sem Docker)

1. **Pré-requisitos**:
   - .NET 10 SDK instalado
   - SQL Server 2025 (ou versão compatível) instalado e rodando localmente

2. **Configure a string de conexão**:

   Edite `appsettings.json` em `src/TheBlog.API/` com suas credenciais de banco de dados:
   ```json
   {
     "ConnectionStrings": {
       "SqlServer": "Server=localhost;Database=YourDatabaseName;User Id=sa;Password=YourPassword;TrustServerCertificate=True;"
     }
   }
   ```

3. **Restaure dependências e execute migrations**:
   ```bash
   dotnet restore
   cd src/TheBlog.API
   dotnet ef database update
   ```

4. **Inicie a aplicação**:
   ```bash
   dotnet run
   ```

## 📚 Endpoints Principais

### Autenticação

- `POST /auth/login` - Realizar login e obter token JWT

### Posts

- `POST /post/me` - Criar novo post (autenticado)
- `GET /post` - Listar todos os posts
- `GET /post/{slug}` - Obter post específico pelo slug
- `GET /post/me` - Listar posts do usuário autenticado
- `GET /post/me/{id}` - Obter post específico do usuário autenticado
- `PUT /post/{id}` - Atualizar post (autenticado)
- `DELETE /post/{id}` - Deletar post (autenticado)

### Usuários

- `POST /user` - Criar novo usuário
- `GET /user/{id}` - Obter dados do usuário

### Upload de Imagens

- `POST /upload` - Upload de imagem para post (autenticado)

## 🔐 Autenticação

A API utiliza autenticação JWT (JSON Web Tokens). Para acessar endpoints protegidos:

1. Faça login usando `POST /auth/login` com suas credenciais
2. Você receberá um token JWT na resposta
3. Inclua o token no header `Authorization: Bearer <seu-token>` nas próximas requisições

## 🐳 Gerenciamento com Docker Compose

### Ver logs
```bash
docker-compose logs -f
```

### Parar os serviços
```bash
docker-compose down
```

### Parar os serviços e remover volumes
```bash
docker-compose down -v
```

### Verificar status dos serviços
```bash
docker-compose ps
```

### Reconstruir a imagem do backend
```bash
docker-compose up -d --build
```

## 📝 Variáveis de Ambiente

No `docker-compose.yml`, você pode configurar:

- `ASPNETCORE_ENVIRONMENT`: Define o ambiente (Development/Production)
- `ConnectionStrings__SqlServer`: String de conexão do SQL Server
- `MSSQL_SA_PASSWORD`: Senha do administrador SQL Server
- `MSSQL_PID`: Edição do SQL Server (Developer/Standard/Enterprise)

## 📂 Estrutura de Volumes

- `mssql_data`: Armazena os dados do SQL Server
- `images_uploads`: Armazena imagens enviadas pelos usuários (em `/app/wwwroot/uploads`)

## 🌐 Rede Docker

A aplicação utiliza uma rede bridge personalizada chamada `theblog-network` para permitir comunicação entre os containers.

## ⚙️ Configuração do SQL Server

O SQL Server é configurado com:
- **Imagem**: SQL Server 2025 (última versão)
- **Porta**: 1433 (padrão)
- **Health Check**: Verifica a saúde do serviço a cada 5 segundos
- **Reinicialização**: Automática em caso de falha

## 📊 Estrutura de Dados

A aplicação gerencia as seguintes entidades principais:

- **Users**: Usuários do blog com autenticação
- **Posts**: Artigos do blog com autor, título, conteúdo e slug
- **Imagens**: Associadas aos posts para enriquecer o conteúdo

## 🆘 Troubleshooting

### Erro de conexão com SQL Server
- Verifique se a porta 1433 está disponível
- Confira a senha no `docker-compose.yml`
- Aguarde o health check completar (pode levar até 50 segundos)

### Erro ao acessar a API
- Verifique se a porta 5000 está disponível
- Certifique-se de que o SQL Server iniciou corretamente
- Consulte os logs: `docker-compose logs backend`

### Permissão negada ao criar volumes
- Execute Docker com privilégios adequados
- No Windows, certifique-se de que o Docker Desktop está rodando
- No Linux, adicione seu usuário ao grupo docker: `sudo usermod -aG docker $USER`

## 📖 Documentação da API

A documentação da API está disponível em formato OpenAPI (JSON):
- **OpenAPI Specification**: http://localhost:5000/openapi/v1.json

Você pode utilizar ferramentas como Postman, Insomnia ou similares para importar e explorar a especificação OpenAPI.

---

**Desenvolvido com ❤️ usando .NET 10**
