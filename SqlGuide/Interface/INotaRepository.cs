using Models;

namespace SqlGuide.Interface;
public interface INotaRepository
{
    public int Insert(int? cdAluno, int? cdTurmaProfessor);
}