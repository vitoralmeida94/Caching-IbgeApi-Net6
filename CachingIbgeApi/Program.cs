using CachingIbgeApi.IbgeService;
using CachingIbgeApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Refit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddRefitClient<IIbgeService>()
    .ConfigureHttpClient(x => x.BaseAddress = new Uri("https://servicodados.ibge.gov.br"));
builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

 app.MapGet("/api/ListarMunicipios/{uf}",
     async (string uf, IIbgeService ibgeService, IMemoryCache cache) =>
     {
         string ufUpper = uf.ToUpper();
         string CACHE_KEY = $"MUNICIPIOS_{ufUpper}";
         if (cache.TryGetValue(CACHE_KEY, out List<Municipio> municipios))
         {
             return Results.Ok(municipios);
         }

         var memoryOptions = new MemoryCacheEntryOptions
         {
             AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2),
             SlidingExpiration = TimeSpan.FromMinutes(1)
         };
         var ufs = await ibgeService.ListaMunicipios(ufUpper);
         cache.Set(CACHE_KEY, ufs,memoryOptions);

         return Results.Ok(ufs);
     });
app.Run();