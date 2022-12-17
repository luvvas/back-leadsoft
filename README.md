# Summary

- [About](#about)
- [Getting Started](#getting_started)
- [Project](#project)

# About <a name = "about"></a>

The goal of this project is to create a miniblog API that manages data from a PostgreSQL database. The application was developed using .NET 6.

# Getting Started <a name = "getting_started"></a>

## Packages

- Microsoft.EntityFrameworkCore (7.0.0)
- Microsoft.EntityFrameworkCore.Tools (7.0.0)
- Npgsql.EntityFrameworkCore.PostgreSQL (7.0.0)
- Swashbuckle.AspNetCore (6.2.3)
- AutoMapper.Extensions.Microsoft.DependencyInjection (12.0.0)

## Instalation

Clone repository

     λ git clone https://github.com/luvvas/back-leadsoft.git
    
or

     λ git clone git@github.com:luvvas/back-leadsoft.git

## Running

The configuration for your database is inside appsettings.json

```json
"ConnectionStrings": {
     "DefaultConnection": "User ID=postgres;Password=admin;Host=localhost;Port=5432;Database=postgres;Pooling=true;"
},
```

Update Migrations

    λ dotnet ef database update
    
or
    
    λ database-update
    
Run Project

    λ .\testeLeadSoft
    λ dotnet run

# Project <a name = "project"></a>

## Entities

- **Author**
  - id: Guid;
  - FirstName: string;
  - LastName: string;
  - Age: number;

- **Article**
  - id: Guid;
  - Title: string;
  - Description: string;
  - Text: string;
  - AuthorId : Guid;
  - CategoryId: Guid;

- **Category**
  - id: Guid;
  - Name: string;
  - Type: string;

- **Comment**
  - id: Guid;
  - Text: string;
  - ArticleId: Guid;

## Routes

The base url is *http://localhost:7269*

### **Create an Author**

     POST /Author
     
**Body example**
```json
{
  "firstName": "John",
  "lastName": "Doe",
  "age": 20,
}
```

**Response example**
```json
  "data": {
    "id": "<AUTHOR_ID>",
    "firstName": "John1",
    "lastName": "Doe1",
    "age": 21
  },
  "success": true,
  "message": "string"
```

### **Update an Author**

     PUT /Author/:id

**Body example**
```json
{
  "id": "<AUTHOR_ID>",
  "firstName": "John1",
  "lastName": "Doe1",
  "age": 21
}
```

**Response example**
```json
  "data": {
    "id": "<AUTHOR_ID>",
    "firstName": "John1",
    "lastName": "Doe1",
    "age": 21
  },
  "success": true,
  "message": "string"
```

### **Get an Author**

     GET /Author/:id

**Response example**
```json
  "data": {
    "id": "<AUTHOR_ID>",
    "firstName": "John1",
    "lastName": "Doe1",
    "age": 21,
    "articles": [
      {
        "id": "<ARTICLE_ID>",
        "title": "teste",
        "description": "teste",
        "text": "teste",
        "authorId": "<AUTHOR_ID>",
        "categoryId": "<CATEGORY_ID>",
        "comments": [
          {
            "id": "<COMMENTARY_ID>",
            "text": "teste",
            "articleId": "<ARTICLE_ID>"
          }
        ]
      }
    ]
  },
  "success": true,
  "message": "string"
```

### **Get all Authors**

     GET /Author/
     
**Response example**
```json
{
  "data": {
      "id": "067220df-ae94-41c8-9452-f4fcf2805cf3",
      "firstName": "John",
      "lastName": "Doe",
      "age": 21,
      "articles": [
        {
          "id": "86703122-878a-43bf-996e-ca4285aefa0d",
          "title": "teste",
          "description": "teste",
          "text": "teste",
          "authorId": "067220df-ae94-41c8-9452-f4fcf2805cf3",
          "categoryId": "9553087f-688f-4a4c-ab21-334a6b69525c",
          "comments": [
            {
              "id": "c7a0f525-0a22-425b-947f-60b183c7be62",
              "text": "teste",
              "articleId": "86703122-878a-43bf-996e-ca4285aefa0d"
            },
            {
              "id": "40561caf-adcf-49a5-9a50-e21e5a8b4050",
              "text": "teste",
              "articleId": "86703122-878a-43bf-996e-ca4285aefa0d"
            }
          ]
        }
      ]
    },
    "success": true,
    "message": "Author successfully listed."
}
```

### **Delete an Author**

     DELETE /Author/:id
     
```json
{
  "data": [
    {
      "id": "<AUTHOR_ID>",
      "firstName": "John1",
      "lastName": "Doe1",
      "age": 21
    },
    {
      "id": "<AUTHOR_ID>",
      "firstName": "John2",
      "lastName": "Doe2",
      "age": 22
    },
  ],
  "success": true,
  "message": ""
}
```
