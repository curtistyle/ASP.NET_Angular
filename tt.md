# ASP.NET

### Understanding endpoint

En una aplicación ASP.NET Core, las solicitudes entrantes son manejadas por puntos finales (*endpoint*). El punto final que produjo la respuesta en la figura 2.14 es una *acción*, que es un método escrito en C#. Una acción está definida en un *controlador*, que es una clase en C# que deriva de la clase base `Microsoft.AspNetCore.Mvc.Controller`, la clase base del controlador incorporada.
Cada método público definido por un controlador es una acción, lo que significa que puedes invocar el método de acción para manejar una solicitud HTTP. La convención en proyectos ASP.NET Core es poner las clases de controlador en una carpeta llamada `Controllers`, que fue creada por la plantilla utilizada para configurar el proyecto.
La plantilla del proyecto agregó un controlador a la carpeta `Controllers` para ayudar a arrancar el desarrollo. El controlador está definido en el archivo de clase llamado `HomeController.cs`. Las clases de controlador contienen un nombre seguido de la palabra `Controller`, lo que significa que cuando ves un archivo llamado `HomeController.cs`, sabes que contiene un controlador llamado `Home`, que es el controlador predeterminado que se utiliza en las aplicaciones ASP.NET Core.

>CONSEJO: No te preocupes si los términos *controlador* (*controller*) y *acción* (*action*) no tienen sentido de inmediato. Simplemente sigue el ejemplo, y verás cómo la solicitud HTTP enviada por el navegador es manejada por código en C#.

Busca el archivo *HomeController.cs* en el Explorador de soluciones o el panel del explorador y haz click en el para abrirlo y editarlo. Veras el siguiente codigo:

```Csharp
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FirstProject.Models;

namespace FirstProject.Controllers;

public class HomeController : Controller {
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger) {
        _logger = logger;
    }
    public IActionResult Index() {
        return View();
    }
    public IActionResult Privacy() {
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, 
        NoStore = true)]
    public IActionResult Error() {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id 
            ?? HttpContext.TraceIdentifier });
    }
}
```

Usando el codigo del editor, remplaza el contenido del archivo *HomeController.cs* para que coincida con el lista.
Tenemos que eliminar todos los metodos menos uno, cambiar el tipo de resultado y su implementacion y remover la sentencia *using* no usada en el *namespace*.

```Csharp
using Microsoft.AspNetCore.Mvc;
namespace FirstProject.Controllers {
    public class HomeController : Controller {
        public string Index() {
            return "Hello World";
        }
    }
}
```

El resultado es que el controlador `Home` define una única acción, llamada `Index`. Estos cambios no producen un efecto dramático, pero hacen una buena demostración. He cambiado el método llamado `Index` para que devuelva la cadena `"Hola Mundo"`.
Utilizando el indicador de PowerShell, ejecuta nuevamente el comando `dotnet run` en la carpeta `FirstProject` y utiliza el navegador para solicitar http://localhost:5000. La configuración del proyecto creada por la plantilla en el listado 2.3 significa que la solicitud HTTP será procesada por la acción `Index` definida por el controlador Home. Dicho de otra manera, la solicitud será procesada por el método `Index` definido por la clase `HomeController`.
La cadena producida por el método `Index` se utiliza como respuesta a la solicitud HTTP del navegador, como se muestra en la figura 2.15.

## Comprendiendo las rutas
El sistema de *enrutamiento* de ASP.NET Core es responsable de seleccionar el punto final que manejará una solicitud HTTP. Una *ruta* es una regla que se utiliza para decidir cómo se maneja una solicitud. Cuando se creó el proyecto, se creó una regla predeterminada para comenzar. Puedes solicitar cualquiera de las siguientes URL, y serán enviadas a la acción `Index` definida por el controlador `Home`:

- /
- /Home
- /Home/Index
Entonces, cuando un navegador solicita http://tusitio/ o http://tusitio/Home, recibe la salida del método `Index` del controlador `HomeController`. Puedes probar esto tú mismo cambiando la URL en el navegador. En este momento, será http://localhost:5000/. Si agregas /Home o /Home/Index a la URL y presionas Enter, verás el mismo resultado `"Hola Mundo"` de la aplicación.

## Comprendiendo la renderización de HTML
La salida del ejemplo anterior no era HTML, solo era la cadena "`Hello World`". Para producir una respuesta HTML a una solicitud del navegador, necesito una vista, que le indique a ASP.NET Core cómo procesar el resultado producido por el método `Index` en una respuesta HTML que pueda ser enviada al navegador.

