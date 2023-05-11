using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GestaoEscolar.Models;

namespace GestaoEscolar.Controllers;

public class LogInController : Controller
{
    
    public IActionResult Index()
    {
        return View();
    }
   
    public IActionResult CreateAccount()
    {
        /*INPUT
        CPF 
        DataNascimento
        EMAIL
        SENHA >= 8

        Verificar no banco se essa pessoa está registrada
        Caso tiver Criar a conta dela correlacionada com a tabela Pessoas
        Caso contrario Negar o Cadastro
        */
        return View();
    }
}
