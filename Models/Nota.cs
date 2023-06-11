namespace GestaoEscolar.Models;

public class Nota
{
    public string? Aluno{get;set;}
    public int? CdNota{get;set;}
    public int? CdAluno{get;set;}
    public int? CdTurmaProfessor{get;set;}
    public float Nota1 {get;set;}
    public float Nota2 {get;set;}
    public float Nota3 {get;set;}
    public float Nota4 {get;set;}
}

public class NotaSubmit : Nota
{
    public List<Nota> Notas{get;set;}
}