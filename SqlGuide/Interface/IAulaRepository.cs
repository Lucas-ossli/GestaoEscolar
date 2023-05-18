using Models;

namespace SqlGuide.Interface;
public interface IAulaRepository
{
    void Insert(Aula model);
    List<Aula> Search(int? cdTurmaProfessor);
    Aula SearchOne(Aula model);

}
