using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using UCS_ProjetoIntegrador_III_A.Models;
using UCS_ProjetoIntegrador_III_A.Models.Enums;
using UCS_ProjetoIntegrador_III_A.Utils;

namespace UCS_ProjetoIntegrador_III_A.Services
{
    public class MenuService
    {
        private readonly LitaDeAlunosService _listaDeAlunosService;
        private readonly LitaDeTurmasService _listaDeTurmasService;

        // Dependências injetadas via construtor
        public MenuService(LitaDeAlunosService listaDeAlunosService, LitaDeTurmasService listaDeTurmasService)
        {
            _listaDeAlunosService = listaDeAlunosService;
            _listaDeTurmasService = listaDeTurmasService;
        }

        public void AbrirMenu()
        {
            Console.WriteLine("Bem-vindo ao sistema de gerenciamento escolar!");

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("=== MENU PRINCIPAL ===");
                Console.WriteLine("1 - Cadastro de Alunos");
                Console.WriteLine("2 - Cadastro de Turmas");
                Console.WriteLine("3 - Matricular Aluno");
                Console.WriteLine("0 - Sair");
                Console.Write("Escolha uma opção: ");

                var escolha = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(escolha)) continue;

                Console.WriteLine();

                switch (escolha)
                {
                    case "1":
                        MostrarAlunoMenu();
                        break;
                    case "2":
                        MostrarTurmaMenu();
                        break;
                    case "3":
                        MatricularAluno();
                        break;
                    case "0":
                        Console.WriteLine("Saindo...");
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        #region Aluno

        private void MostrarAlunoMenu()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("--- CADASTRO DE ALUNOS ---");
                Console.WriteLine("1 - Cadastrar aluno no início da lista");
                Console.WriteLine("2 - Cadastrar aluno no final da lista");
                Console.WriteLine("3 - Remover Aluno do final da lista");
                Console.WriteLine("4 - Ordenar lista de alunos");
                Console.WriteLine("5 - Buscar aluno pela posição");
                Console.WriteLine("6 - Buscar aluno pelo CPF");
                Console.WriteLine("7 - Consultar lista de alunos");
                Console.WriteLine("8 - Consultar lista de alunos ordenada (apenas visual)");
                Console.WriteLine("9 - Consultar quantidade de alunos");
                Console.WriteLine("10 - Consultar alunos matriculados em uma etapa de ensino fora da idade prevista");
                Console.WriteLine("0 - Voltar");
                Console.Write("Escolha uma opção: ");

                var opcao = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(opcao)) continue;

                Console.WriteLine();

                switch (opcao)
                {
                    case "1":
                        var a1 = CriarAluno();
                        _listaDeAlunosService.AdicionaInicio(a1);

                        Console.WriteLine("Aluno cadastrado no início da lista.");
                        break;
                    case "2":
                        var a2 = CriarAluno();
                        _listaDeAlunosService.AdicionaFinal(a2);

                        Console.WriteLine("Aluno cadastrado no final da lista.");
                        break;
                    case "3":
                        if (_listaDeAlunosService.RemoveFinal())
                            Console.WriteLine("Último aluno removido com sucesso.");
                        else
                            Console.WriteLine("Lista vazia. Nenhum aluno para remover.");

                        break;
                    case "4":
                        _listaDeAlunosService.OrdenaPorNome();

                        Console.WriteLine("Lista de alunos ordenada por nome.");

                        break;
                    case "5":
                        Console.Write("Informe a posição: ");

                        var posInput = Console.ReadLine();
                        if (int.TryParse(posInput, out int pos))
                        {
                            var found = _listaDeAlunosService.BuscarAlunoPorPosicao(pos - 1);
                            if (found != null)
                            {
                                EscreveAluno(found);
                            }
                            else
                            {
                                Console.WriteLine("Nenhum aluno na posição informada.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Posição inválida.");
                        }

                        break;
                    case "6":
                        Console.Write("Informe o CPF: ");

                        var cpf = Console.ReadLine();
                        var aluno = _listaDeAlunosService.BuscarAlunoPorCPF(cpf);
                        if (aluno != null)
                        {
                            EscreveAluno(aluno);
                        }
                        else
                        {
                            Console.WriteLine("Nenhum aluno com o CPF informado.");
                        }

                        break;
                    case "7":
                        var todos = _listaDeAlunosService.BuscarTodos();
                        if (todos.Count == 0)
                        {
                            Console.WriteLine("Lista vazia.");
                        }
                        else
                        {
                            for (int i = 0; i < todos.Count; i++)
                            {
                                EscreveAluno(todos[i]);
                            }
                        }

                        break;
                    case "8":
                        var ordenados = _listaDeAlunosService.BuscarTodos().OrderBy(a => a.Nome).ToList();
                        if (ordenados.Count == 0)
                        {
                            Console.WriteLine("Lista vazia.");
                        }
                        else
                        {
                            for (int i = 0; i < ordenados.Count; i++)
                            {
                                EscreveAluno(ordenados[i]);
                            }
                        }

                        break;
                    case "9":
                        Console.WriteLine($"Quantidade de alunos: {_listaDeAlunosService.Count}");

                        break;
                    case "10":

                        TipoEnsino etapa = MenuEtapaEnsino();
                        var alunosEmDesacordo = _listaDeAlunosService
                            .BuscarTodos()
                            .Where(a => a.TurmaId.HasValue 
                                     && a.Turma.EtapaEnsino.Equals(etapa) 
                                     && !a.EtapaEnsino.Equals(etapa))
                            .ToList();

                        Console.WriteLine($"Etapa de Ensino Selecionada: {EnumUtils.GetEnumDescription(etapa)}");
                        Console.WriteLine();
                        Console.WriteLine($"  Alunos em desacordo com o tipo de ensino");

                        if (alunosEmDesacordo != null && alunosEmDesacordo.Count > 0)
                        {
                            foreach (var a in alunosEmDesacordo)
                            {
                                Console.WriteLine($"  - {a.Nome} | CPF: {a.CPF} | Idade: {a.Idade} | Ensino Aluno: {EnumUtils.GetEnumDescription(a.EtapaEnsino)} | Ensino Turma: {EnumUtils.GetEnumDescription(a.Turma.EtapaEnsino)}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nenhuma aluno em desacordo com a matricula.");
                        }
                        break;
                    case "0":

                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");

                        break;
                }
            }
        }

