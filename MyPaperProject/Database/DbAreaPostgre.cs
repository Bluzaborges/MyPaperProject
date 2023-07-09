using MyPaperProject.Global;
using MyPaperProject.Models;
using MyPaperProject.Models.Repositories;
using Npgsql;

namespace MyPaperProject.Database
{
	public class DbAreaPostgre : IAreaRepository
	{
		public List<Area> GetAllAreas()
		{
			List<Area> result = new List<Area>();

			try
			{
				DbAccessPostgre db = new DbAccessPostgre();

				using (NpgsqlCommand cmd = new NpgsqlCommand())
				{
					cmd.CommandText = @"SELECT id, name FROM areas " +
									  @"ORDER BY name;";

					using (cmd.Connection = db.OpenConnection())
					using (NpgsqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							Area area = new Area();

							if (reader["id"] != DBNull.Value)
								area.Id = Convert.ToInt32(reader["id"]);

							if (reader["name"] != DBNull.Value)
								area.Name = reader["name"].ToString();

							result.Add(area);
						}
					}
				}

			} catch(Exception ex)
			{
				Log.Add(LogType.error, "[DbAreaPostgre.GetAllAreas]: " + ex.Message);
			}

			return result;
		}

		public List<Area> GetAllAreasByIdResearcher(int idResearcher)
		{
            List<Area> result = new List<Area>();

            try
            {
                DbAccessPostgre db = new DbAccessPostgre();

                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"SELECT a.name, a.id " +
									  @"FROM researchers_areas AS ra, areas AS a " +
									  @"WHERE ra.id_researcher = @IdResearcher AND ra.id_area = a.id " +
                                      @"ORDER BY a.name;";

					cmd.Parameters.AddWithValue("IdResearcher", idResearcher);

                    using (cmd.Connection = db.OpenConnection())
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Area area = new Area();

							if (reader["id"] != DBNull.Value)
								area.Id = Convert.ToInt32(reader["id"]);

							if (reader["name"] != DBNull.Value)
                                area.Name = reader["name"].ToString();

                            result.Add(area);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Add(LogType.error, "[DbAreaPostgre.GetAllAreasNamesByIdResearcher]: " + ex.Message);
            }

            return result;
        }

        public List<Area> GetAllAreasByIdProject(int idProject)
        {
            List<Area> result = new List<Area>();

            try
            {
                DbAccessPostgre db = new DbAccessPostgre();

                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"SELECT a.id, a.name " +
                                      @"FROM projects_areas AS pa, areas AS a " +
                                      @"WHERE pa.id_project = @IdProject AND pa.id_area = a.id " +
                                      @"ORDER BY a.name;";

                    cmd.Parameters.AddWithValue("IdProject", idProject);

                    using (cmd.Connection = db.OpenConnection())
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Area area = new Area();

							if (reader["id"] != DBNull.Value)
								area.Id = Convert.ToInt32(reader["id"]);

							if (reader["name"] != DBNull.Value)
                                area.Name = reader["name"].ToString();

                            result.Add(area);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Add(LogType.error, "[DbAreaPostgre.GetAllAreasNamesByIdProject]: " + ex.Message);
            }

            return result;
        }
    }
}
