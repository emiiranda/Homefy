# Homefy

Aplicação web para controle de gastos residenciais. Permite gerenciar pessoas, categorias e transações financeiras, com visualização de totais consolidados por pessoa e por categoria.

---

## Tecnologias

### Backend
| Tecnologia | Versão |
|---|---|
| .NET / ASP.NET Core Web API | 9.0 |
| Entity Framework Core | 9.0.13 |
| PostgreSQL | 17 |
| Scalar (documentação da API) | — |

### Frontend
| Tecnologia | Versão |
|---|---|
| React | 19 |
| Vite | 7 |
| TypeScript | 5.9 |
| Tailwind CSS | 4 |
| React Router DOM | 7 |
| Axios | 1.13.6 |
| Nginx (produção) | 1.27 Alpine |

### Infraestrutura
| Serviço | Descrição |
|---|---|
| Docker + Docker Compose | Orquestração de todos os containers |
| pgAdmin 4 | Interface de administração do banco de dados |

---


### Pré-requisitos

- [Donet9](https://dotnet.microsoft.com/pt-br/download/dotnet/9.0) sdk ou runtime instalados.
- [Docker](https://www.docker.com/) instalado e em execução
- [Docker Compose](https://docs.docker.com/compose/) na versão v2 ou mais recente
- [Vite](https://vite.dev/) na versão v3.7.1
- [Axios](https://axios-http.com/)


### Migrações do Banco de Dados
Para aplicar as alterações de estrutura no banco de dados PostgreSQL utilizando o Entity Framework Core, 
execute o comando abaixo num terminal, à partir do diretório do projeto Homefy.Infraestrutura

```bash
dotnet ef database update
```

### Execução dos testes 
Para executar os testes, pode-se utilizar o comando abaixo. A aplicação dispõe de testes unitários para todas as entidades,
e testes específicos para Person nas camadas de Domínio, Aplicação e Infraestrutura.

```bash
dotnet test
```

### Build inicial
O build inicial pode ser feito através do comando abaixo. O docker-compose.yml instala as dependencias 
e compila a solução, iniciando o container Docker com pgAdmin, PostgreSQL, API Backend (ASP.NET Web Api) e Frontend (React)

```bash
docker compose up --build
```

### Rotas API

| Página | Rota | Operações |
| :--- | :--- | :--- |
| Pessoas | `/persons` | Listar, criar, editar, excluir |
| Categorias | `/categories` | Listar, criar |
| Transações | `/transactions` | Listar, criar |
| Totais por Pessoa | `/totals/persons` | Somente leitura |
| Totais por Categoria | `/totals/categories` | Somente leitura |


### Acessando a aplicação
Frontend	http://localhost:3000
API (Scalar)	http://localhost:5077/scalar
pgAdmin	http://localhost:5055

As credenciais de teste podem ser encontradas no arquivo docker-compose.yml e alteradas conforme a necessidade na string de conexão.


