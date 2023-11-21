#pragma warning disable CS8618
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using CRUDelicioso.Models;

namespace CRUDelicioso.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<Plato> ListaPlatos = _context.Platos.ToList();
        ViewBag.ListaPlatos = ListaPlatos;

        // Verificar si hay un mensaje de alerta
        if (TempData.ContainsKey("Alerta"))
        {
            ViewBag.AlertMessage = TempData["Alerta"];
        }

        return View("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult New()
    {
        return View("New");
    }

    // Home/Crear para crear un nuevo plato
    [HttpPost("Home/New")]
    public IActionResult Create(Plato miPlato)
    {
        if (ModelState.IsValid)
        {
            _context.Add(miPlato);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View("New");
    }

    // Mostrar los platos
    [HttpGet("Home/Plato/{platoid}")]
    public IActionResult Mostrar(int platoid)
    {
        List<Plato> elPlato = _context.Platos.Where(id => id.PlatoId == platoid).ToList();
        ViewBag.elPlato = elPlato;

        return View("Mostrar");
    }

    // Eliminar eliminar el plato 
    [HttpPost("Home/Plato/{platoid}/Eliminar")]
    public IActionResult Eliminar(int platoid)
    {
        Plato? PlatoSelecto = _context.Platos.SingleOrDefault(id => id.PlatoId == platoid);
        if (PlatoSelecto != null)
        {
            _context.Platos.Remove(PlatoSelecto);
            _context.SaveChanges();
            TempData["Alerta"] = $"El plato '{PlatoSelecto.Nombre}' ha sido eliminado con éxito.";
        }

        return RedirectToAction("Index");
    }

    // Editar modifica el plato
    [HttpPost("Home/Plato/{platoid}/Editar")]
    public IActionResult Editar(int platoid)
    {
        List<Plato> elPlato = _context.Platos.Where(id => id.PlatoId == platoid).ToList();
        ViewBag.elPlato = elPlato;
        TempData["EditSuccessMessage"] = "El plato ha sido editado exitosamente.";
        return View("Editar");
    }

    // Update actualiza el plato
    [HttpPost("Update/{platoid}")]
    public IActionResult Update(int platoid, string nombre, string chef, int sabor, int calorias, string descripcion)
    {
        Plato? PlatoSelecto = _context.Platos.FirstOrDefault(x => x.PlatoId == platoid);

        if (PlatoSelecto != null)
        {
            PlatoSelecto.Nombre = nombre;
            PlatoSelecto.Chef = chef;
            PlatoSelecto.Calorias = calorias;
            PlatoSelecto.Sabor = sabor;
            PlatoSelecto.Descripcion = descripcion;
            PlatoSelecto.Fecha_Modificacion = DateTime.Now;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        else
        {
            // En caso donde no se encuentra el Plato
            return NotFound();
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
