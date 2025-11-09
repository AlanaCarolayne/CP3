# 📚 Sistema de Empréstimo de Livros


## ✅ 1. Regras de Negócio Implementadas

### ✅ Livro

-   ISBN, Título, Autor, Categoria, Status e Data de Cadastro.
-   Status: Disponível, Emprestado, Reservado.
-   Só pode ser emprestado se estiver disponível.

### ✅ Usuário

-   Tipos: Aluno, Professor, Funcionário.
-   Usuário só pode ter até 3 empréstimos ativos.

### ✅ Empréstimo

-   Status: Ativo, Finalizado, Atrasado.
-   Prazos por tipo de usuário:
    -   Aluno: 7 dias
    -   Professor: 15 dias
    -   Funcionário: 10 dias

### ✅ Multa

-   R\$1,00 por dia de atraso.
-   Usuários com multas pendentes não podem pegar livros.

------------------------------------------------------------------------
## ✅ 2. Diagrama Simples das Entidades

           ┌──────────┐            ┌────────────┐            ┌──────────┐
           │ USUARIO  │ 1        N │ EMPRESTIMO │ N        1 │  LIVRO   │
           └──────────┘────────────└────────────┘────────────└──────────┘
                                   │
                                   │ 1
                                   ▼
                             ┌──────────┐
                             │  MULTA   │
                             └──────────┘

- USUARIO 1 — N EMPRESTIMO: um usuário pode ter vários empréstimos.

- LIVRO 1 — N EMPRESTIMO: um livro pode estar associado a vários empréstimos ao longo do tempo.

- EMPRESTIMO 1 — 1 MULTA: cada empréstimo pode gerar no máximo uma multa.

### Tabelas e atributos

Usuario
- Id
- Nome
- Email
- Tipo (Aluno/Professor/Funcionario)
- DataCadastro

Livro
- Isbn
- Titulo
- Autor
- Categoria
- Status
- DataCadastro

Emprestimo
- Id
- UsuarioId (FK)
- LivroId (FK)
- DataEmprestimo
- DataPrevista
- DataDevolucaoReal
- Status

Multa
- Id
- EmprestimoId (FK)
- ValorMulta
- Status (Pendente/Paga)


---

## ✅ 3. Exemplos de Requisições da API

### Registrar Empréstimo

POST /api/emprestimo/registrar?usuarioId=1&livroIsbn=1001

### Registrar Devolução

POST /api/emprestimo/devolver?emprestimoId=5

### Livros mais emprestados

GET /api/relatorio/livros-mais-emprestados

### Empréstimos atrasados

GET /api/relatorio/atrasados

------------------------------------------------------------------------

## ✅ 4. Como Executar o Projeto

### 1. Clonar

git clone https://github.com/AlanaCarolayne/CP3

### 2. Configurar conexão

Edite o appsettings.json

### 3. Restaurar pacotes

dotnet restore

### 4. Executar

dotnet run

### 5. Abrir Swagger

http://localhost:5001/swagger

------------------------------------------------------------------------
## 👩‍💻 Autoras
Alana Carolayne Moreira Siqueira RM: 552261

Ana Júlia Henriques Neves RM: 98263

FIAP - 3SIS - 2025
