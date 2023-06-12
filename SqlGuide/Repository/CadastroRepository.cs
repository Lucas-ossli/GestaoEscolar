using Microsoft.Data.SqlClient;
using Models;
using SqlGuide.Interface;

namespace SqlGuide.Repository;

public class CadastroRepository : ICadastroRepository
{
    public string ConnectionStr{
        get{return ConnectionString.ConnectionStr;}
    }
    public void create(Pessoa pessoa)
    {
        var sql = @"insert into Cadastros (email, senha, pessoaId) 
                                    values(@email, @senha, @cdPessoa)";

        var ocorrencia = checkDoubleName(pessoa.Nome);
        var email = (pessoa.Nome.Replace(" ","") + (ocorrencia > 1 ? ocorrencia.ToString() : "" ) + "@gestaoescolar.com").ToLower();
        var senha = pessoa.DataNascimento.ToString("ddMMyyyy");
        

        using(var cn = new SqlConnection(ConnectionStr))
        {
            cn.Open();
            using(var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@email",
                Value = email});

                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@senha",
                Value = senha});

                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@cdPessoa",
                Value = pessoa.CdPessoa});

                cmd.ExecuteNonQuery();
            }
        }
    }

    public int checkDoubleName(string nome)
    {
        var sql = @"
                    select count(*) as ocorrencia from Pessoas pf where pf.nome = @nome";

        int ocorrencia = 0;
        using(var cn = new SqlConnection(ConnectionStr))
        {    
            cn.Open();
            using(var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@nome",
                Value = nome
                });
                using (var dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        ocorrencia = Convert.ToInt32(dr["ocorrencia"]);
                    }
                }
            }
        }

        return ocorrencia;
    }

    public Cadastro VerifyLogin(Cadastro cadastro)
    {
        Cadastro.Login = false;

        string sql = @"select 
                            pf.idPessoa, 
                            pf.cargoId 
                        from Cadastros ca

                            inner join Pessoas pf
                            on pf.idPessoa = ca.pessoaId

                        where 
                        ca.email = @email
                        and ca.senha = @senha";
        
        using(var cn = new SqlConnection(ConnectionStr))
        {    
            cn.Open();
            using(var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@email",
                Value = cadastro.Email
                });

                cmd.Parameters.Add(new SqlParameter(){
                ParameterName = "@senha",
                Value = cadastro.Senha
                });


                using (var dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        Cadastro.CdPessoa = Convert.ToInt32(dr["idPessoa"]);
                        Cadastro.CdCargo = Convert.ToInt32(dr["cargoId"]);
                        Cadastro.Login = true;
                    }
                }
            }
        }

        return cadastro;
    }
}