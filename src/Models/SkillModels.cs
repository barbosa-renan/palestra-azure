using System.Text.Json.Serialization;

namespace CustomSkillClassificaPraga.Models;

public class SkillInput
{
    [JsonPropertyName("values")]
    public List<SkillInputValue> Values { get; set; } = new();
}

public class SkillInputValue
{
    [JsonPropertyName("recordId")]
    public string RecordId { get; set; } = "";

    [JsonPropertyName("data")]
    public InputData Data { get; set; } = new();
}

public class InputData
{
    [JsonPropertyName("produto")]
    public string Produto { get; set; } = "";
}

public class SkillOutput
{
    [JsonPropertyName("values")]
    public List<SkillOutputValue> Values { get; set; } = new();
}

public class SkillOutputValue
{
    [JsonPropertyName("recordId")]
    public string RecordId { get; set; } = "";

    [JsonPropertyName("data")]
    public OutputData Data { get; set; } = new();

    [JsonPropertyName("errors")]
    public List<string>? Errors { get; set; }

    [JsonPropertyName("warnings")]
    public List<string>? Warnings { get; set; }
}

public class OutputData
{
    [JsonPropertyName("tipoPraga")]
    public string TipoPraga { get; set; } = "";

    [JsonPropertyName("pragaAlvo")]
    public string PragaAlvo { get; set; } = "";

    [JsonPropertyName("categoriaUso")]
    public string CategoriaUso { get; set; } = "";
}
