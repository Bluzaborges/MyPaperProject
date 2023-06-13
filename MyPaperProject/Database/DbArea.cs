using MyPaperProject.Models;
using MyPaperProject.Global;
using Npgsql;

namespace MyPaperProject.Database
{
	public class DbArea
	{
		public List<Area> GetAllAreas()
		{
			List<Area> result = new List<Area>();

			try
			{
				DbAccess db = new DbAccess();

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
				Log.Add(LogType.error, "[DbArea.GetAllAreas]: " + ex.Message);
			}

			return result;
		}
	}
}