### **CREANDO Y RENDERIZANDO UNA VISTA**

La primera cosa que debemos hacer es modificar nuestro metodo de accion `Index`, como se muestra en el siguiente listado.
Los cambios son mostrado en negrita (bold), los cuales siguen una convencion de principio a fin en este libro para que los ejemplos sean más faciles de seguir.

```Csharp
using Microsoft.AspNetCore.Mvc;
namespace FirstProject.Controllers {
    public class HomeController : Controller {
        public ViewResult Index() {
            return View("MyView");
        }
    }
}    	
```

Cuando devuelvo un objeto ViewResult desde un método de acción, estoy instruyendo a ASP.NET Core que renderice una vista. Creo el ViewResult llamando al método View, especificando el nombre de la vista que quiero usar, que es MyView.

Utiliza Control+C para detener ASP.NET Core y luego utiliza el comando dotnet run para compilar y volver a iniciarlo. Utiliza el navegador para solicitar http://localhost:5000 y verás que ASP.NET Core intenta encontrar la vista, como se muestra por el mensaje de error que se muestra en la figura 2.16.

**Figure 2.16    Trying to find a view**

Este es un mensaje de error útil. Explica que ASP.NET Core no pudo encontrar la vista que especificaste para el método de acción y explica dónde buscó. Las vistas se almacenan en la carpeta Views, organizadas en subcarpetas. Las vistas asociadas con el controlador Home, por ejemplo, se almacenan en una carpeta llamada Views/Home. Las vistas que no son específicas de un solo controlador se almacenan en una carpeta llamada Views/Shared. La plantilla utilizada para crear el proyecto agregó automáticamente las carpetas Home y Shared, y agregó algunas vistas de marcador de posición para iniciar el proyecto.

Si estás utilizando Visual Studio, haz clic derecho en la carpeta Views/Home en el Explorador de Soluciones y selecciona Agregar > Nuevo elemento en el menú emergente. Visual Studio te presentará una lista de plantillas para agregar elementos al proyecto. Localiza el elemento Vista Razor - Vacía, que se encuentra en la sección ASP.NET Core > Web > ASP.NET, como se muestra en la figura 2.17.

Para Visual Studio, es posible que necesites hacer clic en el botón Mostrar todas las plantillas antes de que se muestre la lista de plantillas. Establece el nombre del nuevo archivo como MyView.cshtml y haz clic en el botón Agregar. Visual Studio agregará un archivo llamado MyView.cshtml a la carpeta Views/Home y lo abrirá para editarlo. Reemplaza el contenido del archivo con el que se muestra en el listado 2.8.

**Figure 2.17    Selecting a Visual Studio item template**

Visual Studio Code no proporciona plantillas de elementos. En su lugar, haz clic derecho en la carpeta Views/Home en el panel del explorador de archivos y selecciona Nuevo archivo en el menú emergente. Establece el nombre del archivo como MyView.cshtml y presiona Enter. El archivo se creará y se abrirá para su edición. Agrega el contenido mostrado en el listado 2.8.

> CONSEJO: Es fácil terminar creando el archivo de vista en la carpeta incorrecta. Si no terminaste con un archivo llamado MyView.cshtml en la carpeta Views/Home, entonces arrastra el archivo a la carpeta correcta o elimina el archivo y vuelve a intentarlo.

```Csharp
@{
    Layout = null;
}
<!DOCTYPE html>
<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Index</title>
</head>
<body>
    <div>
        Hello World (from the view)
    </div>
</body>
</html>
The new contents of the view file are mostly HTML. The exception is the part that 
looks like this:
...
@{
    Layout = null;
}
...
```

Esta es una expresión que será interpretada por Razor, que es el componente que procesa el contenido de las vistas y genera HTML que se envía al navegador. Razor es un motor de vistas, y las expresiones en las vistas se conocen como expresiones Razor.
La expresión Razor en el listado 2.8 le indica a Razor que opté por no usar un diseño, que es como una plantilla para el HTML que se enviará al navegador (y que describo en el capítulo 22). Para ver el efecto de crear la vista, utiliza Control+C para detener ASP.NET Core si está en ejecución y utiliza el comando dotnet run para compilar y volver a iniciar la aplicación. Usa un navegador para solicitar http://localhost:5000 y verás el resultado mostrado en la figura 2.18.

