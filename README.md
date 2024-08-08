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

## Exemplos de uso:

Para utilizar os padrões de retorno da Stick.Core é necessário utilizar o ***BaseResponse<T>*** para apresentar resultados diversos. Esta classe foi preparada para aceitar um tipo genérico.
Por exemplo, o retorno de um método do tipo POST retornará uma resposta com o Id do objeto criado no banco de dados, então poderíamos criar uma classe que terá uma propriedade do tipo GUID ou Long chamada *RegisterResult*:

```csharp
public class RegisterResult<T>
{
    public T? Id { get; set; }
}
```

E no controller poderemos especificar o retorno do nosso endpoint da seguinte forma:

```csharp
[HttpPost]
public async Task<BaseResponse<RegisterResult<Guid>>> Add(Model request)
{
    // Código que cria um objeto no banco de dados e retorna
}
```

Na documentação do Swagger será exibido o seguinte retorno de resposta para sucesso:

![swagger1 img](/readme-imgs/swagger_1.png)

Caso houver um erro, será exibido o seguinte retorno padrão:

![swagger2 img](/readme-imgs/swagger_2.png)

Explicação: A classe ***BaseResponse<T>*** recebe como paramêtro o tipo ***RegisterResult<Guid>***

Caso exista algum endpoint que não julgue necessário retornar um objeto de resposta, a classe ***Response*** dará uma resposta de sucesso vazia, porém caso houver erros na execução da aplicação, retornará um objeto de erro padrão igual ao que contém na classe de retorno anterior.

Por exemplo, o retorno de um método do tipo DELETE retornará uma resposta vázia:

```csharp
[HttpPost]
public async Task<Response> Delete(Guid Id)
{
    // Código que deleta um objeto no banco de dados e retorna uma resposta vazia
}
```

***IMPORTANTE***
Para que não haja erros na documentação dos retornos dos endpoints no Swagger é ideal que sempre utilize as classes ***BaseResponse<T>*** ou ***Response*** como bases de respostas para todos os endpoints da sua aplicação, do contrário o filtro para especificar cada tipo de resposta não funcionará corretamente.

### Retornar um status code diferente do padrão (200 ou 400):

Também é possível especificar o StatusCode do retorno da requisição para respostas de sucesso ou erro com métodos de extensão da classe ***ErrorResponse*** chamados *WithErrorStatusCode<T>()* e *WithSuccessStatusCode()* da seguinte forma:

Para retornos de erro:
```csharp
public async Task<BaseResponse<RegisterResult<Guid>>> Add(Model request)
{
    // O método aceita um enum Http Status Code do erro
    return new ErrorResponse("Error", "Houve algum erro na realização da operação.")
            .WithErrorStatusCode<RegisterResult<Guid>>(HttpStatusCode.BadRequest); // É necessário especificar o tipo de retorno do método
}
```

Para retornos de sucesso, podemos utilizar um dos exemplos anteriores, onde a resposta de sucesso de um método chamado Add retorna uma BaseResponse do tipo RegisterResult<Guid>
```csharp
public async Task<BaseResponse<RegisterResult<Guid>>> Add(Model request)
{
    // O método deve vir acompanhado de um enum Http Status Code
    return new RegisterResult<Guid>
    {
        Id = Guid().NewGuid()
    }.WithSuccessStatusCode(HttpStatusCode.Accepted); // Para este caso, não é necessário especificar o tipo de retorno do método
}
```

Para respostas do tipo ***Response*** pode ser feito da seguinte forma:

```csharp
public async Task<Response> Update(User request)
{
    // Para erros
    return Response.ErrorHandle("Error", "Usuário não encontrado.", HttpStatusCode.NotFound);

    // Para sucessos (status code 200)
    return Response.Success();

    // Para sucessos com diferentes status code
    return Response.Success(HttpStatusCode.Accepted);
}
```

## Paginação

Para realizar páginação na sua aplicação é necessário utilizar as classes ***PageRequest*** e ***PageResult***.

Na controller, defina como o seguinte exemplo:
```csharp
[HttpGet]
public async Task<BaseResponse<PageResult<User>>> ListAllUsers([FromQuery] PageRequest request)
{
    // Código para retornar usuários páginados
}
```

Explicação: a classe BaseResponse receberá como tipo um resultado paginado ***PageResult***, onde terá uma lista de Usuários *User*

O usuário deverá especificar na query os parametros **Page**(página atual) e **PageSize**(quantidade de items por página) solicitados no objeto citado acima, assim como, a propriedade ***SortDirection***, indicando a direção de ordenação (ascendente ou descendente), por padrão retornará uma ordenação ascendente.

Para paginar o resultado do seu retorno basta utilizar o método de extensão ***Paginate()*** ou ***PaginateBy()*** para realizar uma ordenação por alguma propriedade específica.
```csharp
public async Task<BaseResponse<PageResult<User>>> ListAllUsers(PageRequest request)
{
    var result = await db.Users
        .Paginate(request) // Paginando sem ordenação
        .ToListAsync(cancellationToken);

    var result_ordered_by_name = await db.Users
        .PaginateBy(request, x => x.Name) // Paginando com ordenação
        .ToListAsync(cancellationToken);

    return new PageResult<User>(result);
}
```