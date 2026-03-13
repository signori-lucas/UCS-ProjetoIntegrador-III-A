# UCS-ProjetoIntegrador-III-A

Resumo técnico

- Linguagem: C# (versão 14.0)
- Plataforma / SDK: .NET 10 (`net10.0`)
- Modelo de execução: aplicação console com `Generic Host` (`Microsoft.Extensions.Hosting`) e injeção de dependência via `Microsoft.Extensions.DependencyInjection`.
- Estrutura principal:
  - `Program.cs` — entrypoint que cria o `Host` e registra serviços.
  - `Services/` — serviços de domínio e menu (`MenuService`, `LitaDeAlunosService`, `LitaDeTurmasService`).
  - `Models/` — modelos de domínio (`Aluno`, `Turma`) e enums (`TipoEnsino`).
  - `Utils/EnumUtils.cs` — helper para obter descrições de enums.

Requisitos

- .NET 10 SDK instalado (compatível com C# 14).
- Ferramentas opcionais: Visual Studio 2022/2023 (ou superior) ou VS Code com C# extension.

Como executar (linha de comando)

1. Abra um terminal na raiz do repositório.
2. Restaurar pacotes (normalmente o `dotnet` faz isso automaticamente):

```bash
dotnet restore
```

3. Compilar o projeto:

```bash
dotnet build ./UCS-ProjetoIntegrador-III-A/UCS-ProjetoIntegrador-III-A.csproj
```

4. Executar o aplicativo (a partir da raiz do repositório):

```bash
dotnet run --project ./UCS-ProjetoIntegrador-III-A/UCS-ProjetoIntegrador-III-A.csproj
```

Ou, para executar diretamente dentro da pasta do projeto:

```bash
cd UCS-ProjetoIntegrador-III-A
dotnet run
```

Observações de uso

- A aplicação é um console interativo. Siga os menus exibidos para cadastrar alunos, turmas e efetuar matrículas.
- O ponto de entrada `Program.cs` cria um `Host` e registra os serviços para injeção de dependência.
- Para adicionar novos serviços, registre-os em `Program.cs` dentro de `ConfigureServices`.

Desenvolvimento e debugging

- Abra a pasta `UCS-ProjetoIntegrador-III-A` ou a solução no Visual Studio/VS Code.
- Use o debugger da IDE para executar e inspecionar objetos.

---
Arquivo gerado automaticamente contendo instruções básicas de execução e detalhes técnicos do projeto.
