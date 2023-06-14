using MyPaperProject.Global;
using MyPaperProject.Models;
using Npgsql;

namespace MyPaperProject.Database
{
	public class DbResearcher
	{
		public bool ResearcherExists(string name, string cpf)
		{
			bool result = false;
			int count = 0;

			try
			{
				DbAccess db = new DbAccess();

				using (NpgsqlCommand cmd = new NpgsqlCommand())
				{
					cmd.CommandText = @"SELECT COUNT(*) FROM researchers " +
									  @"WHERE name = @Name OR cpf = @Cpf;";

					cmd.Parameters.AddWithValue("@Name", name);
					cmd.Parameters.AddWithValue("@Cpf", cpf);	

					using (cmd.Connection = db.OpenConnection())
					{
						count = Convert.ToInt32(cmd.ExecuteScalar());

						if (count > 0)
							result = true;
					}
				}
			}
			catch (Exception ex)
			{
				Log.Add(LogType.error, "[DbResearcher.ResearcherExists]: " + ex.Message);
			}

			return result;
		}

		public int RegisterResearcher(Researcher researcher)
		{
			int result = 0;

			try
			{
				DbAccess db = new DbAccess();

				using (NpgsqlCommand cmd = new NpgsqlCommand())
				{
					cmd.CommandText = @"INSERT INTO researchers (name, cpf, type) " +
									  @"VALUES (@Name, @Cpf, @Type) RETURNING id;";

					cmd.Parameters.AddWithValue("@Name", researcher.Name);
					cmd.Parameters.AddWithValue("@Cpf", researcher.Cpf);
					cmd.Parameters.AddWithValue("@Type", researcher.Type);

					using (cmd.Connection = db.OpenConnection())
					using (NpgsqlDataReader reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							if (reader["id"] != DBNull.Value)
								result = Convert.ToInt32(reader["id"]);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Add(LogType.error, "[DbResearcher.RegisterResearcher]: " + ex.Message);
			}

			return result;
		}

		public bool RegisterResearcherAreas(int idResearcher, int idArea)
		{
			bool result = false;

			try
			{
				DbAccess db = new DbAccess();

				using (NpgsqlCommand cmd = new NpgsqlCommand())
				{
					cmd.CommandText = @"INSERT INTO researchers_areas (id_researcher, id_area) " +
									  @"VALUES (@IdResearcher, @IdArea);";

					cmd.Parameters.AddWithValue("@IdResearcher", idResearcher);
					cmd.Parameters.AddWithValue("@IdArea", idArea);

					using (cmd.Connection = db.OpenConnection())
					{
						cmd.ExecuteNonQuery();
					}
				}

				result = true;
			}
			catch (Exception ex)
			{
				Log.Add(LogType.error, "[DbResearcher.RegisterResearcherAreas]: " + ex.Message);
			}

			return result;
		}

		public bool RegisterResearcherSubareas(int idResearcher, int idSubarea)
		{
			bool result = false;

			try
			{
				DbAccess db = new DbAccess();

				using (NpgsqlCommand cmd = new NpgsqlCommand())
				{
					cmd.CommandText = @"INSERT INTO researchers_subareas (id_researcher, id_subarea) " +
									  @"VALUES (@IdResearcher, @IdSubarea);";

					cmd.Parameters.AddWithValue("@IdResearcher", idResearcher);
					cmd.Parameters.AddWithValue("@IdSubarea", idSubarea);

					using (cmd.Connection = db.OpenConnection())
					{
						cmd.ExecuteNonQuery();
					}
				}

				result = true;
			}
			catch (Exception ex)
			{
				Log.Add(LogType.error, "[DbResearcher.RegisterResearcherSubareas]: " + ex.Message);
			}

			return result;
		}
	}
}
