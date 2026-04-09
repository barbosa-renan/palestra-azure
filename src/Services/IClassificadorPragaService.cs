using CustomSkillClassificaPraga.Models;

namespace CustomSkillClassificaPraga.Services;

public interface IClassificadorPragaService
{
    OutputData Classificar(string nomeProduto);
}
