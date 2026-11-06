using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace WindowsFormsApplication1
{
    public class MonAnInfo
    {
        public int Id;
        public string TenMonAn;
        public string NguoiThem;
    }

   
    public class Bai5Database
    {
        private readonly string connectionString;
        private readonly object dbLock = new object();

        public Bai5Database(string dbPath)
        {
            connectionString = "Data Source=" + dbPath + ";Version=3;";
            CreateTables();
        }

        private SQLiteConnection OpenConnection()
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            return connection;
        }

        private void CreateTables()
        {
            lock (dbLock)
            {
                using (SQLiteConnection connection = OpenConnection())
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                        "CREATE TABLE IF NOT EXISTS NguoiDung (" +
                        "  Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                        "  TenNguoiDung TEXT NOT NULL UNIQUE COLLATE NOCASE);" +
                        "CREATE TABLE IF NOT EXISTS MonAn (" +
                        "  Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                        "  TenMonAn TEXT NOT NULL," +
                        "  IdNguoiDung INTEGER NOT NULL," +
                        "  FOREIGN KEY (IdNguoiDung) REFERENCES NguoiDung(Id));";
                    command.ExecuteNonQuery();
                }
            }
        }

  
        public int GetOrCreateUser(string userName)
        {
            lock (dbLock)
            {
                using (SQLiteConnection connection = OpenConnection())
                {
                    using (SQLiteCommand insert = connection.CreateCommand())
                    {
                        insert.CommandText = "INSERT OR IGNORE INTO NguoiDung (TenNguoiDung) VALUES (@ten)";
                        insert.Parameters.AddWithValue("@ten", userName);
                        insert.ExecuteNonQuery();
                    }
                    using (SQLiteCommand select = connection.CreateCommand())
                    {
                        select.CommandText = "SELECT Id FROM NguoiDung WHERE TenNguoiDung = @ten";
                        select.Parameters.AddWithValue("@ten", userName);
                        return Convert.ToInt32(select.ExecuteScalar());
                    }
                }
            }
        }


        public bool AddMonAn(string tenMonAn, int userId)
        {
            lock (dbLock)
            {
                using (SQLiteConnection connection = OpenConnection())
                {
                    using (SQLiteCommand check = connection.CreateCommand())
                    {
                        check.CommandText =
                            "SELECT COUNT(*) FROM MonAn WHERE IdNguoiDung = @u AND TenMonAn = @t COLLATE NOCASE";
                        check.Parameters.AddWithValue("@u", userId);
                        check.Parameters.AddWithValue("@t", tenMonAn);
                        if (Convert.ToInt32(check.ExecuteScalar()) > 0)
                            return false;
                    }
                    using (SQLiteCommand insert = connection.CreateCommand())
                    {
                        insert.CommandText = "INSERT INTO MonAn (TenMonAn, IdNguoiDung) VALUES (@t, @u)";
                        insert.Parameters.AddWithValue("@t", tenMonAn);
                        insert.Parameters.AddWithValue("@u", userId);
                        insert.ExecuteNonQuery();
                        return true;
                    }
                }
            }
        }


        public bool DeleteMonAn(int monAnId, int userId)
        {
            lock (dbLock)
            {
                using (SQLiteConnection connection = OpenConnection())
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM MonAn WHERE Id = @id AND IdNguoiDung = @u";
                    command.Parameters.AddWithValue("@id", monAnId);
                    command.Parameters.AddWithValue("@u", userId);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

    
        public List<MonAnInfo> GetMonAn(int? userId)
        {
            return Query(userId, false);
        }

     
        public MonAnInfo GetRandomMonAn(int? userId)
        {
            List<MonAnInfo> result = Query(userId, true);
            return result.Count > 0 ? result[0] : null;
        }

        private List<MonAnInfo> Query(int? userId, bool random)
        {
            List<MonAnInfo> list = new List<MonAnInfo>();
            lock (dbLock)
            {
                using (SQLiteConnection connection = OpenConnection())
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    string sql =
                        "SELECT m.Id, m.TenMonAn, n.TenNguoiDung " +
                        "FROM MonAn m JOIN NguoiDung n ON m.IdNguoiDung = n.Id";
                    if (userId.HasValue)
                    {
                        sql += " WHERE m.IdNguoiDung = @u";
                        command.Parameters.AddWithValue("@u", userId.Value);
                    }
                    sql += random ? " ORDER BY RANDOM() LIMIT 1" : " ORDER BY m.Id";
                    command.CommandText = sql;

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MonAnInfo info = new MonAnInfo();
                            info.Id = reader.GetInt32(0);
                            info.TenMonAn = reader.GetString(1);
                            info.NguoiThem = reader.GetString(2);
                            list.Add(info);
                        }
                    }
                }
            }
            return list;
        }
    }
}
