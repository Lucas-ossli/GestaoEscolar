using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GestaoEscolar.Models;
using Microsoft.Data.SqlClient;
using SqlGuide.Repository;

namespace GestaoEscolar.Controllers;

public class ProfessorController : Controller
{
    [HttpGet]
    //TODO - Adicionar o cdProfessor do professor que vira no Identity
    public IActionResult Turmas(int cdProfessor = 3)
    {       
        var turmas = new TurmaRepository().Search(cdProfessor);
        return View(turmas);
    }

    [Route("Professor/Aulas/{CdAula}")]
    public IActionResult Aulas(int cdAula)
    {
        var aulas = new AulaRepository().Search(cdAula);
        return View(aulas);
    }
    
    [Route("Professor/Chamada/{CdAula}")]
    public IActionResult Chamada(int cdAula)
    {
        var model = new ChamadaRepository().Search(cdAula);
        return View(model);
    }

    
}
