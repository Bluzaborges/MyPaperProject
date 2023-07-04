using MyPaperProject.Global;
using MyPaperProject.Models;
using Npgsql;
using System.Reflection;

namespace MyPaperProject.Database
{
	public class DbResultPostgre
	{
		public Result GetResultById(int idResult)
		{
			Result result = new Result();

			try
			{
				DbAccessPostgre db = new DbAccessPostgre();

				using (NpgsqlCommand cmd = new NpgsqlCommand())
				{
					cmd.CommandText = @"SELECT name, description, creation_date FROM results " +
									  @"WHERE id = @Id;";

					cmd.Parameters.AddWithValue("Id", idResult);

					using (cmd.Connection = db.OpenConnection())
					using (NpgsqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							if (reader["name"] != DBNull.Value)
								result.Name = reader["name"].ToString();

							if (reader["description"] != DBNull.Value)
								result.Description = reader["description"].ToString();

							if (reader["creation_date"] != DBNull.Value)
								result.CreationDate = Convert.ToDateTime(reader["creation_date"]);
						}
					}
				}

			}
			catch (Exception ex)
			{
				Log.Add(LogType.error, "[DbResultPostgre.GetResultById]: " + ex.Message);
			}

			return result;
		}

		public List<Attachment> GetAttachmentsByIdResult(int idResult)
		{
            List<Attachment> attachments = new List<Attachment>();

            try
            {
                DbAccessPostgre db = new DbAccessPostgre();

                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
					cmd.CommandText = @"SELECT id, name FROM attachments " +
									  @"WHERE id_result = @IdResult;";

                    cmd.Parameters.AddWithValue("IdResult", idResult);

                    using (cmd.Connection = db.OpenConnection())
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Attachment attachment = new Attachment();

                            if (reader["id"] != DBNull.Value)
                                attachment.Id = Convert.ToInt32(reader["id"]);

                            if (reader["name"] != DBNull.Value)
                                attachment.Name = reader["name"].ToString();

                            attachments.Add(attachment);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Add(LogType.error, "[DbResultPostgre.GetAttachmentsByIdResult]: " + ex.Message);
            }

            return attachments;
        }

		public int RegisterResult(Result result)
		{
			int response = 0;

			try
			{
				DbAccessPostgre db = new DbAccessPostgre();

				using (NpgsqlCommand cmd = new NpgsqlCommand())
				{
					cmd.CommandText = @"INSERT INTO results (name, description, creation_date) " +
									  @"VALUES (@Name, @Description, @CreationDate) RETURNING id;";

					cmd.Parameters.AddWithValue("@Name", result.Name);
					cmd.Parameters.AddWithValue("@Description", result.Description);
					cmd.Parameters.AddWithValue("@CreationDate", DateTime.Now);

					using (cmd.Connection = db.OpenConnection())
					using (NpgsqlDataReader reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							if (reader["id"] != DBNull.Value)
								response = Convert.ToInt32(reader["id"]);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Add(LogType.error, "[DbResultPostgre.RegisterResult]: " + ex.Message);
			}

			return response;
		}

		public bool RegisterAttachment(int idResult, Attachment attachment)
		{
			bool response = false;

			try
			{
				DbAccessPostgre db = new DbAccessPostgre();

				using (NpgsqlCommand cmd = new NpgsqlCommand())
				{
					cmd.CommandText = @"INSERT INTO attachments (id_result, name, content) " +
									  @"VALUES (@IdResult, @Name, @Content);";

					cmd.Parameters.AddWithValue("@IdResult", idResult);
					cmd.Parameters.AddWithValue("@Name", attachment.Name);
					cmd.Parameters.AddWithValue("@Content", attachment.Content);

					using (cmd.Connection = db.OpenConnection())
					{
						cmd.ExecuteNonQuery();
					}
				}

				response = true;
			}
			catch (Exception ex)
			{
				Log.Add(LogType.error, "[DbResultPostgre.RegisterAttachment]: " + ex.Message);
			}

			return response;
		}
	}
}
