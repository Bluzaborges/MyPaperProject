﻿using MyPaperProject.Global;
using MyPaperProject.Models;
using MyPaperProject.Models.Repositories;
using Npgsql;

namespace MyPaperProject.Database
{
	public class DbFundingPostgre : IFundingRepository
	{
        public List<Funding> GetAllFundings()
		{
			List<Funding> result = new List<Funding>();

			try
			{
				DbAccessPostgre db = new DbAccessPostgre();

				using (NpgsqlCommand cmd = new NpgsqlCommand())
				{
					cmd.CommandText = @"SELECT id, name FROM fundings " +
									  @"ORDER BY name;";

					using (cmd.Connection = db.OpenConnection())
					using (NpgsqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							Funding funding = new Funding();

							if (reader["id"] != DBNull.Value)
								funding.Id = Convert.ToInt32(reader["id"]);

							if (reader["name"] != DBNull.Value)
								funding.Name = reader["name"].ToString();

							result.Add(funding);
						}
					}
				}

			}
			catch (Exception ex)
			{
				Log.Add(LogType.error, "[DbFundingPostgre.GetAllFundings]: " + ex.Message);
			}

			return result;
		}
    }
}
