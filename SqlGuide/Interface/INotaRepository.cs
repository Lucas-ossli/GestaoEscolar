using GestaoEscolar.Models;

namespace SqlGuide.Interface;
public interface INotaRepository
{
    public int Insert(int? cdAluno, int? cdTurmaProfessor);
    public List<Nota> Search(int? cdTurmaProfessor);
    public Nota SearchOne(int? cdAluno, int? cdTurmaProfessor);
    public void Update(Nota nota);
    public Nota SearchForAluno(int? cdNota);
}