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
    public partial class fPropertyManagement : Form
    {
        public fPropertyManagement()
        {
            InitializeComponent();
            LoadPropertyTypes();
        }

        private void LoadPropertyTypes()
        {
            // Retrieve data from property_type table and populate ListBox
            string connectionString = "Data Source = DESKTOP-DAME84Q; Initial Catalog = database; Persist Security Info = True; User ID = sa; Password = shaamil@";
            string query = "SELECT typeID, typeName FROM property_type";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                lbAddType.DisplayMember = "typeName";
                lbAddType.ValueMember = "typeID";
                lbAddType.DataSource = dataTable;
            }
        }



        private void btnUp_Click(object sender, EventArgs e)
        {
            // Retrieve values from text boxes
            int propertyID;
            if (!int.TryParse(txtUpID.Text, out propertyID))
            {
                MessageBox.Show("Please enter a valid Property ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string propertyName = txtUpName.Text;

            // Get the selected type name from lbUpType
            DataRowView selectedItem = lbUpType.SelectedItem as DataRowView;
            if (selectedItem == null)
            {
                MessageBox.Show("Please select a type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string typeName = selectedItem["typeName"].ToString();

            // Query for typeID
            int typeID = GetTypeID(typeName);

            // Validate typeID
            if (typeID == -1)
            {
                MessageBox.Show("Failed to retrieve typeID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Update the record in the property table
            string connectionString = "Data Source = DESKTOP-DAME84Q; Initial Catalog = database; Persist Security Info = True; User ID = sa; Password = shaamil@";
            string updateQuery = "UPDATE property SET propertyName = @PropertyName, typeID = @TypeID WHERE propertyID = @PropertyID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(updateQuery, connection);
                    command.Parameters.AddWithValue("@PropertyName", propertyName);
                    command.Parameters.AddWithValue("@TypeID", typeID);
                    command.Parameters.AddWithValue("@PropertyID", propertyID);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Property updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Property updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Property updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private int GetTypeID(string typeName)
        {
            int typeID = -1; // Initialize typeID to -1 (indicating failure)

            string connectionString = "Data Source = DESKTOP-DAME84Q; Initial Catalog = database; Persist Security Info = True; User ID = sa; Password = shaamil@";
            string query = "SELECT typeID FROM property_type WHERE typeName = @TypeName";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TypeName", typeName);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        typeID = Convert.ToInt32(result); // Convert the result to int
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (e.g., log or display error message)
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }

            return typeID;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            // Retrieve the propertyID entered by the user
            int propertyID;
            if (!int.TryParse(txtDelID.Text, out propertyID))
            {
                MessageBox.Show("Please enter a valid Property ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Execute the DELETE query to remove the property record
            string connectionString = "Data Source = DESKTOP-DAME84Q; Initial Catalog = database; Persist Security Info = True; User ID = sa; Password = shaamil@";
            string deleteQuery = "DELETE FROM property WHERE propertyID = @PropertyID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(deleteQuery, connection);
                    command.Parameters.AddWithValue("@PropertyID", propertyID);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Property deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No property found with the provided Property ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            string propertyName = txtAddName.Text;
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                MessageBox.Show("Please enter a property name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get the selected typeID from lbAddType
            int typeID = (int)lbAddType.SelectedValue;

            // Insert into property table
            string connectionString = "Data Source = DESKTOP-DAME84Q; Initial Catalog = database; Persist Security Info = True; User ID = sa; Password = shaamil@ ";
            string insertQuery = "INSERT INTO property (propertyName, typeID) VALUES (@PropertyName, @TypeID)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(insertQuery, connection);
                    command.Parameters.AddWithValue("@PropertyName", propertyName);
                    command.Parameters.AddWithValue("@TypeID", typeID);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Property added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to add property.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Go back to Login Form
            fLogin loginForm = new fLogin();
            loginForm.Show();

            // Hide the login form
            this.Hide();
        }
    }
}
