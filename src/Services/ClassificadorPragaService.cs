using CustomSkillClassificaPraga.Models;

namespace CustomSkillClassificaPraga.Services;

public class ClassificadorPragaService : IClassificadorPragaService
{
    private static readonly List<(string[] PalavrasChave, string TipoPraga, string PragaAlvo, string CategoriaUso)> Classificacoes =
    [
        // FORMICIDAS
        (new[] { "SCOUT", "ATEXZO", "MIREX", "BLITZ", "XEQUE MATE", "ZARTAN", "ATTA MEX", "TUIT" },
        "FORMICIDA", "Formiga cortadeira (Atta/Acromyrmex)", "Controle pré e pós-plantio"),

        // HERBICIDAS — dessecação e pós-emergente
        (new[] { "ZAPP", "TOUCHDOWN", "CHOPPER", "GLIFOSATO", "HELPER", "FINALE", "TRICLOPIR" },
        "HERBICIDA", "Mato-competição (gramíneas e folhas largas)", "Dessecação e pós-emergente"),

        // HERBICIDAS — pré-emergente
        (new[] { "GOAL", "PLEDGE", "SPOTLIGHT", "FORDOR", "PROVENCE", "GALIGAN", "GAMIT", "FUSILADE", "TARGA", "SEQUENCE", "TOPINAM", "PATROL" },
        "HERBICIDA_PRE", "Plantas daninhas (pré-emergência)", "Controle pré-emergente"),

        // FUNGICIDAS
        (new[] { "FALCON", "SUNWARD", "NATIVO", "OPERA", "PRIORI", "SOLARA", "BION", "STONE" },
        "FUNGICIDA", "Ferrugem do eucalipto (Puccinia psidii)", "Controle de doenças foliares"),

        // INSETICIDAS
        (new[] { "CAPTURE", "SUMYZIN", "EVIDENCE", "IMIDA", "ACTARA", "FASTAC", "DIPEL", "WARRANT", "EVENTRA", "OCHIMA", "YAMATO", "SINFONAT" },
        "INSETICIDA", "Pragas de solo e sugadores", "Controle de insetos"),

        // ADJUVANTES e ÓLEOS
        (new[] { "PALMERO", "AGRAL", "OLEO", "IHAROL", "ASSIST", "EXTRAVON", "LIBERATE", "SUPREME", "QUANTIS", "REDUCTUS", "VALEOS" },
        "ADJUVANTE", "N/A", "Melhoria de aplicação"),

        // FERTILIZANTES
        (new[] { "CDA HYDRO", "DRIPSO", "FLORESTA B", "FLORESTA GROWER", "FORTGREEN", "MAP PURIFICADO", "SULFATO", "BORO 10", "STOCKOSORB" },
        "FERTILIZANTE", "N/A", "Nutrição e condicionamento de solo"),

        // FORRAGEIRAS / SEMENTES
        (new[] { "BRAC BRIZ", "MARANDU" },
        "SEMENTE_FORRAGEIRA", "N/A", "Formação de pastagem"),
    ];

    public OutputData Classificar(string nomeProduto)
    {
        if (string.IsNullOrWhiteSpace(nomeProduto))
            return NaoClassificado();

        var upper = nomeProduto.ToUpperInvariant();

        foreach (var (palavras, tipo, praga, categoria) in Classificacoes)
        {
            if (palavras.Any(p => upper.Contains(p)))
                return new OutputData { TipoPraga = tipo, PragaAlvo = praga, CategoriaUso = categoria };
        }

        return NaoClassificado();
    }

    private static OutputData NaoClassificado() =>
        new() { TipoPraga = "NAO_CLASSIFICADO", PragaAlvo = "N/A", CategoriaUso = "N/A" };
}
