
# Sistema de Gestión de Ventas y Facturación

Solución Full-Stack con API REST (.NET Core C#) y Frontend (React + Bootstrap 5) para gestionar clientes, órdenes y facturación.

---

## Tecnologías y Justificación
- **Back-End**: .NET Core, Entity Framework Core, Inyección de Dependencias, manejo de ciclos JSON (`ReferenceHandler.IgnoreCycles`).
- **Front-End**: React (hooks para estado) + Bootstrap 5 (UI responsive).
- **Comunicación**: Axios para peticiones API.

---

## Requerimientos Funcionales
1. **Clientes**: CRUD (POST `/api/GestionClientes`).
2. **Órdenes**: Creación, actualización de stock (PUT/DELETE) y facturación de órdenes (`/api/Order`).
3. **Factura**: Generación y previsualización estructurada (`/api/Factura/{orderId}`).

---

## Ejecución Local
1. **BD**: Ejecutar scripts en `/sqlDb`.
2. **API**: `cd DEADLOCK.APP` -> `dotnet run` (Swagger en `https://localhost:7134`).
3. **App**: `cd Cliente` -> `npm install` -> `npm start` (`http://localhost:3000`).

---
💻 **Desarrollado como evaluación técnica unificada.**




















<img width="1503" height="866" alt="image" src="https://github.com/user-attachments/assets/09ef6966-52ba-483f-ab4f-c5de62d231c3" />
