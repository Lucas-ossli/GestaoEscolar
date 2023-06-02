using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GestaoEscolar.Models;
using SqlGuide.Interface;
using Models;

namespace GestaoEscolar.Controllers;

public class AlunoController : Controller
{
    private readonly IAproveitamentoRepository _aproveitamentoRepository;
    private readonly IChamadaRepository _chamadaRepository;
    private readonly INotaRepository _notaRepository;
    public AlunoController(IAproveitamentoRepository aproveitamentoRepository,IChamadaRepository chamadaRepository,INotaRepository notaRepository)
    {
        _aproveitamentoRepository = aproveitamentoRepository;
        _chamadaRepository = chamadaRepository;
        _notaRepository = notaRepository;
    }
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Aproveitamento()
    {
        //Pegar o cdaluno pelo login
        var model = _aproveitamentoRepository.AlunoInfo(Cadastro.CdPessoa);
        var x = _chamadaRepository.PresencaFalta(model);        
        return View(model);
    }


    [Route("Aluno/NotasAluno/{cdNota}")]
    public IActionResult NotasAluno(int? cdNota)
    {
        var model = _notaRepository.SearchForAluno(cdNota);
        //verificar se o cd nota tem o cdaluno logado
        return View(model);
    }
   
}
