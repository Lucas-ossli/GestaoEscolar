using Microsoft.AspNetCore.Mvc.Rendering;

namespace Models;
public class Turma
{
    public int CdTurma { get; set; }
    public string? NomeDaTurma { get; set; }
    public string? Descricao { get; set; }

    public static IEnumerable<SelectListItem>? SelectList(List<Turma> turmas)
    {
        var retorno = new List<SelectListItem>();

        foreach (var item in turmas)
        {
            retorno.Add(new SelectListItem(){
                Text = item.NomeDaTurma,
                Value = item.CdTurma.ToString()
            });
        }

        return retorno;
    }
}