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
    public partial class fProdStaff : Form
    {
        public fProdStaff()
        {
            InitializeComponent();
            LoadProductionNames();
        }

        private void LoadProductionNames()
        {
            try
            {
                // Populate the ListBox with production names from the production table
                string connectionString = "Data Source = DESKTOP-DAME84Q; Initial Catalog = database; Persist Security Info = True; User ID = sa; Password = shaamil@";
                string query = "SELECT productionName FROM production";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    lbProdName.DisplayMember = "productionName";
                    lbProdName.ValueMember = "productionName"; // Use productionName as the value
                    lbProdName.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading production names: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void fProdStaff_Load(object sender, EventArgs e)
        {

        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            try
            {
                int staffID;
                if (!int.TryParse(txtStaffID.Text, out staffID))
                {
                    MessageBox.Show("Please enter a valid Staff ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string typeName = lbProdName.SelectedValue?.ToString();
                if (typeName == null)
                {
                    MessageBox.Show("Please select a production type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int typeID = GetProductionID(typeName);
                if (typeID == -1)
                {
                    MessageBox.Show("Failed to retrieve typeID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string connectionString = "Data Source = DESKTOP-DAME84Q; Initial Catalog = database; Persist Security Info = True; User ID = sa; Password = shaamil@";
                string query = "INSERT INTO allocation (staffID, productionID) VALUES (@StaffID, @TypeID)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StaffID", staffID);
                    command.Parameters.AddWithValue("@TypeID", typeID);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Allocation added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to add allocation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetProductionID(string typeName)
        {
            int typeID = -1;

            try
            {
                string connectionString = "Data Source = DESKTOP-DAME84Q; Initial Catalog = database; Persist Security Info = True; User ID = sa; Password = shaamil@";
                string query = "SELECT productionID FROM production WHERE productionName = @TypeName";

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TypeName", typeName);

                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        typeID = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving productionID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return typeID;
        }

        private void btnBack_Click(object sender, EventArgs e)
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

