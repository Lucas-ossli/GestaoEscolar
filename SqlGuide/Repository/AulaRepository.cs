
using Microsoft.Data.SqlClient;
using Models;
using SqlGuide.Interface;

namespace SqlGuide.Repository
{
    public class AulaRepository : IAulaRepository
    {
        public string ConnectionStr{
            get{return ConnectionString.ConnectionStr;}
        }
        
        public List<Aula> Search(int cdTurmaProfessor)
        {
            List<Aula> aulas = new List<Aula>();
            var sql = @"
            select 
                au.dataAula, 
                au.descricao, 
                au.idAula 
            from aula au 
            where au.TurmaProfessorId = @CdTurmaProfessor";

            using(var cn = new SqlConnection(ConnectionStr))
            {    
                cn.Open();
                using(var cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add(new SqlParameter(){
                    ParameterName = "@CdTurmaProfessor",
                    Value = cdTurmaProfessor
                    });
                    using (var dr = cmd.ExecuteReader())
                    {
                        while(dr.Read())
                        {
                            aulas.Add(new Aula(){
                                Data = Convert.ToDateTime(dr["dataAula"]),
                                Descricao = dr["descricao"].ToString(),
                                CdAula = Convert.ToInt32(dr["idAula"])
                            });
                        }
                    }
                }
            }

            return aulas;
        }
    }
}