using System.ComponentModel.DataAnnotations;

namespace Models;
public class Pessoa
{
    [Required(ErrorMessage ="O campo Nome deve ser preenchido")]
    public string? Nome { get; set; }
    public int? CdPessoa{get;set;}
    
    [Required(ErrorMessage ="O campo CPF deve ser preenchido")]
    public string? Cpf { get; set; }

    [Required(ErrorMessage ="O campo Data de Nascimento deve ser preenchido")]
    public DateTime DataNascimento { get; set; }
    public string? Telefone { get; set; }
    public int CdCargo { get; set; }
    
}