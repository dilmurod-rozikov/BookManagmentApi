# BookManagementAPI

BookManagementAPI is a simple RESTful API built with **ASP.NET Core 8** and **Entity Framework Core** that provides functionality to manage books. The API supports CRUD operations, soft deletes, and bulk inserts.

## Features
- 📚 **Manage Books** (Add, Update, Delete, Get)
- 🔄 **Bulk Insert & Delete**
- 🗑️ **Soft Delete Implementation**
- 🔍 **Retrieve Only Active Books**
- 📡 **RESTful API with Clean Architecture**
- 🛠 **Uses Dependency Injection & Best Practices**

## Technologies Used
- **ASP.NET Core 8**
- **Entity Framework Core**
- **SQL Server**
- **AutoMapper(could have used)**
- **Swagger (API Documentation)**

## Three-Layered Architecture

- **The project follows a 3-layered architecture to maintain separation of concerns and improve scalability:**
- **Presentation Layer (Controllers) - Handles HTTP requests and responses using ASP.NET Core Web API.**
- **Business Logic Layer (Services) - Implements business rules and interacts with the data layer.**
- **Data Access Layer (Repositories) - Manages database interactions using Entity Framework Core.**

## API Endpoints

### 📌 Book Endpoints
| Method | Endpoint | Description |
|--------|----------------|-------------|
| `GET` | `/api/books` | Get all active books |
| `GET` | `/api/books/{id}` | Get a book by ID |
| `POST` | `/api/books` | Add a new book |
| `PUT` | `/api/books/{id}` | Update book details |
| `DELETE` | `/api/books/{id}` | Soft delete a book |
| `POST` | `/api/books/bulk-insert` | Add multiple books |
| `DELETE` | `/api/books/bulk-delete` | Soft delete multiple books |

---

## Soft Delete Implementation
Instead of permanently removing books, soft delete is implemented by adding an **IsDeleted** flag in the `Book` model:

```csharp
public class Book
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string AuthorName { get; set; }
    public bool IsDeleted { get; set; } = false;
}
```

### Filtering Out Deleted Books
All queries automatically filter out soft-deleted books:
```csharp
public async Task<List<Book>> GetBooks()
{
    return await _context.Books.Where(b => !b.IsDeleted).ToListAsync();
}
```

## Author
**DilMurod Rozikov**  
📧 [dilmurod.rozikovvv@gmail.com 
🔗 [GitHub Profile](https://github.com/dilmurod-rozikov)  

