# Truck Nav (español)

Fork de [TruckNav-Sim](https://github.com/Rares-Muntean/TruckNav-Sim) con la interfaz **ya en español**. No hace falta el TruckNav oficial ni ningún parche de traducción.

**Truck Nav** es un GPS externo para Euro Truck Simulator 2 y American Truck Simulator. Funciona como instalador de Windows, APK o navegador (móvil, tablet o segundo monitor) y ofrece seguimiento y rutas en tiempo real sobre el mapa del juego.

<div align="center">
    <a href="https://discord.gg/C5BTXCF2jC">
        <img src="https://img.shields.io/badge/Discord-Unirse_a_la_comunidad-5865F2?style=for-the-badge&logo=discord&logoColor=white" alt="Discord">
    </a>
    <a href="https://forum.scssoft.com/viewtopic.php?t=349145">
        <img src="https://img.shields.io/badge/Foros_SCS-Tema_oficial-2C3E50?style=for-the-badge&logo=discourse&logoColor=white" alt="Foros SCS">
    </a>
    <a href="https://buymeacoffee.com/raresmnt">
        <img src="https://img.shields.io/badge/Apoyar_el_proyecto-FFDD00?style=for-the-badge&logo=buymeacoffee&logoColor=black" alt="Invitar a un café">
    </a>
    <br />
    <br />
    <img width="300" alt="Mapa de TruckNav" src="https://github.com/user-attachments/assets/6860e478-3c32-4143-97c4-fca8876ce90f" />
    <img width="300" alt="Ajustes de TruckNav" src="https://github.com/user-attachments/assets/a977ea5f-af6f-49e2-adc4-78c8afef9879" />
</div>

## Instalar

### Escritorio (Windows)

1. Descarga **[TruckNav-Setup-spanish-0.5.0.exe](https://github.com/jaimecy/TruckNav-Spanish/releases/download/v0.5.0-es/TruckNav-Setup-spanish-0.5.0.exe)** desde las [Releases](https://github.com/jaimecy/TruckNav-Spanish/releases/tag/v0.5.0-es).
2. Ejecútalo y sigue el asistente de instalación.
3. Abre **TruckNav**. La interfaz sale en español.
4. Si usas tablet o móvil, instala el APK y escribe la IP que muestra la aplicación de PC.

### Navegador / desarrollo (Node.js)

**Requisitos:** [Node.js LTS](https://nodejs.org/) y [Git](https://git-scm.com/downloads).

Si PowerShell bloquea scripts, ejecuta primero:

```powershell
Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass
```

Clona e instala:

```bash
git clone https://github.com/jaimecy/TruckNav-Spanish.git
cd TruckNav-Spanish
npm install
npx nuxi dev --host 0.0.0.0
```

Abre el enlace **Network** de la terminal (`http://192.168.x.x:3000/`). El enlace Local puede fallar al leer la telemetría.

Sigue las instrucciones de la ventana de TruckNav para instalar el plugin de telemetría en la carpeta del juego.

**Opción ZIP:** en GitHub, **Code → Download ZIP**, extrae la carpeta y abre esa carpeta en la terminal.

## Estado actual: demostración / alfa

El núcleo de la navegación funciona, pero el proyecto sigue en desarrollo. El grafo de rutas se construyó a mano en **QGIS** y con scripts (tramos, rotondas e intersecciones).

- **Versión ATS / ETS2:** hasta **1.58** ✅
- **DLC:** todos ✅
- **Mods de mapa:** ninguno incluido de serie ❌ (ProMods se puede descargar desde la app si está disponible)

## Problemas conocidos

### Comportamiento habitual

- **Zonas de empresa:** la ruta puede fallar si estás muy dentro del patio. Acércate un poco a la salida antes de poner destino.
- **Huecos en el mapa:** a veces hay tramos desconectados o cambios de sentido incorrectos; en la mayoría de casos la ruta es la correcta.

### Rendimiento y compatibilidad

> [!NOTE]
> En tablets o móviles antiguos el mapa puede ir a tirones. El mapa base es **ETS2/ATS + todos los DLC (hasta v1.58)**. ProMods y otros mods de mapa **aún no están soportados de forma completa**.

> [!CAUTION]
> **Mods de nombres de empresas reales:** si usas mods que cambian nombres de empresas distintos al de **MLH82**, la navegación puede fallar. La app está pensada para vanilla y *Real companies, gas station & billboards for ATS and ETS2* de **MLH82**.

## Linux y Steam Deck

El soporte nativo oficial de Linux está en la hoja de ruta del proyecto original. Mientras tanto puedes usar la rama de la comunidad de Jeroen van Straten:

[TruckNav Linux (Jeroen van Straten)](https://github.com/jvanstraten/TruckTel)

## Cómo funciona

1. **Telemetría:** un servidor lee coordenadas, velocidad y rumbo del juego en marcha.
2. **Mapa:** las coordenadas internas se convierten a **WGS84** para las librerías de mapas web.
3. **Rutas:** un grafo propio calcula el camino más corto hasta el destino.

<div align="center">
    <img width="895" height="649" alt="GPS de TruckNav" src="https://github.com/user-attachments/assets/4c593709-6f91-4109-9685-bc292ead920e" />
</div>

## Cómo ayudar a mejorar el mapa

Si ves un fallo de navegación, envía:

- una descripción breve
- una captura de la app

a **una** de estas vías (proyecto original):

- raresmnt@yahoo.com
- Discord → canal de errores
- GitHub → Issues

Los avisos se atienden cuando hay tiempo.

## Créditos

### [@truckermudgeon](https://github.com/truckermudgeon)

Gracias por el repositorio **['maps'](https://github.com/truckermudgeon/maps)**: punto de partida para parsear el mapa y convertir coordenadas del juego a WGS84.

### [@RenCloud](https://github.com/RenCloud)

Gracias por **[scs-sdk-plugin](https://github.com/RenCloud/scs-sdk-plugin)**, el puente entre el motor del juego y el navegador.

---

*Buen viaje y a circular con cuidado.*