**Figure 2.18    Rendering a view**

Cuando edité por primera vez el método de acción Index, devolvía un valor de tipo string. Esto significaba que ASP.NET Core no hacía más que pasar el valor de cadena tal cual al navegador. Ahora que el método Index devuelve un ViewResult, Razor se utiliza para procesar una vista y generar una respuesta HTML. Razor pudo localizar la vista porque seguí la convención de nomenclatura estándar, que consiste en colocar archivos de vista en una carpeta cuyo nombre coincida con el del controlador que contiene el método de acción. En este caso, esto significaba poner el archivo de vista en la carpeta Views/Home, ya que el método de acción está definido por el controlador Home.

Puedo devolver otros resultados desde los métodos de acción además de cadenas y objetos ViewResult. Por ejemplo, si devuelvo un RedirectResult, el navegador será redirigido a otra URL. Si devuelvo un HttpUnauthorizedResult, puedo solicitar al usuario que inicie sesión. Estos objetos se conocen colectivamente como resultados de acción. El sistema de resultados de acción te permite encapsular y reutilizar respuestas comunes en acciones. Te contaré más sobre ellos y explicaré las diferentes formas en que se pueden utilizar en el capítulo 19.

**AGREGAR SALIDA DINAMICA**
El objetivo principal de una aplicación web es construir y mostrar salida dinámica. El trabajo del método de acción es construir datos y pasarlos a la vista para que puedan ser utilizados para crear contenido HTML basado en los valores de los datos. Los métodos de acción proporcionan datos a las vistas pasando argumentos al método View, como se muestra en el listado 2.9. Los datos proporcionados a la vista se conocen como el modelo de vista.

##### Listing 2.9 Using a view model in the HomeController.cs file in the Controllers folder
```Csharp
using Microsoft.AspNetCore.Mvc;
namespace FirstProject.Controllers {
    public class HomeController : Controller {
        public ViewResult Index() {
            int hour = DateTime.Now.Hour;
            string viewModel = 
                hour < 12 ? "Good Morning" : "Good Afternoon";
            return View("MyView", viewModel);
        }
    }
}
```

El modelo de vista en este ejemplo es una cadena, y se proporciona a la vista como el segundo argumento del método View. El listado 2.10 actualiza la vista para que reciba y utilice el modelo de vista en el HTML que genera.

**Listing 2.10    Using a view model in the MyView.cshtml file in the Views/Home folder**
```CSharp
@model string
@{
    Layout = null;
}
<!DOCTYPE html>
<html>
    <head>
        <meta name="viewport" content="width=device-width" />
        <title>Index</title>
    </head>
    <body>
        <div>
            @Model World (from the view)
        </div>
    </body>
</html>
```

El tipo del modelo de vista se especifica usando la expresión @model, con una m minúscula.
El valor del modelo de vista se incluye en la salida HTML usando la expresión @Model, con una M mayúscula. (Al principio puede ser difícil recordar cuál es minúscula y cuál es mayúscula, pero pronto se convierte en algo natural).
Cuando se renderiza la vista, los datos del modelo de vista proporcionados por el método de acción se insertan en la respuesta HTML. Utiliza Control+C para detener ASP.NET Core y utiliza el comando dotnet run para compilar y volver a iniciarlo. Utiliza un navegador para solicitar http://localhost:5000 y verás la salida mostrada en la figura 2.19 (aunque es posible que veas el saludo matutino si estás siguiendo este ejemplo antes del mediodía).

**Figure 2.19    Generating dynamic content**

### 2.3.4 Poniendo las piezas juntas

Es un resultado simple, pero este ejemplo revela todos los componentes necesarios para crear una aplicación web ASP.NET Core simple y generar una respuesta dinámica.
La plataforma ASP.NET Core recibe una solicitud HTTP y utiliza el sistema de enrutamiento para hacer coincidir la URL de la solicitud con un punto final. El punto final, en este caso, es el método de acción Index definido por el controlador Home. El método es invocado y produce un objeto ViewResult que contiene el nombre de una vista y un objeto de modelo de vista. El motor de vista Razor localiza y procesa la vista, evaluando la expresión @Model para insertar los datos proporcionados por el método de acción en la respuesta, que se devuelve al navegador y se muestra al usuario. Por supuesto, hay muchas otras características disponibles, pero esta es la esencia de ASP.NET Core, y vale la pena tener en cuenta esta secuencia simple mientras lees el resto del libro.

