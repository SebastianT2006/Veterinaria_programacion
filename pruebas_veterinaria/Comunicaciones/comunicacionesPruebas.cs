using lib_aplicaciones.entidades;
using lib_aplicaciones.nucleo;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using presentaciones_veterinaria.implementaciones;
using System.Net;
using System.Text;

namespace pruebas_unitarias
{
    // Respuesta falsa: en vez de llamar a la API devuelve el codigo y el texto que le indiquemos
    // y guarda la peticion que recibio para poder revisarla.
    public class ManejadorFalso : HttpMessageHandler
    {
        private readonly HttpStatusCode codigo;
        private readonly string contenido;
        public HttpRequestMessage? Peticion { get; private set; }
        public string? Cuerpo { get; private set; }

        public ManejadorFalso(HttpStatusCode codigo, string contenido)
        {
            this.codigo = codigo;
            this.contenido = contenido;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Peticion = request;
            Cuerpo = request.Content == null ? null : await request.Content.ReadAsStringAsync(cancellationToken);
            return new HttpResponseMessage(codigo) { Content = new StringContent(contenido, Encoding.UTF8, "application/json") };
        }
    }

    // Sesion falsa: un diccionario que hace el papel de Session
    public class SesionFalsa : ISession
    {
        private readonly Dictionary<string, byte[]> datos = new();
        public bool IsAvailable => true;
        public string Id => "prueba";
        public IEnumerable<string> Keys => datos.Keys;
        public void Clear() => datos.Clear();
        public Task CommitAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
        public Task LoadAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
        public void Remove(string key) => datos.Remove(key);
        public void Set(string key, byte[] value) => datos[key] = value;
        public bool TryGetValue(string key, out byte[] value) => datos.TryGetValue(key, out value!);
    }

    [TestClass]
    public class comunicacionesPruebas
    {
        private static Comunicaciones Crear(ManejadorFalso manejador, string? idUsuario = null)
        {
            if (idUsuario == null)
                return new Comunicaciones(null, manejador);

            var contexto = new DefaultHttpContext { Session = new SesionFalsa() };
            contexto.Session.SetString("UsuarioId", idUsuario);
            return new Comunicaciones(new HttpContextAccessor { HttpContext = contexto }, manejador);
        }

        // ---------- Respuesta 200: todo salio bien ----------

        [TestMethod]
        public async Task Devuelve_la_entidad_cuando_responde_200()
        {
            var json = JsonConvert.SerializeObject(new personas { Id = 7, Nombre = "Ana" });
            var comunicaciones = Crear(new ManejadorFalso(HttpStatusCode.OK, json));

            var resultado = await comunicaciones.Ejecutar<personas>("personas/Insertar", new personas { Nombre = "Ana" });

            Assert.AreEqual(7, resultado.Id);
            Assert.AreEqual("Ana", resultado.Nombre);
        }

        [TestMethod]
        public async Task Devuelve_una_lista_cuando_responde_200()
        {
            var json = JsonConvert.SerializeObject(new List<personas> { new() { Id = 1 }, new() { Id = 2 } });
            var comunicaciones = Crear(new ManejadorFalso(HttpStatusCode.OK, json));

            var resultado = await comunicaciones.Ejecutar<List<personas>>("personas/Consultar", null);

            Assert.AreEqual(2, resultado.Count);
        }

        [TestMethod]
        public async Task Devuelve_lista_vacia_cuando_no_hay_datos()
        {
            var comunicaciones = Crear(new ManejadorFalso(HttpStatusCode.OK, "[]"));

            var resultado = await comunicaciones.Ejecutar<List<animales>>("animales/Consultar", null);

            Assert.AreEqual(0, resultado.Count);
        }

        // ---------- Respuesta 422: error de validacion ----------

        [TestMethod]
        public async Task Lanza_ValidacionException_cuando_responde_422()
        {
            var comunicaciones = Crear(new ManejadorFalso((HttpStatusCode)422, "Falta el nombre"));

            await Assert.ThrowsExactlyAsync<ValidacionException>(
                () => comunicaciones.Ejecutar<personas>("personas/Insertar", new personas()));
        }

