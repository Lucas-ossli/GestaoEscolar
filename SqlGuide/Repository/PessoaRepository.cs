using Microsoft.Data.SqlClient;
using Models;
using SqlGuide.Interface;

namespace SqlGuide.Repository;

public class PessoaRepository : IPessoaRepository
{
    public string ConnectionStr{
        get{return ConnectionString.ConnectionStr;}
    }

    public void Insert(Pessoa pessoa)
    {
        string sql;

        if(pessoa.Telefone != null)
        {
            sql = @"insert into 
                    Pessoas (
                            nome, 
                            cpf, 
                            dataNascimento,
                            telefone,
                            cargoId) 
                    values  (
                            @nome,
                            @cpf,
                            @dataNascimento,
                            @telefone, 
                            @CdCargo)";
        }else
        {
           sql = @"insert into 
                    Pessoas (
                            nome, 
                            cpf, 
                            dataNascimento,
                            cargoId) 
                    values  (
                            @nome,
                            @cpf,
                            @dataNascimento,
                            @CdCargo)";
        }
        

        using(var cn = new SqlConnection(ConnectionStr))
        {
            cn.Open();
            using(var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@nome",
                Value = pessoa.Nome});

                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@cpf",
                Value = pessoa.Cpf});

                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@dataNascimento",
                Value = pessoa.DataNascimento});

                if(pessoa.Telefone != null)
                {
                    cmd.Parameters.Add(new SqlParameter(){
                    ParameterName = "@telefone",
                    Value = pessoa.Telefone});
                }
               

                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@CdCargo",
                Value = pessoa.CdCargo});

                cmd.ExecuteNonQuery();
            }
        }

        pessoa.CdPessoa = getIdPessoa(pessoa.Cpf);

    }

    public List<Pessoa> SearchAllProfessores()
    {
        var professores = new List<Pessoa>();

        var sql = @"select 
                        pf.idPessoa, 
                        pf.nome 
                    from Pessoas pf 
                    where cargoId = 2";

        
        using(var cn = new SqlConnection(ConnectionStr))
        {    
            cn.Open();
            using(var cmd = new SqlCommand(sql, cn))
            {
                using (var dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        professores.Add(new Pessoa(){
                            CdPessoa = Convert.ToInt32(dr["idPessoa"]),
                            Nome = dr["nome"].ToString()
                        });
                    }
                }
            }
        }

        return professores;
    }

    
    public List<Aluno> SearchAllAlunos(int cdTurmaProfessor)
    {
       var alunos = new List<Aluno>();

        var sql = @"select 
                        pf.idPessoa, 
                        PF.nome 
                        from Pessoas PF
                    where pf.cargoId = 3
                    and pf.idPessoa not in( select ap.alunoId from Aproveitamentos ap where ap.turmaProfessorId = @cdTurmaProfessor)";

        using(var cn = new SqlConnection(ConnectionStr))
        {    
            cn.Open();
            using(var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@cdTurmaProfessor",
                Value = cdTurmaProfessor});

                using (var dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        alunos.Add(new Aluno(){
                            CdPessoa = Convert.ToInt32(dr["idPessoa"]),
                            Nome = dr["nome"].ToString()
                        });
                    }
                }
            }
        }

        return alunos;
    }

    public int getIdPessoa(string cpf)
    {
        
        var sql = @"select pf.idPessoa from pessoas pf where pf.cpf = @cpf";
        int cdPessoa = 0;
        using(var cn = new SqlConnection(ConnectionStr))
        {    
            cn.Open();
            using(var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@cpf",
                Value = cpf
                });
                using (var dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        cdPessoa = Convert.ToInt32(dr["idPessoa"]);
                    }
                }
            }
        }
        return cdPessoa;
    }

}
