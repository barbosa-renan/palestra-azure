using CustomSkillClassificaPraga.Models;
using CustomSkillClassificaPraga.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace CustomSkillClassificaPraga;

public class ClassificaPraga(ILogger<ClassificaPraga> logger, IClassificadorPragaService classificador)
{
    [Function("ClassificaPraga")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
    {
        logger.LogInformation("Custom Skill ClassificaPraga chamada.");

        SkillInput? input;
        try
        {
            input = await JsonSerializer.DeserializeAsync<SkillInput>(req.Body);
        }
        catch (JsonException ex)
        {
            logger.LogWarning(ex, "Payload inválido recebido.");
            return new BadRequestObjectResult("Payload inválido: JSON malformado.");
        }

        if (input?.Values is not { Count: > 0 })
            return new BadRequestObjectResult("Payload inválido: 'values' ausente ou vazio.");

        var outputValues = input.Values.Select(record =>
        {
            logger.LogInformation("Processando recordId: {RecordId}, produto: {Produto}",
                record.RecordId, record.Data?.Produto);

            return new SkillOutputValue
            {
                RecordId = record.RecordId,
                Data = classificador.Classificar(record.Data?.Produto ?? "")
            };
        }).ToList();

        return new OkObjectResult(new SkillOutput { Values = outputValues });
    }
}
