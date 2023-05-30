using System.ComponentModel.DataAnnotations;
using GestaoEscolar.Models;
namespace Models;

public class Aproveitamento
{
   public int CdAluno { get; set; }
   public int cdTurmaProfessor{get;set;}
   public int Presencas{get;set;}
   public int Faltas{get;set;}
   public string Aluno {get;set;}
   public string Professor { get; set; }
   public string Turma { get; set; }
   public string Disciplina{get;set;}
   public int CdNota{get;set;}
   public Nota Notas{get;set;}
}   

public class Comparecimento
{
   public int Presencas { get; set; }
   public int Faltas { get; set; }
}