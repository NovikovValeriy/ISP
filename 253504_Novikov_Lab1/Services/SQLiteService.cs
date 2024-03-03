using _253504_Novikov_Lab1.Entities;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253504_Novikov_Lab1.Services
{
    public class SQLiteService : IDbService
    {
        private const string DB_NAME = "local_db.db3";
        private string databasePath = Path.Combine(FileSystem.AppDataDirectory, DB_NAME);
        private  SQLiteConnection _connection;

        public SQLiteService()
        {
            if (File.Exists(databasePath))
            {
                File.Delete(databasePath);
            }
            _connection = new SQLiteConnection(databasePath);
            _connection.CreateTables<Ward, Patient>();
            Init();
        }
        public IEnumerable<Ward> GetAllWards()
        {
            return _connection.Table<Ward>().ToList();
        }

        public IEnumerable<Patient> GetWardsPatients(int id)
        {
            var wards = _connection.Table<Patient>().ToList();
            return wards.Where(x => x.WardId == id).ToList();
        }

        public void Init()
        {
            List<string> diagnosisList = new List<string>
            {
                "Депрессия",
                "Расстройство тревожности",
                "Шизофрения",
                "Биполярное аффективное расстройство (маниакально-депрессивное психоз)",
                "Расстройства аутистического спектра",
                "Психотическое расстройство",
                "Расстройство личности",
                "Расстройство алкогольного и вещественного злоупотребления",
                "Анорексия", 
                "Булимия",
                "Расстройство дефицита внимания с гиперактивностью (ADHD)",
                "Паническое расстройство",
                "Травматическое стрессовое расстройство (ПТСР)",
                "Cоциальная фобия",
                "Агорафобия",
                "Нарколепсия",
                "Эпилепсия с психическими расстройствами"
            };
            var random = new Random();
            var wards = new List<Ward>
            {
                new Ward {Name = "Палата 1", Capacity = 12},
                new Ward {Name = "Палата 2", Capacity = 10},
                new Ward {Name = "Палата 3", Capacity = 15},
                new Ward {Name = "Палата 4", Capacity = 12}
            };
            _connection.InsertAll(wards);

            List<Ward> DBWards = GetAllWards().ToList();
            var patients = new List<Patient>
            {
                new Patient{FirstName = "Кирилл", LastName = "Маханько", Sex = "M", Age = 18, Diagnosis = diagnosisList[random.Next(diagnosisList.Count)], WardId = DBWards[0].Id},
                new Patient{FirstName = "Иван", LastName = "Павловский", Sex = "M", Age = 43, Diagnosis = diagnosisList[random.Next(diagnosisList.Count)], WardId = DBWards[0].Id},
                new Patient{FirstName = "Евгений", LastName = "Андреев", Sex = "M", Age = 32, Diagnosis = diagnosisList[random.Next(diagnosisList.Count)], WardId = DBWards[0].Id},
                
                new Patient{FirstName = "Анастасия", LastName = "Петрова", Sex = "F", Age = 22, Diagnosis = diagnosisList[random.Next(diagnosisList.Count)], WardId = DBWards[1].Id},
                new Patient{FirstName = "Екатерина", LastName = "Фролова", Sex = "F", Age = 19, Diagnosis = diagnosisList[random.Next(diagnosisList.Count)], WardId = DBWards[1].Id},
                new Patient{FirstName = "Ксения", LastName = "Виноградова", Sex = "F", Age = 23, Diagnosis = diagnosisList[random.Next(diagnosisList.Count)], WardId = DBWards[1].Id},
                new Patient{FirstName = "Кристина", LastName = "Волкова", Sex = "F", Age = 25, Diagnosis = diagnosisList[random.Next(diagnosisList.Count)], WardId = DBWards[1].Id},
                new Patient{FirstName = "Мария", LastName = "Никитина", Sex = "F", Age = 19, Diagnosis = diagnosisList[random.Next(diagnosisList.Count)], WardId = DBWards[1].Id},
                new Patient{FirstName = "Елена", LastName = "Смирнова", Sex = "F", Age = 24, Diagnosis = diagnosisList[random.Next(diagnosisList.Count)], WardId = DBWards[1].Id},

                new Patient{FirstName = "Леонид", LastName = "Лопатин", Sex = "M", Age = 33, Diagnosis = diagnosisList[random.Next(diagnosisList.Count)], WardId = DBWards[2].Id},
                new Patient{FirstName = "Валерий", LastName = "Морозов", Sex = "M", Age = 18, Diagnosis = diagnosisList[random.Next(diagnosisList.Count)], WardId = DBWards[2].Id},
                new Patient{FirstName = "Артем", LastName = "Сидоров", Sex = "M", Age = 24, Diagnosis = diagnosisList[random.Next(diagnosisList.Count)], WardId = DBWards[2].Id},

                new Patient{FirstName = "Александр", LastName = "Жаров", Sex = "M", Age = 26, Diagnosis = diagnosisList[random.Next(diagnosisList.Count)], WardId = DBWards[3].Id},
                new Patient{FirstName = "Степан", LastName = "Ларионов", Sex = "M", Age = 21, Diagnosis = diagnosisList[random.Next(diagnosisList.Count)], WardId = DBWards[3].Id},
                new Patient{FirstName = "Богдан", LastName = "Одинцов", Sex = "M", Age = 19, Diagnosis = diagnosisList[random.Next(diagnosisList.Count)], WardId = DBWards[3].Id},
                new Patient{FirstName = "Владислав", LastName = "Гринкевич", Sex = "M", Age = 45, Diagnosis = diagnosisList[random.Next(diagnosisList.Count)], WardId = DBWards[3].Id},
                new Patient{FirstName = "Олег", LastName = "Гончаров", Sex = "M", Age = 36, Diagnosis = diagnosisList[random.Next(diagnosisList.Count)], WardId = DBWards[3].Id},
                new Patient{FirstName = "Николай", LastName = "Семенов", Sex = "M", Age = 32, Diagnosis = diagnosisList[random.Next(diagnosisList.Count)], WardId = DBWards[3].Id}
            };
            _connection.InsertAll(patients);
        }
    }
}
