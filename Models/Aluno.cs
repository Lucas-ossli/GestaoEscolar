namespace Models;
public class Aluno : Pessoa
{

   public int? CdTurmaProfessor{get;set;}
}

public class AlunoSubmit 
{
   public List<Aluno> Alunos {get;set;}

   public int? CdPessoa{get;set;}

   public int? CdTurmaProfessor{get;set;}
}