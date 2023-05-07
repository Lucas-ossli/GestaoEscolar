using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GestaoEscolar.Models;
using SqlGuide.Interface;
using Models;

namespace GestaoEscolar.Controllers;

public class DiretorController : Controller
{
    private readonly IPessoaRepository _pessoaRepository;
    private readonly ITurmaRepository _turmaRepository;
    private readonly IDisciplinaRepository _disciplinaRepository;
    private readonly ITurmaProfessorRepository _turmaProfRepository;

    public DiretorController(   IPessoaRepository pessoaRepository,
                                ITurmaRepository turmaRepository,
                                IDisciplinaRepository disciplinaRepository,
                                ITurmaProfessorRepository turmaProfRepository)
    {
        _pessoaRepository = pessoaRepository;
        _turmaRepository = turmaRepository;
        _disciplinaRepository = disciplinaRepository;
        _turmaProfRepository = turmaProfRepository;
    }    

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult CadastroPessoa()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CadastroPessoa(Pessoa professor)
    {
        _pessoaRepository.Insert(professor);
        return View();
    }

    public IActionResult CadastroTurma()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CadastroTurma(Turma turma)
    {
        _turmaRepository.Insert(turma);
        return View();
    }

    public IActionResult CadastroDisciplina()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CadastroDisciplina(Disciplina disciplina)
    {
        _disciplinaRepository.Insert(disciplina);
        return View();
    }

    public IActionResult CadastroTurmaProfessor()
    {
        var model = new TurmaProfessor2();
        model.Disciplinas = _disciplinaRepository.SearchAll();
        model.Professores = _pessoaRepository.SearchAllProfessores();
        model.Turmas = _turmaRepository.SearchAllTurmas();
        model.TurmaProfessores = _turmaProfRepository.SearchAll();
        return View(model);
    }

    [HttpPost]
    public IActionResult CadastroTurmaProfessor(TurmaProfessor2 model)
    {
        _turmaProfRepository.Insert(model);
        return RedirectToAction("CadastroTurmaProfessor");
    }

    [Route("Diretor/ExcluirTP/{cdTurmaProfessor}")]
    public IActionResult ExcluirTP(int cdTurmaProfessor)
    {
        //TODO - TERMINAR
        _turmaProfRepository.Delete(cdTurmaProfessor);
        return RedirectToAction("CadastroTurmaProfessor");
    }


}
