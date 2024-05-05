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
    public partial class fClientManagement : Form
    {
        public fClientManagement()
        {
            InitializeComponent();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            // Retrieve values from text boxes
            int clientID;
            if (!int.TryParse(txtUpID.Text, out clientID))
            {
                MessageBox.Show("Please enter a valid Client ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string clientName = txtUpName.Text;

            // Check if the client ID exists in the database
            if (!ClientExists(clientID))
            {
                MessageBox.Show("Client with ID " + clientID + " does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Construct the SQL UPDATE statement
            string query = "UPDATE client SET clientName = @ClientName WHERE clientID = @ClientID";

            // Connection string
            string connectionString = "Data Source = DESKTOP-DAME84Q; Initial Catalog = database; Persist Security Info = True; User ID = sa; Password = shaamil@";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to the command
                        command.Parameters.AddWithValue("@ClientName", clientName);
                        command.Parameters.AddWithValue("@ClientID", clientID);

                        // Execute the UPDATE statement
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Client updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No client record updated.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to check if a client with the given ID exists in the database
        private bool ClientExists(int clientID)
        {
            string query = "SELECT COUNT(*) FROM client WHERE clientID = @ClientID";
            string connectionString = "Data Source = DESKTOP-DAME84Q; Initial Catalog = database; Persist Security Info = True; User ID = sa; Password = shaamil@";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameter to the command
                        command.Parameters.AddWithValue("@ClientID", clientID);

                        // Execute the query and check if any rows are returned
                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

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
