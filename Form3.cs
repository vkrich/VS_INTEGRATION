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
    public partial class Form3 : Form
    {
        int num;
        public Form3(int num)
        {
            InitializeComponent();
            this.num = num;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(Model.connString))
                {
                    connect.Open();


                    string sqlExp = "SELECT [Number polis], [Lastname], [Name], [Address], [Date of birth],[Nazvanie_otdeleniya] FROM [dbo].[Pacient]" +
                        "WHERE [dbo].[Pacient].[Number polis]=@PolisNum";
                    SqlCommand command = new SqlCommand(sqlExp, connect);


                    SqlParameter PolisNum = new SqlParameter("@PolisNum", this.num);

                    command.Parameters.Add(PolisNum);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            //dataGridView1.Rows.Add(reader[0], reader[1], reader[2], reader[3], reader[4], reader[5]);
                            textBoxPolis.Text = reader[0].ToString();
                            textBoxSurname.Text = reader[1].ToString();
                            textBoxName.Text = reader[2].ToString();
                            textBoxAddress.Text = reader[3].ToString();
                            dateTimePickerBirthday.Text = reader[4].ToString();
                            textBoxOtdelenie.Text = reader[5].ToString();


                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            { MessageBox.Show("Ошибка" + ex.Message); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(Model.connString))
                {
                    connect.Open();

                    //Form2 INSERT
                    string sqlExp = "UPDATE [dbo].[Pacient] SET [Number polis]=@polis, [Lastname]=@surname, [Name]=@name, " +
                        "[Address]=@address, [Date of birth]=@birthday, [Nazvanie_otdeleniya]=@otdelenie";


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
                        MessageBox.Show("Данные изменены");
                    }
                    else
                    {
                        MessageBox.Show("Ошибка обновления данных");
                    }
                }
            }
            catch (Exception ex)
            { MessageBox.Show("Ошибка" + ex.Message); }
        }
    }
}
