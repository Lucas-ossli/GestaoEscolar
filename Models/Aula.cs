using System.ComponentModel.DataAnnotations;
namespace Models;
public class Aula
{
    
    [Required(ErrorMessage ="O campo Data deve ser preenchido")]
    public DateTime Data{get;set;}

    [Required(ErrorMessage ="O campo Descrição deve ser preenchido")]
    public string? Descricao{get;set;}
    public int CdAula{get;set;}
    public int? CdTurmaProfessor{get;set;}
}
