# AppMusic

AppMusic es una aplicaci�n para reproducir m�sica, desarrollada con .NET Framework. Esta aplicaci�n permite escuchar m�sica de forma sencilla e incluye la opci�n de selecci�n autom�tica basada en el clima, utilizando una API gratuita. La aplicaci�n permite gestionar todas las canciones, artistas y listas de reproducci�n.

## Arquitectura

El proyecto est� estructurado en diferentes capas y bibliotecas de clases que interact�an entre s� para manejar las funciones de la aplicaci�n. A continuaci�n, se detallan los componentes principales.

![Diagrama de la arquitectura](arquitectura.png)

## Instalaci�n

### Requisitos basicos

- SDK .NET 8.0 o superior
- SQL Server u otro servidor compatible
- API key de Openweathermap

### Pasos

1. **Clona el repositorio**:
    ```bash
    git clone https://github.com/frackfernandez/AppMusic.git
    cd AppMusic
    ```

2. **Restaura las dependencias**:
    ```bash
    dotnet restore
    ```

3. **Configura la cadena de conexi�n de la base de datos:**

    Abre el archivo `ConnectionDB.cs` en la carpeta `Infrastructure` y actualiza la cadena de conexi�n.
    
4. **Configura las credenciales de la API:**

    Abre el archivo `ServiceWeather.cs` en la carpeta `Service` y actualiza las credenciales.

5. **Crea la base de datos:**

El diagrama de la base de datos ilustra la estructura de las tablas y sus relaciones.

![Diagrama de la base de datos](db.png)

6. **Compila el proyecto**:
    ```bash
    dotnet build
    ```

## Contribuciones

Se agradecen las contribuciones. Por favor, sigue el proceso habitual en GitHub para realizar un fork, crea una rama, haz los cambios y env�a un pull request.

## Licencia

Este proyecto est� bajo la [MIT License](https://opensource.org/licenses/MIT).