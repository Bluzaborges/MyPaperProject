using MyPaperProject.Global;
using MyPaperProject.Models;
using Npgsql;

namespace MyPaperProject.Database
{
	public class DbProjectPostgre
	{
		public bool ProjectExists(string name)
		{
			bool result = false;
			int count = 0;

			try
			{
				DbAccessPostgre db = new DbAccessPostgre();

				using (NpgsqlCommand cmd = new NpgsqlCommand())
				{
					cmd.CommandText = @"SELECT COUNT(*) FROM projects " +
									  @"WHERE name = @Name;";

					cmd.Parameters.AddWithValue("@Name", name);

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
				Log.Add(LogType.error, "[DbProjectPostgre.ProjectExists]: " + ex.Message);
			}

			return result;
		}

		public int RegisterProject(Project project)
		{
			int result = 0;

			try
			{
				DbAccessPostgre db = new DbAccessPostgre();

				using (NpgsqlCommand cmd = new NpgsqlCommand())
				{
					cmd.CommandText = @"INSERT INTO projects (name, id_funding, description, funded, ended, ended_date, creation_date) " +
									  @"VALUES (@Name, @IdFunding, @Description, @Funded, @Ended, @EndedDate, @CreationDate) RETURNING id;";

					cmd.Parameters.AddWithValue("@Name", project.Name);
					cmd.Parameters.AddWithValue("@IdFunding", project.IdFunding);
					cmd.Parameters.AddWithValue("@Description", project.Description);
					cmd.Parameters.AddWithValue("@Funded", project.Funded);
					cmd.Parameters.AddWithValue("@Ended", project.Ended);
					cmd.Parameters.AddWithValue("@EndedDate", project.EndedDate);
					cmd.Parameters.AddWithValue("@CreationDate", DateTime.Now);

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
				Log.Add(LogType.error, "[DbProjectPostgre.RegisterProject]: " + ex.Message);
			}

			return result;
		}

		public bool RegisterProjectsResearchers(int idProject, int idResearcher)
		{
			bool result = false;

			try
			{
				DbAccessPostgre db = new DbAccessPostgre();

				using (NpgsqlCommand cmd = new NpgsqlCommand())
				{
					cmd.CommandText = @"INSERT INTO projects_researchers (id_project, id_researcher) " +
									  @"VALUES (@Idproject, @IdResearcher);";

					cmd.Parameters.AddWithValue("@Idproject", idProject);
					cmd.Parameters.AddWithValue("@IdResearcher", idResearcher);

					using (cmd.Connection = db.OpenConnection())
					{
						cmd.ExecuteNonQuery();
					}
				}

				result = true;
			}
			catch (Exception ex)
			{
				Log.Add(LogType.error, "[DbProjectPostgre.RegisterProjectsResearchers]: " + ex.Message);
			}

			return result;
		}
	}
}
