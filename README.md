# 🛒 NovaCommerce API

A **NovaCommerce API** é uma aplicação desenvolvida em **ASP.NET Core (.NET 8)** com o objetivo de simular um sistema de e-commerce completo, aplicando conceitos reais de backend como autenticação, persistência de dados, organização em camadas e deploy em nuvem.

O projeto foi desenvolvido seguindo boas práticas de APIs REST, utilizando **Entity Framework Core** para acesso a dados, **JWT** para autenticação e **Docker** para padronização do ambiente, além de pipeline automatizado com **GitHub Actions**.

---

## 🚀 Tecnologias Utilizadas

- .NET 8 / ASP.NET Core  
- C#  
- Entity Framework Core  
- SQL Server  
- JWT (Autenticação e Autorização)  
- Docker  
- Swagger / OpenAPI  
- GitHub Actions (CI/CD)  
- Deploy em Cloud  

---

## ⚙️ Funcionalidades

- Cadastro e autenticação de usuários com JWT  
- Controle de acesso baseado em autenticação  
- CRUD de Produtos, Categorias e Autores  
- Persistência de dados com Entity Framework Core  
- Documentação automática da API com Swagger  
- Containerização da aplicação com Docker  
- Pipeline de CI/CD automatizado  
- Deploy em ambiente cloud  

---

## 🧩 Estrutura do Projeto



NovaCommerce.API/
├── Controllers/
├── Data/
│ └── DataContext.cs
├── DTOs/
├── Models/
├── Services/
├── Migrations/
├── Program.cs
├── appsettings.json
├── Dockerfile
├── docker-compose.yml
└── .github/workflows/


---

## 🔧 Executando Localmente

Clone o repositório:

```bash
git clone https://github.com/Alef0814/NovaCommerce.API.git


Acesse a pasta do projeto:

cd NovaCommerce.API


Execute com Docker:

docker-compose up --build


Ou execute localmente com .NET CLI:

dotnet restore
dotnet run


Acesse a documentação da API via Swagger:

http://localhost:5000/swagger

☁️ Deploy

O projeto está configurado com pipeline automatizado de CI/CD, onde a cada novo commit no branch principal:

O projeto é buildado

Testado

Publicado automaticamente em ambiente cloud

A automação é realizada utilizando GitHub Actions.

👨‍💻 Autor

Alef do Nascimento Pinto
Desenvolvedor Backend .NET

📌 GitHub: https://github.com/Alef0814

🏁 Status do Projeto

✅ Concluído e em funcionamento
