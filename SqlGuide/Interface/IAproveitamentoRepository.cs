using Models;

namespace SqlGuide.Interface;
public interface IAproveitamentoRepository
{
    List<Pessoa> SearchAlunos(int? cdTurmaProfessor);
    
}
