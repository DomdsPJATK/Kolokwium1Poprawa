using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using Kolokwium1Poprawa.DTOs.Response;
using Kolokwium1Poprawa.Exceptions;

namespace Kolokwium1Poprawa.Services
{
    public class SqlServerServiceDataBase : IServiceDataBase
    {
        private const string ConnectionString = "Data Source=db-mssql;Initial Catalog=s19036;Integrated Security=True";

        public GetMemberResponse GetMember(int id)
        {
            GetMemberResponse memberResponse = null;
            using (var connection = new SqlConnection(ConnectionString))
            using (var com = new SqlCommand())
            {
                com.Connection = connection;
                connection.Open();

                com.CommandText = "SELECT * FROM TeamMember WHERE IdTeamMember = @idMember";
                com.Parameters.Clear();
                com.Parameters.AddWithValue("idMember", id);

                var dataReader = com.ExecuteReader();
                if (!dataReader.Read()) throw new MemberNotExistException($"Czlonek o Id: {id} nie istnieje!");
                memberResponse = new GetMemberResponse()
                {
                    IdTeamMember = id,
                    FirstName = dataReader["FirstName"].ToString(),
                    LastName = dataReader["LastName"].ToString(),
                    Email = dataReader["Email"].ToString(),
                    Tasks = new List<GetTaskMemberResponse>()
                };

                dataReader.Close();

                com.CommandText =
                    "SELECT * FROM Task as tk, Project as pro, TaskType as tt WHERE tk.IdTaskType = tt.IdTaskType AND tk.IdTeam = pro.IdTeam AND tk.IdCreator = @idMember";
                com.Parameters.Clear();
                com.Parameters.AddWithValue("idMember", id);

                dataReader = com.ExecuteReader();
                while (dataReader.Read())
                {
                    memberResponse.Tasks.Add(new GetTaskMemberResponse()
                    {
                        Name = dataReader["tk.Name"].ToString(),
                        Description = dataReader["Description"].ToString(),
                        Deadline = Convert.ToDateTime(dataReader["Deadline"].ToString()),
                        ProjectName = dataReader["pro.Name"].ToString(),
                        Type = dataReader["tt.Name"].ToString()
                    });
                }

                dataReader.Close();

                return memberResponse;
            }
        }

        public void DeleteProject(int id)
        {
            using (var connection = new SqlConnection())
            using (var com = new SqlCommand())
            {
                connection.ConnectionString = ConnectionString;
                com.Connection = connection;
                connection.Open();

                try
                {
                    com.Transaction = connection.BeginTransaction();

                    com.CommandText = "SELECT * FROM Project WHERE IdTeam = @id";
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("id", id);

                    var dataReader = com.ExecuteReader();

                    if (!dataReader.Read())
                    {
                        dataReader.Close();
                        throw new ProjectDoesNotExistException($"Projekt o id: {id} nie istnieje!");
                    }

                    dataReader.Close();

                    com.CommandText = "SELECT IdTask FROM Task WHERE IdTeam = @id";
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("id", id);

                    dataReader = com.ExecuteReader();

                    while (dataReader.Read())
                    {
                        int taskId = Convert.ToInt32(dataReader["IdTask"].ToString());
                        com.CommandText = $"DELETE FROM Task WHERE IdTask = {taskId}";
                        com.ExecuteNonQuery();
                    }

                    dataReader.Close();

                    com.CommandText = "DELETE FROM Project WHERE IdTeam = @id";
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("id", id);
                    com.ExecuteNonQuery();

                    com.Transaction.Commit();
                }
                catch (SqlException e)
                {
                    com.Transaction.Rollback();
                }
            }
        }
    }
}