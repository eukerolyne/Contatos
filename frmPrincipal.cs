using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace contatosEscola
{
    public partial class frmPrincipal : Form
    {
        
        private Contato contato;
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            this.contato = new Contato();
            MySqlDataReader temp = this.contato.listarUsuarios();
            DataTable dt = new DataTable();
            dt.Load(temp);
            dtGridContato.DataSource = dt;
        }

        //Pesquisa(filtro) algo especifico
        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            String filtro = txtFiltro.Text;
            String campo = cbFiltro.Text;
            if (campo == "id")
            {
                campo = "idPessoa";
            }
            txtFiltro.Clear(); //limpar o textbox
            cbFiltro.SelectedIndex = -1; //limpar a combobox

            MySqlDataReader temp = this.contato.listarUsuarios(campo, filtro);
            DataTable dt = new DataTable();
            dt.Load(temp);
            dtGridContato.DataSource = dt;
        }

        //Quando clica duas vezes na linha na tabela
        private void dtGridContato_DoubleClick(object sender, EventArgs e)
        {
            ABRIR();
        }

        private void dtGridContato_SelectionChanged(object sender, EventArgs e)
        {
        }

        //metódo para selecionar e exibir linha selecionada
        void ABRIR()
        {
            int linhaAtual = 0;
            linhaAtual = dtGridContato.CurrentRow.Index;
            try
            {
                txtId.Text = dtGridContato[0, linhaAtual].Value.ToString(); // pega o id
                txtNome.Text = dtGridContato[1, linhaAtual].Value.ToString(); // pega o nome
                txtSobrenome.Text = dtGridContato[2, linhaAtual].Value.ToString(); //pega o sobrenome
                txtEmail.Text = dtGridContato[3, linhaAtual].Value.ToString(); //pega o email
                txtCelular.Text = dtGridContato[4, linhaAtual].Value.ToString(); //pega o celular
            }
            catch
            {
            }
        }

        //Atualizar dados da grade
        private void AtualizarDtGrid()
        {
            MySqlDataReader temp = this.contato.listarUsuarios();
            DataTable dt = new DataTable();
            dt.Load(temp);
            dtGridContato.DataSource = dt;
        }

        //Excluir contato
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if(txtId.Text != "")
            {
                this.contato.IdPessoa = txtId.Text;
                this.contato.excluirContato();
                AtualizarDtGrid();
            }
            else
            {
                MessageBox.Show("Clique duas vezes no contato que deseja excluir!");
            }

        }

        //Cria novo contato e adiciona-lo ao banco de dados
        private void btnNovoContato_Click(object sender, EventArgs e)
        {
            if (txtNome.Text != "" && txtSobrenome.Text != "" && txtEmail.Text != "" && txtCelular.Text != "")
            {
                this.contato.Nome = txtNome.Text;
                this.contato.Sobrenome = txtSobrenome.Text;
                this.contato.Email = txtEmail.Text;
                this.contato.Celular = txtCelular.Text;
                this.contato.cadastrarContato();
                AtualizarDtGrid();
                btnLimpar_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Preencha tudo para cadastrar!");
            }
        }

        //Pega contato selecionado e faz a edição dos dados
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text != "" && txtSobrenome.Text != "" && txtEmail.Text != "" && txtCelular.Text != "")
            {
                this.contato.IdPessoa = txtId.Text;
                this.contato.Nome = txtNome.Text;
                this.contato.Sobrenome = txtSobrenome.Text;
                this.contato.Email = txtEmail.Text;
                this.contato.Celular = txtCelular.Text;
                this.contato.atualizarContato();
                AtualizarDtGrid();
                btnLimpar_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Preencha tudo para alterar!");
            }
        }

        //Mostra tudo depois da pesquisa
        private void btnMostrar_Click(object sender, EventArgs e)
        {
            AtualizarDtGrid();
            txtFiltro.Clear();
        }

        //Limpa os campos preenchidos
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtId.Clear();
            txtNome.Clear();
            txtSobrenome.Clear();
            txtEmail.Clear();
            txtCelular.Clear();

        }
    }
}
