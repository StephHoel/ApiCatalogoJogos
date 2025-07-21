# 🎮 Game Catalog API

API RESTful desenvolvida com .NET 9 para gerenciamento de um catálogo de jogos. Implementa boas práticas modernas como **DDD (Domain-Driven Design)**, **Clean Architecture**, **Minimal APIs**, **testes de integração/unitários** e **documentação via Swagger**.

---

## 🧭 Objetivo

Oferecer uma API limpa, modular e escalável para registrar, consultar, atualizar e excluir jogos, com validação de dados, testes automatizados e arquitetura bem definida.

---

## 🏗️ Estrutura do Projeto (DDD + Clean Code)

```plain
📦 GameCatalog
├── Api/                 → Endpoints Minimal APIs
├── Application/         → Serviços de aplicação (orquestra lógica)
├── Domain/              → Entidades, DTOs, interfaces e mensagens
├── Infrastructure/      → Acesso a dados (repositórios, EF Core)
├── Tests/               → Testes unitários e de integração
```

---

## 🚀 Tecnologias e Ferramentas

- [.NET 9](https://devblogs.microsoft.com/dotnet/announcing-dotnet-9-preview/)
- Minimal APIs
- Entity Framework Core (InMemory DB)
- Swagger (Swashbuckle)
- xUnit + FluentAssertions
- Custom FakeRepository para testes integrados

---

## 📌 Endpoints Disponíveis

| Verbo HTTP | Rota                     | Descrição                     |
|------------|--------------------------|-------------------------------|
| `GET`      | `/api/games`             | Lista paginada de jogos       |
| `GET`      | `/api/games/{id}`        | Detalhes de um jogo           |
| `POST`     | `/api/games`             | Criação de novo jogo          |
| `PUT`      | `/api/games/{id}`        | Atualização de um jogo        |
| `DELETE`   | `/api/games/{id}`        | Remoção de um jogo            |

> **Observação:** Todos os endpoints incluem documentação Swagger com `.WithSummary()`, `.WithDescription()`, `.Produces()` etc.

---

## ✅ Regras de Negócio

- O preço do jogo deve ser maior que zero.
- O `Id` do corpo e da URL devem ser iguais nas atualizações.
- A paginação é obrigatória na listagem (`page >= 1`, `pageSize ∈ [1, 50]`).

---

## 🧪 Testes

- **Unitários:** Serviços e repositórios (InMemory)
- **Integração:** Rotas com substituição de `IGameRepository` via `FakeRepository`

Executar:

```bash
dotnet test
```

---

## 🔧 Como executar localmente

1. Clone o projeto:

```bash
git clone https://github.com/StephHoel/GameCatalog.git
cd GameCatalog
```

2. Execute o projeto:

```bash
dotnet run --project Api
```

3. Acesse a documentação Swagger:

```url
https://localhost:{PORT}/swagger
```

---

## 📚 Exemplo de JSON de jogo

```json
{
  "id": "GUID",
  "title": "Zelda: Breath of the Wild",
  "developer": "Nintendo",
  "price": 199.99
}
```

---

## 🤝 Contribuindo

Pull Requests são bem-vindos! Apenas siga os padrões de clean code, separação de responsabilidades e testes sempre que possível.

---

## 🪪 Licença

Projeto desenvolvido por Steph Hoel para fins de estudo e demonstração de arquitetura moderna com .NET.
