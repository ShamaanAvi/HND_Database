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

namespace FinalProject
{
    public partial class fLocationManagement : Form
    {
        public fLocationManagement()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            // Retrieve values from text boxes
            int locationID;
            if (!int.TryParse(txtUpID.Text, out locationID))
            {
                MessageBox.Show("Please enter a valid Location ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string locationName = txtUpName.Text;
            string country = txtUpCountry.Text;

            // Update the record in the location table
            string connectionString = "Data Source = DESKTOP-DAME84Q; Initial Catalog = database; Persist Security Info = True; User ID = sa; Password = shaamil@";
            string updateQuery = "UPDATE location SET locationName = @LocationName, country = @Country WHERE locationID = @LocationID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(updateQuery, connection);
                    command.Parameters.AddWithValue("@LocationName", locationName);
                    command.Parameters.AddWithValue("@Country", country);
                    command.Parameters.AddWithValue("@LocationID", locationID);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Location updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to update location. Location ID not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            // Retrieve the locationID from the textbox
            int locationID;
            if (!int.TryParse(txtDelID.Text, out locationID))
            {
                MessageBox.Show("Please enter a valid Location ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Delete the record from the location table
            string connectionString = "Data Source = DESKTOP-DAME84Q; Initial Catalog = database; Persist Security Info = True; User ID = sa; Password = shaamil@";
            string deleteQuery = "DELETE FROM location WHERE locationID = @LocationID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(deleteQuery, connection);
                    command.Parameters.AddWithValue("@LocationID", locationID);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Location deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No location found with the specified Location ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Retrieve values from text boxes
            string locationName = txtAddName.Text;
            string country = txtAddCountry.Text;

            // Insert the record into the location table
            string connectionString = "Data Source = DESKTOP-DAME84Q; Initial Catalog = database; Persist Security Info = True; User ID = sa; Password = shaamil@";
            string insertQuery = "INSERT INTO location (locationName, country) VALUES (@LocationName, @Country); SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(insertQuery, connection);
                    command.Parameters.AddWithValue("@LocationName", locationName);
                    command.Parameters.AddWithValue("@Country", country);
                    int newLocationID = Convert.ToInt32(command.ExecuteScalar());

                    MessageBox.Show("Location added successfully. Location ID: " + newLocationID, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Instantiate a new instance of the fEdit form
            fUser Userform = new fUser();

            // Show the fEdit form
            Userform.Show();

            // Hide the user form
            this.Hide();
        }
    }
}
