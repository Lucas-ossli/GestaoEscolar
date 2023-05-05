using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GestaoEscolar.Models;

namespace GestaoEscolar.Controllers;

public class AlunoController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
   
}
