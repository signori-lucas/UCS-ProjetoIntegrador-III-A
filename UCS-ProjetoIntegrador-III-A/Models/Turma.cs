using System;
using System.Collections.Generic;
using System.Text;
using UCS_ProjetoIntegrador_III_A.Models.Enums;
using UCS_ProjetoIntegrador_III_A.Utils;

namespace UCS_ProjetoIntegrador_III_A.Models
{
    public class Turma : Object
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Codigo { get; set; }
        public TipoEnsino EtapaEnsino { get; set; }
        public int Ano { get; set; }
        public int LimiteVagas { get; set; }
        public List<Aluno> Alunos { get; set; } = new List<Aluno>();
        public int NumeroMatriculados
            => Alunos?.Count ?? 0;

        public override bool Equals(object objeto)
        {
            if (objeto is null) return false;

            var turma = objeto as Turma;

            return Codigo.Equals(turma.Codigo) && EtapaEnsino.Equals(turma.EtapaEnsino) && Ano.Equals(turma.Ano);
        }

        public override string ToString()
        {
            return $"Turma: {Codigo} | Ensino: {EnumUtils.GetEnumDescription(EtapaEnsino)} | Ano: {Ano} | LimiteVagas: {LimiteVagas} | Matriculados: {NumeroMatriculados}";
        }
    }
}
