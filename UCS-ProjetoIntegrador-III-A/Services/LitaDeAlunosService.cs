using System;
using System.Collections.Generic;
using UCS_ProjetoIntegrador_III_A.Models;

namespace UCS_ProjetoIntegrador_III_A.Services
{
    public class LitaDeAlunosService
    {
        private readonly List<Aluno> _alunos = new();

        public void AdicionaInicio(Aluno aluno)
        {
            if (aluno == null) throw new ArgumentNullException(nameof(aluno));
            _alunos.Insert(0, aluno);
        }

        public void AdicionaFinal(Aluno aluno)
        {
            if (aluno == null) throw new ArgumentNullException(nameof(aluno));
            _alunos.Add(aluno);
        }

        public bool RemoveFinal()
        {
            if (_alunos.Count == 0) return false;
            _alunos.RemoveAt(_alunos.Count - 1);
            return true;
        }

        public void OrdenaPorNome()
        {
            _alunos.Sort((a, b) => string.Compare(a?.Nome, b?.Nome, StringComparison.CurrentCultureIgnoreCase));
        }

        public Aluno BuscarAlunoPorPosicao(int index)
        {
            if (index < 0 || index >= _alunos.Count) return null;
            return _alunos[index];
        }

        public Aluno? BuscarAlunoPorCPF(string cpf)
            => !String.IsNullOrEmpty(cpf) ? _alunos.FirstOrDefault(a => a.CPF.Equals(cpf)) : null;

        public Aluno? BuscarAlunoPorNome(string nome)
            => !String.IsNullOrEmpty(nome) ? _alunos.FirstOrDefault(a => a.Nome
            .Equals(nome)) : null;


        public IReadOnlyList<Aluno> BuscarTodos()
        {
            return _alunos.AsReadOnly();
        }

        public int Count => _alunos.Count;
    }
}
