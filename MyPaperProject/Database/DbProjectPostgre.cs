using MyPaperProject.Global;
using MyPaperProject.Models;
using Npgsql;

namespace MyPaperProject.Database
{
	public class DbProjectPostgre
	{
        public List<Project> GetAllProjects()
        {
            List<Project> result = new List<Project>();

            try
            {
                DbAccessPostgre db = new DbAccessPostgre();

                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"SELECT id, id_funding, name, funded, ended, ended_date, creation_date FROM projects " +
                                      @"WHERE deleted = false " +
                                      @"ORDER BY name;";

                    using (cmd.Connection = db.OpenConnection())
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Project project = new Project();

                            if (reader["id"] != DBNull.Value)
                                project.Id = Convert.ToInt32(reader["id"]);

                            if (reader["id_funding"] != DBNull.Value)
                                project.IdFunding = Convert.ToInt32(reader["id_funding"]);

                            if (reader["name"] != DBNull.Value)
                                project.Name = reader["name"].ToString();

                            if (reader["funded"] != DBNull.Value)
                                project.Funded = Convert.ToBoolean(reader["funded"]);

                            if (reader["ended"] != DBNull.Value)
                                project.Ended = Convert.ToBoolean(reader["ended"]);

                            if (reader["ended_date"] != DBNull.Value)
                                project.EndedDate = Convert.ToDateTime(reader["ended_date"]);

                            if (reader["creation_date"] != DBNull.Value)
                                project.CreationDate = Convert.ToDateTime(reader["creation_date"]);

                            result.Add(project);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Add(LogType.error, "[DbProjectPostgre.GetAllProjects]: " + ex.Message);
            }

            return result;
        }

		public Project GetProjectById(int id)
		{
			Project result = new Project();

			try
			{
				DbAccessPostgre db = new DbAccessPostgre();

				using (NpgsqlCommand cmd = new NpgsqlCommand())
				{
					cmd.CommandText = @"SELECT id_funding, name, description, funded, ended, ended_date FROM projects " +
									  @"WHERE id = @Id AND deleted = false;";

					cmd.Parameters.AddWithValue("Id", id);

					using (cmd.Connection = db.OpenConnection())
					using (NpgsqlDataReader reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							if (reader["id_funding"] != DBNull.Value)
								result.IdFunding = Convert.ToInt32(reader["id_funding"]);

							if (reader["name"] != DBNull.Value)
								result.Name = reader["name"].ToString();

							if (reader["description"] != DBNull.Value)
								result.Description = reader["description"].ToString();

							if (reader["funded"] != DBNull.Value)
								result.Funded = Convert.ToBoolean(reader["funded"]);

							if (reader["ended"] != DBNull.Value)
								result.Ended = Convert.ToBoolean(reader["ended"]);

							if (reader["ended_date"] != DBNull.Value)
								result.EndedDate = Convert.ToDateTime(reader["ended_date"]);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Add(LogType.error, "[DbProjectPostgre.GetProjectById]: " + ex.Message);
			}

			return result;
		}

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

        public bool RegisterProjectsAreas(int idProject, int idArea)
        {
            bool result = false;

            try
            {
                DbAccessPostgre db = new DbAccessPostgre();

                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"INSERT INTO projects_areas (id_project, id_area) " +
                                      @"VALUES (@Idproject, @IdArea);";

                    cmd.Parameters.AddWithValue("@Idproject", idProject);
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
                Log.Add(LogType.error, "[DbProjectPostgre.RegisterProjectsAreas]: " + ex.Message);
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

		public bool RegisterProjectsResults(int idProject, int idResult)
		{
			bool result = false;

			try
			{
				DbAccessPostgre db = new DbAccessPostgre();

				using (NpgsqlCommand cmd = new NpgsqlCommand())
				{
					cmd.CommandText = @"INSERT INTO projects_results (id_project, id_result) " +
									  @"VALUES (@Idproject, @IdResult);";

					cmd.Parameters.AddWithValue("@Idproject", idProject);
					cmd.Parameters.AddWithValue("@IdResult", idResult);

					using (cmd.Connection = db.OpenConnection())
					{
						cmd.ExecuteNonQuery();
					}
				}

				result = true;
			}
			catch (Exception ex)
			{
				Log.Add(LogType.error, "[DbProjectPostgre.RegisterProjectsResults]: " + ex.Message);
			}

			return result;
		}

		public bool UpdateProject(Project project)
		{
			bool result = false;
			DbAccessPostgre db = new DbAccessPostgre();

			try
			{
				using (NpgsqlCommand cmd = new NpgsqlCommand())
				{
					cmd.CommandText = @"UPDATE projects " +
									  @"SET id_funding = @IdFunding, name = @Name, description = @Description, " + 
									  @"funded = @Funded, ended = @Ended, ended_date = @EndedDate " +
									  @"WHERE id = @Id;";

					cmd.Parameters.AddWithValue("@Id", project.Id);
					cmd.Parameters.AddWithValue("@Name", project.Name);
					cmd.Parameters.AddWithValue("@IdFunding", project.IdFunding);
					cmd.Parameters.AddWithValue("@Description", project.Description);
					cmd.Parameters.AddWithValue("@Funded", project.Funded);
					cmd.Parameters.AddWithValue("@Ended", project.Ended);
					cmd.Parameters.AddWithValue("@EndedDate", project.EndedDate);

					using (cmd.Connection = db.OpenConnection())
					{
						cmd.ExecuteNonQuery();
					}
				}

				result = true;
			}
			catch (Exception ex)
			{
				Log.Add(LogType.error, "[DbProjectPostgre.UpdateProject]: " + ex.Message);
			}

			return result;
		}
	}
}
