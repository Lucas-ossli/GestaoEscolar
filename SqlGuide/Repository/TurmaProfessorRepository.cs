using Microsoft.Data.SqlClient;
using Models;
using SqlGuide.Interface;

namespace SqlGuide.Repository;
public class TurmaProfessorRepository : ITurmaProfessorRepository
{
    public string ConnectionStr{
        get{return ConnectionString.ConnectionStr;}
    }

    public void Insert(TurmaProfessor2 model)
    {
        var sql = @"insert into TurmaProfessor(ano, TurmaId, ProfessorId, DisciplinaId, ativo) 
                                        values(@ano, @cdTurma, @cdProfessor, @cdDisciplina, 1)";

        try
        {
            using(var cn = new SqlConnection(ConnectionStr))
            {
                cn.Open();
                using(var cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add(new SqlParameter(){
                        ParameterName = "@ano",
                        Value = model.Ano
                    });

                    cmd.Parameters.Add(new SqlParameter(){
                    ParameterName = "@cdTurma",
                    Value = model.CdTurma});

                    cmd.Parameters.Add(new SqlParameter(){
                    ParameterName = "@cdProfessor",
                    Value = model.CdProfessor});

                    cmd.Parameters.Add(new SqlParameter(){
                    ParameterName = "@cdDisciplina",
                    Value = model.CdDisciplina});

                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (System.Exception ex)
        {   
            throw new Exception(ex.Message);
        }
        
    }

    public List<TurmaProfessor> Search(int cdProfessor, bool ativo)
    {
        var turmas = new List<TurmaProfessor>();
        //select do nome e numero da turma do professor 
        var sql = @"select  
                        tp.idTurmaProfessor,
                        tp.ativo,
                        di.nomeDisciplina,
                        year(tp.ano) as ano,
                        tu.Turma
                    from TurmaProfessor  tp 
                        inner join Disciplinas di on di.idDisciplina = tp.DisciplinaId
                        inner join Turma tu on tu.idTurma = tp.TurmaId 
                    where tp.ProfessorId = @CdProfessor ";

        if(ativo){
            sql += "and tp.ativo = 1 ";
        }

        sql += "order by nomeDisciplina";
    
    
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
                                CdTurmaProfessor = Convert.ToInt32(dr["idTurmaProfessor"]),
                                Ativo = Convert.ToInt32(dr["ativo"]) == 1 ? true : false,
                                Ano = DateTime.ParseExact(dr["ano"].ToString()+"0101", "yyyyMMdd", null) 
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

    public List<TurmaProfessor> SearchAll(bool ativo)
    {
        var turmasProfessor = new List<TurmaProfessor>();

        var sql = @"select  PF.nome, 
                            DI.nomeDisciplina, 
                            TU.Turma, 
                            tp.idTurmaProfessor,
                            year(tp.ano) as ano,
                            tp.ativo 
                    from TurmaProfessor tp
                            inner join Pessoas PF on tp.ProfessorId = PF.idPessoa
                            inner join Disciplinas DI on tp.DisciplinaId = DI.idDisciplina
                            inner join Turma TU on tp.TurmaId = TU.idTurma ";

        if(ativo){
            sql+= "where tp.ativo = 1 ";
        }

        sql +="order by nome ";

        using(var cn = new SqlConnection(ConnectionStr))
        {

            cn.Open();
            using(var cmd = new SqlCommand(sql, cn))
            {

                using(var dr = cmd.ExecuteReader())
                {
                    if(dr.HasRows)
                    {
                        while(dr.Read())
                        {
                            turmasProfessor.Add(new TurmaProfessor(){
                                NomeProfessor = dr["nome"].ToString(),
                                NomeDaDisciplina = dr["nomeDisciplina"].ToString(),
                                NomeDaTurma = dr["Turma"].ToString(),
                                CdTurmaProfessor = Convert.ToInt32(dr["idTurmaProfessor"]),
                                Ativo = Convert.ToInt32(dr["ativo"]) == 1 ? true : false,
                                Ano = DateTime.ParseExact(dr["ano"].ToString()+"0101", "yyyyMMdd", null)
                            });
                        }
                    }
                }
            }
            return turmasProfessor;
        }
    }

   public void InativarTP(int? cdTurmaProfessor)
    {
        // TODO -  CRIAR UMA PROCEDURE 
        var sql = @"update TurmaProfessor set ativo = 0 where idTurmaProfessor = @cdTurmaProfessor 
                     
                    update Aproveitamentos set Ativo = 0 where turmaProfessorId =  @cdTurmaProfessor";

        using(var cn = new SqlConnection(ConnectionStr))
        {
            cn.Open();
            using(var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@cdTurmaProfessor",
                Value = cdTurmaProfessor
                });

                cmd.ExecuteNonQuery();
            }
        }
    }

    public void AtivarTP(int? cdTurmaProfessor)
    {
        /* TODO -  CRIAR UMA PROCEDURE 
        var sql = @"update TurmaProfessor set ativo = 1 where idTurmaProfessor = @cdTurmaProfessor  
                     
                    update Aproveitamentos set Ativo = 1 where turmaProfessorId =  @cdTurmaProfessor
                    "
        */
        var sql = @"update TurmaProfessor set ativo = 1 where idTurmaProfessor = @cdTurmaProfessor 
                    
                    update Aproveitamentos set Ativo = 1 where turmaProfessorId =  @cdTurmaProfessor";

        using(var cn = new SqlConnection(ConnectionStr))
        {
            cn.Open();
            using(var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@cdTurmaProfessor",
                Value = cdTurmaProfessor
                });

                cmd.ExecuteNonQuery();
            }
        }
    }

    public bool HasTurma(TurmaProfessor2 TurmaProfessor)
    {
        var sql = @"select * from TurmaProfessor tp 
                        where 
                        tp.ano = @ano 
                        and tp.TurmaId = @cdTurma
                        and tp.ProfessorId = @cdProfessor
                        and tp.DisciplinaId = @cdDisciplina";
        
        using(var cn = new SqlConnection(ConnectionStr))
        {
            cn.Open();
            using(var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@ano",
                Value = TurmaProfessor.Ano});

                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@cdTurma",
                Value = TurmaProfessor.CdTurma});

                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@cdProfessor",
                Value = TurmaProfessor.CdProfessor});

                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@cdDisciplina",
                Value = TurmaProfessor.CdDisciplina});

                using(var dr = cmd.ExecuteReader())
                {
                    if(dr.HasRows)
                    {
                        return true;
                    }
                }
            }
            
        }

        return false;
    }
}
