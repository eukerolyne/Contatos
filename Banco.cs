using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace contatosEscola
{
    internal class Banco
    {
        private String host = "localhost";
        private String nomeBanco = "contatos";
        private String usuario = "root";
        private String senha = "dias004756*";
        private String porta = "3306";
        private MySqlConnection con;
        private MySqlCommand cmd;

        public Banco()
        {
            Conectar();
        }
        public void Conectar()
        {
            String stringCon = "SERVER=" + this.host + ";DATABASE=" + this.nomeBanco + ";UID=" + this.usuario + ";PASSWORD=" + this.senha + "; PORT=" + this.porta + ";";
            this.con = new MySqlConnection(stringCon);
            this.cmd = this.con.CreateCommand();
            this.con.Open();
        }
        public void NonQuerry(String sql)
        {
            this.cmd.CommandText = sql;
            this.cmd.ExecuteNonQuery();
        }
        public MySqlDataReader Querry(String sql)
        {
            this.cmd.CommandText = sql;
            return this.cmd.ExecuteReader();
        }
    }
}
