using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace UCS_ProjetoIntegrador_III_A.Models.Enums
{
    public enum TipoEnsino
    {
        [Description("Infantil")]
        Infantil = 0,
        [Description("Fundamental Anos Iniciais")]
        FundamentalInicial,
        [Description("Fundamental Anos Finais")]
        FundamentalFinal,
        [Description("Médio")]
        Medio,
        [Description("Não Definido")]
        NaoDefinido
    }
}
