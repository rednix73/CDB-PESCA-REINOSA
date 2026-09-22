# Assessment: SDK-style Conversion (Setup-cdbpesca)

## Proyectos evaluados
| Proyecto | Path | packages.config | Includes explícitos | Custom Imports | Tipo especial | Riesgo |
|---------|------|-----------------|---------------------|----------------|---------------|-------|
| Setup-cdbpesca | CLub_Deportivo_v2/Setup-cdbpesca/Setup-cdbpesca.wixproj | No | Sí (Product.wxs) | Sí (Wix.targets, $(WixTargetsPath)) | WiX installer | Alto |

## Línea base
- Solution build: No (no se ha intentado construir aún desde este flujo)
- Cambios pendientes en git: Sí (se commitearon antes de crear la rama de trabajo)

## Hallazgos clave
- El proyecto WiX es un proyecto MSBuild clásico (no SDK-style). Tiene imports explícitos a Microsoft WiX targets y una entrada <Compile Include="Product.wxs" /> para el archivo WXS. Esto significa que la conversión a SDK-style no es directa y probablemente innecesaria para arreglar la compilación del instalador.
- Riesgos altos para conversión SDK-style: WiX usa targets personalizados y dependencias de herramientas (WiX Toolset). Convertir a SDK-style podría requerir mantener o re-adaptar estos imports y asegurar compatibilidad con las herramientas de compilación.

## Recomendación inmediata
1. Intentar compilar el proyecto .wixproj para capturar el error de compilación actual del instalador (msbuild). Esto nos dará el mensaje de error concreto y permitirá decidir si la solución requiere actualizar WiX Toolset, corregir rutas, o ajustar el .wixproj.
2. No convertir a SDK-style sin primero resolver la causa de fallo: la conversión es un cambio mayor y no es la primera opción para arreglar un instalador que no compila.

## Next steps propuestos
- Ejecutar: msbuild CLub_Deportivo_v2\Setup-cdbpesca\Setup-cdbpesca.wixproj /t:Build /p:Configuration=Debug /p:Platform=x86 y recopilar salida.
- Informar del error encontrado y proponer corrección (instalar WiX Toolset, corregir rutas de import, arreglar Product.wxs).