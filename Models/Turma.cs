using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
public class Turma
{
    public string? NomeDaTurma { get; set; }
    public string? NomeDaDisciplina { get; set; }
    public int QtdAlunos { get; set; }
    public int CdTurmaProfessor { get; set; }
}