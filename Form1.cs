using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void LoadTable()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(Model.connString))
                {
                    connect.Open();

                    string sqlExp = "SELECT [Number polis], [Lastname], [Name], [Address], [Date of birth],[Nazvanie_otdeleniya] FROM [dbo].[Pacient], [dbo].[Otdelenie] WHERE [dbo].[Pacient].[Cod_otdeleniya]=[dbo].[Otdelenie].[Cod_otdeleniya] ";

                    SqlCommand command = new SqlCommand(sqlExp, connect);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add(reader[0], reader[1], reader[2], reader[3], reader[4], reader[5]);
                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            { MessageBox.Show("Ошибка" + ex.Message); }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadTable();
            LoadOtdeleniya();
        }

        private void LoadOtdeleniya()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(Model.connString))
                {
                    connect.Open();

                    string sqlExp = "SELECT [Nazvanie_otdeleniya] FROM [dbo].[Otdelenie] ";

                    SqlCommand command = new SqlCommand(sqlExp, connect);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            comboBox1.Items.Add(reader[0]);

                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            { MessageBox.Show("Ошибка" + ex.Message); }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            try
            {
                using (SqlConnection connect = new SqlConnection(Model.connString))
                {
                    connect.Open();

                   
                    string sqlExp = "SELECT [Number polis], [Lastname], [Name], [Address], [Date of birth],[Nazvanie_otdeleniya] FROM [dbo]" +
                        "WHERE [dbo].[Pacient].[Cod_otdeleniya]=[dbo].[Otdelenie].[Cod_otdeleniya] AND [Nazvanie_otdeleniya]=@NameDep";
                    SqlCommand command = new SqlCommand(sqlExp, connect);


                    SqlParameter NameDep = new SqlParameter("@NameDep", comboBox1.Text);

                    command.Parameters.Add(NameDep);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add(reader[0], reader[1], reader[2], reader[3], reader[4], reader[5]);

                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            { MessageBox.Show("Ошибка" + ex.Message); }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            DialogResult result = form2.ShowDialog();

            if (result == DialogResult.OK)
            { //функция загрузки таблицы из БД
                dataGridView1.Rows.Clear();
                LoadTable();
            }
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            int num = 0;
            int selectedRows = dataGridView1.SelectedRows.Count;
            if (selectedRows == 0)
            {
                MessageBox.Show("Выделите строку для изменения левой кнопкой мыши");
            }
            else if (selectedRows == 1)
            {
                num = dataGridView1.CurrentRow.Index;
            }
            else
            {
                MessageBox.Show("Выделено более одной строки");            
            }

            Form3 form3 = new Form3(Convert.ToInt32(dataGridView1.Rows[num].Cells[0].Value));
            DialogResult result = form3.ShowDialog();

            if (result == DialogResult.OK)
            { 
                dataGridView1.Rows.Clear();
                LoadTable();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int num = 0;
            int selectedRows = dataGridView1.SelectedRows.Count;
            if (selectedRows == 0)
            {
                MessageBox.Show("Выделите строку для изменения левой кнопкой мыши");
            }
            else if (selectedRows == 1)
            {
                num = dataGridView1.CurrentRow.Index;
            }
            else
            {
                MessageBox.Show("Выделено более одной строки");
            }

            int polis = Convert.ToInt32(dataGridView1.Rows[num].Cells[0].Value);
            string str = $"Вы действительно хотите удалить строку c полисом {polis}?";
            DialogResult result = MessageBox.Show(str, "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection connect = new SqlConnection(Model.connString))
                    {
                        connect.Open();

                        //Form2 INSERT
                        string sqlExp = "DELETE FROM [dbo].[Pacient] WHERE [dbo].[Pacient].[Number polis]=@polis";


                        SqlCommand command = new SqlCommand(sqlExp, connect);

                        SqlParameter Polis = new SqlParameter("@polis", polis);                        

                        command.Parameters.Add(Polis);
                       

                        int checker = command.ExecuteNonQuery();

                        if (checker == 1)
                        {
                            MessageBox.Show("Данные удалены");
                        }
                        else
                        {
                            MessageBox.Show("Ошибка удаления данных");
                        }
                    }
                }
                catch (Exception ex)
                { MessageBox.Show("Ошибка" + ex.Message); }
            }
        }
    }
}












