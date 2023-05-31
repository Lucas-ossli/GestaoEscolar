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
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

   [HttpPost]
    public IActionResult Login(Cadastro cadastro)
    {
        // if(_cadastroRepository.VerifyLogin(cadastro))
        // {
        //     //var logingeral =_pessoaRepository.Search(cadastro.CdPessoa);// guarda o cargo dele no sistema;
        //     //redirect to firsts page
        // }
        return View();
    }
}