Resumen:
- El desarrollo de ASP.NET Core se puede realizar con Visual Studio o Visual Studio Code, o puedes elegir tu propio editor de código.
- La mayoría de los editores de código proporcionan compilaciones de código integradas, pero la forma más confiable de obtener resultados consistentes en todas las herramientas y plataformas es mediante el uso del comando dotnet.
- ASP.NET Core depende de los puntos finales para procesar las solicitudes HTTP.
- Los puntos finales pueden estar escritos completamente en C# o usar HTML que ha sido anotado con expresiones de código.

# Your First ASP.NET Core application 

Ahora que estás listo para el desarrollo de ASP.NET Core, es hora de crear una aplicación simple. En este capítulo, crearás una aplicación de entrada de datos utilizando ASP.NET Core. Mi objetivo es demostrar ASP.NET Core en acción, así que aceleraré un poco el ritmo y omitiré algunas explicaciones sobre cómo funcionan las cosas detrás de escena. Pero no te preocupes; volveré a visitar estos temas en profundidad en capítulos posteriores.

### 3.1 Preparando el escenario

Imagina que un amigo ha decidido organizar una fiesta de Nochevieja y me ha pedido que cree una aplicación web que permita a sus invitados confirmar su asistencia electrónicamente. Me ha pedido estas cuatro características clave:
- Una página de inicio que muestre información sobre la fiesta
- Un formulario que se pueda utilizar para confirmar la asistencia
- Validación para el formulario de confirmación de asistencia, que mostrará una página de agradecimiento
- Una página de resumen que muestre quién vendrá a la fiesta

En este capítulo, crearé un proyecto ASP.NET Core y lo utilizaré para crear una aplicación simple que contenga estas características; una vez que todo funcione, aplicaré algún estilo para mejorar la apariencia de la aplicación finalizada.

### 3.2 Creando el proyecto

Abre un símbolo del sistema de PowerShell desde el menú de inicio de Windows, navega hasta una ubicación conveniente y ejecuta los comandos en el listado 3.1 para crear un proyecto llamado PartyInvites.

TIP: Puedes descargar el proyecto de ejemplo para este capítulo, y para todos los demás capítulos de este libro, desde https://github.com/manningbooks/pro-asp.net-core-7. Consulta el capítulo 1 para obtener ayuda si tienes problemas al ejecutar los ejemplos.

**Listing 3.1    Creating a new project**
```
dotnet new globaljson --sdk-version 7.0.100 --output PartyInvites
dotnet new mvc --no-https --output PartyInvites --framework net7.0
dotnet new sln -o PartyInvites
dotnet sln PartyInvites add PartyInvites
```

Estos son los mismos comandos que utilicé para crear el proyecto en el capítulo 2. Estos comandos aseguran que obtengas el punto de inicio correcto del proyecto que utiliza la versión requerida de .NET.

### 3.2.1 Preparando el proyecto

Abre el proyecto (abriendo el archivo PartyInvites.sln con Visual Studio o la carpeta PartyInvites en Visual Studio Code) y cambia el contenido del archivo launchSettings.json en la carpeta Properties, como se muestra en el listado 3.2, para establecer el puerto que se utilizará para escuchar las solicitudes HTTP.

**Listing 3.2    Setting ports in the launchSettings.json file in the Properties folder**
```
{
  "iisSettings": {
    "windowsAuthentication": false,
    "anonymousAuthentication": true,
    "iisExpress": {
      "applicationUrl": "http://localhost:5000",
      "sslPort": 0
    }
  },
  "profiles": {
    "PartyInvites": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "http://localhost:5000",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "IIS Express": {
      "commandName": "IISExpress",
      "launchBrowser": true,
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```
Reemplaza el contenido del archivo HomeController.cs en la carpeta Controllers con el código mostrado en el listado 3.3.

**Listing 3.3    The new contents of the HomeController.cs file in the Controllers folder**
```CSharp
using Microsoft.AspNetCore.Mvc;
namespace PartyInvites.Controllers {
    public class HomeController : Controller {
        
        public IActionResult Index() {
            return View();
        }
    }
}
```

Esto proporciona un punto de partida limpio para la nueva aplicación, definiendo un único método de acción que selecciona la vista predeterminada para su renderización. Para proporcionar un mensaje de bienvenida a los invitados a la fiesta, abre el archivo Index.cshtml en la carpeta Views/Home y reemplaza el contenido con el que se muestra en el listado 3.4.

