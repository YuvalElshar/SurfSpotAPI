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
        private readonly string connectionString = "Data Source=media.ruppin.ac.il;Initial Catalog=igroup240_test1;User ID=igroup240;Password=********;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public IEnumerable<SurfSpot> getSurfSpots()
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

        private SqlCommand CreateGetSurfSpotsCommand(SqlConnection con)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SPGetSurfSpots";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
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
