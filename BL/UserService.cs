using BL.DTOs;
using BL.Models;
using DAL;
using Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class UserService
    {
        public static bool CheckLogin(string userName,out Guid token)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new Exception("Invalid Username.");

            using(var connection = new SqlConnection(Common.SettingsFileService.GetSetting("ConnectionStrings:Database")))
            {
                token = _setToken(_selectUser(userName, connection,null), connection);
            }
            return true;
        }
        public static UserEntity SelectUser(string userName, Guid? Token)
        {
            using (var connection = new SqlConnection(Common.SettingsFileService.GetSetting("ConnectionStrings:Database")))
            {
               return _selectUser(userName, connection, Token);
            }
        }

        public static UserEntity _selectUser(string userName, SqlConnection connection, Guid? Token)
        {
            UserEntity userEntity = null;

            using (SqlCommand cmd = connection.CreateCommand())
            {
                List<SqlParameter> commandParameters = new List<SqlParameter>();
                cmd.CommandText = "[dbo].[SELECT_USER]";
                if (!string.IsNullOrWhiteSpace(userName))
                {
                    commandParameters.Add(new SqlParameter("@Username", userName));
                    commandParameters.Add(new SqlParameter("@SelectByUserName", 1));
                }
                else if (Token.HasValue)
                {
                    commandParameters.Add(new SqlParameter("@Token", Token.Value));
                    commandParameters.Add(new SqlParameter("@SelectByToken", 1));
                }
                else
                {
                    throw new Exception("Invalid Username or Token");
                }

                cmd.Parameters.AddRange(commandParameters.ToArray());
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                bool wasOpen = connection.State == System.Data.ConnectionState.Open;
                if (!wasOpen)
                {
                    connection.Open();
                }
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    userEntity = new UserEntity();
                    foreach (var property in typeof(UserEntity).GetProperties())
                    {
                        var value = reader[property.Name];
                        if (value != DBNull.Value)
                        {
                             property.SetValue(userEntity, value);
                        }
                    }
                    reader.Close();
                }
                if (!wasOpen)
                {
                    connection.Close();
                }
            }
            return userEntity;
        }

        private static Guid _setToken(UserEntity user, SqlConnection connection)
        {
            if (user == null)
                throw new Exception("Invalid username");

            Guid newGuid = Guid.NewGuid();
            using (SqlCommand cmd = connection.CreateCommand())
            {
                List<SqlParameter> commandParameters = new List<SqlParameter>();
                cmd.CommandText = "[dbo].[LOGIN_USER]";
                commandParameters.Add(new SqlParameter("@UserId", user.Id));
                commandParameters.Add(new SqlParameter("@NewToken", newGuid.ToString()));
                commandParameters.Add(new SqlParameter("@TokenExp", DateTime.Now));

                cmd.Parameters.AddRange(commandParameters.ToArray());
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                bool wasOpen = connection.State == System.Data.ConnectionState.Open;
                if (!wasOpen)
                {
                    connection.Open();
                }
                cmd.ExecuteReader();
                if (!wasOpen)
                {
                    connection.Close();
                }
            }
            return newGuid;

        }

        public static void UpdateStatus(int userId, int? statusId)
        {
            using (var connection = new SqlConnection(Common.SettingsFileService.GetSetting("ConnectionStrings:Database")))
            {
                _updateStatus(userId, statusId, connection);
            }
        }
        public static void _updateStatus(int userId, int? statusId, SqlConnection connection)
        {
            using (SqlCommand cmd = connection.CreateCommand())
            {
                List<SqlParameter> commandParameters = new List<SqlParameter>();
                cmd.CommandText = "[dbo].[USER_UPDATE_STATUS]";
                if (!statusId.HasValue)
                {
                    throw new Exception("Invalid User or Status");
                }

                commandParameters.Add(new SqlParameter("@Status", statusId.Value));
                commandParameters.Add(new SqlParameter("@UserId", userId));
                cmd.Parameters.AddRange(commandParameters.ToArray());
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                bool wasOpen = connection.State == System.Data.ConnectionState.Open;
                if (!wasOpen)
                {
                    connection.Open();
                }
                cmd.ExecuteReader();
                if (!wasOpen)
                {
                    connection.Close();
                }
            }
        }

        public static List<UserDTO> SelectAllUsers(int? idNot,int? status,List<int> statusesList)
        {
            using (var connection = new SqlConnection(Common.SettingsFileService.GetSetting("ConnectionStrings:Database")))
            {
                return _selectAllUsers(idNot, status, statusesList, connection).Select(x=>ModelToDTO(EntityToModel(x))).ToList();
            }
        }

        public static List<UserEntity> _selectAllUsers(int? idNot,int? status, List<int> statusesList,SqlConnection connection)
        {
            List<UserEntity> userEntities = new List<UserEntity>();

            using (SqlCommand cmd = connection.CreateCommand())
            {
                List<SqlParameter> commandParameters = new List<SqlParameter>();
                cmd.CommandText = "[dbo].[SELECT_ALL_USERS]";
                if (idNot.HasValue)
                {
                    commandParameters.Add(new SqlParameter("@IdNot", idNot.Value));
                    commandParameters.Add(new SqlParameter("@SelectByIdNot", 1));
                }
                if (status.HasValue)
                {
                    commandParameters.Add(new SqlParameter("@Status", status.Value));
                    commandParameters.Add(new SqlParameter("@SelectByStatus", 1));
                }
                if (statusesList!=null && statusesList.Count>0)
                {
                    commandParameters.Add(new SqlParameter("@StatusesList", string.Join(",", statusesList)));
                    commandParameters.Add(new SqlParameter("@SelectByStatusesList", 1));
                }

                cmd.Parameters.AddRange(commandParameters.ToArray());
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                bool wasOpen = connection.State == System.Data.ConnectionState.Open;
                if (!wasOpen)
                {
                    connection.Open();
                }
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    UserEntity userEntity = new UserEntity();
                    foreach (var property in typeof(UserEntity).GetProperties())
                    {
                        var value = reader[property.Name];
                        if (value != DBNull.Value)
                        {
                            property.SetValue(userEntity, value);
                        }
                    }
                    userEntities.Add(userEntity);
                }
                reader.Close();

                if (!wasOpen)
                {
                    connection.Close();
                }
            }
            return userEntities;
        }

        public static UserModel EntityToModel(UserEntity entity)
        {
            return new UserModel()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                UserName = entity.UserName,
                StatusId = entity.StatusId,
            };
        }

        public static UserDTO ModelToDTO (UserModel model)
        {
            return new UserDTO()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                StatusId = model.StatusId,
                StatusName = WorkStatuses()[model.StatusId]
            };
        }
        public static Dictionary<int, string> WorkStatuses()
        {
            Dictionary<int, string> statues = new Dictionary<int, string>();
            statues.Add(1, "Working");
            statues.Add(2, "Working Remotely");
            statues.Add(3, "On Vacation");
            statues.Add(4, "Business Trip");
            return statues;
        }
    }


}
