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
    public partial class fStaff : Form
    {
        public fStaff()
        {
            InitializeComponent();
        }



        private int GetTypeID(string typeName)
        {
            int typeID = -1; // Initialize typeID to -1 (indicating failure)

            string connectionString = "Data Source = DESKTOP-DAME84Q; Initial Catalog = database; Persist Security Info = True; User ID = sa; Password = shaamil@;";
            string query = "SELECT typeID FROM staff_type WHERE typeName = @TypeName";

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

        private void btnSal_Click(object sender, EventArgs e)
        {
            // Ensure a staff category is selected in the ListBox
            if (lbStaffCat.SelectedItem == null)
            {
                MessageBox.Show("Please select a staff category.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get the selected staff category name
            string selectedStaffCategory = lbStaffCat.SelectedItem.ToString();

            // Get the fee value from the txtSalary TextBox
            decimal newFee;
            if (!decimal.TryParse(txtSal.Text, out newFee))
            {
                MessageBox.Show("Please enter a valid fee value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Update the fee in the staff_type table
            string connectionString = "Data Source = DESKTOP-DAME84Q; Initial Catalog = database; Persist Security Info = True; User ID = sa; Password = shaamil@";
            string updateQuery = @"
                                 UPDATE staff_type
                                 SET fee = @NewFee
                                 WHERE typeName = @TypeName
                                 ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(updateQuery, connection);
                    command.Parameters.AddWithValue("@NewFee", newFee);
                    command.Parameters.AddWithValue("@TypeName", selectedStaffCategory);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Fee updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to update fee.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            // Retrieve values from text boxes
            int staffID;
            if (!int.TryParse(txtUpID.Text, out staffID))
            {
                MessageBox.Show("Please enter a valid Staff ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string staffName = txtUpName.Text;

            // Get the selected typeID from lbUpType
            string selectedTypeName = lbUpType.SelectedItem?.ToString();
            if (selectedTypeName == null)
            {
                MessageBox.Show("Please select a staff type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Query for typeID
            int typeID = GetTypeID(selectedTypeName);

            if (typeID == -1)
            {
                MessageBox.Show("Failed to retrieve typeID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Construct the SQL UPDATE statement
            string connectionString = "Data Source = DESKTOP-DAME84Q; Initial Catalog = database; Persist Security Info = True; User ID = sa; Password = shaamil@";
            string updateQuery = @"
                                 UPDATE staff 
                                 SET staffName = @StaffName, typeID = @TypeID 
                                 WHERE staffID = @StaffID
                                 ";

            // Execute the UPDATE statement
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(updateQuery, connection);
                    command.Parameters.AddWithValue("@StaffID", staffID);
                    command.Parameters.AddWithValue("@StaffName", staffName);
                    command.Parameters.AddWithValue("@TypeID", typeID);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Property record updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to update staff record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            // Retrieve the staffID from the txtDelID textbox
            int staffID;
            if (!int.TryParse(txtDelID.Text, out staffID))
            {
                MessageBox.Show("Please enter a valid Staff ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Construct the SQL DELETE statement
            string connectionString = "Data Source = DESKTOP-DAME84Q; Initial Catalog = database; Persist Security Info = True; User ID = sa; Password = shaamil@";
            string deleteQueryStaff = "DELETE FROM staff WHERE staffID = @StaffID";
            string deleteQueryAllocation = "DELETE FROM allocation WHERE staffID = @StaffID";

            // Execute the DELETE statements within a transaction
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Temporarily disable foreign key constraints
                    SqlCommand disableConstraintsCmd = new SqlCommand("ALTER TABLE allocation NOCHECK CONSTRAINT FK_allocation_staff; ALTER TABLE staff NOCHECK CONSTRAINT FK_staff_staff_type", connection, transaction);
                    disableConstraintsCmd.ExecuteNonQuery();

                    // Delete record from the staff table
                    SqlCommand deleteStaffCmd = new SqlCommand(deleteQueryStaff, connection, transaction);
                    deleteStaffCmd.Parameters.AddWithValue("@StaffID", staffID);
                    int rowsAffectedStaff = deleteStaffCmd.ExecuteNonQuery();

                    // Delete relevant records from the allocation table
                    SqlCommand deleteAllocationCmd = new SqlCommand(deleteQueryAllocation, connection, transaction);
                    deleteAllocationCmd.Parameters.AddWithValue("@StaffID", staffID);
                    int rowsAffectedAllocation = deleteAllocationCmd.ExecuteNonQuery();

                    // Commit the transaction if successful
                    transaction.Commit();

                    if (rowsAffectedStaff > 0)
                    {
                        MessageBox.Show("Location record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No staff record found with the provided ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    // Rollback the transaction if any error occurs
                    transaction.Rollback();
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    // Enable foreign key constraints again
                    SqlCommand enableConstraintsCmd = new SqlCommand("ALTER TABLE allocation CHECK CONSTRAINT FK_allocation_staff; ALTER TABLE staff CHECK CONSTRAINT FK_staff_staff_type", connection);
                    enableConstraintsCmd.ExecuteNonQuery();

                    connection.Close();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Retrieve the staffName from the txtAddName textbox
            string staffName = txtAddName.Text.Trim();
            if (string.IsNullOrEmpty(staffName))
            {
                MessageBox.Show("Please enter a valid staff name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Retrieve the selected type name from the lbAddType list box
            string typeName = lbAddType.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(typeName))
            {
                MessageBox.Show("Please select a valid staff type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Retrieve the typeID corresponding to the selected typeName
            int typeID = GetTypeID(typeName);

            // Check if typeID retrieval was successful
            if (typeID == -1)
            {
                MessageBox.Show("Failed to retrieve typeID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Construct the SQL INSERT statement
            string connectionString = "Data Source = DESKTOP-DAME84Q; Initial Catalog = database; Persist Security Info = True; User ID = sa; Password = shaamil@";
            string insertQuery = "INSERT INTO staff (staffName, typeID) VALUES (@StaffName, @TypeID)";

            // Execute the INSERT statement
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(insertQuery, connection);
                    command.Parameters.AddWithValue("@StaffName", staffName);
                    command.Parameters.AddWithValue("@TypeID", typeID);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Production record added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to add staff record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Go back to Login Form
            fUser userForm = new fUser();
            userForm.Show();

            // Hide the login form
            this.Hide();
        }
    }
}
