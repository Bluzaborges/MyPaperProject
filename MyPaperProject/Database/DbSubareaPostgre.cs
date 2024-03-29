﻿using MyPaperProject.Global;
using MyPaperProject.Models;
using MyPaperProject.Models.Repositories;
using Npgsql;

namespace MyPaperProject.Database
{
	public class DbSubareaPostgre : ISubareaRepository
	{
		public List<Subarea> GetAllSubareas()
		{
			List<Subarea> result = new List<Subarea>();

			try
			{
				DbAccessPostgre db = new DbAccessPostgre();

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
				Log.Add(LogType.error, "[DbSubareaPostgre.GetAllSubareas]: " + ex.Message);
			}

			return result;
		}

        public List<Subarea> GetAllSubareasByIdResearcher(int idResearcher)
        {
            List<Subarea> result = new List<Subarea>();

            try
            {
                DbAccessPostgre db = new DbAccessPostgre();

                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"SELECT s.id, s.name " +
                                      @"FROM researchers_subareas AS rs, subareas AS s " +
                                      @"WHERE rs.id_researcher = @IdResearcher AND rs.id_subarea = s.id " +
                                      @"ORDER BY s.name;";

                    cmd.Parameters.AddWithValue("IdResearcher", idResearcher);

                    using (cmd.Connection = db.OpenConnection())
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
							Subarea subarea = new Subarea();

							if (reader["id"] != DBNull.Value)
								subarea.Id = Convert.ToInt32(reader["id"]);

							if (reader["name"] != DBNull.Value)
                                subarea.Name = reader["name"].ToString();

							result.Add(subarea);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Add(LogType.error, "[DbSubareaPostgre.GetAllSubareasNamesByIdResearcher]: " + ex.Message);
            }

            return result;
        }
    }
}
