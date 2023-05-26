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
            idChamada,
            Pf.nome,
            Pf.idPessoa, 
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
                            CdAluno = Convert.ToInt32(dr["idPessoa"]),
                            CdChamada = Convert.ToInt32(dr["idChamada"]),
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
        
        if(chamada.Count > 0)
        {
            chamada.First().CdAula = cdAula;
        }
        else
        {
            chamada.Add(new Chamada(){
                CdAula = cdAula
            });
        }

        return chamada;
        
    }
    
    public void Update(List<Chamada> model)
    {
        var sql = @"
                update chamada 
                set presenca1 = @presenca1 , 
                    presenca2 = @presenca2,
                    presenca3 = @presenca3, 
                    presenca4 = @presenca4
                where idChamada = @cdChamada 
                ";

        foreach (var item in model)
        {
            
            using(var cn = new SqlConnection(ConnectionStr))
            {
                cn.Open();
                using(var cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add(new SqlParameter(){
                    ParameterName = "@presenca1",
                    Value = item.Presenca1});

                    cmd.Parameters.Add(new SqlParameter(){
                    ParameterName = "@presenca2",
                    Value = item.Presenca2});

                    cmd.Parameters.Add(new SqlParameter(){
                    ParameterName = "@presenca3",
                    Value = item.Presenca3});

                    cmd.Parameters.Add(new SqlParameter(){
                    ParameterName = "@presenca4",
                    Value = item.Presenca4});

                    cmd.Parameters.Add(new SqlParameter(){
                    ParameterName = "@cdChamada",
                    Value = item.CdChamada});
                    
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

    public void Insert(List<Aluno> alunos, int? cdAula)
    {
        var sql = @"insert into chamada(aulaId, alunoId)values(@cdAula,@cdAluno)";

        foreach (var item in alunos)
        {
            using(var cn = new SqlConnection(ConnectionStr))
            {
                cn.Open();
                using(var cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add(new SqlParameter(){
                    ParameterName = "@cdAluno",
                    Value = item.CdPessoa});

                    cmd.Parameters.Add(new SqlParameter(){
                    ParameterName = "@cdAula",
                    Value = cdAula});
                    
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
