
Anotaciones de importancia:
	* La configuracion CSS se encuentran en el archivo "wwwroot/css/site.css", este puede ser aplicado de manera global a todas las páginas del proyecto.
	* La configuracion CSS para la página del login se encuentra en "wwwroot/css/loginStyle.css"


Servicios e inyección de dependencia
    ********************************
    ***			NO TOCAR		 ***
    ********************************
    Los servicios localizados en la carpeta "/Services/AppServices/" son servicios del sistema, es decir, son servicios base de la aplicación, 
    de preferencia NO deben modificarse, en caso de querer modificarlos, se deben modificar con cuidado.
	    Servicios de la aplicacion:
        * IHttpClientFactory_
        * HttpClientFactory
        * SessionStorageService
        * TokenizerService
        * UserService
        * AuthenticationService
        * AuthenticationStateProvider_


    El servicio de conexión a la API:
        builder.Services.AddHttpClient("API", client => {
            client.BaseAddress = new Uri("https://localhost:7103/");
        });
    Funciona para que pueda ser modificado facilmente en tiempo de desarrollo, solo debemos agregar una nueva conexión a la lista de clientes establecidos, 
    y luego podremos llamarlas desde cualquier archivo del proyecto

*********************************
*********************************
*****       Importante:     *****
*********************************
*********************************
    La forma de manejar la data en Blazor funciona de la siguiente manera:
    Ejemplo 1
    1. Crear el MODEL del objeto que vamos a consultar, por ejemplo el USUARIO, que servirá para almacenar los datos del usuario autenticado
    2. Crear un "Servicio" para manejar la data del modelo definido anteriormente, por ejemplo, si deseamos manejar la información de nuestro usuario autenticado, debemos
        crear un MODELO para el usuario y un SERVICIO para manipular dicho objeto, es por eso que en el proyecto tenemos los siguientes archivos:
            "/Models/Sesion/User"  <-- Este es el modelo, lo que anteriormente conociamos como Structs
            "/AppServices/UserService" <-- Este es el servicio, que se encarga de crear localmente la variable User
    Ejemplo 2:
    1. El servicio para llenar los datos de los combos y toda la información correspondiente a la página en sí, son manejados por el servicio:
            "/Services/Page/PageElementsService"
        Y utiliza el modelo:
            "/Models/Page/PageElementsModel"
    2. El servicio llama a la API para obtener TODOS los valores en la primera carga y los almacena en la propiedad:
                public PageElementsModel PageElements { get; private set; }
    3. Para manejar los valores de estos elementos hemos definido un MODELO llamado "PageElementsModel"
       Ejemplo de USO:
            Si en un futuro desea agregar un nuevo elemento multiopcion a la página, debe hacer lo siguiente:
                1. Crear un modelo para dicho elemento. por ejemplo, crearemos un modelo para mostrar los rangos de sueldo:
                        "/Models/Page/PageElements/RangoSueldo.cs"
                2. Actualizar el Modelo general, es decir, el archivo "/Models/Page/PageElementsModel"
                        public List<RangoSueldo> lst_RangoSueldo { get; set; }
                3. Hacer lo mismo en la API, es decir, en la API también debe crear el modelo y debe actualizar el valor de respuesta.
                ** Nota Importante **
                    El orden de la declaración y el nombre de la variable en el modelo general debe ser identico al establecido en la api, por ejemplo:
                    Si en la API ud tiene:
                        public class PageElementsModel {
                            public List<EstadoAfiliado> lst_EstadoAfil { get; set; }
                            public List<Genero> lst_Genero { get; set; }
                            public List<Nacionalidad> lst_Nacionalidad { get; set; }
                            public List<EstadoCivil> lst_EstadoCivil { get; set; }
                            public List<Jornada> lst_Jornada { get; set; }
                            public List<Operadora> lst_Operadora { get; set; }
                            ... más propiedades ...
                    En el modelo general del FRONT ud debe tener:
                        public class PageElementsModel {
                            public List<EstadoAfiliado> lst_EstadoAfil { get; set; }
                            public List<Genero> lst_Genero { get; set; }
                            public List<Nacionalidad> lst_Nacionalidad { get; set; }
                            public List<EstadoCivil> lst_EstadoCivil { get; set; }
                            public List<Jornada> lst_Jornada { get; set; }
                            public List<Operadora> lst_Operadora { get; set; }
                            ... más propiedades ...
                    
                    Si ud cambia el nombre, éste NO SERÁ deserializado por el servicio "PageElementsService" ya que esto se hace directamente basado en el MODEL
                            PageElements = JsonConvert.DeserializeObject<PageElementsModel>(Resp);


*********************************
*********************************
*****       Importante:     *****
*********************************
*********************************
    Para obtener la información del registro consultado (Prospecto o Afiliado) desde la Base de datos, trabajaremos de la siguiente manera:
    1. Se ha creado modelo "/Models/BD/BDModel.cs" 
        Este es el encargado de recibir TODA la información del Afiliado o del Prospecto desde la BD, es decir, es el JSON sin formato, es el texto en bruto tal cual 
        como llega desde la BD. 
        El orden de la declaración y el nombre de la variable en el modelo BDMODEL debe ser identico al establecido en la api, por ejemplo:
            Si en la API ud tiene:
                public class BDModel {
                    public string co_empr { get; set; };
                    public string ci_cedu { get; set; };
                    ... MÁS CÓDIGO ...
            En el modelo BDMODEL del FRONT ud debe tener:
            public class BDModel {
                    public string co_empr { get; set; }; <-- Mismo nombre, mismo tipo
                    public string ci_cedu { get; set; };
                    ... MÁS CÓDIGO ...

    