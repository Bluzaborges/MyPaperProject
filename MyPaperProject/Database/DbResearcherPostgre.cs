using MyPaperProject.Global;
using MyPaperProject.Models;
using MyPaperProject.Models.Repositories;
using Npgsql;
using System.Xml.Linq;

namespace MyPaperProject.Database
{
	public class DbResearcherPostgre : IResearcherRepository
	{
		public Researcher GetResearcherById(int id)
		{
			Researcher result = new Researcher();

			try
			{
				DbAccessPostgre db = new DbAccessPostgre();

				using (NpgsqlCommand cmd = new NpgsqlCommand())
				{
					cmd.CommandText = @"SELECT * FROM researchers " +
									  @"WHERE id = @Id AND deleted = false;";

					cmd.Parameters.AddWithValue("@Id", id);

					using (cmd.Connection = db.OpenConnection())
					using (NpgsqlDataReader reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							if (reader["name"] != DBNull.Value)
								result.Name = reader["name"].ToString();

							if (reader["cpf"] != DBNull.Value)
								result.Cpf = reader["cpf"].ToString();

							if (reader["type"] != DBNull.Value)
								result.Type = reader["type"].ToString();
						}
					}
				}

			}
			catch (Exception ex)
			{
				Log.Add(LogType.error, "[DbResearcher.GetResearcherById]: " + ex.Message);
			}

			return result;
		}

