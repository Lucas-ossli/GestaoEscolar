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

    public void Insert(int? cdAluno, int? cdTurmaProfessor, int cdNota)
    {
        var sql = @"insert into Aproveitamentos (alunoId, turmaProfessorId, notaId, Ativo) 
                                          values(@cdAluno, @cdTurmaProfessor, @cdNota, 1)";

        
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

                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@cdNota",
                Value = cdNota});
                
                cmd.ExecuteNonQuery();
            }
        }                                  
    }

    public List<Aproveitamento> AlunoInfo(int? cdAluno)
    {

        List<Aproveitamento> aproveitamento = new List<Aproveitamento>();

        var sql = @"select 
                        PF.idPessoa, 
                        PF.nome, 
                        PFP.nome as Professor, 
                        TU.Turma, 
                        DI.nomeDisciplina, 
                        AP.notaId,
						AP.turmaProfessorId
                    from Aproveitamentos AP
                        inner join Pessoas PF
                        on PF.idPessoa = AP.alunoId

                        inner join TurmaProfessor TP
                        on AP.turmaProfessorId = TP.idTurmaProfessor

                        inner join Pessoas PFP
                        on PFP.idPessoa = TP.ProfessorId --professor

                        inner join Turma TU
                        on TU.idTurma = TP.TurmaId

                        inner join Disciplinas DI
                        on DI.idDisciplina = TP.DisciplinaId

        where AP.alunoId = @cdAluno";


        using(var cn = new SqlConnection(ConnectionStr))
        {    
            cn.Open();
            using(var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@cdAluno",
                Value = cdAluno
                });

                using (var dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        aproveitamento.Add(new Aproveitamento(){
                            CdAluno = Convert.ToInt32(dr["idPessoa"]),
                            CdNota = Convert.ToInt32(dr["notaId"]),
                            Aluno = dr["nome"].ToString(),
                            Professor = dr["Professor"].ToString(),
                            Turma = dr["Turma"].ToString(),
                            Disciplina = dr["nomeDisciplina"].ToString(),
                            cdTurmaProfessor = Convert.ToInt32(dr["turmaProfessorId"])
                        });
                    }
                }
            }
        }

        return aproveitamento;
    }
}
