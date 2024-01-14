﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Conet.Models;

namespace Conet.Services
{
    public class UserInfoDAC
    {
        private readonly SqlConnection sql;
        public UserInfoDAC(SqlConnection sqlCon)
        {
            sql = sqlCon;
        }

        void PrepareSql(SqlCommand cmd, UserInfo user)
        {
            SqlParameter paramNickName = new("NickName", SqlDbType.NChar, 10);
            SqlParameter paramID = new("ID", SqlDbType.Char, 20);
            SqlParameter paramPassword = new("Password", SqlDbType.Char, 64);
            SqlParameter paramRegion = new("Region", SqlDbType.NVarChar, 20);

            paramNickName.Value = user.NickName;
            paramID.Value = user.ID;
            paramPassword.Value = user.Password;
            paramRegion.Value = user.Region;

            cmd.Parameters.Add(paramNickName);
            cmd.Parameters.Add(paramID);
            cmd.Parameters.Add(paramPassword);
            cmd.Parameters.Add(paramRegion);
        }

        public async Task InsertAsync(UserInfo user)
        {
            await ExcuteSqlNonQueryAsync(user, @"INSERT INTO UserInfo(NickName, ID, Password, Region)
    VALUES (@NickName, @ID, @Password, @Region)");
        }

        public async Task Update(UserInfo user)
        {
            await ExcuteSqlNonQueryAsync(user,
                @"UPDATE UserInfo SET NickName=@NickName, Password=@Password, Region=@Region WHERE ID=@ID");
        }

        public async Task DeleteAsync(UserInfo user)
        {
            await ExcuteSqlNonQueryAsync(user, @"DELETE FROM UserInfo WHERE ID=@ID");
        }

        private async Task ExcuteSqlNonQueryAsync(UserInfo user, string txt)
        {
            SqlCommand cmd = new(txt, sql);
            PrepareSql(cmd, user);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<bool> IDExistsAsync(string ID)
        {
            string txt = "SELECT COUNT(*) FROM UserInfo WHERE ID = @ID";

            SqlCommand cmd = new(txt, sql);
            SqlParameter paramID = new("ID", SqlDbType.Char, 20);
            paramID.Value = ID;
            cmd.Parameters.Add(paramID);

            return await cmd.ExecuteScalarAsync() is int i && i != 0;
        }

        public async Task<bool> NickNameExistsAsync(string NickName)
        {
            string txt = @"SELECT COUNT(*) FROM UserInfo WHERE NickName = @NickName";

            SqlCommand cmd = new(txt, sql);
            SqlParameter paramNickName = new("NickName", SqlDbType.NChar, 10);
            paramNickName.Value = NickName;
            cmd.Parameters.Add(paramNickName);

            return await cmd.ExecuteScalarAsync() is int i && i != 0;
        }

        public async Task<UserInfo> GetUserAsync(string ID)
        {
            string txt = "SELECT * FROM UserInfo WHERE ID = @ID";

            SqlCommand cmd = new(txt, sql);
            SqlParameter paramID = new("ID", SqlDbType.Char, 20);
            paramID.Value = ID;
            cmd.Parameters.Add(paramID);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            reader.Read();
            return new(reader.GetString(0).Trim(), ID); // 굳이 Password는 넣지 않음.
        }
    }
}