        [TestMethod]
        public async Task El_mensaje_de_ValidacionException_es_el_de_la_API()
        {
            var comunicaciones = Crear(new ManejadorFalso((HttpStatusCode)422, "Falta el nombre"));

            var error = await Assert.ThrowsExactlyAsync<ValidacionException>(
                () => comunicaciones.Ejecutar<personas>("personas/Insertar", new personas()));

            Assert.AreEqual("Falta el nombre", error.Message);
        }

        // ---------- Otros errores ----------

        [TestMethod]
        public async Task Lanza_Exception_cuando_responde_500()
        {
            var comunicaciones = Crear(new ManejadorFalso(HttpStatusCode.InternalServerError, "fallo"));

            var error = await Assert.ThrowsExactlyAsync<Exception>(
                () => comunicaciones.Ejecutar<personas>("personas/Consultar", null));

            StringAssert.StartsWith(error.Message, "Error Comunicaci");
        }

        [TestMethod]
        public async Task Lanza_Exception_cuando_responde_404()
        {
            var comunicaciones = Crear(new ManejadorFalso(HttpStatusCode.NotFound, ""));

            await Assert.ThrowsExactlyAsync<Exception>(
                () => comunicaciones.Ejecutar<personas>("personas/NoExiste", null));
        }

        // ---------- Lo que se envia a la API ----------

        [TestMethod]
        public async Task Envia_la_peticion_con_POST()
        {
            var manejador = new ManejadorFalso(HttpStatusCode.OK, "{}");
            await Crear(manejador).Ejecutar<personas>("personas/Insertar", new personas());

            Assert.AreEqual(HttpMethod.Post, manejador.Peticion!.Method);
        }

        [TestMethod]
        public async Task Envia_el_cuerpo_en_formato_JSON()
        {
            var manejador = new ManejadorFalso(HttpStatusCode.OK, "{}");
            await Crear(manejador).Ejecutar<personas>("personas/Insertar", new personas { Nombre = "Ana" });

            StringAssert.Contains(manejador.Cuerpo!, "\"Nombre\":\"Ana\"");
            Assert.AreEqual("application/json", manejador.Peticion!.Content!.Headers.ContentType!.MediaType);
        }

        // ---------- La sesion: cabecera X-Usuario ----------

        [TestMethod]
        public async Task Envia_la_cabecera_X_Usuario_cuando_hay_sesion()
        {
            var manejador = new ManejadorFalso(HttpStatusCode.OK, "{}");
            await Crear(manejador, "5").Ejecutar<personas>("personas/Insertar", new personas());

            Assert.IsTrue(manejador.Peticion!.Headers.Contains("X-Usuario"));
            Assert.AreEqual("5", manejador.Peticion.Headers.GetValues("X-Usuario").First());
        }

        [TestMethod]
        public async Task No_envia_la_cabecera_X_Usuario_sin_sesion()
        {
            var manejador = new ManejadorFalso(HttpStatusCode.OK, "{}");
            await Crear(manejador).Ejecutar<personas>("personas/Insertar", new personas());

            Assert.IsFalse(manejador.Peticion!.Headers.Contains("X-Usuario"));
        }

        // ---------- Una prueba por cada entidad: la ruta a la que llama ----------

        [TestMethod]
        [DataRow("animales")]
        [DataRow("citas")]
        [DataRow("clientes")]
        [DataRow("compras")]
        [DataRow("consultas")]
        [DataRow("detalleCompras")]
        [DataRow("detalleFacturas")]
        [DataRow("empleados")]
        [DataRow("especies")]
        [DataRow("facturas")]
        [DataRow("fincas")]
        [DataRow("historiasClinicas")]
        [DataRow("personas")]
        [DataRow("productos")]
        [DataRow("proveedores")]
        [DataRow("razas")]
        [DataRow("servicios")]
        [DataRow("tratamientos")]
        [DataRow("vacunaciones")]
        [DataRow("veterinarios")]
        public async Task Llama_a_la_ruta_de_cada_entidad(string entidad)
        {
            var manejador = new ManejadorFalso(HttpStatusCode.OK, "[]");
            await Crear(manejador).Ejecutar<List<object>>(entidad + "/Consultar", null);

            Assert.AreEqual("/" + entidad + "/Consultar", manejador.Peticion!.RequestUri!.AbsolutePath);
        }
    }
}
