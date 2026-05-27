# 🚀 API mínimal - projeto .NET

Projeto desenvolvido utilizando .NET, com arquitetura em camadas, testes automatizados e integração com banco de dados MySQL.

---

## 📁 Estrutura do Projeto

```
Api/         -> API principal (Domínio, Serviços, Infraestrutura)
Tests/       -> Testes unitários (MSTest)
Dockerfile   -> Configuração para container Docker
```

---

## ⚙️ Tecnologias Utilizadas

- .NET 10
- Entity Framework Core
- MySQL
- MSTest
- Docker

---

## ✅ Funcionalidades Implementadas

- Cadastro de Administrador  
- Listagem de Administradores  
- Busca de Administrador por ID  
- Login de Administrador  
- Testes unitários para entidades e serviços  

---

## 🧠 Arquitetura

O projeto segue uma separação em camadas:

- **Domínio** → Entidades e regras de negócio  
- **Serviços** → Lógica da aplicação  
- **Infraestrutura** → Banco de dados (EF Core)  
- **Testes** → Testes automatizados  

---

## 🌐 Acesso à API

A API está disponível online em:

- API: https://minimal-api-1.onrender.com

---

## 📄 Interface Swagger

A documentação interativa da API pode ser acessada em:

- Swagger UI: https://minimal-api-1.onrender.com/swagger/index.html

O Swagger permite testar os endpoints diretamente pelo navegador, facilitando a visualização e validação das rotas da API.

---

## 🧪 Testes

Os testes foram implementados utilizando MSTest, cobrindo:

- Entidades (Get/Set)  
- Serviços (regras de negócio)  

Boas práticas aplicadas nos testes:

- ✔ Controle manual de dados  
- ✔ Banco limpo antes de cada execução  
- ✔ Sem uso de `HasData` para evitar conflitos  

---

## 🐳 Como executar com Docker

```bash
docker build -t minimal-api .
docker run -p 5000:5000 minimal-api
```

---

## ▶️ Como executar o projeto

### Rodar a API

```bash
cd Api
dotnet run
```

### Rodar os testes

```bash
cd Tests
dotnet test
```

---

## 📌 Boas práticas aplicadas

- Separação de responsabilidades  
- Testes automatizados com MSTest  
- Uso de Entity Framework Core  
- Controle de dados nos testes  
- Código organizado e modular  

---


Rubia de Souza Brito
