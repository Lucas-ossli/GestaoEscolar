using Models;

namespace SqlGuide.Interface;
public interface ICadastroRepository
{
    void create(Pessoa pessoa);
    public int checkDoubleName(string nome);
}
