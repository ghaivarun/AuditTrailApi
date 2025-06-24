# 🔍 AuditTrail API (.NET 8)

## 📌 Overview

This is a .NET 8 Web API that compares two object states (`Before` and `After`) and tracks changes based on specified audit actions: `Created`, `Updated`, or `Deleted`. It is a stateless API with no database — responses are returned directly from memory.

---

## ✅ Features

- Compares object differences dynamically at runtime.
- Supports three audit action types: Created, Updated, Deleted.
- Returns only changed fields with `before` and `after` values.
- Includes metadata such as:
  - `timestamp`
  - `userId`
  - `entityName`
- Input validation handled in the controller.

---

## 🏗️ Tech Stack

- ASP.NET Core Web API (.NET 8)
- C#
- Swagger (Swashbuckle)
- JSON Serialization (System.Text.Json)

---

## 🚀 How to Run

1. **Open in Visual Studio 2022+** or VS Code.
2. Restore dependencies (if needed):
   ```bash
   dotnet restore
   ```
3. Run the application:
   ```bash
   dotnet run
   ```
4. Navigate to Swagger UI:
   ```
   https://localhost:<port>/swagger
   ```

---

## 📤 Endpoint

### `POST /api/AuditTrail/log`

#### ✅ Sample Input

```json
{
  "before": { "Id": 1, "Name": "Varun", "Age": 30 },
  "after":  { "Id": 1, "Name": "Ajay",  "Age": 31 },
  "userId": "admin123",
  "entityName": "User",
  "action": "Updated"
}
```

#### ✅ Sample Output

```json
{
  "entityName": "User",
  "userId": "admin123",
  "action": "Updated",
  "timestamp": "2025-06-24T14:00:00Z",
  "changes": {
    "Name": { "before": "Varun", "after": "Ajay" },
    "Age":  { "before": "30", "after": "31" }
  }
}
```

---

## 🧪 Input Validation Rules

| Action   | Required Fields     |
|----------|---------------------|
| Created  | `after` only        |
| Updated  | `before` + `after`  |
| Deleted  | `before` only       |

Returns `400 Bad Request` if validation fails.

---

## ⚠️ Error Handling

- All unhandled exceptions are caught by custom middleware.
- Response format:
```json
{
  "status": 500,
  "message": "An unexpected error occurred.",
  "detail": "Exception message here"
}
```

---

## 📁 Project Structure

```
AuditTrailApi/
├── Controllers/
├── Models/
├── Services/
├── Program.cs
├── .gitignore
├── README.md
```

---

## 👤 Author

**Varun Ghai**  
.NET Full Stack Developer  
📧 Varunghai922@gmail.com  

---

## ✅ Notes
- No database or persistence — all logic is in-memory and stateless.
- Built and tested with .NET 8 SDK.
