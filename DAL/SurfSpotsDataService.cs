using SurfSpotAPI.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace SurfSpotAPI.DAL
{
    class SurfSpotsDataService
    {
        private readonly string connectionString = "Data Source=media.ruppin.ac.il;Initial Catalog=igroup240_test1;User ID=igroup240;Password=igroup240_16454;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        
        
        public List<SurfSpot> getSurfSpots()
        {
            SqlConnection con = Connect();
            SqlCommand command = CreateGetSurfSpotsCommand(con);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<SurfSpot> allSurfSpots = new List<SurfSpot>();
            while (dr.Read())
            {
                SurfSpot s = new SurfSpot();

                s.Id = Int32.Parse(dr["id"].ToString());
                s.Name = dr["name"].ToString();
                s.Latitude = Convert.ToDouble(dr["latitude"]);
                s.Longitude = Convert.ToDouble(dr["longitude"]);
                s.Dangers = dr["dangers"].ToString();
                s.WaveLength = dr["wave_length"].ToString();
                s.WaveDirection = dr["wave_direction"].ToString();
                s.BestMonthsToSurf = dr["best_months_to_surf"].ToString();
                s.BestTideToSurf = dr["best_tide_toSurf"].ToString();
                s.Info = dr["info"].ToString();

                allSurfSpots.Add(s);
            }
            con.Close();
            return allSurfSpots;

        }

        public SurfSpot AddSurfSpot(SurfSpot s)
        {
            using(SqlConnection con = Connect()) {

                
                SqlCommand cmd = CreatePostSurfSpotCommand(con, s);
                object insertedId = cmd.ExecuteScalar();
                if(insertedId != null)
                {
                    int id = Convert.ToInt32(insertedId);
                    s.Id = id;
                    return s;
                }
                return null;
            }
  
        }

        private SqlCommand CreateGetSurfSpotsCommand(SqlConnection con)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SPGetSurfSpots";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10;

            return command;
        }

        private SqlCommand CreatePostSurfSpotCommand(SqlConnection con,SurfSpot surfSpot)
        {
            SqlCommand command = new SqlCommand("SPInsertSurfSpot", con);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@name", surfSpot.Name);
            command.Parameters.AddWithValue("@latitude", surfSpot.Latitude);
            command.Parameters.AddWithValue("@longitude", surfSpot.Longitude);
            command.Parameters.AddWithValue("@info", surfSpot.Info);
            command.Parameters.AddWithValue("@best_months_to_surf", surfSpot.BestMonthsToSurf);
            command.Parameters.AddWithValue("@best_tide_toSurf", surfSpot.BestTideToSurf);
            command.Parameters.AddWithValue("@dangers", surfSpot.Dangers);
            command.Parameters.AddWithValue("@wave_length", surfSpot.WaveLength);
            command.Parameters.AddWithValue("@wave_direction", surfSpot.WaveDirection);

            command.CommandTimeout = 10;

            return command;
        }

        private SqlCommand CreatePostSurfSpotsCommand(SqlConnection con, SurfSpot surfSpot)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = @$"
                                insert into surfSpots (name,latitude,longitude,info,best_months_to_surf,best_tide_toSurf,dangers,waveLength,waveDirection) 
                                values('{surfSpot.Name}', {surfSpot.Latitude}, {surfSpot.Longitude},'{surfSpot.Info}','{surfSpot.BestMonthsToSurf}', '{surfSpot.BestTideToSurf}', '{surfSpot.Dangers}', '{surfSpot.WaveLength}', '{surfSpot.WaveDirection}')";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 10;

            return command;
        }

        private SqlConnection Connect()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;
        }
    }
}
