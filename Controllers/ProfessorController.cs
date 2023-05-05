using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GestaoEscolar.Models;
using Microsoft.Data.SqlClient;
using SqlGuide.Interface;

namespace GestaoEscolar.Controllers;

public class ProfessorController : Controller
{
    private readonly ITurmaProfessorRepository _turmaProfessorRepository;
    private readonly IAulaRepository _aulaRepository;
    private readonly IChamadaRepository _chamadaRepository;

    public ProfessorController(ITurmaProfessorRepository turmaProfessorRepository,IAulaRepository aulaRepository,IChamadaRepository chamadaRepository)
    {
        _turmaProfessorRepository = turmaProfessorRepository;
        _aulaRepository = aulaRepository;
        _chamadaRepository= chamadaRepository;
    }

    [HttpGet]
    //TODO - Adicionar o cdProfessor do professor que vira no Identity
    public IActionResult TurmasProfessor(int cdProfessor = 3)
    {       
        var turmas = _turmaProfessorRepository.Search(cdProfessor);
        return View(turmas);
    }

    [Route("Professor/Aulas/{CdAula}")]
    public IActionResult Aulas(int cdAula)
    {
        var aulas = _aulaRepository.Search(cdAula);
        return View(aulas);
    }
    
    [Route("Professor/Chamada/{CdAula}")]
    public IActionResult Chamada(int cdAula)
    {
        var model = _chamadaRepository.Search(cdAula);
        return View(model);
    }

    
}
