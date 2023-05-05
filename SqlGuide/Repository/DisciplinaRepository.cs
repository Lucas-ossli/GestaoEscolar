using Microsoft.Data.SqlClient;
using Models;
using SqlGuide.Interface;

namespace SqlGuide.Repository;

public class DisciplinaRepository : IDisciplinaRepository
{
    public string ConnectionStr{
        get{return ConnectionString.ConnectionStr;}
    }

    public void Insert(Disciplina disciplina)
    {
        var sql = @"insert into Disciplinas (nomeDisciplina, cargaHoraria) 
                    values (@nome, @cargaHoraria)";
                    
        using(var cn = new SqlConnection(ConnectionStr))
        {
            cn.Open();
            using(var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@Nome",
                Value = disciplina.NomeDisciplina});

                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@cargaHoraria",
                Value = disciplina.CargaHoraria});
                

                cmd.ExecuteNonQuery();
            }
        }
    }

    public List<Disciplina> SearchAll()
    {
        var disciplinas = new List<Disciplina>();

        var sql = @"select 
                        di.idDisciplina, 
                        di.nomeDisciplina, 
                        di.cargaHoraria 
                    from Disciplinas di";

        using(var cn = new SqlConnection(ConnectionStr))
        {    
            cn.Open();
            using(var cmd = new SqlCommand(sql, cn))
            {
                using (var dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        disciplinas.Add(new Disciplina(){
                            CdDisciplina = Convert.ToInt32(dr["idDisciplina"]),
                            NomeDisciplina = dr["nomeDisciplina"].ToString(),
                            CargaHoraria = Convert.ToInt32(dr["cargaHoraria"])
                        });
                    }
                }
            }
        }
        return disciplinas;
    }
}