
public class Chamada
{

    public string Aluno {get;set;}
    public int CdTurma {get;set;}
    public int CdAluno{get;set;}
    public bool Presenca1 {get;set;}
    public bool Presenca2 {get;set;}
    public bool Presenca3 {get;set;}
    public bool Presenca4 {get;set;}

    public Chamada()
    {
        
    }
    public Chamada(string nome)
    {
        Aluno = nome;
        Presenca1 = true;
        Presenca2 = false;
        Presenca3 = true;
        Presenca4 = false;
    }
    public void Insert(List<Chamada> model)
    {
        //TODO SQL
        /* 
        insert into Chamada values(@IdAluno, @presenca1, @presenca2, @presenca3, @presenca4);
        */
    }

    public void Search(List<Chamada> model)
    {
        //TODO SQL
        /* 
        select * from Chamada ch
        where Turma = @turma
        order by ch.Aluno asc
        */

    }



}