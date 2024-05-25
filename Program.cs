using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Projeto_Web_Lh_Pets_versão_1;

namespace projeto_web_pets2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.MapGet("/", () => "Projeto Web - LH Pets versão 1");
            app.UseStaticFiles();
            app.MapGet("/index", (HttpContext contexto) =>
            {
                contexto.Response.Redirect("index.html", false);
            });

            Banco dba = new Banco();

            app.MapGet("/listaClientes", (HttpContext contexto) =>
            {
                contexto.Response.WriteAsync(dba.GetListaString());
            });

            app.Run();
        }
    }
}
