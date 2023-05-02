using Microsoft.Data.SqlClient;
using SqlGuide.Interface;

namespace SqlGuide.Repository
{

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
    }
}