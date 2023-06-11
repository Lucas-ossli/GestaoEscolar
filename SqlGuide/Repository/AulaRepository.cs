using Microsoft.Data.SqlClient;
using Models;
using SqlGuide.Interface;

namespace SqlGuide.Repository;
public class AulaRepository : IAulaRepository
{
    public string ConnectionStr{
        get{return ConnectionString.ConnectionStr;}
    }

    public List<Aula> Search(int? cdTurmaProfessor)
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

    public void Insert(Aula model)
    {
        var sql = @"insert into aula(TurmaProfessorId,descricao,dataAula) 
                    values(@cdTurmaProfessor,@descricao,@data)";
                    
        using(var cn = new SqlConnection(ConnectionStr))
        {
            cn.Open();
            using(var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@cdTurmaProfessor",
                Value = model.CdTurmaProfessor});

                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@descricao",
                Value = model.Descricao});

                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@data",
                Value = model.Data});
                
                cmd.ExecuteNonQuery();
            }
        }
    }

    public Aula SearchOne(Aula model)
    {
        try
        {
                var sql = @" select 
                                al.idAula 
                            from aula al 
                                where 
                            al.TurmaProfessorId = @cdTurmaProfessor and 
                            al.descricao = @descricao and 
                            al.dataAula = CONVERT(date, @data) ";
            
            var data= model.Data.Year.ToString() +"-" + model.Data.Month.ToString() +"-" + model.Data.Day.ToString();
            using(var cn = new SqlConnection(ConnectionStr))
            {    
                cn.Open();
                using(var cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add(new SqlParameter(){
                    ParameterName = "@CdTurmaProfessor",
                    Value = model.CdTurmaProfessor
                    });

                    cmd.Parameters.Add(new SqlParameter(){
                    ParameterName = "@descricao",
                    Value = model.Descricao
                    });

                    cmd.Parameters.Add(new SqlParameter(){
                    ParameterName = "@data",
                    Value = data
                    });

                    
                    using (var dr = cmd.ExecuteReader())
                    {
                        if(dr.HasRows)
                        {
                            while(dr.Read()){
                                model.CdAula = Convert.ToInt32(dr["idAula"]);
                            }
                        }
                        
                    }
                }
            }
            
            return model;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }

        
    }

    public int GetTurmaProfessor(int cdAula){

        var sql = @"select top 1 al.TurmaProfessorId 
                    from aula al 
                    where idAula = 3";

        int cdTurmaProfessor = 0;

        using(var cn = new SqlConnection(ConnectionStr))
        {    
            cn.Open();
            using(var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@cdAula",
                Value = cdAula
                });

                using (var dr = cmd.ExecuteReader())
                {
                    if(dr.HasRows)
                    {
                        while(dr.Read()){
                            cdTurmaProfessor = Convert.ToInt32(dr["TurmaProfessorId"]);
                        }
                    }
                    
                }
            }
        }

        return cdTurmaProfessor;
    }
}

