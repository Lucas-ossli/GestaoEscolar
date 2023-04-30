using Models;

namespace SqlGuide.Interface
{

    public interface IAulaRepository
    {
        List<Aula> Search(int cdTurmaProfessor);

    }
}