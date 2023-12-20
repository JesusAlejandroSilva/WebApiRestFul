using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using WebApi.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class ServicesAPI: IServicesAPI
    {
        private static string _user;
        private static string _password;
        private static string baseUrl;
        private static string tokens;

        public ServicesAPI()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            _user = builder.GetSection("ApiSettings:user").Value;
            _password = builder.GetSection("ApiSettings:password").Value;
            baseUrl = builder.GetSection("ApiSettings:baseUrl").Value;
        }

        public async Task Autentication() 
        {
            var cliente = new HttpClient();

            cliente.BaseAddress = new Uri(baseUrl);

            var credenciales = new Credenciales() { user = _user, password = _password };

            var content = new StringContent(JsonConvert.SerializeObject(credenciales), Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync("api/Authentication/Authentication", content);
            var json_respuesta = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ResultCredencial>(json_respuesta);

            tokens = result.tokens;
        
        }

        public async Task<Aspirante> Buscar(int idAspirante)
        {
            Aspirante aspirante = new Aspirante();

           // await Autentication();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(baseUrl);
            // cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens);
            var response = await cliente.GetAsync($"api/Aspirante/Buscar/{idAspirante}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoAPI>(json);
                aspirante = resultado.Aspirante;
            }

            return aspirante;
        }

        public async Task<bool> Editar(Aspirante aspirante)
        {
            bool responses = false;

            //  await Autentication();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(baseUrl);
            // cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens);
            var content = new StringContent(JsonConvert.SerializeObject(aspirante), Encoding.UTF8, "application/json");
            var response = await cliente.PutAsync("api/Aspirante/Editar", content);

            if (response.IsSuccessStatusCode)
            {
                responses = true;
            }

            return responses;
        }

        public async Task<bool> Eliminar(int idAspirante)
        {
            bool responses = false;

          //  await Autentication();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(baseUrl);
            // cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens);
            var response = await cliente.DeleteAsync($"api/Aspirante/Eliminar/{idAspirante}");

            if (response.IsSuccessStatusCode)
            {
                responses = true;
            }

            return responses;
        }

        public async Task<bool> Guardar(Aspirante aspirante)
        {
            bool responses = false;

            //await Autentication();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(baseUrl);
            // cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens);
            var content = new StringContent(JsonConvert.SerializeObject(aspirante), Encoding.UTF8, "application/json");
            var response = await cliente.PostAsync("api/Aspirante/Guardar", content);

            if (response.IsSuccessStatusCode)
            {
                responses = true;
            }

            return responses;
        }

        public async Task<List<Aspirante>> Listar()
        {
            List<Aspirante> lista = new List<Aspirante>();

            //await Autentication();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(baseUrl);
           // cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens);
            var response = await cliente.GetAsync("api/Aspirante/Listar");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoAPI>(json);
                lista = resultado.response;
            }

            return lista;
        }
    }
}
