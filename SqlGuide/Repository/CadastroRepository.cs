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
        var email = pessoa.Nome + (ocorrencia > 0 ? (ocorrencia+1).ToString() : "" ) + "@gestaoescolar.com";
        var senha = pessoa.Cpf + pessoa.DataNascimento.ToString("ddMMyyyy");
        

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
}