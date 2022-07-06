# 1. API-Rest

Construção de uma API-Rest baseada em um curso

Requisitos:

- SDK .Net 5.0;
- MySql;
- MySql Workbench;
- Postman;

> Esta branch não segue a parte em que há alteração na arquitetura do projeto

-------

# 2. Sumário
<br>

- [1. API-Rest](#1-api-rest)
- [2. Sumário](#2-sumário)
- [3. Criar a Solution](#3-criar-a-solution)
- [4. Criar o projeto](#4-criar-o-projeto)
- [5. Adicionar o projeto na solution](#5-adicionar-o-projeto-na-solution)
- [6. Adicionar o .gitignore](#6-adicionar-o-gitignore)
- [7. Criar uma Controller(CalculadoraController)](#7-criar-uma-controllercalculadoracontroller)
- [8. Criar a Model e Service](#8-criar-a-model-e-service)
- [9. Postman](#9-postman)
- [10. Conexão com MySql](#10-conexão-com-mysql)
- [11. Versionamento de EndPoints](#11-versionamento-de-endpoints)

--------

# 3. Criar a Solution
<br>

    dotnet new sln --name RestWithASPNET

---------

# 4. Criar o projeto
<br>

    dotnet new webapi -o RestWithASPNET -f net5.0

-------

# 5. Adicionar o projeto na solution
<br>

    dotnet sln add .\RestWithASPNET\RestWithASPNET.csproj

------

# 6. Adicionar o .gitignore
<br>

    dotnet new gitignore
    git add .gitignore

---------

# 7. Criar uma Controller(CalculadoraController)
<br>

1. Deletar os arquivos WeatherForecast.cs e WeatherForecastController.cs;
2. Criar a "CalculadoraController.cs" em Controllers;
3. Herdar de ControllerBase e inserir as Annotations \[ApiController] e \[Route("\[controller]")];
4. Criar os métodos usando Annotation \[Http{Get/Post/Put/Delete}("rota/{parametro}")];

>Obs. é possível alterar as portas e urls da aplicação em Properties, launchSettings.json;

----

# 8. Criar a Model e Service
<br>

1. Criar o diretório Model e a entidade/classe com as propriedades(Person.cs);
2. Criar o diretório Services e a Interface com os métodos da entidade criada na Model(IPersonService.cs);
3. Criar a classe concreta que implementa os métodos da interface (PersonServiceImplementation.cs);
4. Na classe Startap adicionar o serviço usando `services.AddScoped<Interface, Classe_que_a_implementa>();`
5. Crie a controller PersonController e faça a injeção de dependência de IPersonService;

-----

# 9. Postman
<br>

É um ferramenta capaz de fazer requisições com outros verbos http além do GET, muito utilizado para testar rotas de APIs;

Como utilizar:

1. Escolha o verbo/método http, por exemplo \[POST];
2. Insira a rota, ex.: http://localhost:5000/api/person ;
3. Dependendo do verbo utilizado, insira o as informações no header (se necessário);
4. Dependendo do verbo utilizado, insira o as informações no body (se necessário/sempre verifique o formato utilizado JSON, XML...);


> Obs. pode ser necessário desativar a verificação de certificado SSL no postman para que o mesmo funcione.

----

# 10. Conexão com MySql
<br>

1. Para fazer a conexão com com o Banco de Dados MySql é necessário instalar o pacote Pomelo.EntityFrameworkCore.MySql. Isso pode ser feito usando alguma extensão do NuGet ou pelo comando abaixo:

        dotnet add .\RestWithASPNET\  package Pomelo.EntityFrameworkCore.MySql --version 5.0.4

2. Criar a classe de contexto, Context > MySqlContext.cs;
3. Inserir a ConnectionString no appsettings.json;
4. Configurar a ConnectionString no Startup.cs;
5. Dependendo do ambiente, pode ser necessário utilizar algumas Annotations na Entidade, como: 
   - `[Table("nome_tabela")]` - faz o bind com o nome de uma tabela (em caso de nomes de tabelas diferentes);
   - `[Column("atributo")]` - faz o bind com uma coluna na tabela (em caso de nomes de colunas diferentes);

-----

# 11. Versionamento de EndPoints
<br>

O versionamento de EndPoints é importante para que a API continue funcionando para seus clientes mesmo após atualizações no código. Logo, aqueles que consumiam a versão inicial de sua aplicação(v1.0) podem continuar a utiliza-la sem a necessidade imediata de migrar para a nova versão (v2.0);

Uma forma de versionar a aplicação seria usar as rotas:

**Versão v1.0**

`https://localhost:5001/api/v1.0/controller`

**Versão v2.0**

`https://localhost:5001/api/v2.0/controller`

<br>

Também é possível utilizar pacotes de versionamento como o `Microsoft.AspNetCore.Mvc.Versioning`, onde é possível fazer o versionamento por namespace, pela rota, etc.

    dotnet add package Microsoft.AspNetCore.Mvc.Versioning --version 5.0.0

Após instalar o pacote adicionar a configuração na classe Startup.cs

```c#
services.AddApiVersioning();
```

Para usar na Controller:

```c#
[ApiVersion( "1.0" )] //Esta annotation
[ApiController]
[Route( "[controller]/v{version:apiVersion}" )]//versão
public class PeopleController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok( new[] { new Person() } );
}
```

[Link do reposítório](https://github.com/dotnet/aspnet-api-versioning/wiki/New-Services-Quick-Start#aspnet-web-api-with-odata-v40)

-----