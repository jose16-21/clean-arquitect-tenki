# HolaMundoNet10 API

API REST con .NET 10 implementando **Clean Architecture**, **CQRS Pattern** y **FluentValidation**.

## 🏗️ Arquitectura

### Capas de Clean Architecture
```
┌─────────────────────────────────────┐
│        Presentation Layer           │
│  (Program.cs, Endpoints, DI)        │
├─────────────────────────────────────┤
│        Application Layer            │
│  (DTOs, Services, UseCases, CQRS)   │
├─────────────────────────────────────┤
│      Infrastructure Layer           │
│     (Repositories, Persistence)     │
├─────────────────────────────────────┤
│          Domain Layer               │
│      (Entities, Validators)         │
└─────────────────────────────────────┘
```

### Patrones Implementados
- ✅ **Clean Architecture**: Separación en 4 capas
- ✅ **CQRS**: Commands y Queries separados
- ✅ **Repository Pattern**: Abstracción de persistencia
- ✅ **Use Case Pattern**: Lógica de negocio encapsulada
- ✅ **Dependency Injection**: Contenedor IoC configurado
- ✅ **FluentValidation**: Validaciones declarativas

## 🚀 Inicio Rápido

### Ejecutar con .NET
```bash
cd HolaMundoNet10
dotnet run
```

### Ejecutar con Docker
```bash
# Construir imagen
docker build -t holamundonet10:latest .

# Ejecutar contenedor
docker run -d --name holamundonet10 -p 8080:8080 holamundonet10:latest
```

La API estará disponible en: **http://localhost:8080**

## 📚 Documentación API

### Swagger UI
Accede a la documentación interactiva: **http://localhost:8080/swagger**

### Endpoints Disponibles

#### Generales
- `GET /` - Información básica de la API
- `GET /info` - Características y arquitectura
- `GET /health` - Health check
- `GET /saludar/{nombre}` - Saludo personalizado
- `POST /calcular` - Operaciones matemáticas

#### Usuarios - Legacy (v1)
- `POST /api/usuarios` - Crear usuario con validación

#### Usuarios - Use Cases (v2)
- `POST /api/v2/usuarios` - Crear usuario con caso de uso

#### Usuarios - CQRS (v3)
- `POST /api/v3/usuarios` - Command: Crear usuario
- `GET /api/v3/usuarios` - Query: Listar usuarios (paginado)
- `GET /api/v3/usuarios/{id}` - Query: Obtener usuario

#### Formularios - Legacy (v1)
- `POST /api/formularios` - Crear formulario con validación

#### Formularios - Use Cases (v2)
- `POST /api/v2/formularios` - Crear formulario con caso de uso

#### Formularios - CQRS (v3)
- `POST /api/v3/formularios` - Command: Crear formulario
- `GET /api/v3/formularios` - Query: Listar formularios (paginado)
- `GET /api/v3/formularios/{id}` - Query: Obtener formulario

## 📝 Ejemplos de Uso

### Crear Usuario (CQRS)
```bash
curl -X POST http://localhost:8080/api/v3/usuarios \
  -H "Content-Type: application/json" \
  -d '{
    "nombre": "Juan Pérez",
    "email": "juan@example.com",
    "edad": 30,
    "salario": 60000,
    "fechaNacimiento": "1995-01-01",
    "activo": true,
    "telefono": "1234567890",
    "roles": ["user"]
  }'
```

### Listar Usuarios (CQRS)
```bash
# Sin parámetros (usa defaults: página 1, 10 items)
curl http://localhost:8080/api/v3/usuarios

# Con paginación personalizada
curl "http://localhost:8080/api/v3/usuarios?Pagina=1&TamanoPagina=20&Filtro=juan"
```

### Crear Formulario (CQRS)
```bash
curl -X POST http://localhost:8080/api/v3/formularios \
  -H "Content-Type: application/json" \
  -d '{
    "titulo": "Encuesta 2025",
    "descripcion": "Encuesta de satisfacción",
    "cantidad": 10,
    "precio": 100.0,
    "descuento": 15,
    "fechaInicio": "2025-12-01",
    "fechaFin": "2026-06-30",
    "etiquetas": ["encuesta", "2025"]
  }'
```

## 🛠️ Tecnologías

- **.NET 10** - Framework principal
- **C# 13** - Lenguaje de programación
- **FluentValidation 11.10.0** - Validaciones
- **Swashbuckle 7.2.0** - Documentación OpenAPI
- **Minimal APIs** - Endpoints ligeros
- **JSON Source Generation** - Serialización optimizada
- **Docker** - Containerización

## 🏛️ Estructura del Proyecto

```
HolaMundoNet10/
├── Domain/
│   ├── Entities/              # Entidades del dominio
│   │   ├── Usuario.cs
│   │   └── Formulario.cs
├── Application/
│   ├── DTOs/                  # Data Transfer Objects
│   ├── Services/              # Servicios (Legacy)
│   ├── UseCases/              # Casos de uso (v2)
│   ├── CQRS/                  # Commands & Queries (v3)
│   │   ├── Commands/
│   │   └── Queries/
│   └── Validators/            # FluentValidation
├── Infrastructure/
│   └── Persistence/
│       └── Repositories/      # Implementaciones de repositorios
├── Presentation/
│   ├── Endpoints/             # Definición de endpoints
│   └── Extensions/            # Configuración de servicios
├── Program.cs                 # Punto de entrada
└── Dockerfile                 # Imagen Docker
```

## 🧪 Compilación y Pruebas

### Compilar
```bash
dotnet build -c Release
```

### Ejecutar Pruebas
```bash
# Health check
curl http://localhost:8080/health

# Crear y listar usuarios
curl -X POST http://localhost:8080/api/v3/usuarios \
  -H "Content-Type: application/json" \
  -d '{"nombre":"Test","email":"test@test.com","edad":25,"salario":50000,"fechaNacimiento":"2000-01-01","activo":true,"telefono":"123456","roles":["user"]}'

curl http://localhost:8080/api/v3/usuarios
```

## 📦 Docker

### Build de la Imagen
```bash
docker build -t holamundonet10:latest .
```

### Ejecutar Contenedor
```bash
docker run -d --name holamundonet10 -p 8080:8080 holamundonet10:latest
```

### Ver Logs
```bash
docker logs holamundonet10
```

### Detener Contenedor
```bash
docker stop holamundonet10
docker rm holamundonet10
```

## 📊 Características de .NET 10

- ✅ **Rendimiento**: 30% más rápido que .NET 8
- ✅ **Minimal APIs**: Endpoints con menos código
- ✅ **JSON Source Generation**: Serialización sin reflexión
- ✅ **Native AOT**: Compilación anticipada (opcional)
- ✅ **C# 13**: Nuevas características del lenguaje

## 👥 Principios SOLID

- **SRP**: Cada clase tiene una única responsabilidad
- **OCP**: Extensible mediante interfaces
- **LSP**: Implementaciones sustituibles
- **ISP**: Interfaces segregadas
- **DIP**: Dependencias hacia abstracciones

## 📖 Versiones de la API

La aplicación expone 3 versiones arquitecturales para demostrar diferentes patrones:

1. **Legacy (v1)** - `/api/*` - Servicios tradicionales
2. **Use Cases (v2)** - `/api/v2/*` - Patrón de casos de uso
3. **CQRS (v3)** - `/api/v3/*` - Command Query Responsibility Segregation

Se recomienda usar **v3 (CQRS)** para nuevas implementaciones.

## 📄 Licencia

Este proyecto es de código abierto y está disponible bajo la licencia MIT.
