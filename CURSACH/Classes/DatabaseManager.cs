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
        private const string ConnectionString = "Data Source=TecServis.db;Version=2;";

        public static List<Notification> GetUserNotifications(int userId)
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
                    command.Parameters.AddWithValue("@userId", userId);

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

        public bool AddUser(string fio, string phone, string login, string password, string type)
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

        public static List<Request> GetWaitingRequests()
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
                        command.Parameters.AddWithValue("@masterID", CurrentUser.UserId);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var request = new Request
                                {
                                    requestID = reader.GetInt32(0),
                                    startDate = reader.IsDBNull(1) ? null : reader.GetString(1),
                                    homeTechType = reader.IsDBNull(2) ? null : reader.GetString(2),
                                    homeTechModel = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    problemDescryption = reader.IsDBNull(4) ? null : reader.GetString(4),
                                    requestStatus = reader.IsDBNull(5) ? null : reader.GetString(5),
                                    completionDate = reader.IsDBNull(6) ? null : reader.GetString(6),
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
        public static List<Request> GetInProcessRequests()
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
                        command.Parameters.AddWithValue("@masterID", CurrentUser.UserId);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var request = new Request
                                {
                                    requestID = reader.GetInt32(0),
                                    startDate = reader.IsDBNull(1) ? null : reader.GetString(1),
                                    homeTechType = reader.IsDBNull(2) ? null : reader.GetString(2),
                                    homeTechModel = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    problemDescryption = reader.IsDBNull(4) ? null : reader.GetString(4),
                                    requestStatus = reader.IsDBNull(5) ? null : reader.GetString(5),
                                    completionDate = reader.IsDBNull(6) ? null : reader.GetString(6),
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
        public static List<Request> GetDoneRequests()
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
                        command.Parameters.AddWithValue("@masterID", CurrentUser.UserId);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var request = new Request
                                {
                                    requestID = reader.GetInt32(0),
                                    startDate = reader.IsDBNull(1) ? null : reader.GetString(1),
                                    homeTechType = reader.IsDBNull(2) ? null : reader.GetString(2),
                                    homeTechModel = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    problemDescryption = reader.IsDBNull(4) ? null : reader.GetString(4),
                                    requestStatus = reader.IsDBNull(5) ? null : reader.GetString(5),
                                    completionDate = reader.IsDBNull(6) ? null : reader.GetString(6),
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




        internal static List<Tasks> GetTasksByStatusAndUserId(int userId, string status)
        {
            List<Tasks> tasks = new List<Tasks>();

            string query = $"SELECT * FROM Tasks WHERE UserId = {userId} and TaskStatus = N'{status}'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        // Создаем новый объект Task и заполняем его данными из базы данных
                        Tasks task = new Tasks();
                        task.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        task.Description = reader.GetString(reader.GetOrdinal("TaskDescription"));
                        task.CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"));
                        task.Deadline = reader.GetDateTime(reader.GetOrdinal("Deadline"));
                        task.UserId = reader.GetInt32(reader.GetOrdinal("UserId"));
                        task.Status = reader.GetString(reader.GetOrdinal("TaskStatus"));
                        task.Priority = reader.GetString(reader.GetOrdinal("TaskPriority"));
                        task.ProjectId = reader.GetInt32(reader.GetOrdinal("ProjectId"));

                        // Добавляем задачу в список
                        tasks.Add(task);
                    }
                    reader.Close();
                }
            }

            return tasks;
        }

        public static List<Tasks> GetAllTasksByUser(int UserId)
        {
            List<Tasks> tasks = new List<Tasks>();

            string query = $"SELECT * FROM Tasks WHERE UserId = {UserId} AND LTRIM(RTRIM(TaskStatus)) != N'Завершена'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {         

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        // Создаем новый объект Task и заполняем его данными из базы данных
                        Tasks task = new Tasks();
                        task.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        task.Description = reader.GetString(reader.GetOrdinal("TaskDescription"));
                        task.CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"));
                        task.Deadline = reader.GetDateTime(reader.GetOrdinal("Deadline"));
                        task.UserId = reader.GetInt32(reader.GetOrdinal("UserId"));
                        task.Status = reader.GetString(reader.GetOrdinal("TaskStatus"));
                        task.Priority = reader.GetString(reader.GetOrdinal("TaskPriority"));
                        task.ProjectId = reader.GetInt32(reader.GetOrdinal("ProjectId"));

                        // Добавляем задачу в список
                        tasks.Add(task);
                    }
                    reader.Close();
                }
            }

            return tasks;
        }


        internal static Users GetUserById(int userId)
        {
            Users user = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Users WHERE Id = @UserId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                user = new Users();
                                user.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                                user.Email = reader.GetString(reader.GetOrdinal("Email"));
                                user.Name = reader.GetString(reader.GetOrdinal("UserName"));
                                user.Password = reader.GetString(reader.GetOrdinal("UserPassword"));
                                user.Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? null : reader.GetString(reader.GetOrdinal("Phone"));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при получении пользователя из базы данных: " + ex.Message);
            }

            return user;
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

        public static string GetProjectById(Tasks task)
        {
            int ProjectId = task.ProjectId;
            string ProjectName = "";
            string query = $"SELECT ProjectName FROM Projects WHERE ProjectId = '{ProjectId}'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    connection.Open();
                    object result = command.ExecuteScalar();

                  
                    if (result != null && result != DBNull.Value)
                    {
                        ProjectName = Convert.ToString(result); 
                    }
                }
            }

            return ProjectName; 
        }

       

        public static void DeleteTask(int taskId)
        {
            // Создаем строку запроса SQL для удаления задачи с указанным taskId
            string query = $"DELETE FROM Tasks WHERE Id = {taskId}";

            // Используем подключение к базе данных
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Создаем команду SQL с запросом
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Открываем подключение
                    connection.Open();
                    // Выполняем команду SQL для удаления задачи
                    command.ExecuteNonQuery();
                }
            }
        }


        public static List<ProjectsClass> GetProjectsFromDatabase()
        {
            List<ProjectsClass> projects = new List<ProjectsClass>();

            string query = "SELECT ProjectId, ProjectName FROM Projects";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int projectId = reader.GetInt32(reader.GetOrdinal("ProjectId"));
                        string projectName = reader.GetString(reader.GetOrdinal("ProjectName"));

                        ProjectsClass project = new ProjectsClass(projectId, projectName);
                        projects.Add(project);
                    }

                    reader.Close();
                }
            }

            return projects;
        }


        public static void UpdateTask(Tasks task)
        {
            string query = "UPDATE Tasks SET  UserId=@UserId, ProjectId=@ProjectId, TaskDescription = @Description, Deadline = @Deadline, TaskStatus = @Status, TaskPriority = @Priority WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Добавляем параметры для предотвращения SQL-инъекций и передаем значения из объекта task
                    command.Parameters.AddWithValue("@Description", task.Description);
                    command.Parameters.AddWithValue("@Deadline", task.Deadline);
                    command.Parameters.AddWithValue("@Status", task.Status);
                    command.Parameters.AddWithValue("@ProjectId", task.ProjectId);
                    command.Parameters.AddWithValue("@Priority", task.Priority);
                    command.Parameters.AddWithValue("@UserId", task.UserId);
                    command.Parameters.AddWithValue("@Id", task.Id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        internal static void AddTask(Tasks task)
        {
            string query = $"INSERT INTO Tasks (TaskDescription, Deadline, TaskStatus, TaskPriority, UserId, ProjectId) VALUES (@Description, @Deadline, @Status, @Priority, @UserId, @ProjectId)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Добавляем параметры для предотвращения атак вроде SQL инъекций
                    command.Parameters.AddWithValue("@Description", task.Description);
                    command.Parameters.AddWithValue("@Deadline", task.Deadline);
                    command.Parameters.AddWithValue("@Status", task.Status);
                    command.Parameters.AddWithValue("@Priority", task.Priority);
                    command.Parameters.AddWithValue("@ProjectId", task.ProjectId);
                    command.Parameters.AddWithValue("@UserId", task.UserId);



                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        // Обработка ошибок, например, вывод сообщения об ошибке или запись в лог
                        Console.WriteLine("Ошибка при добавлении задачи в базу данных: " + ex.Message);
                    }
                }
            }
        }

        // для окна Проектов
        internal static int GtProjectIdByName(string projectName)
        {
            int projectId = -1; // Инициализируем ID проекта значением -1 (которое будет обозначать неудачу при поиске)

            string query = "SELECT ProjectId FROM Projects WHERE ProjectName = @ProjectName";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Добавляем параметр для имени проекта
                    command.Parameters.AddWithValue("@ProjectName", projectName);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar(); // Получаем результат выполнения запроса (ID проекта)

                        // Если результат не равен NULL, значит проект найден
                        if (result != null && result != DBNull.Value)
                        {
                            projectId = Convert.ToInt32(result); // Преобразуем результат в целое число и присваиваем projectId
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Обработка ошибок, например, вывод сообщения об ошибке или запись в лог
                        MessageBox.Show("Ошибка при поиске ID проекта в базе данных: " + ex.Message);
                    }
                }
            }

            return projectId; // Возвращаем ID проекта (или -1 в случае неудачи)
        }

        internal static List<ProjectsClass> GetAllProjects()
        {
            List<ProjectsClass> projects = new List<ProjectsClass>();

            // SQL-запрос для выборки всех проектов
            string query = "SELECT ProjectId, ProjectName FROM Projects";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    // Чтение результатов запроса и создание объектов ProjectsClass
                    while (reader.Read())
                    {
                        int projectId = reader.GetInt32(reader.GetOrdinal("ProjectId"));
                        string projectName = reader.GetString(reader.GetOrdinal("ProjectName"));

                        // Создание объекта ProjectsClass и добавление его в список
                        ProjectsClass project = new ProjectsClass(projectId, projectName);
                        projects.Add(project);
                    }

                    reader.Close();
                }
            }

            return projects;
        }

        internal static List<Tasks> GetTasksByProjectId(int id)
        {
            List<Tasks> tasks = new List<Tasks>();

            string query = $"SELECT * FROM Tasks WHERE ProjectId = {id}";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Tasks task = new Tasks();
                        task.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        task.Description = reader.GetString(reader.GetOrdinal("TaskDescription"));
                        task.CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"));
                        task.Deadline = reader.GetDateTime(reader.GetOrdinal("Deadline"));
                        task.UserId = reader.GetInt32(reader.GetOrdinal("UserId"));
                        task.Status = reader.GetString(reader.GetOrdinal("TaskStatus"));
                        task.Priority = reader.GetString(reader.GetOrdinal("TaskPriority"));
                        task.ProjectId = reader.GetInt32(reader.GetOrdinal("ProjectId"));

                        tasks.Add(task);
                    }

                    reader.Close();
                }
            }

            return tasks;
        }

        internal static void AddProject(string projectName)
        {
            int projectId = -1; // Инициализируем идентификатор проекта значением по умолчанию

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Projects (ProjectName) VALUES (@ProjectName); ";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProjectName", projectName);
                        // Получаем идентификатор добавленного проекта
                        projectId = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                MessageBox.Show("Проект успешно добавлен");
            }
            catch (Exception ex)
            {
                // Обработка ошибок при добавлении проекта
                Console.WriteLine("Ошибка при добавлении проекта: " + ex.Message);
            }

        }

        internal static bool UpdateUserProfile(int userId, string newName, string newEmail, string newPassword, string newPhone)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Создаем команду для обновления данных пользователя
                    string query = "UPDATE Users SET UserName = @NewName, Email = @NewEmail, UserPassword = @NewPassword, Phone = @NewPhone WHERE Id = @UserId";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Добавляем параметры к команде
                    command.Parameters.AddWithValue("@NewName", newName);
                    command.Parameters.AddWithValue("@NewEmail", newEmail);
                    command.Parameters.AddWithValue("@NewPassword", newPassword);
                    command.Parameters.AddWithValue("@NewPhone", newPhone);
                    command.Parameters.AddWithValue("@UserId", userId);

                    // Выполняем команду
                    int rowsAffected = command.ExecuteNonQuery();

                    // Если были затронуты какие-то строки, возвращаем true
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                // Логирование ошибки, если необходимо
                Console.WriteLine("Error updating user profile: " + ex.Message);
                return false;
            }
        }

  

        public static List<Users> LoadAllUsers()
        {
            List<Users> users = new List<Users>();

            string query = "SELECT * FROM Users";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        // Создаем новый объект пользователя и заполняем его данными из базы данных
                        Users user = new Users();
                        user.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        user.Name = reader.GetString(reader.GetOrdinal("UserName"));
                        user.Email = reader.GetString(reader.GetOrdinal("Email"));
                        user.Password = reader.GetString(reader.GetOrdinal("UserPassword"));
                        user.Phone = reader.GetString(reader.GetOrdinal("Phone"));

                        // Добавляем пользователя в список
                        users.Add(user);
                    }
                }
            }

            return users;
        }

        internal static int GetUserIdByName(String userName)
        {
            int userId = -1; // Инициализируем ID проекта значением -1 (которое будет обозначать неудачу при поиске)

            string query = "SELECT Id FROM Users WHERE UserName = @UserName";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Добавляем параметр для имени проекта
                    command.Parameters.AddWithValue("@UserName", userName.Trim());

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar(); // Получаем результат выполнения запроса (ID проекта)

                        // Если результат не равен NULL, значит проект найден
                        if (result != null && result != DBNull.Value)
                        {
                            userId = Convert.ToInt32(result); // Преобразуем результат в целое число и присваиваем projectId
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Обработка ошибок, например, вывод сообщения об ошибке или запись в лог
                        MessageBox.Show("Ошибка при поиске ID пользователя в базе данных: " + ex.Message);
                    }
                }
            }

            return userId; // Возвращаем ID проекта (или -1 в случае неудачи)
        }

        internal static List<Tasks> GetFilteredTasks(int projectId, int userId, string status, string priority, int daysUntilTheDeadline)
        {
            List<Tasks> tasks = new List<Tasks>();

            string query = "SELECT * FROM Tasks WHERE 1=1"; // Начало запроса без каких-либо условий

            if (projectId != -1)
            {
                query += $" AND ProjectId = {projectId}"; // Добавляем критерий по ProjectId, если он был выбран
            }

            if (userId != -1)
            {
                query += $" AND UserId = {userId}"; // Добавляем критерий по UserId, если он был выбран
            }

            if (!(status == "Любой"))
            {
                query += $" AND TaskStatus = N'{status}'"; // Добавляем критерий по статусу, если он был выбран
            }

            if (priority != "Любой")
            {

                query += $" AND TaskPriority = {Convert.ToInt32(priority)}"; // Добавляем критерий по приоритету, если он был выбран
            }

            if (daysUntilTheDeadline != -1)
            {
                query += $" AND Deadline < DATEADD(day, {daysUntilTheDeadline}, GETDATE())"; // Добавляем критерий по дедлайну, если он был выбран
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Tasks task = new Tasks();
                        task.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        task.Description = reader.GetString(reader.GetOrdinal("TaskDescription"));
                        task.CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"));
                        task.Deadline = reader.GetDateTime(reader.GetOrdinal("Deadline"));
                        task.UserId = reader.GetInt32(reader.GetOrdinal("UserId"));
                        task.Status = reader.GetString(reader.GetOrdinal("TaskStatus"));
                        task.Priority = reader.GetString(reader.GetOrdinal("TaskPriority"));
                        task.ProjectId = reader.GetInt32(reader.GetOrdinal("ProjectId"));

                        tasks.Add(task);
                    }

                    reader.Close();
                }
            }

            return tasks;
        }
    }
}
 
    
