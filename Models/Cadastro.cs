using Microsoft.AspNetCore.Mvc;

namespace Models;
public class Cadastro 
{
   public string Email{get;set;}
   public string Senha{get;set;}
   public static int CdPessoa{get;set;}
   public static int CdCargo{get;set;}
   public static bool Login{get;set;}
}