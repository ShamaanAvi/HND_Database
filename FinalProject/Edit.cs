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
    public partial class fEdit : Form
    {
        public fEdit()
        {
            InitializeComponent();

            LoadProductions(); // Call the method to load productions
            LoadProductionTypes(); // Populate ListBox with production types
            LoadClients(); // Populate ListBox with clients
        }

        private void LoadProductionTypes()  
        {
            // Retrieve data from production_type table and populate ListBox
            string connectionString = "Data Source = DESKTOP-DAME84Q; Initial Catalog = database; Persist Security Info = True; User ID = sa; Password = shaamil@";
            string query = "SELECT typeID, typeName FROM production_type";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                lbType.DisplayMember = "typeName";
                lbType.ValueMember = "typeID";
                lbType.DataSource = dataTable;
            }
        }

        private DataTable productionTable;
        private void LoadProductions()
        {
            try
            {
                // Populate the productionsTable DataTable with data from the production table
                string connectionString = "Data Source = DESKTOP-DAME84Q; Initial Catalog = database; Persist Security Info = True; User ID = sa; Password = shaamil@";

                string query = "SELECT productionID, productionName FROM production";

                productionTable = new DataTable();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.Fill(productionTable);
                }

                // Bind the productionsTable DataTable to the ListBox
                lbProd.DisplayMember = "productionName";
                lbProd.ValueMember = "productionID";
                lbProd.DataSource = productionTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading productions: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadClients()
        {
            // Retrieve data from client table and populate ListBox
            string connectionString = "Data Source = DESKTOP-DAME84Q; Initial Catalog = database; Persist Security Info = True; User ID = sa; Password = shaamil@";
            string query = "SELECT clientID, clientName FROM client";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                lbClient.DisplayMember = "clientName";
                lbClient.ValueMember = "clientID";
                lbClient.DataSource = dataTable;
            }
        }

        private void btnDelProd_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source = DESKTOP-DAME84Q; Initial Catalog = database; Persist Security Info = True; User ID = sa; Password = shaamil@";
            // Check if any production is selected
            if (lbProd.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a production to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Confirm deletion
            DialogResult result = MessageBox.Show("Are you sure you want to delete the selected production(s)?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
                return;

            // Temporarily disable foreign key constraints
            string disableConstraintsQuery = @"
            ALTER TABLE client NOCHECK CONSTRAINT FK__productio__clien__73BA3083;
            ALTER TABLE production_location NOCHECK CONSTRAINT FK__productio__produ__05D8E0BE;
            ALTER TABLE production_type NOCHECK CONSTRAINT FK__productio__typeI__74AE54BC;
            ALTER TABLE property_location NOCHECK CONSTRAINT FK__property___produ__02FC7413;
            ALTER TABLE production_location NOCHECK CONSTRAINT fk_production_location;
            ";

            // Delete selected production(s) from the production table
            string deleteQuery = "DELETE FROM production WHERE productionID = @ProductionID";

            // Delete related records from other tables
            string deleteRelatedRecordsQuery = @"
            DELETE FROM client WHERE productionID = @ProductionID;
            DELETE FROM production_location WHERE productionID = @ProductionID;
            DELETE FROM production_type WHERE productionID = @ProductionID;
            DELETE FROM property_location WHERE productionID = @ProductionID;
            ";

            // Re-enable foreign key constraints
            string enableConstraintsQuery = @"
            ALTER TABLE client CHECK CONSTRAINT FK__productio__clien__73BA3083;
            ALTER TABLE production_location CHECK CONSTRAINT FK__productio__produ__05D8E0BE;
            ALTER TABLE production_type CHECK CONSTRAINT FK__productio__typeI__74AE54BC;
            ALTER TABLE property_location CHECK CONSTRAINT FK__property___produ__02FC7413;
            ALTER TABLE production_location CHECK CONSTRAINT fk_production_location;
            ";

            // Execute queries within a transaction
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Temporarily disable foreign key constraints
                    using (SqlCommand disableConstraintsCommand = new SqlCommand(disableConstraintsQuery, connection, transaction))
                    {
                        disableConstraintsCommand.ExecuteNonQuery();
                    }

                    // Delete selected production(s) from the production table
                    foreach (DataRowView selectedItem in lbProd.SelectedItems)
                    {
                        int productionID = (int)selectedItem["productionID"];

                        using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection, transaction))
                        {
                            deleteCommand.Parameters.AddWithValue("@ProductionID", productionID);
                            deleteCommand.ExecuteNonQuery();
                        }

                        // Delete related records from other tables
                        using (SqlCommand deleteRelatedRecordsCommand = new SqlCommand(deleteRelatedRecordsQuery, connection, transaction))
                        {
                            deleteRelatedRecordsCommand.Parameters.AddWithValue("@ProductionID", productionID);
                            deleteRelatedRecordsCommand.ExecuteNonQuery();
                        }
                    }

                    // Re-enable foreign key constraints
                    using (SqlCommand enableConstraintsCommand = new SqlCommand(enableConstraintsQuery, connection, transaction))
                    {
                        enableConstraintsCommand.ExecuteNonQuery();
                    }

                    // Commit the transaction
                    transaction.Commit();

                    // Reload productions after deletion
                    LoadProductions();
                }
                catch (Exception ex)
                {
                    // Rollback the transaction if any error occurs
                    transaction.Rollback();
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Retrieve values from text boxes
            int productionID ;
            if (!int.TryParse(txtProdID.Text, out productionID))
            {
                MessageBox.Show("Please enter a valid Production ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string prodName = txtProdName.Text;

            // Get the selected type name from lbType
            string productionType = lbType.SelectedValue?.ToString();
            if (productionType == null)
            {
                MessageBox.Show("Please select a type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Debugging statement to output the selected production type
            Console.WriteLine("Selected production type: " + productionType);

            int days;
            if (!int.TryParse(txtDays.Text, out days))
            {
                MessageBox.Show("Please enter a valid number of days.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get the selected client name from lbClient
            string clientID = lbClient.SelectedValue?.ToString();  
            if (clientID == null)
            {
                MessageBox.Show("Please select a client.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Query for typeID and clientID
            int typeID = GetTypeID(productionType);

            // Debugging statement to output the retrieved typeID
            Console.WriteLine("Retrieved typeID: " + typeID);

            int ClientID = GetClientID(clientID);

            if (typeID == -1 || ClientID == -1)
            {
                MessageBox.Show("Failed to retrieve typeID or clientID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Construct the SQL INSERT statement
            string connectionString = "Data Source = DESKTOP-DAME84Q; Initial Catalog = database; Persist Security Info = True; User ID = sa; Password = shaamil@";
            string insertQuery = @"
                                 INSERT INTO production (productionID ,productionName, typeID, days, clientID)
                                 VALUES (@ProdID, @ProdName, @ProductionType, @Days, @ClientID)
                                 ";

            // Execute the INSERT statement
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {     
                    connection.Open();
                    SqlCommand command = new SqlCommand(insertQuery, connection);
                    command.Parameters.AddWithValue("@ProdID", productionID);
                    command.Parameters.AddWithValue("@ProdName", prodName);
                    command.Parameters.AddWithValue("@TypeName", productionType);
                    command.Parameters.AddWithValue("@Days", days);
                    command.Parameters.AddWithValue("@ClientID", clientID);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Production added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to add production.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private int GetTypeID(string typeName)
        {
            int typeID = -1; // Initialize typeID to -1 (indicating failure)

            string connectionString = "Data Source = DESKTOP-DAME84Q; Initial Catalog = database; Persist Security Info = True; User ID = sa; Password = shaamil@";
            string query = "SELECT typeID FROM production_type WHERE typeName = @TypeName";

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

        private int GetClientID(string clientName)
        {
            int clientID = -1; // Initialize clientID to -1 (indicating failure)

            string connectionString = "Data Source = DESKTOP-DAME84Q; Initial Catalog = database; Persist Security Info = True; User ID = sa; Password = shaamil@";
            string query = "SELECT clientID FROM client WHERE clientName = @ClientName";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ClientName", clientName);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        clientID = Convert.ToInt32(result); // Convert the result to int
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (e.g., log or display error message)
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }

            return clientID;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Go back to Login Form
            fUser userForm = new fUser();
            userForm.Show();

            // Hide the login form
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void lbStaffCat_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtSal_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
        

