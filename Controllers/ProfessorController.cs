using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GestaoEscolar.Models;

namespace GestaoEscolar.Controllers;

public class ProfessorController : Controller
{
    
    public IActionResult Index(int id)
    {       
        
        var turmas = new List<Turma>();

        turmas.Add(new Turma(){
            Nome = "1º A",
            QtdAlunos = 5,
            CdTurma = 1
            
        });

         turmas.Add(new Turma(){
            Nome = "1º B",
            QtdAlunos = 4,
            CdTurma = 2
        });

        return View(turmas);
    }

    [HttpGet]
    public IActionResult Chamada(int id)
    {
        var lista = new List<Chamada>();

        if(id == 1){
            lista.Add(new Chamada("Pedro"));
            lista.Add(new Chamada("Christian"));
            lista.Add(new Chamada("Jean"));
            lista.Add(new Chamada("Larissa"));
            lista.Add(new Chamada("João"));
            
        }
        else if(id == 2)
        {
            lista.Add(new Chamada("Leonardo"));
            lista.Add(new Chamada("Lucas"));
            lista.Add(new Chamada("Karen"));
            lista.Add(new Chamada("Christian H."));
        }
        
        //var retorno = chamada.Search();
        
        return View(lista);
        
    }

    [HttpPost]
    public IActionResult Chamada(List<Chamada> model)
    {
        var chamada = new Chamada();
        chamada.Insert(model);
        
        return View();
    }
   
}
