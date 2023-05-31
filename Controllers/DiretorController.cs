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
    
    private readonly ICadastroRepository _cadastroRepository;

    public DiretorController(   IPessoaRepository pessoaRepository,
                                ITurmaRepository turmaRepository,
                                IDisciplinaRepository disciplinaRepository,
                                ITurmaProfessorRepository turmaProfRepository,
                                ICadastroRepository cadastroRepository)
    {
        _pessoaRepository = pessoaRepository;
        _turmaRepository = turmaRepository;
        _disciplinaRepository = disciplinaRepository;
        _turmaProfRepository = turmaProfRepository;
        _cadastroRepository=cadastroRepository;
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
    public IActionResult CadastroPessoa(Pessoa pessoa)
    {
        _pessoaRepository.Insert(pessoa);
        _cadastroRepository.create(pessoa);
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

        if(ModelState.IsValid){
            _disciplinaRepository.Insert(disciplina);
            return RedirectToAction("index");
        }
        else
        {
           return View(disciplina);
        }

        
    }

    [HttpGet]
    [Route("Diretor/CadastroTurmaProfessor/{ativo}")]
    [Route("Diretor/CadastroTurmaProfessor")]
    public IActionResult CadastroTurmaProfessor(bool ativo = true)
    {
        var model = new TurmaProfessor2();
        model.Disciplinas = _disciplinaRepository.SearchAll();
        model.Professores = _pessoaRepository.SearchAllProfessores();
        model.Turmas = _turmaRepository.SearchAllTurmas();
        model.TurmaProfessores = _turmaProfRepository.SearchAll(ativo);
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
        _turmaProfRepository.InativarTP(cdTurmaProfessor);
        return RedirectToAction("CadastroTurmaProfessor");
    }

    [Route("Diretor/InativarTP/{cdTurmaProfessor}")]
    public IActionResult InativarTP(int cdTurmaProfessor)
    {
        //TODO - TERMINAR
        _turmaProfRepository.InativarTP(cdTurmaProfessor);
        return RedirectToAction("CadastroTurmaProfessor");
    }

    [Route("Diretor/AtivarTP/{cdTurmaProfessor}")]
    public IActionResult AtivarTP(int cdTurmaProfessor)
    {
        _turmaProfRepository.AtivarTP(cdTurmaProfessor);
        return RedirectToAction("CadastroTurmaProfessor");
    }


}
