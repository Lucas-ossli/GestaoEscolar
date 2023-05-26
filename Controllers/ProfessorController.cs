using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GestaoEscolar.Models;
using Microsoft.Data.SqlClient;
using SqlGuide.Interface;
using Models;

namespace GestaoEscolar.Controllers;

public class ProfessorController : Controller
{
    private readonly ITurmaProfessorRepository _turmaProfessorRepository;
    private readonly IAulaRepository _aulaRepository;
    private readonly IChamadaRepository _chamadaRepository;
    private readonly IAproveitamentoRepository _aproveitamentoRepository;
    private readonly IPessoaRepository _pessoaRepository;
    private readonly INotaRepository _notaRepository;

    public ProfessorController( ITurmaProfessorRepository turmaProfessorRepository,
                                IAulaRepository aulaRepository,
                                IChamadaRepository chamadaRepository,
                                IAproveitamentoRepository aproveitamentoRepository,
                                IPessoaRepository pessoaRepository,
                                INotaRepository notaRepository)
    {
        _turmaProfessorRepository = turmaProfessorRepository;
        _aulaRepository = aulaRepository;
        _chamadaRepository= chamadaRepository;
        _aproveitamentoRepository = aproveitamentoRepository;
        _pessoaRepository = pessoaRepository;
        _notaRepository = notaRepository;
    }

    [HttpGet]
    [Route("Professor/TurmasProfessor/{cdProfessor}/{ativo}")]
    [Route("Professor/TurmasProfessor/{cdProfessor}")]
    [Route("Professor/TurmasProfessor")]
    //TODO - Adicionar o cdProfessor do professor que vira no Identity
    public IActionResult TurmasProfessor(int cdProfessor = 3, bool ativo = true)
    {       
        var turmas = _turmaProfessorRepository.Search(cdProfessor, ativo);
        return View(turmas);
    }

    [Route("Professor/Aulas/{cdTurmaProfessor}")]
    public IActionResult Aulas(int cdTurmaProfessor)
    {
        var aulas = _aulaRepository.Search(cdTurmaProfessor);
        if(aulas.Count == 0){
            aulas.Add(new Aula(){
                CdTurmaProfessor = cdTurmaProfessor
            });
        }
        else{
            aulas.First().CdTurmaProfessor = cdTurmaProfessor;
        }

        return View(aulas);
    }
    
    [Route("Professor/Chamada/{CdAula}")]
    public IActionResult Chamada(int cdAula)
    {
        var model = _chamadaRepository.Search(cdAula);
        return View(model);
    }

    public IActionResult AtualizarPresenca(List<Chamada> model)
    {
        _chamadaRepository.Update(model as List<Chamada>);
        return RedirectToAction("Chamada", new { cdAula = model.First().CdAula});
    }

    [Route("Professor/Alunos/{CdTurmaProfessor}")]
    public IActionResult Alunos(int CdTurmaProfessor)
    {
        var model = _aproveitamentoRepository.SearchAlunos(CdTurmaProfessor);
        if(model.Any()){
            model.First().CdTurmaProfessor = CdTurmaProfessor;
        }else{
            model.Add(new Aluno(){
                CdTurmaProfessor = CdTurmaProfessor
            });
        }

        return View(model);
    }

    [HttpGet]
    [Route("Professor/CadastrarAula/{cdTurmaProfessor}")]
    public IActionResult CadastrarAula(int? cdTurmaProfessor)
    {
        var model = new Aula(){
            CdTurmaProfessor = cdTurmaProfessor
        };
        return View(model);
    }

    [HttpPost]
    public IActionResult CadastrarAula(Aula model)
    {
        //Verificar se o model tem o cdTP
        _aulaRepository.Insert(model);
        model = _aulaRepository.SearchOne(model);
        var alunos = _aproveitamentoRepository.SearchAlunos(model.CdTurmaProfessor);
        _chamadaRepository.Insert(alunos, model.CdAula);
        return RedirectToAction("Aulas", new { CdTurmaProfessor = model.CdTurmaProfessor });
    }

    [HttpGet]
    [Route("Professor/IncluirAluno/{CdTurmaProfessor}")]
    public IActionResult IncluirAluno(int? cdTurmaProfessor)
    {
        AlunoSubmit model = new AlunoSubmit();
        model.Alunos = _pessoaRepository.SearchAllAlunos();//TODO(TROCAR) :procurar apenas os alunos que não estão nesta cdTurmaProfessor
        return View(model);
    }

    [HttpPost]
    public IActionResult IncluirAluno(AlunoSubmit model)
    {
        var aulas = _aulaRepository.Search(model.CdTurmaProfessor);
        if(aulas.Any()){
            List<Aluno> alunos = new List<Aluno>(){new Aluno(){
            CdPessoa = model.CdPessoa
            }};
            foreach(var item in aulas){
                _chamadaRepository.Insert(alunos, item.CdAula);
            }
        }
        var cdNota = _notaRepository.Insert(model.CdPessoa, model.CdTurmaProfessor);
        _aproveitamentoRepository.Insert(model.CdPessoa, model.CdTurmaProfessor, cdNota);//tem que passar o id da nota

        return RedirectToAction("Alunos",new { CdTurmaProfessor = model.CdTurmaProfessor });
    }

    [HttpGet]
    [Route("Professor/Notas/{CdTurmaProfessor}")]
    public IActionResult Notas(int? cdTurmaProfessor)
    {
        NotaSubmit model = new NotaSubmit();
        model.Notas = _notaRepository.Search(cdTurmaProfessor);
        return View(model);
    }

    [HttpGet]
    [Route("Professor/EditarNota/{cdAluno}/{CdTurmaProfessor}")]
    public IActionResult EditarNota(int? cdAluno, int? cdTurmaProfessor)
    {
        var model = _notaRepository.SearchOne(cdAluno, cdTurmaProfessor);
        return View(model);
    }

    [HttpPost]
    public IActionResult SalvarNota(Nota model)
    {
        _notaRepository.Update(model);
        return RedirectToAction("Notas", new {cdTurmaProfessor = model.CdTurmaProfessor});
    }
}
