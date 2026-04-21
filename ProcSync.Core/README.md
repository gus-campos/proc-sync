# ProcSync

## Descrição

Projeto em C# desenvolvido com .NET para sincronização de processos.

---

## Pré-requisitos desenvolvimento

- [.NET SDK 8.0+](https://dotnet.microsoft.com/)
- Git

---

## Como executar o projeto

No diretório raiz da solução, execute:

```bash
dotnet run --project ProcSync.Core
```

Para rodar os testes, execute:

```bash
dotnet test ProcSync.Tests
```

---

## Estrutura do projeto

- `ProcSync.Core` → lógica principal da aplicação
- `ProcSync.Tests` → testes automatizados
- `ProcSync.ConsoleApp` → interface de linha de comando (CLI)

---

## Padronização de código

O projeto utiliza:

- `.editorconfig` para padronização de estilo
- Allman style (chaves em nova linha)
- PascalCase para membros públicos (métodos e propriedades)
- camelCase para variáveis locais
- \_camelCase para campos privados
- IPascalCase para interfaces

<!-- Para formatar o código antes de commitar:

```bash
dotnet format
``` -->

---

## Build do projeto

```bash
dotnet build
```

---
