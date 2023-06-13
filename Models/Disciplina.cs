using System.ComponentModel.DataAnnotations;

namespace Models;

public class Disciplina
{
    public int CdDisciplina {get;set;}

    [Required(ErrorMessage ="O campo Nome Disciplina deve ser preenchido")]
    public string NomeDisciplina {get;set;}

    [Range(1, 80, ErrorMessage = "A carga horária deve estar entre 1 e 80.")]
    public int CargaHoraria {get;set;}
}   