using Microsoft.AspNetCore.Mvc;
using TruckRouteAPI.Models;
using TruckRouteAPI.Services.Interface;
using Route = TruckRouteAPI.Models.Route;

namespace TruckRouteAPI.Controllers
{
    public class TruckRouteController : Controller
    {
        private readonly ITruckRouteService _truckRouteService;

        public TruckRouteController(ITruckRouteService truckRouteService)
        {
            _truckRouteService = truckRouteService;
        }

        [HttpPost("usuarios")]
        public IActionResult CreateUser([FromBody] User user)
        {
            try
            {
                _truckRouteService.CreateUser(user);
               
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the user: {ex.Message}");
            }
        }

        [HttpGet("buscar usuário")]
        public IActionResult GetUserById(long id)
        {
            try
            {
                var user = _truckRouteService.GetUserById(id);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpPost("adicionar rota")]
        public IActionResult AddRoute([FromBody] Route route)
        {
            try
            {
                _truckRouteService.AddRoute(route);

                return Ok();
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpPost("salvar rota no arquivo")]
        public IActionResult SaveRouteFile(string routeName, string password)
        {
            try
            {
                _truckRouteService.SaveRoutetoFile(routeName, password);

                return Content("Arquivo Salvo com sucesso");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
    }
}
