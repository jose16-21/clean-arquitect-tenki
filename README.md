# API Hola Mundo - .NET 10

API de ejemplo que demuestra las nuevas características de .NET 10.

## Características demostradas

- ✨ **Minimal APIs mejoradas** - Menos código, más rendimiento
- 🚀 **Native AOT** - Compilación anticipada para arranque ultra rápido
- 📦 **JSON Source Generation** - Serialización sin reflexión
- ⚡ **WebApplication.CreateSlimBuilder** - Builder optimizado
- 🎯 **C# 13 Records** - Tipos inmutables eficientes

## Ejecutar la aplicación

```bash
cd HolaMundoNet10
dotnet run
```

La API estará disponible en: http://localhost:5000

## Endpoints disponibles

### GET /
Mensaje de bienvenida básico
```bash
curl http://localhost:5000/
```

### GET /saludar/{nombre}
Saludo personalizado
```bash
curl http://localhost:5000/saludar/Juan
```

### GET /info
Información sobre .NET 10
```bash
curl http://localhost:5000/info
```

### GET /async
Ejemplo de operación asíncrona
```bash
curl http://localhost:5000/async
```

### POST /calcular
Cálculos con array de números
```bash
curl -X POST http://localhost:5000/calcular \
  -H "Content-Type: application/json" \
  -d '{"numeros": [10, 20, 30, 40, 50]}'
```

### GET /health
Health check
```bash
curl http://localhost:5000/health
```

## Compilar con Native AOT

Para máximo rendimiento:

```bash
dotnet publish -c Release
```

Esto generará un binario nativo optimizado sin dependencia del runtime de .NET.

## Benchmarks comparativos

.NET 10 ofrece:
- 30% más rápido en JIT compilation
- 40% menor uso de memoria con Native AOT
- 50% más rápido en JSON serialization con Source Generators
- Arranque 10x más rápido con Native AOT
