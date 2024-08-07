# Stick.Core

O pacote Stick.Core foi desenvolvido pelo Instituto SENAI de Tecnologia da Informação e Comunicação (ISTIC) para fornecer uma base padronizada e ferramentas essenciais para o desenvolvimento de aplicações em C#. Este pacote inclui funcionalidades de paginação e padrões de resposta que facilitam a criação de APIs.

## Como configurar?

Para configurar o Stick.Core em seu projeto em C# .NET, é necessário adicionar algumas injeções de dependência na StartUp.cs ou Program.cs do seu projeto.

Primeiramente precisamos configurar alguns filtros e conversores json no método ***AddControllers***

```csharp
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers(options =>
    {
        options.Filters.Add<CustomActionFilter>(); // Adicionando filtro
    })
    .AddJsonOptions(options =>
    {
        // Adicionando conversores json
        options.JsonSerializerOptions.Converters.Add(new BaseResponseJsonConverterFactory());
        options.JsonSerializerOptions.Converters.Add(new ResponseJsonConverterFactory());
    });
```

Para documentar o retorno das respostas tanto de sucesso quanto de erro é necessário adicionar algumas configurações no método ***AddSwaggerGen***

```csharp
services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(d => d.GetSchemaId()); // Identifica os schemas genéricos e adiciona-os
    options.OperationFilter<BaseResponseOperationFilter>(); // Adicionando filtros para os tipos de resposta
});
```