using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Models;
public class Turma
{
    public int CdTurma { get; set; }
    
    [Required(ErrorMessage ="O campo Nome da Turma deve ser preenchido")]
    public string? NomeDaTurma { get; set; }

    [Required(ErrorMessage ="O campo Descrição deve ser preenchido")]
    public string? Descricao { get; set; }
}