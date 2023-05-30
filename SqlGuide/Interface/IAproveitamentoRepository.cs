using Models;

namespace SqlGuide.Interface;
public interface IAproveitamentoRepository
{
    List<Aluno> SearchAlunos(int? cdTurmaProfessor);

    void Insert(int? cdAluno, int? cdTurmaProfessor, int cdNota);

    List<Aproveitamento> AlunoInfo(int? cdAluno);
    
}
