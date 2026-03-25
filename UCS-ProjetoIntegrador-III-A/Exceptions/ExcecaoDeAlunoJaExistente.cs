using System;

namespace UCS_ProjetoIntegrador_III_A.Exceptions
{
    public class ExcecaoDeAlunoJaExistente : Exception
    {
        const string MENSAGEM_PADRAO = "Aluno já cadastrado.";
        public ExcecaoDeAlunoJaExistente()
            : base(MENSAGEM_PADRAO)
        {
        }

        public ExcecaoDeAlunoJaExistente(string message)
            : base(message)
        {
        }
    }
}
