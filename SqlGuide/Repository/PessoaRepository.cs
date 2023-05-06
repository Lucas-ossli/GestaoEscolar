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
        var sql = @"insert into 
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
                                @CdCargo)	
        ";

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

                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@telefone",
                Value = pessoa.Telefone});

                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@CdCargo",
                Value = pessoa.CdCargo});

                cmd.ExecuteNonQuery();
            }
        }

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
                            CdCargo = Convert.ToInt32(dr["idPessoa"]),
                            Nome = dr["nome"].ToString()
                        });
                    }
                }
            }
        }

        return professores;
    }
}
