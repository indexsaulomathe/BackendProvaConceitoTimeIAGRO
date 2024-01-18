# Documentação do Projeto

## Requisitos de Instalação

Antes de começar, certifique-se de ter o Dotnet SDK 8.0.101 instalado.
Você pode baixá-lo [aqui](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-8.0.101-windows-x64-installer).

Verifique se o SDK foi instalado corretamente utilizando o seguinte comando no terminal:

```bash 
dotnet --list-sdks
```

### Como Rodar o Projeto

1. Clonar o Repositório

Clone o repositório usando o seguinte comando:

```bash
 git clone https://github.com/indexsaulomathe/BackendProvaConceitoTimeIAGRO
```

2. Abrir a Pasta no Visual Studio Code

Abra a pasta clonada no Visual Studio Code para facilitar a visualização e execução do projeto.

3. Rodar a API
   3.1. Rodar a Catalogo.API
   Abra no terminal na pasta /src/Catalogo.API.

```bash
cd src/Catalogo.API
```

Em seguida, execute os seguintes comandos:

```bash
dotnet restore
dotnet build
dotnet run
```

3.1.1 Testar os Endpoints
Se desejar testar os endpoints disponíveis, há um arquivo chamado rest.http na pasta /resClient/. Esse arquivo contém rotas montadas que podem ser testadas diretamente no Visual Studio Code com a extensão REST Client.

VS Marketplace Link: https://marketplace.visualstudio.com/items?itemName=humao.rest-client
