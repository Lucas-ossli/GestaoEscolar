using Models;

namespace SqlGuide.Interface;
public interface ITurmaProfessorRepository
{
    public List<TurmaProfessor> Search(int cdProfessor);
}