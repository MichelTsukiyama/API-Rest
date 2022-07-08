# API-Rest

Construção de uma API-Rest baseada em um curso

Requisitos:

- SDK .Net 5.0;
- MySql;
- MySql Workbench;
- Docker for Windows;
- Postman;

-------

# Sumário
<br>



--------

# Criar a Solution
<br>

    dotnet new sln --name RestWithASPNET

---------

# Criar o projeto
<br>

    dotnet new webapi -o RestWithASPNET -f net5.0

-------

# Adicionar o projeto na solution
<br>

    dotnet sln add .\RestWithASPNET\RestWithASPNET.csproj

------

# Adicionar o .gitignore
<br>

    dotnet new gitignore
    git add .gitignore

---------

# Criar uma Controller(CalculadoraController)
<br>

1. Deletar os arquivos WeatherForecast.cs e WeatherForecastController.cs;
2. Criar a "CalculadoraController.cs" em Controllers;
3. Herdar de ControllerBase e inserir as Annotations \[ApiController] e \[Route("\[controller]")];
4. Criar os métodos usando Annotation \[Http{Get/Post/Put/Delete}("rota/{parametro}")];

>Obs. é possível alterar as portas e urls da aplicação em Properties, launchSettings.json;

----

# Criar a Model e Service
<br>

1. Criar o diretório Model e a entidade/classe com as propriedades(Person.cs);
2. Criar o diretório Services e a Interface com os métodos da entidade criada na Model(IPersonService.cs);
3. Criar a classe concreta que implementa os métodos da interface (PersonServiceImplementation.cs);
4. Na classe Startap adicionar o serviço usando `services.AddScoped<Interface, Classe_que_a_implementa>();`
5. Crie a controller PersonController e faça a injeção de dependência de IPersonService;

-----

# Postman
<br>

É um ferramenta capaz de fazer requisições com outros verbos http além do GET, muito utilizado para testar rotas de APIs;

Como utilizar:

1. Escolha o verbo/método http, por exemplo \[POST];
2. Insira a rota, ex.: http://localhost:5000/api/person ;
3. Dependendo do verbo utilizado, insira o as informações no header (se necessário);
4. Dependendo do verbo utilizado, insira o as informações no body (se necessário/sempre verifique o formato utilizado JSON, XML...);


> Obs. pode ser necessário desativar a verificação de certificado SSL no postman para que o mesmo funcione.

----

# Conexão com MySql
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

# Versionamento de EndPoints
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

# Alterações na arquitetura do projeto
<br>

O projeto será separado em camadas, apesar do projeto ficar mais complexo ele divide as tarefas mantendo o código menor e mais compreensível;

- Business fica responsável pelas regras de negócio (validações);
- Repository fica responsável apenas pela persistência dos dados;
  
<br>

**Trajeto da requisição até o banco de dados**

Controller -> Método Http -> Camada Business -> Repository -> Context -> BD;

<br>

**Etapas**

1. Renomear o diretório e todos os namespaces de Services para Business, incluindo as diretivas using;
2. Copiar a pasta Business e colar em RestWithASPNET, na sequência renomear a cópia para Repository;
3. Em Repository renomear IPersonService para IPersonRepository, e IPersonServiceImplementation para IPersonRepositoryImplementation, também alterando namespaces e referências;
4. Em PersonController substituir todos os service por business também;
5. Alterar a injeção de dependência em PersonBusinessImplementation para `private readonly IPersonrepository _repository` alterando também o construtor e os métodos quando necessário;
6. Atualizar a classe Startup, adicionando AddScoped() tanto para a camada Business quanto para Repository; 

----

# Migrations
<br>

As migrações são um forma de atualizar/criar o banco de dados automáticamente (sem criar as tabelas manualmente). Elas facilitam o desenvolvimento e tem como vantagem criar um histórico, e permitindo reverter para versões anteriores.

As migrações usam seu arquivo de contexto e as Classes em suas propriedades DbSet<> para criar as tabelas. Ex.:

```c# 
public DbSet<Person> Persons { get; set; }
```

Os comandos usados para fazer migrações e atualizar as tabelas no banco de dados são:

    dotnet ef migrations add <nome_da_migração>
    dotnet ef database update

> Obs. É necessário ter o pacote `Microsoft.EntityFrameWorkCore.Tools` para usar o comando `dotnet ef` .

<br>

**Outras extensões úteis para trabalhar com migrações**

- [Evolve](https://www.nuget.org/packages/Evolve/3.1.0-alpha7#usedby-body-tab) ;
- [Serilog](https://www.nuget.org/packages/Serilog/2.11.1-dev-01397) ;
- [Serilog.AspNetCore](https://www.nuget.org/packages/Serilog.AspNetCore/6.0.0-dev-00265) ;
- [Serilog.Sinks.Console](https://www.nuget.org/packages/Serilog.Sinks.Console/4.0.2-dev-00890) ;
- [MySql.Data](https://www.nuget.org/packages/MySql.Data) ;

1. Após inserir todas as dependências acima, inserir `public IWebHostEnviroment Enviroment { get;}` na classe Startup.cs e seu construtor;
2. Adicionar `Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();` no construtor da classe Startup.cs;
3. Adicionar a condição `if(Environment.IsDevelopment())
            {
                MigrateDatabase(connectionString);
            }` no ConfigureServices() na classe Startup.cs;

4. Criar o método `MigrateDatabase()` na classe Startup.cs;
5. Criar os diretórios db/dataset e db/migrations no diretório RestWithASPNET;
6. Criar o arquivo `V1__Create_Table_Person.sql` em migrations, conforme documentação do Evolve;
7. Criar o arquivo `V2__Populate_Table_Person.sql` em dataset, conforme documentação do Evolve;
8. Basta rodar o projeto e o Evolve aplica automáticamente as migrações;

> Obs. Ele cria uma tabela para controlar as alterações. 
> 
> Novas migrations devem ter seu nome alterado para V3__, V4__ ...
>
>Tentar alterar uma migração vai gerar um erro, uma vez que ela já foi migrada;

----