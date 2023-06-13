using MyPaperProject.Global;
using MyPaperProject.Models;
using Npgsql;

namespace MyPaperProject.Database
{
	public class DbSubarea
	{
		public List<Subarea> GetAllSubareas()
		{
			List<Subarea> result = new List<Subarea>();

			try
			{
				DbAccess db = new DbAccess();

				using (NpgsqlCommand cmd = new NpgsqlCommand())
				{
					cmd.CommandText = @"SELECT id, id_area, name FROM subareas " +
									  @"ORDER BY name;";

					using (cmd.Connection = db.OpenConnection())
					using (NpgsqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							Subarea subarea = new Subarea();

							if (reader["id"] != DBNull.Value)
								subarea.Id = Convert.ToInt32(reader["id"]);

							if (reader["id_area"] != DBNull.Value)
								subarea.IdArea = Convert.ToInt32(reader["id_area"]);

							if (reader["name"] != DBNull.Value)
								subarea.Name = reader["name"].ToString();

							result.Add(subarea);
						}
					}
				}

			}
			catch (Exception ex)
			{
				Log.Add(LogType.error, "[DbSubarea.GetAllSubareas]: " + ex.Message);
			}

			return result;
		}
	}
}
