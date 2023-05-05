using Microsoft.Data.SqlClient;
using Models;
using SqlGuide.Interface;

namespace SqlGuide.Repository;

public class TurmaRepository : ITurmaRepository
{
    public string ConnectionStr{
        get{return ConnectionString.ConnectionStr;}
    }

    public void Insert(Turma turma)
    {
        var sql = @"insert into Turma(Turma) values(@Nome)";
        using(var cn = new SqlConnection(ConnectionStr))
        {
            cn.Open();
            using(var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@Nome",
                Value = turma.NomeDaTurma});

                // cmd.Parameters.Add(new SqlParameter(){
                // ParameterName = "@cpf",
                // Value = turma.Descricao});

                cmd.ExecuteNonQuery();
            }
        }
    }

    public List<Turma> SearchAllTurmas()
    {
        var turmas = new List<Turma>();

        var sql = @"select 
                        tu.idTurma, 
                        tu.Turma 
                    from Turma tu";

         using(var cn = new SqlConnection(ConnectionStr))
        {    
            cn.Open();
            using(var cmd = new SqlCommand(sql, cn))
            {
                using (var dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        turmas.Add(new Turma(){
                            CdTurma = Convert.ToInt32(dr["idTurma"]),
                            NomeDaTurma = dr["Turma"].ToString()
                        });
                    }
                }
            }
        }
        return turmas;
    }
}