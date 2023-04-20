using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GestaoEscolar.Models;

namespace GestaoEscolar.Controllers;

public class DiretorController : Controller
{
    
    public IActionResult Index()
    {
        return View();
    }
   
}
