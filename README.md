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

