using CURSACH.View;
using CURSACH.Classes;

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Data.SQLite;


namespace CURSACH
{
    internal class DatabaseManager
    {
        private const string ConnectionString = "Data Source=TecServis.db;Version=3;";

        public static int GetMidTimeOfDoneRequestsByType(string type)
        {
            int midTime = 0;

            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();

                    string query = @"
                SELECT AVG(completionDate - startDate)
                FROM Requests
                WHERE requestStatus = 'Готова к выдаче'
                AND homeTechType = @type";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@type", type);

                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            midTime = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Здесь можно добавить логирование ошибки
                Console.WriteLine($"Error getting mid time of done requests by type: {ex.Message}");
            }

            return midTime;
        }

        public static int GetNumberOfDoneRequestsByType(string type)
        {
            int numberOfDoneRequests = 0;

            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();

                    string query = @"
                SELECT COUNT(*)
                FROM Requests
                WHERE requestStatus = 'Готова к выдаче'
                AND homeTechType = @type";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@type", type);

                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            numberOfDoneRequests = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Здесь можно добавить логирование ошибки
                Console.WriteLine($"Error getting number of done requests by type: {ex.Message}");
            }

            return numberOfDoneRequests;
        }

        public static int GetMidTimeByMasterId(int masterId)
        {
            int midTime = 0;

            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();

                    string query = @"
                SELECT AVG(julianday(completionDate) - julianday(startDate))
                FROM Requests
                WHERE masterID = @masterId
                AND requestStatus = 'Завершена'";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@masterId", masterId);

                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            midTime = Convert.ToInt32(Math.Round(Convert.ToDouble(result)));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Здесь можно добавить логирование ошибки
                Console.WriteLine($"Error getting mid time by master ID: {ex.Message}");
            }

            return midTime;
        }

        public static int GetDoneRequestsByMasterId(int masterId)
        {
            int count = 0;

            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();

                    string query = @"
                SELECT COUNT(*)
                FROM Requests
                WHERE masterID = @masterId
                AND requestStatus = 'Готова к выдаче'";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@masterId", masterId);

                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            count = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Здесь можно добавить логирование ошибки
                Console.WriteLine($"Error getting done requests by master ID: {ex.Message}");
            }

            return count;
        }

        public static int GetCurrentRequestsByMasterId(int masterId)
        {
            int count = 0;

            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();

                    string query = @"
                SELECT COUNT(*)
                FROM Requests
                WHERE masterID = @masterId
                AND requestStatus = 'В процессе ремонта'";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@masterId", masterId);

                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            count = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Здесь можно добавить логирование ошибки
                Console.WriteLine($"Error getting current requests by master ID: {ex.Message}");
            }

            return count;
        }

        public static List<Request> GetUnassignedRequests()
        {
            List<Request> unassignedRequests = new List<Request>();

            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();

                    string query = @"
                SELECT requestID, startDate, homeTechType, homeTechModel, problemDescryption, requestStatus, completionDate, repairParts, masterID, clientID
                FROM Requests
                WHERE masterID IS NULL";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var request = new Request
                                {
                                    requestID = reader.GetInt32(0),
                                    startDate = (DateTime)(reader.IsDBNull(1) ? (DateTime?)null : reader.GetDateTime(1)),
                                    homeTechType = reader.IsDBNull(2) ? null : reader.GetString(2),
                                    homeTechModel = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    problemDescryption = reader.IsDBNull(4) ? null : reader.GetString(4),
                                    requestStatus = reader.IsDBNull(5) ? null : reader.GetString(5),
                                    completionDate = (DateTime)(reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6)),
                                    repairParts = reader.IsDBNull(7) ? null : reader.GetString(7),
                                    masterID = reader.IsDBNull(8) ? (int?)null : reader.GetInt32(8),
                                    clientID = reader.GetInt32(9)
                                };

                                unassignedRequests.Add(request);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Здесь можно добавить логирование ошибки
                Console.WriteLine($"Error getting unassigned requests: {ex.Message}");
            }

            return unassignedRequests;
        }

        public static List<Request> GetAllRequests()
        {
            List<Request> requests = new List<Request>();

            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();

                string query = "SELECT * FROM Requests";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Request request = new Request
                            {
                                requestID = Convert.ToInt32(reader["requestID"]),
                                startDate = reader.IsDBNull(reader.GetOrdinal("startDate")) ? DateTime.MinValue : Convert.ToDateTime(reader["startDate"]),
                                homeTechType = reader["homeTechType"].ToString(),
                                homeTechModel = reader["homeTechModel"].ToString(),
                                problemDescryption = reader["problemDescryption"].ToString(),
                                requestStatus = reader["requestStatus"].ToString(),
                                completionDate = reader.IsDBNull(reader.GetOrdinal("completionDate")) ? DateTime.MinValue : Convert.ToDateTime(reader["completionDate"]),
                                repairParts = reader["repairParts"].ToString(),
                                masterID = reader.IsDBNull(reader.GetOrdinal("masterID")) ? (int?)null : Convert.ToInt32(reader["masterID"]),
                                clientID = Convert.ToInt32(reader["clientID"])
                            };
                            requests.Add(request);
                        }
                    }
                }
            }

            return requests;
        }

        public static List<Request> GetAllRequestsWait()
        {
            List<Request> requests = new List<Request>();

            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT requestID, startDate, homeTechType, homeTechModel, problemDescryption, requestStatus, completionDate, repairParts, masterID, clientID
                        FROM Requests";

                    using (var command = new SQLiteCommand(query, connection))
                    {

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var request = new Request
                                {
                                    requestID = reader.GetInt32(0),
                                    startDate = (DateTime)(reader.IsDBNull(1) ? (DateTime?)null : reader.GetDateTime(1)),
                                    homeTechType = reader.IsDBNull(2) ? null : reader.GetString(2),
                                    homeTechModel = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    problemDescryption = reader.IsDBNull(4) ? null : reader.GetString(4),
                                    requestStatus = reader.IsDBNull(5) ? null : reader.GetString(5),
                                    completionDate = (DateTime)(reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6)),
                                    repairParts = reader.IsDBNull(7) ? null : reader.GetString(7),
                                    masterID = reader.IsDBNull(8) ? (int?)null : reader.GetInt32(8),
                                    clientID = reader.GetInt32(9)
                                };

                                requests.Add(request);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Здесь можно добавить логирование ошибки
                Console.WriteLine($"Error getting waiting requests: {ex.Message}");
            }

            return requests;
        }

        public static int GetMidTime()
        {
            int averageDays = 0;

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string query = @"
            SELECT AVG(julianday(completionDate) - julianday(starteDate)) 
            FROM Requests 
            WHERE requestStatus = 'Готова к выдаче';"; 

                using (var command = new SQLiteCommand(query, connection))
                {
                    var result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        averageDays = Convert.ToInt32(Convert.ToDouble(result));
                    }
                }
            }

            return averageDays;
        }

        public static int GetNumberOfDoneRequests()
        {
            int numberOfDoneRequests = 0;

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string query = @"
            SELECT COUNT(*) 
            FROM Requests 
            WHERE requestStatus = 'Готова к выдаче';"; 

                using (var command = new SQLiteCommand(query, connection))
                {
                    numberOfDoneRequests = Convert.ToInt32(command.ExecuteScalar());
                }
            }

            return numberOfDoneRequests;
        }

        public static void AddRequest(Request request)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string query = @"
                    INSERT INTO Requests (startDate, homeTechType, homeTechModel, problemDescryption, requestStatus) 
                    VALUES (@startDate, @homeTechType, @homeTechModel, @problemDescryption, @requestStatus);";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@startDate", request.startDate);
                    command.Parameters.AddWithValue("@homeTechType", request.homeTechType);
                    command.Parameters.AddWithValue("@homeTechModel", request.homeTechModel);
                    command.Parameters.AddWithValue("@problemDescryption", request.problemDescryption);
                    command.Parameters.AddWithValue("@requestStatus", request.requestStatus);

                    command.ExecuteNonQuery();
                }
            }
        }
        public static void UpdateRequest(Request request)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string query = @"
            UPDATE Requests
            SET 
                problemDescryption = @problemDescryption,
                completionDate = @completionDate,
                startDate = @startDate,
                requestStatus = @requestStatus,
                masterID = @masterID,
                homeTechModel = @homeTechModel,
                repairParts = @repairParts
            WHERE 
                requestID = @requestID";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@problemDescryption", request.problemDescryption);
                    command.Parameters.AddWithValue("@completionDate", request.completionDate);
                    command.Parameters.AddWithValue("@startDate", request.startDate);
                    command.Parameters.AddWithValue("@requestStatus", request.requestStatus);
                    command.Parameters.AddWithValue("@masterID", request.masterID == -1 ? (object)DBNull.Value : request.masterID); // Если masterID равен -1, установить значение DBNull
                    command.Parameters.AddWithValue("@homeTechModel", request.homeTechModel);
                    command.Parameters.AddWithValue("@repairParts", request.repairParts);
                    command.Parameters.AddWithValue("@requestID", request.requestID);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static List<Comment> GetCommentsByRequestId(int requestId)
        {
            List<Comment> comments = new List<Comment>();

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string query = @"
            SELECT commentID, message, masterID, requestID
            FROM Comments
            WHERE requestID = @requestId";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@requestId", requestId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comments.Add(new Comment
                            {
                                commentID = reader.GetInt32(0),
                                message = reader.GetString(1),
                                masterID = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                                requestID = reader.GetInt32(3)
                            });
                        }
                    }
                }
            }

            return comments;
        }

        public static Request GetRequestById(int requestId)
        {
            Request request = null;

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string query = @"
            SELECT requestID, startDate, homeTechType, homeTechModel, problemDescryption, 
                   requestStatus, completionDate, repairParts, masterID, clientID
            FROM Requests
            WHERE requestID = @requestId";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@requestId", requestId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            request = new Request
                            {
                                requestID = reader.GetInt32(0),
                                startDate = reader.GetDateTime(1),
                                homeTechType = reader.GetString(2),
                                homeTechModel = reader.GetString(3),
                                problemDescryption = reader.GetString(4),
                                requestStatus = reader.GetString(5),
                                completionDate = (DateTime)(reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6)),
                                repairParts = reader.IsDBNull(7) ? null : reader.GetString(7),
                                masterID = reader.IsDBNull(8) ? (int?)null : reader.GetInt32(8),
                                clientID = reader.GetInt32(9)
                            };
                        }
                    }
                }
            }

            return request;
        }

        public static List<User> GetAllMasters()
        {
            List<User> masters = new List<User>();

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT userID, fio, phone, login, password, type FROM Users WHERE type = @type";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@type", "Мастер");

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User master = new User
                            {
                                userID = reader.GetInt32(0),
                                fio = reader.GetString(1),
                                phone = reader.GetString(2),
                                login = reader.GetString(3),
                                password = reader.GetString(4),
                                type = reader.GetString(5)
                            };

                            masters.Add(master);
                        }
                    }
                }
            }

            return masters;
        }

        public static User GetUserById(int userId)
        {
            User user = null;

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT userID, fio, phone, login, password, type FROM Users WHERE userID = @userId";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User
                            {
                                userID = reader.GetInt32(0),
                                fio = reader.GetString(1),
                                phone = reader.GetString(2),
                                login = reader.GetString(3),
                                password = reader.GetString(4),
                                type = reader.GetString(5)
                            };
                        }
                    }
                }
            }

            return user;
        }

        public static List<Notification> GetUserNotifications()
        {
            var notifications = new List<Notification>();

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string query = @"
                SELECT notificationID, message, additionDate, isWatched, userID, requestID 
                FROM Notifications 
                WHERE userID = @userId";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", CurrentUser.userId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var notification = new Notification
                            {
                                notificationID = reader.GetInt32(0),
                                message = reader.GetString(1),
                                additionDate = reader.GetDateTime(2),
                                isWatched = reader.GetBoolean(3),
                                userID = reader.GetInt32(4),
                                requestID = reader.GetInt32(5)
                            };
                            notifications.Add(notification);
                        }
                    }
                }
            }

            return notifications;
        }

        public static bool AddUser(string fio, string phone, string login, string password, string type)
        {
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();

                    string query = @"
                        INSERT INTO Users (fio, phone, login, password, type) 
                        VALUES (@fio, @phone, @login, @password, @type);";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@fio", fio);
                        command.Parameters.AddWithValue("@phone", phone);
                        command.Parameters.AddWithValue("@login", login);
                        command.Parameters.AddWithValue("@password", password);
                        command.Parameters.AddWithValue("@type", type);

                        command.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                // Здесь можно добавить логирование ошибки
                Console.WriteLine($"Error adding user: {ex.Message}");
                return false;
            }
        }

        
        public static List<Request> GetWaitingRequestsByClient()
        {
            List<Request> requests = new List<Request>();

            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT requestID, startDate, homeTechType, homeTechModel, problemDescryption, requestStatus, completionDate, repairParts, masterID, clientID
                        FROM Requests
                        WHERE clientID = @clientID AND requestStatus = 'Новая заявка';";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@clientID", CurrentUser.userId);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var request = new Request
                                {
                                    requestID = reader.GetInt32(0),
                                    startDate = (DateTime)(reader.IsDBNull(1) ? (DateTime?)null : reader.GetDateTime(1)),
                                homeTechType = reader.IsDBNull(2) ? null : reader.GetString(2),
                                    homeTechModel = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    problemDescryption = reader.IsDBNull(4) ? null : reader.GetString(4),
                                    requestStatus = reader.IsDBNull(5) ? null : reader.GetString(5),
                                    completionDate = (DateTime)(reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6)),
                                repairParts = reader.IsDBNull(7) ? null : reader.GetString(7),
                                    masterID = reader.IsDBNull(8) ? (int?)null : reader.GetInt32(8),
                                    clientID = reader.GetInt32(9)
                                };

                                requests.Add(request);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Здесь можно добавить логирование ошибки
                Console.WriteLine($"Error getting waiting requests: {ex.Message}");
            }

            return requests;
        }
        public static List<Request> GetInProcessRequestsByClient()
        {
            List<Request> requests = new List<Request>();

            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT requestID, startDate, homeTechType, homeTechModel, problemDescryption, requestStatus, completionDate, repairParts, masterID, clientID
                        FROM Requests
                        WHERE clientID = @clientID AND requestStatus = 'В процессе ремонта';";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@clientID", CurrentUser.userId);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var request = new Request
                                {
                                    requestID = reader.GetInt32(0),
                                    startDate = (DateTime)(reader.IsDBNull(1) ? (DateTime?)null : reader.GetDateTime(1)),
                                    homeTechType = reader.IsDBNull(2) ? null : reader.GetString(2),
                                    homeTechModel = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    problemDescryption = reader.IsDBNull(4) ? null : reader.GetString(4),
                                    requestStatus = reader.IsDBNull(5) ? null : reader.GetString(5),
                                    completionDate = (DateTime)(reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6)),
                                    repairParts = reader.IsDBNull(7) ? null : reader.GetString(7),
                                    masterID = reader.IsDBNull(8) ? (int?)null : reader.GetInt32(8),
                                    clientID = reader.GetInt32(9)
                                };

                                requests.Add(request);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Здесь можно добавить логирование ошибки
                Console.WriteLine($"Error getting waiting requests: {ex.Message}");
            }

            return requests;
        }
        public static List<Request> GetDoneRequestsByClient()
        {
            List<Request> requests = new List<Request>();

            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT requestID, startDate, homeTechType, homeTechModel, problemDescryption, requestStatus, completionDate, repairParts, masterID, clientID
                        FROM Requests
                        WHERE clientID = @clientID AND requestStatus = 'Готова к выдаче';";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@clientID", CurrentUser.userId);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var request = new Request
                                {
                                    requestID = reader.GetInt32(0),
                                    startDate = (DateTime)(reader.IsDBNull(1) ? (DateTime?)null : reader.GetDateTime(1)),
                                    homeTechType = reader.IsDBNull(2) ? null : reader.GetString(2),
                                    homeTechModel = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    problemDescryption = reader.IsDBNull(4) ? null : reader.GetString(4),
                                    requestStatus = reader.IsDBNull(5) ? null : reader.GetString(5),
                                    completionDate = (DateTime)(reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6)),
                                    repairParts = reader.IsDBNull(7) ? null : reader.GetString(7),
                                    masterID = reader.IsDBNull(8) ? (int?)null : reader.GetInt32(8),
                                    clientID = reader.GetInt32(9)
                                };

                                requests.Add(request);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Здесь можно добавить логирование ошибки
                Console.WriteLine($"Error getting waiting requests: {ex.Message}");
            }

            return requests;
        }

        public static List<Request> GetWaitingRequestsByMaster()
        {
            List<Request> requests = new List<Request>();

            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT requestID, startDate, homeTechType, homeTechModel, problemDescryption, requestStatus, completionDate, repairParts, masterID, clientID
                        FROM Requests
                        WHERE masterID = @masterID AND requestStatus = 'Новая заявка';";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@masterID", CurrentUser.userId);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var request = new Request
                                {
                                    requestID = reader.GetInt32(0),
                                    startDate = (DateTime)(reader.IsDBNull(1) ? (DateTime?)null : reader.GetDateTime(1)),
                                    homeTechType = reader.IsDBNull(2) ? null : reader.GetString(2),
                                    homeTechModel = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    problemDescryption = reader.IsDBNull(4) ? null : reader.GetString(4),
                                    requestStatus = reader.IsDBNull(5) ? null : reader.GetString(5),
                                    completionDate = (DateTime)(reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6)),
                                    repairParts = reader.IsDBNull(7) ? null : reader.GetString(7),
                                    masterID = reader.IsDBNull(8) ? (int?)null : reader.GetInt32(8),
                                    clientID = reader.GetInt32(9)
                                };

                                requests.Add(request);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Здесь можно добавить логирование ошибки
                Console.WriteLine($"Error getting waiting requests: {ex.Message}");
            }

            return requests;
        }
        public static List<Request> GetInProcessRequestsByMaster()
        {
            List<Request> requests = new List<Request>();

            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT requestID, startDate, homeTechType, homeTechModel, problemDescryption, requestStatus, completionDate, repairParts, masterID, clientID
                        FROM Requests
                        WHERE masterID = @masterID AND requestStatus = 'В процессе ремонта';";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@masterID", CurrentUser.userId);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var request = new Request
                                {
                                    requestID = reader.GetInt32(0),
                                    startDate = (DateTime)(reader.IsDBNull(1) ? (DateTime?)null : reader.GetDateTime(1)),
                                    homeTechType = reader.IsDBNull(2) ? null : reader.GetString(2),
                                    homeTechModel = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    problemDescryption = reader.IsDBNull(4) ? null : reader.GetString(4),
                                    requestStatus = reader.IsDBNull(5) ? null : reader.GetString(5),
                                    completionDate = (DateTime)(reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6)),
                                    repairParts = reader.IsDBNull(7) ? null : reader.GetString(7),
                                    masterID = reader.IsDBNull(8) ? (int?)null : reader.GetInt32(8),
                                    clientID = reader.GetInt32(9)
                                };

                                requests.Add(request);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Здесь можно добавить логирование ошибки
                Console.WriteLine($"Error getting waiting requests: {ex.Message}");
            }

            return requests;
        }
        public static List<Request> GetDoneRequestsByMaster()
        {
            List<Request> requests = new List<Request>();

            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT requestID, startDate, homeTechType, homeTechModel, problemDescryption, requestStatus, completionDate, repairParts, masterID, clientID
                        FROM Requests
                        WHERE masterID = @masterID AND requestStatus = 'Готова к выдаче';";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@masterID", CurrentUser.userId);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var request = new Request
                                {
                                    requestID = reader.GetInt32(0),
                                    startDate = (DateTime)(reader.IsDBNull(1) ? (DateTime?)null : reader.GetDateTime(1)),
                                    homeTechType = reader.IsDBNull(2) ? null : reader.GetString(2),
                                    homeTechModel = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    problemDescryption = reader.IsDBNull(4) ? null : reader.GetString(4),
                                    requestStatus = reader.IsDBNull(5) ? null : reader.GetString(5),
                                    completionDate = (DateTime)(reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6)),
                                    repairParts = reader.IsDBNull(7) ? null : reader.GetString(7),
                                    masterID = reader.IsDBNull(8) ? (int?)null : reader.GetInt32(8),
                                    clientID = reader.GetInt32(9)
                                };

                                requests.Add(request);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Здесь можно добавить логирование ошибки
                Console.WriteLine($"Error getting waiting requests: {ex.Message}");
            }

            return requests;
        }


        public static int AuthenticateUser(string login, string password)
        {
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT userID 
                        FROM Users 
                        WHERE login = @login AND password = @password;";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@login", login);
                        command.Parameters.AddWithValue("@password", password);

                        var result = command.ExecuteScalar();

                        if (result != null)
                        {
                            return Convert.ToInt32(result);
                        }
                        else
                        {
                            return -1; // Пользователь не найден
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Здесь можно добавить логирование ошибки
                Console.WriteLine($"Error authenticating user: {ex.Message}");
                return -1; // Ошибка при аутентификации
            }
        }
    }
}
 
    
