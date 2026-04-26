# ProcSync

## Descrição

Projeto desenvolvido com C# para simulação de sincronização de processos.

## Estrutura do projeto

A estrutura base é a seguinte, e é composta de 3 sub projetos:

```
├── ProcSync.ConsoleApp
├── ProcSync.Core
├── ProcSync.Tests
└── README.md (você está aqui)
```

A abstração deste projeto se organiza nos seguintes níveis:

Program -> Commands -> Handlers -> Simulators -> Interfaces --> Domain

### `ProcSync.ConsoleApp` → interface de linha de comando (CLI)

É a aplicação de console, ou interface de linha de comando (CLI) pela qual o usuário pode interagir com o programa. Para mais detalhes, executar com -h de argumento, ou executar o programa sem passar nenhum argumento, para ver a ajuda.

```
├── ProcSync.ConsoleApp
│ ├── Commands
│ ├── Handlers
│ └── Program.cs
```

- `Program.cs` é o ponto de entrada do progama, que invoca o mando root, que processa o input passado de argumento, levando a execução de outros subcomandos.
- `Commands` são as classes que constroem os objetos dos comandos, e representam cada um dos comandos aceitos pelo programa. Alguns comandos vão chamar um handler responsável pela execução da lógica desejada. Uma dessas classes em especial, a `RootComandFactory` é responsável por construir o comando raíz (root).
- `Handlers` são as classes responsáveis por parsear os inputs, e chamar a execução do que de fato se deseja executar.

### `ProcSync.Core` → lógica principal da aplicação

Responsável pela lógica central da aplicação. Segue a seguinte estrutura:

```
├── ProcSync.Core
│   ├── Domain
│   ├── Interfaces
│   └── Simulators
```

- Em `Simulators` estão as lógicas que fazem a simulação de cada problema proposto, usando interfaces definidas pros atores e recursos.
- Em `Interfaces` estão as interfaces que os simuladores operam, e que representam de forma abstraída o comportamento que eles terão, e são respeitadas pelas diferentes implementações da mesma coisa.
- Em `Domain` estão as diferentes implementações para as interfaces, com as lógicas para produzir os comportamentos desejados (ou indesejados). Alguns sendo compatíveis com concorrência e outros não.

### `ProcSync.Tests` → testes automatizados

## Padronização de código

O projeto utiliza:

- `.editorconfig` para padronização de estilo
- Allman style (chaves em nova linha)
- PascalCase para membros públicos (métodos e propriedades)
- camelCase para variáveis locais
- \_camelCase para campos privados
- IPascalCase para interfaces

## Pré-requisitos desenvolvimento

- [.NET SDK 8.0+](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Git

## Como executar o projeto

No diretório raiz da solução, execute:

```bash
dotnet run --project ProcSync.ConsoleApp
```

Para rodar os testes, execute:

```bash
dotnet test ProcSync.Tests
```

## Build do projeto

```bash
dotnet build
```