        private void EscreveAluno(Aluno aluno)
        {
            Console.WriteLine($"Nome: {aluno.Nome} | CPF: {aluno.CPF} | Endereço: {aluno.Endereco} | Data Nascimento: {aluno.DataNascimento:dd/MM/yyyy} | Idade: {aluno.Idade} | Ensino: {EnumUtils.GetEnumDescription(aluno.EtapaEnsino)} | Turma: {aluno.Turma?.Codigo ?? "Não matriculado"}");
        }

        private Aluno CriarAluno()
        {
            var aluno = new Aluno();

            Console.Write("Informe o nome: ");
            aluno.Nome = Console.ReadLine()?.Trim();

            Console.Write("Informe o CPF: ");
            aluno.CPF = Console.ReadLine()?.Trim();

            Console.Write("Informe o endereço: ");
            aluno.Endereco = Console.ReadLine()?.Trim();

            DateTime data;
            while (true)
            {
                Console.Write("Informe a data de nascimento no formato dd/MM/yyyy: ");
                var input = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Data de nascimento é obrigatória. Tente novamente.");
                    continue;
                }

                if (DateTime.TryParseExact(input, new[] { "dd/MM/yyyy", "yyyy-MM-dd" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out data))
                {
                    aluno.DataNascimento = data;
                    break;
                }

                Console.WriteLine("Formato de data inválido. Use o formato dd/MM/yyyy.");
            }

            return aluno;
        }

        #endregion

        #region Turma

        private void MostrarTurmaMenu()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("--- CADASTRO DE TURMAS ---");
                Console.WriteLine("1 - Cadastrar Turma");
                Console.WriteLine("2 - Mostrar Lista de Turmas");
                Console.WriteLine("3 - Relação de Alunos Matriculados nas Turmas");
                //Console.WriteLine("4 - Relação de Alunos Matriculados em desacordo com o seu Tipo de Ensino");                
                Console.WriteLine("0 - Voltar");
                Console.Write("Escolha uma opção: ");

                var opc = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(opc)) continue;

                Console.WriteLine();

