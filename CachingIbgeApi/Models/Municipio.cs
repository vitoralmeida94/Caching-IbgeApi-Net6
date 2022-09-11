using Newtonsoft.Json;

namespace CachingIbgeApi.Models;

public class Mesorregiao
{
    public int id { get; set; }
    public string nome { get; set; }
    public UF UF { get; set; }
}

public class Microrregiao
{
    public int id { get; set; }
    public string nome { get; set; }
    public Mesorregiao mesorregiao { get; set; }
}

public class Regiao
{
    public int id { get; set; }
    public string sigla { get; set; }
    public string nome { get; set; }
}

public class RegiaoImediata
{
    public int id { get; set; }
    public string nome { get; set; }

    [JsonProperty("regiao-intermediaria")]
    public RegiaoIntermediaria RegiaoIntermediaria { get; set; }
}

public class RegiaoIntermediaria
{
    public int id { get; set; }
    public string nome { get; set; }
    public UF UF { get; set; }
}

public class Municipio
{
    public int id { get; set; }
    public string nome { get; set; }
    public Microrregiao microrregiao { get; set; }

    [JsonProperty("regiao-imediata")]
    public RegiaoImediata RegiaoImediata { get; set; }
}

public class UF
{
    public int id { get; set; }
    public string sigla { get; set; }
    public string nome { get; set; }
    public Regiao regiao { get; set; }
}