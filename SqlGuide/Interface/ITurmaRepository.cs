namespace SqlGuide.Interface
{

    public interface ITurmaRepository
    {
        public List<Turma> Search(int cdProfessor);

    }
}