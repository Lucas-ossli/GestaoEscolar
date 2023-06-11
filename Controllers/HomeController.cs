﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GestaoEscolar.Models;
using Models;
using SqlGuide.Interface;

namespace GestaoEscolar.Controllers;

public class HomeController : Controller
{

    private readonly ICadastroRepository _cadastroRepository;
    private readonly IPessoaRepository _pessoaRepository;
    public HomeController(ICadastroRepository cadastroRepository,IPessoaRepository pessoaRepository)
    {
        _cadastroRepository = cadastroRepository;
        _pessoaRepository = pessoaRepository;
    }

    public IActionResult LogOut()
    {
        Cadastro.Login = false;
        Cadastro.CdCargo = 0;
        Cadastro.CdPessoa = 0;
        return RedirectToAction("Login");
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Login()
    {

         if(Cadastro.Login)
        {
            switch (Cadastro.CdCargo)
            {
                case 1:
                    return RedirectToAction("Index", "Diretor");
                break;
                case 2:
                    return RedirectToAction("TurmasProfessor", "Professor", new{cdTurmaProfessor = Cadastro.CdPessoa});
                break;

                case 3:
                    return RedirectToAction("Aproveitamento", "Aluno");
                break;
            }        
        }

        return View();
    }

   [HttpPost]
    public IActionResult Login(Cadastro cadastro)
    {
        if(!Cadastro.Login)
        {
            _cadastroRepository.VerifyLogin(cadastro);
        }
        
        if(Cadastro.Login)
        {
            switch (Cadastro.CdCargo)
            {
                case 1:
                    return RedirectToAction("Index", "Diretor");
                break;
                case 2:
                    return RedirectToAction("TurmasProfessor", "Professor", new{cdTurmaProfessor = Cadastro.CdPessoa});
                break;

                case 3:
                    return RedirectToAction("Aproveitamento", "Aluno");
                break;
                default:
                    return RedirectToAction("Login", "Home");
                break;
            }        
        }
        ViewBag.Error = "Usuário/Senha inválidos";
        
        return View();
    }
}