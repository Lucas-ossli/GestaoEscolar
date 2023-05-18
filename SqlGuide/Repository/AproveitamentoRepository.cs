using Microsoft.Data.SqlClient;
using Models;
using SqlGuide.Interface;

namespace SqlGuide.Repository;
public class AproveitamentoRepository : IAproveitamentoRepository
{
    public string ConnectionStr{
        get{return ConnectionString.ConnectionStr;}
    }

    public List<Aluno> SearchAlunos(int? cdTurmaProfessor)
    {
        var Alunos = new List<Aluno>();

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
                        Alunos.Add(new Aluno(){
                            Nome = dr["nome"].ToString(),
                            CdPessoa = Convert.ToInt32(dr["idPessoa"])
                        });
                    }
                }
            }
            
            return Alunos;
        }
    }

    public void Insert(int? cdAluno, int? cdTurmaProfessor)
    {
        var sql = @"insert into Aproveitamentos (alunoId, turmaProfessorId, Ativo) 
                                          values(@cdAluno, @cdTurmaProfessor, 1)";


        using(var cn = new SqlConnection(ConnectionStr))
        {
            cn.Open();
            using(var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@cdAluno",
                Value = cdAluno});

                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@cdTurmaProfessor",
                Value = cdTurmaProfessor});
                
                cmd.ExecuteNonQuery();
            }
        }                                  
    }
}
