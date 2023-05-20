using Microsoft.Data.SqlClient;
using Models;
using SqlGuide.Interface;

namespace SqlGuide.Repository;

public class NotaRepository : INotaRepository
{
     public string ConnectionStr{
        get{return ConnectionString.ConnectionStr;}
    }

    public int Insert(int? cdAluno, int? cdTurmaProfessor)
    {
       var sql = @" insert into nota(TurmaProfessorId, alunoId) 
                              values(@cdTurmaProfessor, @cdAluno)";

        using(var cn = new SqlConnection(ConnectionStr))
        {
            cn.Open();
            using(var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@cdTurmaProfessor",
                Value = cdTurmaProfessor});

                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@cdAluno",
                Value = cdAluno});

                cmd.ExecuteNonQuery();
            }
        }


        sql = @"select 
                    idNota 
                from nota 
                    where TurmaProfessorId = @cdTurmaProfessor and alunoId = @cdAluno";
        int cdNota = 0;
         using(var cn = new SqlConnection(ConnectionStr))
        {    
            cn.Open();
            using(var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@cdTurmaProfessor",
                Value = cdTurmaProfessor
                });

                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@cdAluno",
                Value = cdAluno
                });
                using (var dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        cdNota = Convert.ToInt32(dr["idNota"]);
                    }
                }
            }
        }

        return cdNota;
    }
}