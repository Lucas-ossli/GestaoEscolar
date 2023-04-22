using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GestaoEscolar.Models;

namespace GestaoEscolar.Controllers;

public class ProfessorController : Controller
{
    [HttpGet]
    //TODO - Adicionar o Id do professor que vira no Identity
    public IActionResult Index(/*int id*/)
    {       
        
        var turmas = new List<Turma>();
        turmas.Add(new Turma(){
            NomeDaDisciplina = "Banco de dados",
            NomeDaTurma = "1-a",
            QtdAlunos = 20,
            CdTurma = 1,
            CdDisciplina = 1
        });

        //var turmas = new Turma().Search(1);

        return View(turmas);
    }
    
    [Route("Professor/Chamada/{cdturma}/{cddisciplina}")]
    public IActionResult Chamada(int cdTurma, int cddisciplina)
    {
        var lista = new List<Chamada>();

        lista.Add(new Chamada("Pedro"));
        lista.Add(new Chamada("Christian"));
        lista.Add(new Chamada("Jean"));
        lista.Add(new Chamada("Larissa"));
        lista.Add(new Chamada("João"));
    
        lista.Add(new Chamada("Leonardo"));
        lista.Add(new Chamada("Lucas"));
        lista.Add(new Chamada("Karen"));
        lista.Add(new Chamada("Christian H."));
    
        
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
