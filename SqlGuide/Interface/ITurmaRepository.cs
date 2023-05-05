using Models;

namespace SqlGuide.Interface;
public interface ITurmaRepository
{
    public void Insert(Turma turma);

    public List<Turma> SearchAllTurmas();
    
}