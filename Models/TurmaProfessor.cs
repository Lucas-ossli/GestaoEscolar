namespace Models;
public class TurmaProfessor
{
    public string? NomeDaTurma { get; set; }
    public string? NomeDaDisciplina { get; set; }
    public int QtdAlunos { get; set; }
    public int CdTurmaProfessor { get; set; }
}

//TODO - JUNTAR AS DUAS CLASSES EM UMA
public class TurmaProfessor2
{
    public List<Pessoa>? Professores {get;set;}
    public List<Turma>? Turmas {get;set;}
    public List<Disciplina>? Disciplinas {get;set;}

    public int CdProfessor {get;set;}
    public int CdTurma {get;set;}
    public int CdDisciplina {get;set;}
}