                switch (opc)
                {
                    case "1":
                        var turma = CriarTurma();
                        _listaDeTurmasService.Adiciona(turma);
                        Console.WriteLine("Turma cadastrada com sucesso.");
                        break;
                    case "2":
                        var turmas = _listaDeTurmasService.BuscarTodas();
                        if (turmas.Count == 0)
                        {
                            Console.WriteLine("Nenhuma turma cadastrada.");
                        }
                        else
                        {
                            foreach (var t in turmas)
                                EscreveTurma(t);
                        }
                        break;
                    case "3":
                        var todas = _listaDeTurmasService.BuscarTodas();
                        if (todas.Count == 0)
                        {
                            Console.WriteLine("Nenhuma turma cadastrada.");
                        }
                        else
                        {
                            foreach (var t in todas)
                            {
                                EscreveTurma(t);

                                if (t.Alunos != null && t.Alunos.Count > 0)
                                {
                                    Console.WriteLine($"  Alunos:");
                                    foreach (var a in t.Alunos)
                                    {
                                        Console.WriteLine($"  - {a.Nome} | CPF: {a.CPF} | Idade: {a.Idade} | Ensino: {EnumUtils.GetEnumDescription(a.EtapaEnsino)}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Nenhuma aluno matriculado.");
                                }
                            }
                        }
                        break;
                    case "4":
                        var turmasAlunos = _listaDeTurmasService.BuscarTodas();
                        if (turmasAlunos.Count == 0)
                        {
                            Console.WriteLine("Nenhuma turma cadastrada.");
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine($"  Alunos em desacordo com o tipo de ensino da turma");
                            foreach (var t in turmasAlunos)
                            {
                                Console.WriteLine();
                                EscreveTurma(t);

                                var alunosEmDesacordo = t.Alunos?.Where(a => a.EtapaEnsino != t.EtapaEnsino).ToList();
                                if (alunosEmDesacordo != null && alunosEmDesacordo.Count > 0)
                                {                                    
                                    foreach (var a in alunosEmDesacordo)
                                    {
                                        Console.WriteLine($"  - {a.Nome} | CPF: {a.CPF} | Idade: {a.Idade} | Ensino: {EnumUtils.GetEnumDescription(a.EtapaEnsino)}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Nenhuma aluno matriculado.");
                                }
                            }
                        }
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        private void EscreveTurma(Turma turma)
        {
            Console.WriteLine($"Turma: {turma.Codigo} | Ensino: {EnumUtils.GetEnumDescription(turma.EtapaEnsino)} | Ano: {turma.Ano} | LimiteVagas: {turma.LimiteVagas} | Matriculados: {turma.NumeroMatriculados}");
        }        

        private Turma CriarTurma()
        {
            var turma = new Turma();

            Console.Write("Informe o código da turma: ");
            turma.Codigo = Console.ReadLine()?.Trim();

            turma.EtapaEnsino = MenuEtapaEnsino();

            Console.Write("Informe o ano: ");
            if (int.TryParse(Console.ReadLine(), out int ano)) 
                turma.Ano = ano;

            Console.Write("Informe o limite de vagas: ");
            if (int.TryParse(Console.ReadLine(), out int vagas)) 
                turma.LimiteVagas = vagas;

            return turma;
        }

        private TipoEnsino MenuEtapaEnsino()
        {
            Console.WriteLine("Escolha a etapa de ensino:");
            var tipoEnsinoValues = Enum.GetValues(typeof(TipoEnsino));
            for (int i = 0; i < tipoEnsinoValues.Length; i++)
            {
                var v = (TipoEnsino)tipoEnsinoValues.GetValue(i);
                if (v.Equals(TipoEnsino.NaoDefinido))
                    continue;

                Console.WriteLine($"{i} - {EnumUtils.GetEnumDescription(v)}");
            }

            while (true)
            {
                Console.Write("Informe a opção: ");
                var opt = Console.ReadLine()?.Trim();
                if (int.TryParse(opt, out int idx) && idx >= 0 && idx < tipoEnsinoValues.Length)
                {
                    return (TipoEnsino)tipoEnsinoValues.GetValue(idx);
                }

                Console.WriteLine("Opção inválida. Tente novamente.");
            }
        }

        #endregion

        #region Matricular Aluno

        private void MatricularAluno()
        {
            Console.Write("Informe o código da turma: ");
            var codigo = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(codigo))
            {
                Console.WriteLine("Código de turma inválido.");
                return;
            }

            var turma = _listaDeTurmasService.BuscarPorCodigo(codigo);
            if (turma == null)
            {
                Console.WriteLine("Turma não encontrada.");
                return;
            }

            Console.Write("Informe o nome do aluno: ");
            var nome = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(nome))
            {
                Console.WriteLine("Nome de aluno inválido.");
                return;
            }

            var aluno = _listaDeAlunosService.BuscarAlunoPorNome(nome);
            if (aluno == null)
            {
                Console.WriteLine("Aluno não encontrado na lista de alunos.");
                return;
            }

            if (aluno.Turma != null)
            {
                Console.WriteLine($"Aluno já matriculado na turma {aluno.Turma.Codigo}.");
                return;
            }

            var ocupados = turma.Alunos?.Count ?? 0;
            if (ocupados >= turma.LimiteVagas)
            {
                Console.WriteLine("Turma sem vagas disponíveis.");
                return;
            }

            turma.Alunos.Add(aluno);
            aluno.Turma = turma;
            aluno.TurmaId = turma.Id;

            Console.WriteLine($"Aluno {aluno.Nome} matriculado na turma {turma.Codigo} com sucesso.");
        }

        #endregion
    }
}
