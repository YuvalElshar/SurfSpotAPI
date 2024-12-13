using SurfSpotAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace SurfSpotAPI.DAL
{
    class UsersDataService
    {
        private readonly string connectionString = "Data Source=media.ruppin.ac.il;Initial Catalog=igroup240_test1;User ID=igroup240;Password=igroup240_16454;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        // Retrieve all users
        public List<User> GetUsers()
        {
            SqlConnection con = Connect();
            SqlCommand command = CreateGetUsersCommand(con);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<User> allUsers = new List<User>();

            while (dr.Read())
            {
                User u = new User
                {
                    Id = Int32.Parse(dr["Id"].ToString()),
                    UserName = dr["UserName"].ToString(),
                    Email = dr["Email"].ToString(),
                    Password = dr["Password"].ToString(),
                    FullName = dr["FullName"].ToString(),
                    CreatedAt = Convert.ToDateTime(dr["CreatedAt"]),
                    UpdatedAt = Convert.ToDateTime(dr["UpdatedAt"])
                };
                allUsers.Add(u);
            }
            con.Close();
            return allUsers;
        }

        // Add a new user
        public User AddUser(User user)
        {
            using (SqlConnection con = Connect())
            {
                SqlCommand cmd = CreatePostUserCommand(con, user);
                object insertedId = cmd.ExecuteScalar();

                if (insertedId != null)
                {
                    int id = Convert.ToInt32(insertedId);
                    user.Id = id;
                    return user;
                }
                return null;
            }
        }

        // Create command to retrieve users
        private SqlCommand CreateGetUsersCommand(SqlConnection con)
        {
            SqlCommand command = new SqlCommand
            {
                CommandText = "SPGetUsers", // Assume you have a stored procedure SPGetUsers
                Connection = con,
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 10
            };
            return command;
        }

        // Create command to insert a new user
        private SqlCommand CreatePostUserCommand(SqlConnection con, User user)
        {
            SqlCommand command = new SqlCommand("SPInsertUser", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@UserName", user.UserName);
            command.Parameters.AddWithValue("@Password", user.Password); // Hash password before calling this function
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@FullName", user.FullName ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@CreatedAt", user.CreatedAt);
            command.Parameters.AddWithValue("@UpdatedAt", user.UpdatedAt);

            return command;
        }

        // Connect to the database
        private SqlConnection Connect()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;
        }
    }
}
