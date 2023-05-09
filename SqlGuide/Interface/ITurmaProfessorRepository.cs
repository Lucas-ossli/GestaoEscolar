using Models;

namespace SqlGuide.Interface;
public interface ITurmaProfessorRepository
{
    public List<TurmaProfessor> Search(int cdProfessor, bool ativo);
    public List<TurmaProfessor> SearchAll();
    public void Insert(TurmaProfessor2 model);
    public void Delete(int? cdTurmaProfessor);
}
