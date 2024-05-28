using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;




namespace CURSACH.Classes
{
    public class DatabaseHelper { 

    private const string DatabaseName = "TecServis.db";
    private const int CurrentDatabaseVersion = 3; // Обновите версию базы данных

    public DatabaseHelper()
    {
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        bool databaseExists = File.Exists(DatabaseName);
        bool versionChanged = CheckDatabaseVersionChanged();

        if (databaseExists && versionChanged)
        {
            File.Delete(DatabaseName);
        }

        if (!databaseExists || versionChanged)
        {
            CreateDatabase();
            CreateTables();
            FillInitialData();
            UpdateDatabaseVersion();
        }
    }

    private bool CheckDatabaseVersionChanged()
    {
        // Check if version file exists
        if (!File.Exists("version.txt"))
        {
            return true;
        }

        int savedVersion;
        if (int.TryParse(File.ReadAllText("version.txt"), out savedVersion))
        {
            return savedVersion != CurrentDatabaseVersion;
        }

        return true;
    }

    private void UpdateDatabaseVersion()
    {
        File.WriteAllText("version.txt", CurrentDatabaseVersion.ToString());
    }

    private void CreateDatabase()
    {
        SQLiteConnection.CreateFile(DatabaseName);
    }

    private void CreateTables()
    {
        using (var connection = new SQLiteConnection($"Data Source={DatabaseName};Version=3;"))
        {
            connection.Open();

            string createUsersTable = @"
                    CREATE TABLE Users (
                        userID INTEGER PRIMARY KEY AUTOINCREMENT,
                        fio TEXT NOT NULL,
                        phone TEXT NOT NULL,
                        login TEXT NOT NULL,
                        password TEXT NOT NULL,
                        type TEXT NOT NULL
                    );";

            string createRequestsTable = @"
                    CREATE TABLE Requests (
                        requestID INTEGER PRIMARY KEY AUTOINCREMENT,
                        startDate DATE,
                        homeTechType TEXT NOT NULL,
                        homeTechModel TEXT NOT NULL,
                        problemDescryption TEXT NOT NULL,
                        requestStatus TEXT NOT NULL,
                        completionDate DATE,
                        repairParts TEXT,
                        masterID INTEGER,
                        clientID INTEGER NOT NULL,
                        FOREIGN KEY (masterID) REFERENCES Users(userID),
                        FOREIGN KEY (clientID) REFERENCES Users(userID)
                    );";

            string createCommentsTable = @"
                    CREATE TABLE Comments (
                        commentID INTEGER PRIMARY KEY AUTOINCREMENT,
                        message TEXT NOT NULL,
                        masterID INTEGER,
                        requestID INTEGER NOT NULL,
                        FOREIGN KEY (masterID) REFERENCES Users(userID),
                        FOREIGN KEY (requestID) REFERENCES Requests(requestID)
                    );";

            string createNotificationsTable = @"
                    CREATE TABLE Notifications (
                        notificationID INTEGER PRIMARY KEY AUTOINCREMENT,
                        message TEXT NOT NULL,
                        additionDate DATETIME DEFAULT CURRENT_TIMESTAMP,
                        isWatched BOOLEAN NOT NULL CHECK (isWatched IN (0, 1)),
                        userID INTEGER NOT NULL,
                        requestID INTEGER NOT NULL,
                        FOREIGN KEY (userID) REFERENCES Users(userID),
                        FOREIGN KEY (requestID) REFERENCES Requests(requestID)
                    );";

            using (var command = new SQLiteCommand(createUsersTable, connection))
            {
                command.ExecuteNonQuery();
            }

            using (var command = new SQLiteCommand(createRequestsTable, connection))
            {
                command.ExecuteNonQuery();
            }

            using (var command = new SQLiteCommand(createCommentsTable, connection))
            {
                command.ExecuteNonQuery();
            }

            using (var command = new SQLiteCommand(createNotificationsTable, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    private void FillInitialData()
    {
        using (var connection = new SQLiteConnection($"Data Source={DatabaseName};Version=3;"))
        {
            connection.Open();

            string insertUsers = @"
                    INSERT INTO Users (fio, phone, login, password, type) VALUES
                    ('Трубин Никита Юрьевич', '89210563128', 'kasoo', 'root', 'Менеджер'),
                    ('Мурашов Андрей Юрьевич', '89535078985', 'murashov123', 'qwerty', 'Мастер'),
                    ('Степанов Андрей Викторович', '89210673849', 'test1', 'test1', 'Мастер'),
                    ('Перина Анастасия Денисовна', '89990563748', 'perinaAD', '250519', 'Оператор'),
                    ('Мажитова Ксения Сергеевна', '89994563847', 'krutiha1234567', '1234567890', 'Оператор'),
                    ('Семенова Ясмина Марковна', '89994563847', 'login1', 'pass1', 'Мастер'),
                    ('Баранова Эмилия Марковна', '89994563841', 'login2', 'pass2', 'Заказчик'),
                    ('Егорова Алиса Платоновна', '89994563842', 'login3', 'pass3', 'Заказчик'),
                    ('Титов Максим Иванович', '89994563843', 'login4', 'pass4', 'Заказчик'),
                    ('Иванов Марк Максимович', '89994563844', 'login5', 'pass5', 'Мастер');";

            string insertRequests = @"
                    INSERT INTO Requests (startDate, homeTechType, homeTechModel, problemDescryption, requestStatus, completionDate, repairParts, masterID, clientID) VALUES
                    ('2023-06-06', 'Фен', 'Ладомир ТА112 белый', 'Перестал работать', 'В процессе ремонта', NULL, NULL, 2, 7),
                    ('2023-05-05', 'Тостер', 'Redmond RT-437 черный', 'Перестал работать', 'В процессе ремонта', NULL, NULL, 3, 7),
                    ('2022-07-07', 'Холодильник', 'Indesit DS 316 W белый', 'Не морозит одна из камер холодильника', 'Готова к выдаче', '2023-01-01', NULL, 2, 8),
                    ('2023-08-02', 'Стиральная машина', 'DEXP WM-F610NTMA/WW белый', 'Перестали работать многие режимы стирки', 'Новая заявка', NULL, NULL, NULL, 8),
                    ('2023-08-02', 'Мультиварка', 'Redmond RMC-M95 черный', 'Перестала включаться', 'Новая заявка', NULL, NULL, NULL, 9),
                    ('2023-08-02', 'Фен', 'Ладомир ТА113 чёрный', 'Перестал работать', 'Готова к выдаче', '2023-08-03', NULL, 2, 7),
                    ('2023-07-09', 'Холодильник', 'Indesit DS 314 W серый', 'Гудит, но не замораживает', 'Готова к выдаче', '2023-08-03', 'Мотор обдува морозильной камеры холодильника', 2, 8);";

            string insertComments = @"
                    INSERT INTO Comments (message, masterID, requestID) VALUES
                    ('Интересная поломка', 2, 1),
                    ('Очень странно, будем разбираться!', 3, 2),
                    ('Скорее всего потребуется мотор обдува!', 2, 7),
                    ('Интересная проблема', 2, 1),
                    ('Очень странно, будем разбираться!', 3, 6);";

            using (var command = new SQLiteCommand(insertUsers, connection))
            {
                command.ExecuteNonQuery();
            }

            using (var command = new SQLiteCommand(insertRequests, connection))
            {
                command.ExecuteNonQuery();
            }

            using (var command = new SQLiteCommand(insertComments, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
}
