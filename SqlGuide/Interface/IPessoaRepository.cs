using Models;

namespace SqlGuide.Interface;

public interface IPessoaRepository
{
    public void Insert(Pessoa pessoa);

    public List<Pessoa> SearchAllProfessores();

    public List<Aluno> SearchAllAlunos();
}
