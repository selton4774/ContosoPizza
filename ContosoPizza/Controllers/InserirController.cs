using ContosoPizza.Services;
using ContosoPizza.Models;
using Microsoft.AspNetCore.Mvc;
using ContosoPizza.Data;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]
public class InserirController : ControllerBase
{
    PizzaService _service;

    public InserirController(PizzaService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult Create()
    {
        List<Pizza> pizzas = Database.Inserir();

        foreach (var pizza in pizzas)
        {
            _service.Create(pizza);
        }

        return Ok();
    }
}