		public bool ResearcherExists(string name, string cpf)
		{
			bool result = false;
			int count = 0;

			try
			{
				DbAccessPostgre db = new DbAccessPostgre();

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
				DbAccessPostgre db = new DbAccessPostgre();

				using (NpgsqlCommand cmd = new NpgsqlCommand())
				{
					cmd.CommandText = @"INSERT INTO researchers (name, cpf, type, creation_date) " +
									  @"VALUES (@Name, @Cpf, @Type, @CreationDate) RETURNING id;";

					cmd.Parameters.AddWithValue("@Name", researcher.Name);
					cmd.Parameters.AddWithValue("@Cpf", researcher.Cpf);
					cmd.Parameters.AddWithValue("@Type", researcher.Type);
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
				Log.Add(LogType.error, "[DbResearcher.RegisterResearcher]: " + ex.Message);
			}

			return result;
		}

		public bool RegisterResearcherAreas(int idResearcher, int idArea)
		{
			bool result = false;

			try
			{
				DbAccessPostgre db = new DbAccessPostgre();

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
				DbAccessPostgre db = new DbAccessPostgre();

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

		public List<Researcher> GetAllResearchers()
		{
			List<Researcher> result = new List<Researcher>();

			try
			{
				DbAccessPostgre db = new DbAccessPostgre();

				using (NpgsqlCommand cmd = new NpgsqlCommand())
				{
					cmd.CommandText = @"SELECT * FROM researchers " +
									  @"WHERE deleted = false " +
									  @"ORDER BY name;";

					using (cmd.Connection = db.OpenConnection())
					using (NpgsqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							Researcher researcher = new Researcher();

							if (reader["id"] != DBNull.Value)
								researcher.Id = Convert.ToInt32(reader["id"]);

							if (reader["name"] != DBNull.Value)
								researcher.Name = reader["name"].ToString();

							if (reader["cpf"] != DBNull.Value)
								researcher.Cpf = reader["cpf"].ToString();

							if (reader["type"] != DBNull.Value)
								researcher.Type = reader["type"].ToString();

							result.Add(researcher);
						}
					}
				}

			}
			catch (Exception ex)
			{
				Log.Add(LogType.error, "[DbResearcher.GetAllResearchers]: " + ex.Message);
			}

			return result;
		}

		public List<Researcher> GetAllResearchersAreas()
		{
			List<Researcher> result = new List<Researcher>();

			try
			{
				DbAccessPostgre db = new DbAccessPostgre();

				using (NpgsqlCommand cmd = new NpgsqlCommand())
				{
					cmd.CommandText = @"SELECT ra.id_researcher, ra.id_area, r.name, r.type " +
									  @"FROM researchers_areas AS ra, researchers AS r " +
									  @"WHERE r.id = ra.id_researcher " +
									  @"AND r.deleted = false;";

					using (cmd.Connection = db.OpenConnection())
					using (NpgsqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							Researcher researcher = new Researcher();

							if (reader["id_researcher"] != DBNull.Value)
								researcher.Id = Convert.ToInt32(reader["id_researcher"]);

							if (reader["id_area"] != DBNull.Value)
								researcher.IdArea = Convert.ToInt32(reader["id_area"]);

							if (reader["name"] != DBNull.Value)
								researcher.Name = reader["name"].ToString();

							if (reader["type"] != DBNull.Value)
								researcher.Type = reader["type"].ToString();

							result.Add(researcher);
						}
					}
				}

			}
			catch (Exception ex)
			{
				Log.Add(LogType.error, "[DbResearcher.GetAllResearchersAreas]: " + ex.Message);
			}

			return result;
		}

        public List<Researcher> GetAllResearchersByIdProject(int idProject)
        {
            List<Researcher> result = new List<Researcher>();

            try
            {
                DbAccessPostgre db = new DbAccessPostgre();

                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"SELECT r.id, r.name, r.type " +
                                      @"FROM projects_researchers AS pr, researchers AS r " +
                                      @"WHERE pr.id_project = @IdProject AND pr.id_researcher = r.id " +
                                      @"ORDER BY r.name;";

                    cmd.Parameters.AddWithValue("IdProject", idProject);

                    using (cmd.Connection = db.OpenConnection())
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
							Researcher researcher = new Researcher();

							if (reader["id"] != DBNull.Value)
								researcher.Id = Convert.ToInt32(reader["id"]);

							if (reader["name"] != DBNull.Value)
                                researcher.Name = reader["name"].ToString();

                            if (reader["type"] != DBNull.Value)
                                researcher.Type = reader["type"].ToString();

							result.Add(researcher);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Add(LogType.error, "[DbResearcher.GetAllResearchersByIdProject]: " + ex.Message);
            }

            return result;
        }

		public bool UpdateResearcher(Researcher researcher)
		{
			bool result = false;
			DbAccessPostgre db = new DbAccessPostgre();

			try
			{
				using (NpgsqlCommand cmd = new NpgsqlCommand())
				{
					cmd.CommandText = @"UPDATE researchers " +
									  @"SET name = @Name, cpf = @Cpf " +
									  @"WHERE id = @Id;";

					cmd.Parameters.AddWithValue("@Id", researcher.Id);
					cmd.Parameters.AddWithValue("@Name", researcher.Name);
					cmd.Parameters.AddWithValue("@Cpf", researcher.Cpf);

					using (cmd.Connection = db.OpenConnection())
					{
						cmd.ExecuteNonQuery();
					}
				}

				result = true;
			}
			catch (Exception ex)
			{
				Log.Add(LogType.error, "[DbResearcher.UpdateResearcher]: " + ex.Message);
			}

			return result;
		}

		public bool ResearcherHaveProject(int idResearcher)
		{
			bool result = false;
			int count = 0;

			try
			{
				DbAccessPostgre db = new DbAccessPostgre();

				using (NpgsqlCommand cmd = new NpgsqlCommand())
				{
					cmd.CommandText = @"SELECT COUNT(*) FROM projects_researchers " +
									  @"WHERE id_researcher = @IdResearcher;";

					cmd.Parameters.AddWithValue("@IdResearcher", idResearcher);

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
				Log.Add(LogType.error, "[DbResearcher.ResearcherHaveProject]: " + ex.Message);
			}

			return result;
		}

		public bool DeleteResearcherById(int id)
		{
			bool result = false;
			DbAccessPostgre db = new DbAccessPostgre();

			try
			{
				using (NpgsqlCommand cmd = new NpgsqlCommand())
				{
					cmd.CommandText = @"UPDATE researchers " +
									  @"SET deleted = true, deleted_date = @DeletedDate " +
									  @"WHERE id = @Id;";

					cmd.Parameters.AddWithValue("@Id", id);
					cmd.Parameters.AddWithValue("@DeletedDate", DateTime.Now);

					using (cmd.Connection = db.OpenConnection())
					{
						cmd.ExecuteNonQuery();
					}
				}

				result = true;
			}
			catch (Exception ex)
			{
				Log.Add(LogType.error, "[DbResearcher.DeleteResearcherById]: " + ex.Message);
			}

			return result;
		}
	}
}
