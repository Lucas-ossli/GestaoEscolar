using Microsoft.Data.SqlClient;
using Models;
using SqlGuide.Interface;

namespace SqlGuide.Repository;
public class ChamadaRepository : IChamadaRepository
{
    
    public string ConnectionStr{get{return ConnectionString.ConnectionStr ;}}

    public List<Chamada> Search(int cdAula)
    {
        var chamada = new List<Chamada>();
        string sql = @"
        select 
            Pf.nome, 
            Al.dataAula, 
            Ch.presenca1, 
            Ch.presenca2, 
            Ch.presenca3, 
            Ch.presenca4 
        from chamada Ch
            inner join Pessoas Pf on Pf.idPessoa = Ch.alunoId
            inner join aula Al on Al.idAula = @CdAula
        where Ch.aulaId = @CdAula
        order by Pf.nome";

        using(var cn = new SqlConnection(ConnectionStr))
        {    
            cn.Open();
            using(var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@CdAula",
                Value = cdAula
                });
                using (var dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        chamada.Add(new Chamada(){
                            Aluno = dr["nome"].ToString(),
                            Data = Convert.ToDateTime(dr["dataAula"]),
                            Presenca1 = Convert.ToInt32(dr["presenca1"]) == 1 ? true : false,
                            Presenca2 = Convert.ToInt32(dr["Presenca2"]) == 1 ? true : false,
                            Presenca3 = Convert.ToInt32(dr["Presenca3"]) == 1 ? true : false,
                            Presenca4 = Convert.ToInt32(dr["Presenca4"]) == 1 ? true : false,
                        });
                    }
                }
            }
        }
        
        return chamada;
        
    }
}
