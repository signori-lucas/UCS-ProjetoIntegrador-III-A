using System;
using System.Collections.Generic;
using System.Text;
using UCS_ProjetoIntegrador_III_A.Models.Enums;

namespace UCS_ProjetoIntegrador_III_A.Models
{
    public class Turma
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Codigo { get; set; }
        public TipoEnsino EtapaEnsino { get; set; }
        public int Ano { get; set; }
        public int LimiteVagas { get; set; }
        public List<Aluno> Alunos { get; set; } = new List<Aluno>();
        public int NumeroMatriculados
            => Alunos?.Count ?? 0;
    }
}
