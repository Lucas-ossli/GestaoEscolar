using Models;

namespace SqlGuide.Interface;
public interface ITurmaProfessorRepository
{
    public List<TurmaProfessor> Search(int cdProfessor, bool ativo);
    public List<TurmaProfessor> SearchAll(bool ativo);
    public void Insert(TurmaProfessor2 model);
    public void InativarTP(int? cdTurmaProfessor);
    public void AtivarTP(int? cdTurmaProfessor);
}
