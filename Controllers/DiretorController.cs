using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GestaoEscolar.Models;
using SqlGuide.Interface;
using Models;

namespace GestaoEscolar.Controllers;

public class DiretorController : Controller
{
    private readonly IPessoaRepository _pessoaRepository;

    public DiretorController(IPessoaRepository pessoaRepository)
    {
        _pessoaRepository = pessoaRepository;
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

    
   
}
