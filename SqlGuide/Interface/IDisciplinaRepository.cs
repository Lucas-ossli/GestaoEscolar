using Models;

namespace SqlGuide.Interface;
public interface IDisciplinaRepository
{
    public void Insert(Disciplina disciplina);
    public List<Disciplina> SearchAll();
}