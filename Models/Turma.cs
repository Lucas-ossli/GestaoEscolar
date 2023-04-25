public class Turma
{

    public string? NomeDaTurma { get; set; }
    public string? NomeDaDisciplina { get; set; } //Engenharia
    public int QtdAlunos { get; set; }
    public int CdTurma { get; set; }
    public int CdDisciplina { get; set; }
    public int CdProfessor { get; set; }
    public int CdTurmaProfessor { get; set; }


    public List<Turma> Search(int cdProfessor)
    {
         var turmas = new List<Turma>();
        //select do nome e numero da turma do professor 
        var sql = @"select  
                    tp.idTurmaProfessor,
                    di.nomeDisciplina,
                    tu.Turma
                    from TurmaProfessor  tp 
                    inner join Disciplinas di on di.idDisciplina = tp.DisciplinaId
                    inner join Turma tu on tu.idTurma = tp.TurmaId 
                    where tp.ProfessorId = @CdProfessor 
                    order by nomeDisciplina";
        
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
                                CdTurmaProfessor = Convert.ToInt32(dr["idTurmaProfessor"])
                            });
                        }
                    }
                }
            }
        }
        */

        
        var sql2Count = @"select count(*) as QuantidadeAluno 
                        from Aproveitamentos Ap 
                        where Ap.turmaProfessorId = @CdTurmaProf";
        List<int> qtdAlunos = new List<int>();
        foreach(var item in turmas){
            /*
            using(var cmd EntityCommand(sql2Count, conn))
            {
                cmd.Parameters.Add(new EntityParameter(){
                ParameterName = "@CdTurmaProf",
                value = item.CdTurmaProfessor
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