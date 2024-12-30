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
                    user.CreatedAt = DateTime.Now;
                    user.UpdatedAt = DateTime.Now;
                    return user;
                }
                return null;
            }
        }

        // Validate user credentials
        public int ValidateUser(string email, string password)
        {
            using (SqlConnection con = Connect())
            {
                SqlCommand cmd = new SqlCommand("SPValidateUser", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Hash the password before sending (replace `HashPassword` with your hash function)
                string hashedPassword = HashPassword(password);

                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", hashedPassword);

                var returnValue = new SqlParameter
                {
                    ParameterName = "@ReturnVal",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.ReturnValue
                };
                cmd.Parameters.Add(returnValue);

                cmd.ExecuteNonQuery();

                return (int)returnValue.Value; // Return the result of the stored procedure
            }
        }

        // Like a SurfSpot
        public bool LikeSurfSpot(int userId, int surfSpotId)
        {
            using (var connection = Connect())
            {
                // Check if the user already liked the surf spot
                var checkCmd = new SqlCommand("SELECT COUNT(1) FROM dbo.UserLikedSpots WHERE UserId = @UserId AND SurfSpotId = @SurfSpotId", connection);
                checkCmd.Parameters.AddWithValue("@UserId", userId);
                checkCmd.Parameters.AddWithValue("@SurfSpotId", surfSpotId);

                int count = (int)checkCmd.ExecuteScalar();
                if (count > 0)
                    return false; // User has already liked the spot

                // Add the like
                var insertCmd = new SqlCommand("INSERT INTO dbo.UserLikedSpots (UserId, SurfSpotId) VALUES (@UserId, @SurfSpotId)", connection);
                insertCmd.Parameters.AddWithValue("@UserId", userId);
                insertCmd.Parameters.AddWithValue("@SurfSpotId", surfSpotId);

                insertCmd.ExecuteNonQuery();
                return true; // Successfully liked the spot
            }
        }

        // Unlike a SurfSpot
        public bool UnlikeSurfSpot(int userId, int surfSpotId)
        {
            using (var connection = Connect())
            {
                // Check if the user has liked the spot
                var checkCmd = new SqlCommand("SELECT COUNT(1) FROM dbo.UserLikedSpots WHERE UserId = @UserId AND SurfSpotId = @SurfSpotId", connection);
                checkCmd.Parameters.AddWithValue("@UserId", userId);
                checkCmd.Parameters.AddWithValue("@SurfSpotId", surfSpotId);

                int count = (int)checkCmd.ExecuteScalar();
                if (count == 0)
                    return false; // User has not liked the spot

                // Remove the like
                var deleteCmd = new SqlCommand("DELETE FROM dbo.UserLikedSpots WHERE UserId = @UserId AND SurfSpotId = @SurfSpotId", connection);
                deleteCmd.Parameters.AddWithValue("@UserId", userId);
                deleteCmd.Parameters.AddWithValue("@SurfSpotId", surfSpotId);

                deleteCmd.ExecuteNonQuery();
                return true; // Successfully unliked the spot
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

        // Retrieve all liked spots for a specific user
        public List<SurfSpot> GetLikedSpots(int userId)
        {
            using (var connection = Connect())
            {
                // SQL query to get all liked spots for the user
                var command = new SqlCommand("SPGetLikedSpots", connection);

                command.Parameters.AddWithValue("@UserId", userId);

                SqlDataReader reader = command.ExecuteReader();
                List<SurfSpot> likedSpots = new List<SurfSpot>();

                while (reader.Read())
                {
                    SurfSpot spot = new SurfSpot
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        //Image = reader["Image"].ToString(),
                        Info = reader["Info"].ToString(),
                        Dangers = reader["Dangers"].ToString(),
                        WaveLength = reader["wave_length"].ToString(),
                        BestMonthsToSurf = reader["best_months_to_surf"].ToString()
                    };

                    likedSpots.Add(spot);
                }

                return likedSpots;
            }
        }


        // Hash password (implement your own secure hashing logic)
        private string HashPassword(string password)
        {
            // Example only: Use a strong password hashing library such as BCrypt or PBKDF2 in production
            return password; // Replace with hashed password
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
