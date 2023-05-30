using Models;
namespace SqlGuide.Interface;

public interface IChamadaRepository
{
    public List<Chamada> Search(int cdTurmaProfessor);
    public void Insert(List<Aluno> alunos, int? cdAula);
    public void Update(List<Chamada> model);
    public Comparecimento PresencaFalta(List<Aproveitamento> alunos);
}
