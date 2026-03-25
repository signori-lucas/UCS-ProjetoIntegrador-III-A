using System;
using System.Collections.Generic;
using System.Linq;
using UCS_ProjetoIntegrador_III_A.Exceptions;
using UCS_ProjetoIntegrador_III_A.Models;

namespace UCS_ProjetoIntegrador_III_A.Services
{
    public class LitaDeTurmasService
    {
        private readonly List<Turma> _turmas = new();

        public void Adiciona(Turma turma)
        {
            this.ValidarTurma(turma);

            _turmas.Add(turma);
        }

        public Turma BuscarPorCodigo(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo)) 
                return null;
            
            return _turmas.FirstOrDefault(a => a.Codigo.Equals(codigo));
        }

        public IReadOnlyList<Turma> BuscarTodas()
        {
            return _turmas.AsReadOnly();
        }

        public int Count => _turmas.Count;


        #region validações

        private void ValidarTurma(Turma aluno)
        {
            if (aluno == null)
                throw new ExcecaoDeTurmaJaExistente("Objeto aluno nulo");

            var alunoExistente = this._turmas.FirstOrDefault(a => a.Equals(aluno));
            if (alunoExistente != null)
                throw new ExcecaoDeTurmaJaExistente();
        }

        #endregion 
    }
}
