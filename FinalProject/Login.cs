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
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        

        SqlConnection connection = new SqlConnection(@"Data Source = DESKTOP-DAME84Q; Initial Catalog = database; Persist Security Info=True;User ID = sa; Password=shaamil@");

        // Declare the public static variable to store the userID
        public static int UserID { get; set; }
        public static int RoleID { get; set; }


        private void txtUser_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Set the UserID property
            string userName = txtUser.Text;
            string password = txtPass.Text;

            // Perform authentication and retrieve the user ID from the database
            int userID = RetrieveUserIDFromDatabase(userName, password);

            // Assign the retrieved user ID to the public variable
            UserID = userID;

            // Perform authentication and retrieve the user ID from the database
            int roleID = GetRoleIDFromDatabase(userName, password);

            // Assign the retrieved role ID to the public variable
            RoleID = roleID;

            // SQL query to retrieve user information based on the provided username and password
            string query = $"SELECT roleID FROM [user] WHERE userName = '{userName}' AND password = '{password}'";

            // Create a SqlConnection object
            using (SqlConnection connection = new SqlConnection(@"Data Source = DESKTOP-DAME84Q; Initial Catalog = database; Persist Security Info=True; User ID = sa; Password=shaamil@"))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Create a SqlCommand object with the query and connection
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Execute the query and retrieve the data
                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            int UserID = Convert.ToInt32(result);
                            int RoleID = Convert.ToInt32(result);

                            // Open the fUser form for all role IDs
                            fUser userForm = new fUser();
                            userForm.Show();

                            // Hide the login form (optional)
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fLogin_Load(object sender, EventArgs e)
        {

        }

        private int RetrieveUserIDFromDatabase(string userName, string password)
        {
            int userID = 0;

            string query = $"SELECT userID FROM [user] WHERE userName = '{userName}'";

            using (SqlConnection connection = new SqlConnection(@"Data Source = DESKTOP-DAME84Q; Initial Catalog = database; Persist Security Info=True; User ID = sa; Password=shaamil@"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", userName);

                    var result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int parsedUserID))
                    {
                        userID = parsedUserID;
                    }
                }
            }
            return userID;
        }

        private int GetRoleIDFromDatabase(string userName, string password)
        {
            int roleID = 0;
           
            string query = $"SELECT roleID FROM [user] WHERE userName = '{userName}'";

            using (SqlConnection connection = new SqlConnection(@"Data Source = DESKTOP-DAME84Q; Initial Catalog = database; Persist Security Info=True; User ID = sa; Password=shaamil@"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int parsedRoleID))
                {
                    roleID = parsedRoleID;
                }
            }

            return roleID;
        }

    }
}

