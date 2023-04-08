using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(Model.connString))
                {
                    connect.Open();

                    //Form2 INSERT
                    string sqlExp = "INSERT INTO [dbo].[Pacient] ([Number polis], [Lastname], [Name], [Address], [Date of birth], [Nazvanie_otdeleniya])" +
                        "VALUES (@polis, @surname, @name, @address, @birthday, @otdelenie)";


                    SqlCommand command = new SqlCommand(sqlExp, connect);

                    SqlParameter Polis = new SqlParameter("@polis", textBoxPolis.Text);
                    SqlParameter Surname = new SqlParameter("@surname", textBoxSurname.Text);
                    SqlParameter Name = new SqlParameter("@name", textBoxName.Text);
                    SqlParameter Address = new SqlParameter("@address", textBoxAddress.Text);
                    SqlParameter Birthday = new SqlParameter("@birthday", dateTimePickerBirthday.Value);
                    SqlParameter Otdelenie = new SqlParameter("@otdel", textBoxOtdelenie);

                    command.Parameters.Add(Polis);
                    command.Parameters.Add(Surname);
                    command.Parameters.Add(Name);
                    command.Parameters.Add(Address);
                    command.Parameters.Add(Birthday);
                    command.Parameters.Add(Otdelenie);

                    int checker = command.ExecuteNonQuery();

                    if (checker == 1)
                    {
                        MessageBox.Show("Данные добавлены");
                    }
                    else
                    {
                        MessageBox.Show("Ошибка записи");
                    }
                }
            }
            catch (Exception ex)
            { MessageBox.Show("Ошибка" + ex.Message); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
