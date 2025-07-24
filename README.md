# ☕ Cafetería Institucional - Sistema Web

Este proyecto es una aplicación web desarrollada en **ASP.NET con C#** para gestionar las operaciones de una cafetería institucional. Está diseñado para facilitar el control de ventas, productos, usuarios y reportes en un entorno administrativo eficiente y moderno.

## 🚀 Características principales

- 🧾 Gestión de productos: agregar, editar, eliminar y visualizar inventario.
- 👥 Administración de usuarios: roles, permisos y autenticación.
- 💰 Registro de ventas: interfaz para cajeros con cálculo automático.
- 📊 Reportes dinámicos: ventas por día, producto más vendido, ingresos totales.
- 🎨 Interfaz responsiva: diseño adaptado con **Bootstrap** y Razor Views.
- 🔐 Seguridad: autenticación y autorización basada en roles.

## 🛠️ Tecnologías utilizadas

| Tecnología     | Propósito                          |
|----------------|------------------------------------|
| ASP.NET MVC    | Estructura del proyecto web        |
| C#             | Lógica de negocio y controladores  |
| Razor Views    | Renderizado dinámico del frontend  |
| Bootstrap      | Estilos responsivos y UI moderna   |
| JavaScript     | Funcionalidad dinámica en cliente  |
| SQL Server     | Base de datos relacional           |
| Entity Framework | ORM para acceso a datos          |

## 📦 Instalación y configuración

### Requisitos previos

- Visual Studio 2022 o superior
- .NET SDK 6.0 o superior
- SQL Server (Express o completo)

### Pasos para ejecutar el proyecto

```bash
# Clona el repositorio
git clone https://github.com/tu-usuario/cafeteria-institucional.git

# Abre el proyecto en Visual Studio
# Restaura los paquetes NuGet
# Configura la cadena de conexión en appsettings.json

# Ejecuta migraciones (si usas EF Core)
Update-Database

# Ejecuta la aplicación
Ctrl + F5


# Clona el repositorio (link)
git clone https://github.com/tu-usuario/cafeteria-institucional.git

#CafeteriaInstitucional/
│
├── Controllers/        # Controladores MVC
├── Models/             # Modelos de datos
├── Views/              # Razor Views
├── wwwroot/            # Archivos estáticos (CSS, JS, imágenes)
├── Data/               # Contexto de base de datos
├── Services/           # Lógica de negocio adicional
├── appsettings.json    # Configuración general
└── Program.cs          # Punto de entrada

#Autores 

Ricardo Mora Lopez 
Dylan Castro Bolaños
Kendall Calvo Alfaro
Jordan Segura Carrillo
Fabian Cambronero Nuñez

MIT License

Copyright (c) 2025 Dylan

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
