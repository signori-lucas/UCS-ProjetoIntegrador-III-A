using System;
using System.Collections.Generic;
using System.Text;

namespace UCS_ProjetoIntegrador_III_A.Exceptions
{
    public class ExcecaoDeTurmaJaExistente : Exception
    {
        const string MENSAGEM_PADRAO = "Turma já cadastrada.";
        public ExcecaoDeTurmaJaExistente()
            : base(MENSAGEM_PADRAO)
        {
        }

        public ExcecaoDeTurmaJaExistente(string message)
            : base(message)
        {
        }
    }
}
