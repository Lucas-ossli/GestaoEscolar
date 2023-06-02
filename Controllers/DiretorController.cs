﻿using System.Diagnostics;
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

    public bool VerifyCargo()
    {
        if(Cadastro.CdCargo == Cargo.Diretor.GetHashCode())
        {
            return true;
        }
        return false;
    }
    
    public IActionResult Index()
    {
        if(VerifyCargo())
        {
            return View(); 
        }
        return RedirectToAction("Login", "Home");
        
    }
    public IActionResult CadastroPessoa()
    {
        if(VerifyCargo())
        {
            return View();
        }
        return RedirectToAction("Login", "Home");
    }

    [HttpPost]
    public IActionResult CadastroPessoa(Pessoa pessoa)
    {
        if(VerifyCargo())
        {
            _pessoaRepository.Insert(pessoa);
            _cadastroRepository.create(pessoa);
            return View();
        }
        return RedirectToAction("Login", "Home");
    }

    public IActionResult CadastroTurma()
    {
        if(VerifyCargo())
        {
            return View();
        }
        return RedirectToAction("Login", "Home");
    }

    [HttpPost]
    public IActionResult CadastroTurma(Turma turma)
    {
        if(VerifyCargo())
        {
            _turmaRepository.Insert(turma);
            return View();
        }
        return RedirectToAction("Login", "Home");       
    }

    public IActionResult CadastroDisciplina()
    {
        if(VerifyCargo())
        {
            return View(); 
        }
        return RedirectToAction("Login", "Home");
    }

    [HttpPost]
    public IActionResult CadastroDisciplina(Disciplina disciplina)
    {
        if(VerifyCargo())
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
        return RedirectToAction("Login", "Home");
    }

    [HttpGet]
    [Route("Diretor/CadastroTurmaProfessor/{ativo}")]
    [Route("Diretor/CadastroTurmaProfessor")]
    public IActionResult CadastroTurmaProfessor(bool ativo = true)
    {
        if(VerifyCargo())
        {
            var model = new TurmaProfessor2();
            model.Disciplinas = _disciplinaRepository.SearchAll();
            model.Professores = _pessoaRepository.SearchAllProfessores();
            model.Turmas = _turmaRepository.SearchAllTurmas();
            model.TurmaProfessores = _turmaProfRepository.SearchAll(ativo);
            return View(model);
        }
        return RedirectToAction("Login", "Home");
    }

    [HttpPost]
    public IActionResult CadastroTurmaProfessor(TurmaProfessor2 model)
    {
        if(VerifyCargo())
        {
            _turmaProfRepository.Insert(model);
            return RedirectToAction("CadastroTurmaProfessor");   
        }
        return RedirectToAction("Login", "Home");
    }

    [Route("Diretor/ExcluirTP/{cdTurmaProfessor}")]
    public IActionResult ExcluirTP(int cdTurmaProfessor)
    {
        if(VerifyCargo())
        {
            _turmaProfRepository.InativarTP(cdTurmaProfessor);
            return RedirectToAction("CadastroTurmaProfessor");    
        }
        return RedirectToAction("Login", "Home");
    }

    [Route("Diretor/InativarTP/{cdTurmaProfessor}")]
    public IActionResult InativarTP(int cdTurmaProfessor)
    {
        if(VerifyCargo())
        {
           _turmaProfRepository.InativarTP(cdTurmaProfessor);
            return RedirectToAction("CadastroTurmaProfessor");    
        }
        return RedirectToAction("Login", "Home");
    }

    [Route("Diretor/AtivarTP/{cdTurmaProfessor}")]
    public IActionResult AtivarTP(int cdTurmaProfessor)
    {
        if(VerifyCargo())
        {
            _turmaProfRepository.AtivarTP(cdTurmaProfessor);
            return RedirectToAction("CadastroTurmaProfessor");     
        }
        return RedirectToAction("Login", "Home");
    }
}
