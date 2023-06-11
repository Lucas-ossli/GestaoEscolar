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
    

    public bool VerifyCargo()
    {
        if(Cadastro.CdCargo != Cargo.Aluno.GetHashCode())
        {
            return true;
        }
        return false;
    }

    [HttpGet]
    [Route("Professor/TurmasProfessor/{cdProfessor}/{ativo}")]
    [Route("Professor/TurmasProfessor/{cdProfessor}")]
    [Route("Professor/TurmasProfessor")]
    //TODO - Adicionar o cdProfessor do professor que vira no Identity
    public IActionResult TurmasProfessor(int cdProfessor, bool ativo = true)
    {       
        if(VerifyCargo())
        {
            var turmas = _turmaProfessorRepository.Search(Cadastro.CdPessoa, ativo);
            return View(turmas);
        }
        return RedirectToAction("Login", "Home");
    }

    [Route("Professor/Aulas/{cdTurmaProfessor}")]
    public IActionResult Aulas(int cdTurmaProfessor)
    {
        if(VerifyCargo())
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
        return RedirectToAction("Login", "Home");
    }
    
    [Route("Professor/Chamada/{CdAula}")]
    public IActionResult Chamada(int cdAula)
    {
        if(VerifyCargo())
        {
            var model = _chamadaRepository.Search(cdAula);
            return View(model);
        }
        return RedirectToAction("Login", "Home");
       
    }

    public IActionResult AtualizarPresenca(List<Chamada> model)
    {
        if(VerifyCargo())
        {
            _chamadaRepository.Update(model as List<Chamada>);
            return RedirectToAction("Aulas", new { cdTurmaProfessor = _aulaRepository.GetTurmaProfessor(model.First().CdAula)});
        }
        return RedirectToAction("Login", "Home");
    }

    [Route("Professor/Alunos/{CdTurmaProfessor}")]
    public IActionResult Alunos(int CdTurmaProfessor)
    {
        if(VerifyCargo())
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
        return RedirectToAction("Login", "Home");
    }

    [HttpGet]
    [Route("Professor/CadastrarAula/{cdTurmaProfessor}")]
    public IActionResult CadastrarAula(int? cdTurmaProfessor)
    {
        if(VerifyCargo())
        {
            var model = new Aula(){
                CdTurmaProfessor = cdTurmaProfessor
            };
            return View(model);
        }
        return RedirectToAction("Login", "Home");
    }

    [HttpPost]
    public IActionResult CadastrarAula(Aula model)
    {
        if(VerifyCargo())
        {
            if(ModelState.IsValid){
                _aulaRepository.Insert(model);
                model = _aulaRepository.SearchOne(model);
                var alunos = _aproveitamentoRepository.SearchAlunos(model.CdTurmaProfessor);
                _chamadaRepository.Insert(alunos, model.CdAula);
                return RedirectToAction("Aulas", new { CdTurmaProfessor = model.CdTurmaProfessor });
            }
            return View(model);
        }
        return RedirectToAction("Login", "Home");
    }
    
    [HttpGet]
    [Route("Professor/IncluirAluno/{CdTurmaProfessor}")]
    public IActionResult IncluirAluno(int? cdTurmaProfessor)
    {
        if(VerifyCargo())
        {
            AlunoSubmit model = new AlunoSubmit();
            model.Alunos = _pessoaRepository.SearchAllAlunos();//TODO(TROCAR) :procurar apenas os alunos que não estão nesta cdTurmaProfessor
            return View(model);
        }
        return RedirectToAction("Login", "Home");       
    }

    [HttpPost]
    public IActionResult IncluirAluno(AlunoSubmit model)
    {
        if(VerifyCargo())
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
            _aproveitamentoRepository.Insert(model.CdPessoa, model.CdTurmaProfessor, cdNota);
            return RedirectToAction("Alunos",new { CdTurmaProfessor = model.CdTurmaProfessor });
        }
        return RedirectToAction("Login", "Home");
    }

    [HttpGet]
    [Route("Professor/Notas/{CdTurmaProfessor}")]
    public IActionResult Notas(int? cdTurmaProfessor)
    {
        if(VerifyCargo())
        {
            NotaSubmit model = new NotaSubmit();
            model.Notas = _notaRepository.Search(cdTurmaProfessor);
            return View(model);   
        }
        return RedirectToAction("Login", "Home");
    }

    [HttpGet]
    [Route("Professor/EditarNota/{cdAluno}/{CdTurmaProfessor}")]
    public IActionResult EditarNota(int? cdAluno, int? cdTurmaProfessor)
    {
        if(VerifyCargo())
        {
            var model = _notaRepository.SearchOne(cdAluno, cdTurmaProfessor);
            return View(model);
        }
        return RedirectToAction("Login", "Home");
    }

    [HttpPost]
    public IActionResult SalvarNota(Nota model)
    {
        if(VerifyCargo())
        {
            _notaRepository.Update(model);
            return RedirectToAction("Notas", new {cdTurmaProfessor = model.CdTurmaProfessor});
        }
        return RedirectToAction("Login", "Home");
    }
}
