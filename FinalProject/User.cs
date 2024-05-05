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
    public partial class fUser : Form
    {
        public fUser()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source = localhost; Initial Catalog = DIPLOMA; Persist Security Info=True;User ID = sa; Password=1Secure*Password1");

        private void Student_Load(object sender, EventArgs e)
        {
            if (fLogin.RoleID == 1)
            {
                adminBox.Visible = true;
            }
            else
            {
                adminBox.Visible = false;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Go back to Login Form
            fLogin loginForm = new fLogin();
            loginForm.Show();

            // Hide the login form
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Clear existing data in the DataGridView
            dataG.DataSource = null;

            // Query to retrieve production data along with client details
            string query = @"
                           SELECT p.productionID, p.productionName, pt.typeName AS productionType,
                           p.days AS numberOfDays, c.clientName AS client
                           FROM production p
                           INNER JOIN client c ON p.clientID = c.clientID
                           INNER JOIN production_type pt ON p.typeID = pt.typeID
                           ";

            // Execute the query and fill a DataTable with the results
            DataTable dtProductions = ExecuteQuery(query);

            // Check if any data was returned
            if (dtProductions != null && dtProductions.Rows.Count > 0)
            {
                // Bind the DataTable to the DataGridView
                dataG.DataSource = dtProductions;
            }
            else
            {
                MessageBox.Show("No productions found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private DataTable ExecuteQuery(string query)
        {
            // Connection string to your SQL Server database
            string connectionString = "Data Source = DESKTOP-DAME84Q; Initial Catalog = database; Persist Security Info = True; User ID = sa; Password = shaamil@";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataTable dataTable = new DataTable();
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return dataTable;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Instantiate a new instance of the fEdit form
            fEdit editForm = new fEdit();

            // Show the fEdit form
            editForm.Show();

            // Hide the user form
            this.Hide();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            // Instantiate a new instance of the fEdit form
            fStaff staffForm = new fStaff();

            // Show the fEdit form
            staffForm.Show();

            // Hide the user form
            this.Hide();
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            // Query to retrieve all records from the staff table
            string query = "SELECT * FROM staff";

            // Connection string
            string connectionString = "Data Source = DESKTOP-DAME84Q; Initial Catalog = database; Persist Security Info = True; User ID = sa; Password = shaamil@";

            try
            {
                // Create a DataTable to hold the data
                DataTable dataTable = new DataTable();

                // Create a SqlDataAdapter to execute the query and fill the DataTable
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.Fill(dataTable);
                }

                // Bind the DataTable to the DataGridView
                dataG.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            // Query to retrieve all records from the client table
            string query = "SELECT * FROM client";

            // Connection string
            string connectionString = "Data Source = DESKTOP-DAME84Q; Initial Catalog = database; Persist Security Info = True; User ID = sa; Password = shaamil@";

            try
            {
                // Create a DataTable to hold the data
                DataTable dataTable = new DataTable();

                // Create a SqlDataAdapter to execute the query and fill the DataTable
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.Fill(dataTable);
                }

                // Bind the DataTable to the DataGridView
                dataG.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditClients_Click(object sender, EventArgs e)
        {
            // Instantiate a new instance of the fEdit form
            fClientManagement clientform = new fClientManagement();

            // Show the fEdit form
            clientform.Show();

            // Hide the user form
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Instantiate a new instance of the fEdit form
            fProdStaff prodStaffform = new fProdStaff();

            // Show the fEdit form
            prodStaffform.Show();

            // Hide the user form
            this.Hide();
        }

        private void btnAllocation_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "Data Source = DESKTOP-DAME84Q; Initial Catalog = database; Persist Security Info = True; User ID = sa; Password = shaamil@";
                string query = @"SELECT s.staffName, p.productionName
                         FROM allocation a
                         INNER JOIN staff s ON a.staffID = s.staffID
                         INNER JOIN production p ON a.productionID = p.productionID";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataG.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while fetching allocations: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "Data Source = DESKTOP-DAME84Q; Initial Catalog = database; Persist Security Info = True; User ID = sa; Password = shaamil@";
                string query = "SELECT * FROM property";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataG.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while fetching property records: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "Data Source = DESKTOP-DAME84Q; Initial Catalog = database; Persist Security Info = True; User ID = sa; Password = shaamil@";
                string query = "SELECT * FROM location";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataG.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while fetching location records: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPropLoc_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "Data Source = DESKTOP-DAME84Q; Initial Catalog = database; Persist Security Info = True; User ID = sa; Password = shaamil@";
                string query = @"
                               SELECT p.propertyName, l.locationName, l.country
                               FROM property_location pl
                               INNER JOIN property p ON pl.propertyID = p.propertyID
                               INNER JOIN location l ON pl.locationID = l.locationID
                               ";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataG.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while fetching property-location records: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditProperty_Click(object sender, EventArgs e)
        {
            // Instantiate a new instance of the fEdit form
            fPropertyManagement propStaffform = new fPropertyManagement();

            // Show the fEdit form
            propStaffform.Show();

            // Hide the user form
            this.Hide();
        }

        private void btnEditLocation_Click(object sender, EventArgs e)
        {
            // Instantiate a new instance of the fEdit form
            fLocationManagement Locationform = new fLocationManagement();

            // Show the fEdit form
            Locationform.Show();

            // Hide the user form
            this.Hide();
        }
    }
}
