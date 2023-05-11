using Models;
namespace SqlGuide.Interface;

public interface IChamadaRepository
{
    public List<Chamada> Search(int cdTurmaProfessor);

    public void Insert(List<Pessoa> alunos, int? cdAula);
}