**Listing 3.4    Replacing the contents of the Index.cshtml file in the Views/Home folder**

```Csharp
@{
    Layout = null;
}
<!DOCTYPE html>
<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Party!</title>
</head>
<body>
    <div>
        <div>
            We're going to have an exciting party.<br />
            (To do: sell it better. Add pictures or something.)
       </div>
    </div>
</body>
</html>
```

Ejecuta el comando que se muestra en el listado 3.5 en la carpeta PartyInvites para compilar y ejecutar el proyecto.

**Listing 3.5    Compiling and running the project**

`dotnet watch`

Una vez que el proyecto haya comenzado, se abrirá una nueva ventana del navegador y verás los detalles de la fiesta (bueno, el marcador de posición para los detalles, pero captas la idea), como se muestra en la figura 3.1.

**Figure 3.1 Adding to the view HTML**

Deja el comando dotnet watch ejecutándose. A medida que realices cambios en el proyecto, verás que el código se compila automáticamente y que los cambios se muestran automáticamente en el navegador.
Si cometes un error siguiendo los ejemplos, es posible que notes que el comando dotnet watch indica que no puede actualizar automáticamente el navegador. Si eso sucede, selecciona la opción para reiniciar la aplicación.

### 3.2.2 Agregando un modelo de datos

El modelo de datos es la parte más importante de cualquier aplicación ASP.NET Core. El modelo es la representación de los objetos, procesos y reglas del mundo real que definen el tema, conocido como el dominio, de la aplicación. El modelo, a menudo llamado modelo de dominio, contiene los objetos C# (conocidos como objetos de dominio) que conforman el universo de la aplicación y los métodos que los manipulan. En la mayoría de los proyectos, el objetivo de la aplicación ASP.NET Core es proporcionar al usuario acceso al modelo de datos y las funciones que le permiten interactuar con él.
La convención para una aplicación ASP.NET Core es que las clases de modelo de datos se definen en una carpeta llamada Models, que fue agregada al proyecto por la plantilla utilizada en el listado 3.1.
No necesito un modelo complejo para el proyecto PartyInvites porque es una aplicación tan simple. Solo necesito una clase de dominio que llamaré GuestResponse. Este objeto representará una confirmación de asistencia de un invitado.
Si estás utilizando Visual Studio, haz clic derecho en la carpeta Models y selecciona Agregar > Clase en el menú emergente. Establece el nombre de la clase como GuestResponse.cs y haz clic en el botón Agregar. Si estás utilizando Visual Studio Code, haz clic derecho en la carpeta Models, selecciona Nuevo archivo e ingresa GuestResponse.cs como nombre de archivo. Utiliza el nuevo archivo para definir la clase mostrada en el listado 3.6.

**Listing 3.6    The contents of the GuestResponse.cs file in the Models folder**

```CSharp
namespace PartyInvites.Models {
    public class GuestResponse {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public bool? WillAttend { get; set; }
    }
}
```

Observa que todas las propiedades definidas por la clase GuestResponse son anulables. Explico por qué esto es importante en la sección "Adding Validation" más adelante en el capítulo.

Reiniciando la compilación automática
Puede que veas una advertencia producida por el comando dotnet watch que te indica que no se puede aplicar una recarga en caliente. El comando dotnet watch no puede manejar todos los tipos de cambios, y algunos cambios provocan que el proceso de reconstrucción automática falle. Verás este mensaje en la línea de comandos:

watch : ¿Quieres reiniciar tu aplicación 
    - Sí (y) / No (n) / Siempre (a) / Nunca (v)?

Presiona a para reconstruir siempre el proyecto. Microsoft realiza mejoras frecuentes en el comando dotnet watch, por lo que las acciones que provocan este problema pueden cambiar.

### 3.2.3 Creando una segunda acción y vista

Uno de los objetivos de mi aplicación es incluir un formulario de confirmación de asistencia, lo que significa que necesito definir un método de acción que pueda recibir solicitudes para ese formulario. Una única clase controladora puede definir múltiples métodos de acción, y la convención es agrupar acciones relacionadas en la misma controladora. El listado 3.7 agrega un nuevo método de acción al controlador Home. Los controladores pueden devolver diferentes tipos de resultados, que se explican en capítulos posteriores.

