# 🚀 Guía de Configuración de Tenki Cloud para HolaMundoNet10

## ✅ Paso 1: Ya completado - Registro en Tenki Cloud
Ya te registraste en https://app.tenki.cloud/

## 📋 Paso 2: Conectar tu repositorio de GitHub

### 2.1 Acceder al Dashboard de Tenki
1. Inicia sesión en https://app.tenki.cloud/
2. Ve a la sección de **"Runners"** o **"Organizations"**

### 2.2 Instalar la GitHub App de Tenki
1. En el dashboard de Tenki, haz clic en **"Add Organization"** o **"Connect GitHub"**
2. Selecciona tu cuenta de GitHub (`jose16-21`)
3. Instala la Tenki GitHub App en tu repositorio
4. Otorga permisos a Tenki para acceder a:
   - `clean-arquitect-tenki` (o todos los repositorios si prefieres)

### 2.3 Configurar el Runner
1. Después de conectar, Tenki generará automáticamente un **runner group**
2. Copia el nombre del runner o usa los estándar:
   - `tenki-standard-small-2c-4g` (2 vCPU, 4GB RAM)
   - `tenki-standard-medium-4c-8g` (4 vCPU, 8GB RAM) ⭐ Recomendado
   - `tenki-standard-large-8c-16g` (8 vCPU, 16GB RAM)
   - `tenki-autoscale` (escala automáticamente) 🚀 Mejor opción

## 🔧 Paso 3: Workflows ya están listos

He creado 2 workflows para ti:

### 📄 `.github/workflows/ci-cd.yml`
Workflow principal de CI/CD que se ejecuta en:
- Push a `main`, `develop`, o ramas `feature/*`
- Pull requests a `main` o `develop`

**Incluye:**
- ✅ Build del proyecto .NET 10
- ✅ Restauración de dependencias
- ✅ Compilación en Release
- ✅ Tests (si existen)
- ✅ Health check de la aplicación
- ✅ Build de imagen Docker (solo en main)
- ✅ Performance check con autoscale

### 📄 `.github/workflows/tenki-test.yml`
Workflow de prueba para comparar diferentes runners de Tenki:
- Prueba con runner Small (2 vCPU)
- Prueba con runner Medium (4 vCPU)
- Prueba con runner Autoscale

**Ejecutar:** Puedes ejecutarlo manualmente desde GitHub Actions UI

## 🚀 Paso 4: Push y verificar

### 4.1 Commit y push de los workflows
```bash
git add .github/
git commit -m "feat: Agregar workflows de CI/CD con Tenki Cloud"
git push origin feature/config-tenki
```

### 4.2 Verificar en GitHub
1. Ve a tu repositorio: https://github.com/jose16-21/clean-arquitect-tenki
2. Click en la pestaña **"Actions"**
3. Deberías ver los workflows ejecutándose con Tenki runners

### 4.3 Ver los resultados
- Los workflows se ejecutarán automáticamente
- Verás el ícono de Tenki en los runners
- Compara la velocidad vs GitHub runners estándar

## 📊 Paso 5: Monitorear en Tenki Dashboard

### 5.1 Ver uso y métricas
1. Ve a https://app.tenki.cloud/
2. En el dashboard verás:
   - ⏱️ Tiempo de ejecución de cada job
   - 💰 Costos (tienes $10 gratis mensuales)
   - 📈 Performance comparado con GitHub runners
   - 🔄 Jobs en ejecución

### 5.2 Créditos gratis
- Cada mes recibes **$10 en créditos gratis**
- Esto equivale a **~12,500 minutos** en runner small
- Suficiente para desarrollo y pruebas

## 💡 Comparación de Costos

### GitHub Hosted Runners (ubuntu-latest)
- 2 vCPU: **$0.008/min** = $0.48/hora
- Más lento por ser compartido

### Tenki Runners
- 2 vCPU, 4GB: **$0.0008/min** = $0.048/hora (90% más barato) 🎉
- 4 vCPU, 8GB: **$0.0016/min** = $0.096/hora (90% más barato) 🎉
- **30% más rápido** por ser bare metal 🚀

## 🔐 Seguridad

Tenki está trabajando en:
- SOC 2 Type II certification
- GDPR compliance
- Infraestructura en data centers certificados

## 🆘 Solución de Problemas

### Error: "Runner not found"
**Solución:** Verifica que instalaste correctamente la GitHub App de Tenki en tu repositorio.

### Error: "Permission denied"
**Solución:** Asegúrate de que Tenki tiene permisos de Actions en tu repositorio.

### Workflows no se ejecutan
**Solución:** 
1. Ve a Settings → Actions → General en GitHub
2. Verifica que "Allow all actions and reusable workflows" esté habilitado

## 📚 Recursos Adicionales

- 📖 Documentación oficial: https://www.tenki.cloud/docs
- 💬 Soporte: hello@tenki.cloud
- 🐛 Issues: En el dashboard de Tenki Cloud

## 🎯 Próximos Pasos Recomendados

1. ✅ Push de los workflows (Paso 4)
2. ⚡ Ejecutar el workflow de prueba manualmente
3. 📊 Comparar métricas en el dashboard de Tenki
4. 🚀 Optimizar tus workflows según necesidades
5. 💰 Revisar costos vs GitHub runners

---

## ⚡ Quick Start Commands

```bash
# 1. Ver los workflows creados
ls -la .github/workflows/

# 2. Commit y push
git add .github/
git commit -m "feat: Configurar Tenki Cloud runners"
git push origin feature/config-tenki

# 3. Ver el status en GitHub
# Abre: https://github.com/jose16-21/clean-arquitect-tenki/actions

# 4. Ejecutar workflow de prueba manualmente
# Ve a Actions → "Prueba de Tenki Runners" → Run workflow
```

---

**¡Listo!** 🎉 Ahora tienes Tenki configurado y listo para usar runners 30% más rápidos y 90% más baratos.
