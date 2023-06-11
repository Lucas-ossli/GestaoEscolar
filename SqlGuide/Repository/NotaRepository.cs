using GestaoEscolar.Models;
using Microsoft.Data.SqlClient;
using Models;
using SqlGuide.Interface;

namespace SqlGuide.Repository;

public class NotaRepository : INotaRepository
{
     public string ConnectionStr{
        get{return ConnectionString.ConnectionStr;}
    }

    public int Insert(int? cdAluno, int? cdTurmaProfessor)
    {
       var sql = @" insert into nota(TurmaProfessorId, alunoId) 
                              values(@cdTurmaProfessor, @cdAluno)";

        using(var cn = new SqlConnection(ConnectionStr))
        {
            cn.Open();
            using(var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@cdTurmaProfessor",
                Value = cdTurmaProfessor});

                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@cdAluno",
                Value = cdAluno});

                cmd.ExecuteNonQuery();
            }
        }


        sql = @"select 
                    idNota 
                from nota 
                    where TurmaProfessorId = @cdTurmaProfessor and alunoId = @cdAluno";
        int cdNota = 0;
        using(var cn = new SqlConnection(ConnectionStr))
        {    
            cn.Open();
            using(var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@cdTurmaProfessor",
                Value = cdTurmaProfessor
                });

                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@cdAluno",
                Value = cdAluno
                });
                using (var dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        cdNota = Convert.ToInt32(dr["idNota"]);
                    }
                }
            }
        }

        return cdNota;
    }

    public List<Nota> Search(int? cdTurmaProfessor)
    {
        var alunos = new List<Nota>();
        var sql = @"select 
                        pf.nome, 
                        nt.alunoId, 
                        nt.nota1, 
                        nt.nota2, 
                        nt.nota3,
                        nt.nota4 
                        
                    from Nota nt 

                        inner join Pessoas pf
                        on pf.idPessoa = nt.alunoId
                        
                    where nt.TurmaProfessorId = @cdturmaProfessor
                    
                    order by nome";

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
                        Nota aluno = new Nota(){
                            Aluno = dr["nome"].ToString(),
                            CdAluno = Convert.ToInt32(dr["alunoId"]),
                            CdTurmaProfessor = cdTurmaProfessor,
                            Nota1 =  float.Parse(dr["nota1"].ToString()),
                            Nota2 =  float.Parse(dr["nota2"].ToString()),
                            Nota3 =  float.Parse(dr["nota3"].ToString()),
                            Nota4 =  float.Parse(dr["nota4"].ToString()),
                        };

                        alunos.Add(aluno);
                    }
                }
            }
        }

        return alunos;
    }

    public Nota SearchOne(int? cdAluno, int? cdTurmaProfessor)
    {
        var sql = @"select 
                        pf.nome,
                        nt.idNota,
                        nt.nota1,
                        nt.nota2,
                        nt.nota3,
                        nt.nota4
                    from nota nt 

                    inner join Pessoas pf on pf.idPessoa = nt.alunoId

                        where nt.alunoId = @cdAluno 
                    and nt.TurmaProfessorId = @cdTurmaProfessor";
        
        Nota aluno = new Nota();

        using(var cn = new SqlConnection(ConnectionStr))
        {    
            cn.Open();
            using(var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@cdTurmaProfessor",
                Value = cdTurmaProfessor
                });

                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@cdAluno",
                Value = cdAluno
                });
                using (var dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        
                        aluno.Aluno = dr["nome"].ToString();
                        aluno.CdNota = Convert.ToInt32(dr["idNota"]);
                        aluno.CdTurmaProfessor = cdTurmaProfessor;
                        aluno.Nota1 =  Convert.ToInt32(dr["nota1"]);
                        aluno.Nota2 =  Convert.ToInt32(dr["nota2"]);
                        aluno.Nota3 =  Convert.ToInt32(dr["nota3"]);
                        aluno.Nota4 =  Convert.ToInt32(dr["nota4"]);
                    }
                }
            }
        }

        return aluno;

    }

    public void Update(Nota aluno)
    {
        var sql = @"update 
                    Nota 
                    set 
                        nota1 = @nota1,
                        nota2 = @nota2,
                        nota3 = @nota3,
                        nota4 = @nota4
                    where idNota = @cdNota";

        using(var cn = new SqlConnection(ConnectionStr))
        {
            cn.Open();
            using(var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@cdNota",
                Value = aluno.CdNota});

                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@nota1",
                Value = aluno.Nota1});

                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@nota2",
                Value = aluno.Nota2});

                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@nota3",
                Value = aluno.Nota3});

                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@nota4",
                Value = aluno.Nota4});

                cmd.ExecuteNonQuery();
            }
        }
    }

    public Nota SearchForAluno(int? cdNota)
    {
        Nota aluno = new Nota();

        var sql = @"select 
                        pf.nome,
                        nt.nota1,
                        nt.nota2,
                        nt.nota3,
                        nt.nota4 
                    from nota nt 
                        inner join Pessoas pf
                        on pf.idPessoa = nt.alunoId
                    where nt.idNota = @cdNota";
        
         using(var cn = new SqlConnection(ConnectionStr))
        {    
            cn.Open();
            using(var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@cdNota",
                Value = cdNota
                });

                using (var dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        aluno.Aluno = dr["nome"].ToString();
                        aluno.Nota1 =  Convert.ToInt32(dr["nota1"]);
                        aluno.Nota2 =  Convert.ToInt32(dr["nota2"]);
                        aluno.Nota3 =  Convert.ToInt32(dr["nota3"]);
                        aluno.Nota4 =  Convert.ToInt32(dr["nota4"]);
                    }
                }
            }
        }

        return aluno;

    }
}