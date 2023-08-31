using BackPuppy.Dtos;
using BackPuppy.Entity;
using BackPuppy.Validaciones;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace BackPuppy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly HttpClient httpClient;
        private readonly AplicationDbContext context;

        public CuentasController(UserManager<IdentityUser> userManager, IConfiguration configuration, SignInManager<IdentityUser> signInManager, HttpClient httpClient, AplicationDbContext context)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.httpClient = httpClient;
            this.context = context;
        }
        [HttpPost("CrearCuentaUsuario")]
        public async Task<ActionResult<ResponseDto<RespuestaAutenticacion>>> CrearCuentaUsuario([FromForm] PersonaDto persona, string password, string rol)
        {
            try
            {
                var credenciales = new CredencialesDto()
                {
                    email = persona.correo,
                    password = password
                };
                var jsonCredenciales = JsonSerializer.Serialize(credenciales);
                var content = new StringContent(jsonCredenciales, Encoding.UTF8, "application/json");
                HttpResponseMessage respuesta = await httpClient.PostAsync("https://localhost:7101/api/Cuentas/CrearCuentaIdentity", content);

                if (respuesta.IsSuccessStatusCode)
                {
                    string responseBody = await respuesta.Content.ReadAsStringAsync();
                    var responseDto = JsonSerializer.Deserialize<ResponseDto<String>>(responseBody);

                    if (responseDto.statusCode == 200)
                    {
                        persona.idCuentaIdentity = responseDto.datos;
                        //AsignarRol
                        var usuarioId = persona.idCuentaIdentity;
                        var rolEnviar = rol;
                        // Crear el objeto que contiene los datos a enviar en el cuerpo
                        var data = new { usuarioId, rol };
                        // Serializar los datos como JSON
                        var json = System.Text.Json.JsonSerializer.Serialize(data);

                        // Crear el contenido de la solicitud HTTP
                        var contenido = new StringContent(json, Encoding.UTF8, "application/json");

                        // Construir la URL con el parámetro usuarioId
                        var url = $"https://localhost:7101/api/Cuentas/asignarRol?usuarioId={usuarioId}&rol={rolEnviar}";
                        // Enviar la solicitud HTTP POST con la URL y el contenido en el cuerpo
                        HttpResponseMessage respuestaCrearRol = await httpClient.PostAsync(url, contenido);

                        var responseContent = await respuestaCrearRol.Content.ReadAsStringAsync();

                        Console.WriteLine(responseContent);

                      

                        var contentPersona = new MultipartFormDataContent();
                        contentPersona.Add(new StringContent(persona.idCuentaIdentity), "idCuentaIdentity");
                        contentPersona.Add(new StringContent(persona.carnet), "carnet");
                        contentPersona.Add(new StringContent(persona.nombres), "nombres");
                        contentPersona.Add(new StringContent(persona.apellidoPaterno), "apellidoPaterno");
                        contentPersona.Add(new StringContent(persona.apellidoMaterno), "apellidoMaterno");
                        contentPersona.Add(new StringContent(persona.celular.ToString()), "celular");
                        contentPersona.Add(new StringContent(persona.correo), "correo");
                        contentPersona.Add(new StringContent(persona.direccion), "direccion");
                        if(persona.fotografia!= null)
                        {
                            var fotografiaStream = persona.fotografia.OpenReadStream();
                            var fotografiaContent = new StreamContent(fotografiaStream);
                            fotografiaContent.Headers.ContentType = new MediaTypeHeaderValue(persona.fotografia.ContentType);
                            fotografiaContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                            {
                                Name = "fotografia",
                                FileName = persona.fotografia.FileName
                            };
                            contentPersona.Add(fotografiaContent);
                        }
                      

                        HttpResponseMessage response = await httpClient.PostAsync("https://localhost:7101/api/Personas/CrearPersona", contentPersona);
                        string responsePersona = await response.Content.ReadAsStringAsync();

                        var responseDtoPersona = JsonSerializer.Deserialize<ResponseDto<persona>>(responsePersona);


                        return Ok(responseDtoPersona);
                    }
                    else
                    {
                        return BadRequest(responseDto);
                    }
                }
                else
                {
                    string responseBody = await respuesta.Content.ReadAsStringAsync();
                    var responseDto = JsonSerializer.Deserialize<ResponseDto<String>>(responseBody);

                    return BadRequest(responseDto);
                }
            }
            catch (Exception e)
            {
                return DetalleProblemaHelper.InternalServerError(HttpContext.Request, detail: e.Message);
            }
        }

        [HttpPost("CrearCuentaIdentity")]
        public async Task<ActionResult<ResponseDto<string>>> CrearCuentaIdentity([FromBody] CredencialesDto credenciales)
        {
            try
            {
                var usuarioEntity = new IdentityUser
                {
                    UserName = credenciales.email,
                    Email = credenciales.email
                };
                var resultado = await userManager.CreateAsync(usuarioEntity, credenciales.password);

                if (resultado.Succeeded)
                {
                    var usuarioCreado = await userManager.FindByNameAsync(usuarioEntity.UserName);
                    var idAsignado = usuarioCreado.Id;

                    var response = new ResponseDto<string>()
                    {
                        statusCode = StatusCodes.Status200OK,
                        fechaConsulta = DateTime.Now,
                        codigoRespuesta = 1001,
                        MensajeRespuesta = "CORRECTO",
                        datos = idAsignado
                    };
                    return Ok(response);
                }
                else
                {
                    var errores = resultado.Errors.Select(e => e.Description).ToList();

                    var response = new ResponseDto<string>()
                    {
                        statusCode = StatusCodes.Status400BadRequest,
                        fechaConsulta = DateTime.Now,
                        codigoRespuesta = 1002,
                        MensajeRespuesta = "ERROR",
                        datos = string.Join(", ", errores)
                    };
                    return BadRequest(response);
                }
            }
            catch (Exception e)
            {
                return DetalleProblemaHelper.InternalServerError(HttpContext.Request, detail: e.Message);
            }
        }


        [HttpPut("ActualizarCuentaIdentity/{idCuentaIdentity}")]
        public async Task<ActionResult<ResponseDto<string>>> ActualizarCuentaIdentity(string idCuentaIdentity, [FromBody] CredencialesDto actualizacionCredenciales)
        {
            try
            {
                var usuario = await userManager.FindByIdAsync(idCuentaIdentity);

                if (usuario == null)
                {
                    return NotFound("Usuario no encontrado");
                }

                usuario.UserName = actualizacionCredenciales.email;
                usuario.Email = actualizacionCredenciales.email;

                var token = await userManager.GeneratePasswordResetTokenAsync(usuario);
                var resultadoReset = await userManager.ResetPasswordAsync(usuario, token, actualizacionCredenciales.password);

                if (resultadoReset.Succeeded)
                {
                    var resultadoUpdate = await userManager.UpdateAsync(usuario);

                    if (resultadoUpdate.Succeeded)
                    {
                        var response = new ResponseDto<string>
                        {
                            statusCode = StatusCodes.Status200OK,
                            fechaConsulta = DateTime.Now,
                            codigoRespuesta = 1001,
                            MensajeRespuesta = "CORRECTO",
                            datos = "Cuenta actualizada exitosamente"
                        };
                        return Ok(response);
                    }
                }

                var errores = resultadoReset.Errors.Select(e => e.Description).ToList();

                var responseError = new ResponseDto<string>
                {
                    statusCode = StatusCodes.Status400BadRequest,
                    fechaConsulta = DateTime.Now,
                    codigoRespuesta = 1002,
                    MensajeRespuesta = "ERROR",
                    datos = string.Join(", ", errores)
                };
                return BadRequest(responseError);
            }
            catch (Exception e)
            {
                return DetalleProblemaHelper.InternalServerError(HttpContext.Request, detail: e.Message);
            }
        }





        [HttpPost("login")]
        public async Task<ActionResult<RespuestaAutenticacion>> loginUsuario([FromBody] CredencialesDto credenciales)
        {
            var resultado = await signInManager.PasswordSignInAsync(credenciales.email, credenciales.password, isPersistent: false, lockoutOnFailure: false);
            if (resultado.Succeeded)
            {
                return await ConstruirToken(credenciales);
            }
            else
            {
                return BadRequest("Inicio de sesion incorrecto, verifique sus credenciales");
            }

        }

        private async Task<ActionResult<RespuestaAutenticacion>> ConstruirToken(CredencialesDto credenciales)
        {
            try
            {
                var usuario = await userManager.FindByEmailAsync(credenciales.email);
                var roles = await userManager.GetClaimsAsync(usuario);
                List<persona> personasFiltradas;
                var rutaFoto = "";
                var userId = "";
                var nombreUsuarioLogeado = "";
                var claims = new List<Claim>
        {
            new Claim("email", credenciales.email)
        };

                foreach (var claim in roles)
                {
                    if (claim.Type == "role")
                    {
                        claims.Add(new Claim("role", claim.Value));
                    }
                }
                //Obtener Imagen

                userId = usuario.Id;
                    personasFiltradas = await context.Personas.Where(x => x.idCuentaIdentity == userId).ToListAsync();
                nombreUsuarioLogeado = personasFiltradas[0].nombres + " " + personasFiltradas[0].apellidoPaterno + " " +
                personasFiltradas[0].apellidoMaterno;
                    rutaFoto = personasFiltradas[0].fotografia;
                Claim idClaim = new Claim("Id", userId);
                Claim nombreUserLogeado = new Claim("nombreUsuarioLogeado", nombreUsuarioLogeado);

                if (rutaFoto != null)
                {
                    System.Net.WebClient webClient = new System.Net.WebClient();
                    byte[] imageBytes = webClient.DownloadData(rutaFoto);
                    string base64String = System.Convert.ToBase64String(imageBytes);
                    string imageSrc = "data:image/jpeg;base64," + base64String;
                    Claim rutaFotoUserLogeado = new Claim("rutaFoto", imageSrc);
                    claims.Add(rutaFotoUserLogeado);
                }
                claims.Add(idClaim);
                claims.Add(nombreUserLogeado);

                var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llaveJWT"]));
                var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);
                var expiracion = DateTime.Now.AddDays(1);

                var token = new JwtSecurityToken(
                    issuer: null,
                    audience: null,
                    claims: claims,
                    expires: expiracion,
                    signingCredentials: creds
                );

                var respuestaAutenticacion = new RespuestaAutenticacion
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiracion = expiracion
                };

                return Ok(respuestaAutenticacion);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }



        [HttpPost("asignarRol")]
        public async Task<ActionResult> asignarRol(string usuarioId, string rol)
        {
            var usuario = await userManager.FindByIdAsync(usuarioId);
            var claims = await userManager.GetClaimsAsync(usuario);
            if (claims.Count == 0)
            {
                await userManager.AddClaimAsync(usuario, new Claim("role", rol));
            }
            else
            {
                var nameClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
                List<Claim> claimsToRemove = new List<Claim>();
                foreach (var claim in claims)
                {
                    if (claim.Type == "role")
                    {
                        claimsToRemove.Add(claim);
                    }
                }
                await userManager.RemoveClaimsAsync(usuario, claimsToRemove);
                await userManager.UpdateAsync(usuario);
                await userManager.AddClaimAsync(usuario, new Claim("role", rol));
                claims = await userManager.GetClaimsAsync(usuario);
            }

            var response = new ResponseDto<long>()
            {
                statusCode = StatusCodes.Status200OK,
                fechaConsulta = DateTime.Now,
                codigoRespuesta = 1001,
                MensajeRespuesta = "CORRECTO",
                claims = claims
            };
            return Ok(response);
        }

       


    }
}
