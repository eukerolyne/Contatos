using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace contatosEscola
{
    internal class Contato
    {
        private string idPessoa;
        private string nome;
        private string sobrenome;
        private string email;
        private string celular;
        private Banco banco;

        public Contato()
        {
            this.banco = new Banco();
        }

        public void atualizarContato()
        {
            this.banco.NonQuerry("update pessoa set nome='" + this.nome + "', sobrenome='" + this.sobrenome + "', email='" + this.email + "', celular='" + this.celular + "' where idpessoa=" + this.idPessoa + ";");
        }

        public void cadastrarContato()
        {
            this.banco.NonQuerry("insert into pessoa (nome, sobrenome, email, celular) values ('" + this.nome + "', '" + this.sobrenome + "', '" + this.email + "','" + this.celular + "');");
        }

        public void excluirContato()
        {
            this.banco.NonQuerry("delete from pessoa where idpessoa=" + this.idPessoa + ";");

        }

        public MySqlDataReader listarUsuarios()
        {
            return this.banco.Querry("Select * from pessoa order by idPessoa asc");
        }

        public MySqlDataReader listarUsuarios(String campo, String filtro)
        {
            if (filtro == "")
            {
                return listarUsuarios();
            }
            return this.banco.Querry("Select * from pessoa where " + campo + " = '" + filtro + "' order by idPessoa asc");
        }


        public string IdPessoa { get => idPessoa; set => idPessoa = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Sobrenome { get => sobrenome; set => sobrenome = value; }
        public string Email { get => email; set => email = value; }
        public string Celular { get => celular; set => celular = value; }
    }
}
