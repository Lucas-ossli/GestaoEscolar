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
                                Ativo = Convert.ToInt32(dr["ativo"]) == 1 ? true : false
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

    public List<TurmaProfessor> SearchAll()
    {
        var turmasProfessor = new List<TurmaProfessor>();

        var sql = @"select  PF.nome, 
                            DI.nomeDisciplina, 
                            TU.Turma, 
                            tp.idTurmaProfessor 
                    from TurmaProfessor tp
                            inner join Pessoas PF on tp.ProfessorId = PF.idPessoa
                            inner join Disciplinas DI on tp.DisciplinaId = DI.idDisciplina
                            inner join Turma TU on tp.TurmaId = TU.idTurma
                    order by nome";
                    
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
                                CdTurmaProfessor = Convert.ToInt32(dr["idTurmaProfessor"])
                            });
                        }
                    }
                }
            }
            return turmasProfessor;
        }
    }

    public void Delete(int? cdTurmaProfessor)
    {
        var sql = "delete from TurmaProfessor where idTurmaProfessor = @cdTurmaProfessor";

        using(var cn = new SqlConnection(ConnectionStr))
            {
                cn.Open();
                using(var cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add(new SqlParameter(){
                    ParameterName = "@cdTurmaProfessor",
                    Value = cdTurmaProfessor});

                    cmd.ExecuteNonQuery();
                }
            }
    }
}
