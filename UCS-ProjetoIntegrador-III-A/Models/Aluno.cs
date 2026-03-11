using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using UCS_ProjetoIntegrador_III_A.Models.Enums;

namespace UCS_ProjetoIntegrador_III_A.Models
{
    public class Aluno
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? TurmaId { get; set; }
        public virtual Turma Turma { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Endereco { get; set; }
        public DateTime DataNascimento { get; set; }

        public int Idade
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - DataNascimento.Year;
                if (DataNascimento.Date > today.AddYears(-age)) age--;
                return age;
            }
        }

        public TipoEnsino EtapaEnsino 
        {
            get 
            {
                if (Idade < 6)
                    return TipoEnsino.Infantil;
                else if (Idade >= 6 && Idade <= 11)
                    return TipoEnsino.FundamentalInicial;
                else if (Idade >= 11 && Idade <= 15)
                    return TipoEnsino.FundamentalFinal;
                else if (Idade >= 15 && Idade <= 18)
                    return TipoEnsino.Medio;
                else
                    return TipoEnsino.NaoDefinido;
            }
        }
    }
}
