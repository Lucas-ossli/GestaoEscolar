public class Turma
{
    public int CdTurma { get; set; }
    public string? Nome { get; set; }
    public int QtdAlunos { get; set; }
    public int CdProfessor { get; set; }


    public List<Turma> Search(int cdProfessor)
    {
        //select do nome e numero da turma do professor 
        /*
        select
        Pr.IdTurma,
        Tu.Nome,
        count(Al)
        from Professor Pr 
        where IdProfessor = @CdProfessor
        inner join Turma Tu
        on Tu.IdTurma = Pr.IdTurma
        inner join Aluno Al
        on Al.IdTurma = Pr.IdTurma
        */
        var turmas = new List<Turma>();

        
        foreach(var i in  turmas){
            /*
            select count(*) from Aluno Al
            where Al.IdTurma = @CdTurma
            */
        }
        
        return null;
    }

}