public class Turma
{

    public string? NomeDaTurma { get; set; }
    public string? NomeDaDisciplina { get; set; } //Engenharia
    public int QtdAlunos { get; set; }
    public int CdTurma { get; set; }
    public int CdDisciplina { get; set; }
    public int CdProfessor { get; set; }


    public List<Turma> Search(int cdProfessor)
    {
         var turmas = new List<Turma>();
        //select do nome e numero da turma do professor 
        var sql = "select  "
        +"di.nomeDisciplina, "
        +"tu.Nome, "
        +"tu.idTurma, "
        +"di.idDisciplina" 
        + "from Disciplinas di, TurmaProfessor Tp "
        + "inner join Turma tu on tu.idTurma = Tp.TurmaId "
        + "where di.professorId = @CdProfessor and Tp.ProfessorId = @CdProfessor ";
        
        /*
        using(var cn = new SqlConnection(_conn))
        {
            cn.Open();
            using(var cmd = new SqlCommand(sql, cn))
            {
                using(var dr = cmd.ExecuteReader())
                {
                    if(dr.HasRows)
                    {
                        while(dr.Read())
                        {
                            turmas.Add(new Turma(){
                                NomeDaDisciplina = dr["nomeDisciplina"].ToString(),
                                NomeDaTurma = dr["Nome"].ToString(),
                                CdTurma = Convert.ToInt32(dr["TurmaId"]),
                                CdDisciplina = Convert.ToInt32(dr["idDisciplina"])
                            });
                        }
                    }
                }
            }
        }
        */

        var sql2Count = "select count(*) as QuantidadeAluno from Aproveitamentos Ap "
        +"where Ap.TurmaId = @CdTurma and Ap.disciplinaId = @CdDisciplina";
        List<int> qtdAlunos = new List<int>();
        foreach(var item in turmas){
            /*
            

            using(var cmd EntityCommand(sql2Count, conn))
            {
                cmd.Parameters.Add(new EntityParameter(){
                ParameterName = "@CdTurma",
                value = item.CdTurma
                });

                cmd.Parameters.Add(new EntityParameter(){
                    ParameterName = "@CdDisciplina",
                    value = item.CdDisciplina
                });

                using (DbDataReader rdr = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                {
                    while(rdr.Read())
                    {
                        qtdAlunos.add(Convert.ToInt32(rdr["QuantidadeAluno"]));
                    }
                }
            }
            */
        }
        
        for(int i =0; i < turmas.Count ; i++){
            turmas[i].QtdAlunos = qtdAlunos[i];
        }

        return turmas;
    }

}