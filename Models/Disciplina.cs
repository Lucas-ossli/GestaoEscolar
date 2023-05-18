using System.ComponentModel.DataAnnotations;

namespace Models;

public class Disciplina
{
    public int CdDisciplina {get;set;}

    [Required(ErrorMessage ="O campo Nome Disciplina deve ser preenchido")]
    public string NomeDisciplina {get;set;}

    [Required(ErrorMessage ="O campo Carga Horária deve ser preenchido")]
    public int CargaHoraria {get;set;}
}   