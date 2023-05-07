using Microsoft.Data.SqlClient;
using Models;
using SqlGuide.Interface;

namespace SqlGuide.Repository;
public class AproveitamentoRepository : IAproveitamentoRepository
{
    public string ConnectionStr{
        get{return ConnectionString.ConnectionStr;}
    }
    public List<Pessoa> SearchAlunos(int cdTurmaProfessor)
    {
        var Alunos = new List<Pessoa>();

        var sql = @"select 
                        pf.nome,
                        pf.idPessoa
                    from Aproveitamentos ap 
                        inner join Pessoas pf on ap.alunoId = pf.idPessoa
                    where ap.turmaProfessorId = @cdTurmaProfessor";

        
        using(var cn = new SqlConnection(ConnectionStr))
        {    
            cn.Open();
            using(var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@cdTurmaProfessor",
                Value = cdTurmaProfessor
                });
                using (var dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        Alunos.Add(new Pessoa(){
                            Nome = dr["nome"].ToString(),
                            CdPessoa = Convert.ToInt32(dr["idPessoa"])
                        });
                    }
                }
            }

            return Alunos;
        }
    }
}
