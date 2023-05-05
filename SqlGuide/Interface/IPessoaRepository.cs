using Models;

namespace SqlGuide.Interface;

public interface IPessoaRepository
{
    public void Insert(Pessoa pessoa);

    public List<Professor> SearchAllProfessores();
}