**Listing 3.7    Adding an action in the HomeController.cs file in the Controllers folder**

```Csharp
using Microsoft.AspNetCore.Mvc;
namespace PartyInvites.Controllers {
    public class HomeController : Controller {
        
        public IActionResult Index() {
            return View();
        }
        public ViewResult RsvpForm() {
            return View();
        }
    }
}
```

Ambos métodos de acción invocan el método View sin argumentos, lo cual puede parecer extraño, pero recuerda que el motor de vistas Razor usará el nombre del método de acción al buscar un archivo de vista, como se explicó en el capítulo 2. Esto significa que el resultado del método de acción Index le dice a Razor que busque una vista llamada Index.cshtml, mientras que el resultado del método de acción RsvpForm le dice a Razor que busque una vista llamada RsvpForm.cshtml.
Si estás utilizando Visual Studio, haz clic derecho en la carpeta Views/Home y selecciona Agregar > Nuevo elemento en el menú emergente. Selecciona el elemento Vista Razor - Vacío, establece el nombre como RsvpForm.cshtml y haz clic en el botón Agregar para crear el archivo. Reemplaza el contenido con el mostrado en el listado 3.8.
Si estás utilizando Visual Studio Code, haz clic derecho en la carpeta Views/Home y selecciona Nueva archivo en el menú emergente. Establece el nombre del archivo como RsvpForm.cshtml y agrega el contenido mostrado en el listado 3.8.

**Listing 3.8    The contents of the RsvpForm.cshtml file in the Views/Home folder**

```Csharp
@{
    Layout = null;
}
<!DOCTYPE html>
<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>RsvpForm</title>
</head>
<body>
    <div>
        This is the RsvpForm.cshtml View
    </div>
</body>
</html>
```

Este contenido es simplemente HTML estático por el momento. Usa el navegador para solicitar http://localhost:5000/home/rsvpform. El motor de vista Razor localiza el archivo RsvpForm.cshtml y lo utiliza para producir una respuesta, como se muestra en la figura 3.2.

**Figure 3.2 Rendering a second view**

#### 3.2.4 Vinculando métodos de acción.

Quiero poder crear un enlace desde la vista Index para que los invitados puedan ver la vista RsvpForm sin tener que conocer la URL que apunta a un método de acción específico, como se muestra en el listado 3.9.

**Listing 3.9    Adding a link in the Index.cshtml file in the Views/Home folder**

```Csharp
@{
    Layout = null;
}
<!DOCTYPE html>
<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Party!</title>
</head>
<body>
    <div>
        <div>
            We're going to have an exciting party.<br />
            (To do: sell it better. Add pictures or something.)
        </div>
        <a asp-action="RsvpForm">RSVP Now</a>
    </div>
</body>
</html>
```

La adición al listado es un elemento "a" que tiene un atributo "asp-action". Este atributo es un ejemplo de un atributo de ayudante de etiquetas, que es una instrucción para Razor que se ejecutará cuando se renderice la vista. El atributo "asp-action" es una instrucción para agregar un atributo "href" al elemento "a" que contiene una URL para un método de acción. Explico cómo funcionan los ayudantes de etiquetas en los capítulos 25-27, pero este ayudante de etiquetas indica a Razor que inserte una URL para un método de acción definido por el mismo controlador para el cual se está renderizando la vista actual.
Utiliza el navegador para solicitar http://localhost:5000 y verás el enlace que ha creado el ayudante, como se muestra en la figura 3.3.

**Figure 3.3    Linking between action methods**

Pasa el mouse sobre el enlace "RSVP Now" en el navegador. Verás que el enlace apunta a http://localhost:5000/Home/RsvpForm.
Aquí está en juego un principio importante, que es que debes utilizar las funciones proporcionadas por ASP.NET Core para generar URLs, en lugar de codificarlas directamente en tus vistas. Cuando el ayudante de etiquetas creó el atributo href para el elemento "a", inspeccionó la configuración de la aplicación para determinar cuál debería ser la URL. Esto permite que la configuración de la aplicación se pueda cambiar para admitir diferentes formatos de URL sin necesidad de actualizar ninguna vista.

### 3.2.5 Construyendo el formulario.

Ahora que he creado la vista y puedo acceder a ella desde la vista de Índice, voy a desarrollar el contenido del archivo RsvpForm.cshtml para convertirlo en un formulario HTML para editar objetos GuestResponse, como se muestra en el listado 3.10.