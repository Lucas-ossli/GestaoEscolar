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
    private readonly IAproveitamentoRepository _aproveitamentoRepository;

    public ProfessorController( ITurmaProfessorRepository turmaProfessorRepository,
                                IAulaRepository aulaRepository,
                                IChamadaRepository chamadaRepository,
                                IAproveitamentoRepository aproveitamentoRepository)
    {
        _turmaProfessorRepository = turmaProfessorRepository;
        _aulaRepository = aulaRepository;
        _chamadaRepository= chamadaRepository;
        _aproveitamentoRepository = aproveitamentoRepository;
    }

    [HttpGet]
    [Route("Professor/TurmasProfessor/{cdProfessor}/{ativo}")]
    [Route("Professor/TurmasProfessor")]
    //TODO - Adicionar o cdProfessor do professor que vira no Identity
    public IActionResult TurmasProfessor(int cdProfessor = 3, bool ativo = true)
    {       
        var turmas = _turmaProfessorRepository.Search(cdProfessor, ativo);
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

    [Route("Professor/Alunos/{CdTurmaProfessor}")]
    public IActionResult Alunos(int CdTurmaProfessor)
    {
        var model = _aproveitamentoRepository.SearchAlunos(CdTurmaProfessor);
        return View(model);
    }

    
}
