using Microsoft.Data.SqlClient;
using Models;
using SqlGuide.Interface;

namespace SqlGuide.Repository;
public class TurmaProfessorRepository : ITurmaProfessorRepository
{
    public string ConnectionStr{
        get{return ConnectionString.ConnectionStr;}
    }
    public List<TurmaProfessor> Search(int cdProfessor)
    {
        var turmas = new List<TurmaProfessor>();
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
    
    
        using(var cn = new SqlConnection(ConnectionStr))
        {
            cn.Open();
            using(var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@CdProfessor",
                Value = cdProfessor
                });
                using(var dr = cmd.ExecuteReader())
                {
                    if(dr.HasRows)
                    {
                        while(dr.Read())
                        {
                            turmas.Add(new TurmaProfessor(){
                                NomeDaDisciplina = dr["nomeDisciplina"].ToString(),
                                NomeDaTurma = dr["Turma"].ToString(),
                                CdTurmaProfessor = Convert.ToInt32(dr["idTurmaProfessor"])
                            });
                        }
                    }
                }
            }
        }
    

        sql = @"select count(*) as QuantidadeAluno 
                    from Aproveitamentos Ap 
                    where Ap.turmaProfessorId = @CdTurmaProf";

        List<int> qtdAlunos = new List<int>();
        foreach(var item in turmas){
            using(var cn = new SqlConnection(ConnectionStr))
            {    
                cn.Open();
                using(var cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add(new SqlParameter(){
                    ParameterName = "@CdTurmaProf",
                    Value = item.CdTurmaProfessor
                    });
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while(rdr.Read())
                        {
                            qtdAlunos.Add(Convert.ToInt32(rdr["QuantidadeAluno"]));
                        }
                    }
                }
            }
        }
    
        for(int i =0; i < turmas.Count ; i++){
            turmas[i].QtdAlunos = qtdAlunos[i];
        }

        return turmas;
    }
}
