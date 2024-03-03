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
        private readonly SQLiteAsyncConnection _connection;

        public SQLiteService()
        {
            if (File.Exists(databasePath))
            {
                File.Delete(databasePath);
            }
            _connection = new SQLiteAsyncConnection(databasePath);
            Task.Run(async () =>
            {
                await _connection.CreateTablesAsync<Ward, Patient>();
                await Init();
            }).Wait();
            
        }
        public async Task<IEnumerable<Ward>> GetAllWards()
        {
            //await Init();
            return await _connection.Table<Ward>().ToListAsync();
        }

        public async Task<IEnumerable<Patient>> GetWardsPatients(int id)
        {
            //await Init();
            return await _connection.Table<Patient>().Where(x => x.WardId == id).ToListAsync();
        }

        public async Task Init()
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
            var wards = new List<Ward>
            {
                new Ward {Name = "Палата 1", Capacity = 12},
                new Ward {Name = "Палата 2", Capacity = 10},
                new Ward {Name = "Палата 3", Capacity = 15},
                new Ward {Name = "Палата 4", Capacity = 12}
            };
            await _connection.InsertAllAsync(wards);

            List<Ward> DBWards = await _connection.Table<Ward>().ToListAsync();
            var patients = new List<Patient>
            {
                new Patient{FirstName = "Кирилл", LastName = "Маханько", Sex = "Мужской", Age = 18, Diagnosis = diagnosisList[16], WardId = DBWards[0].Id},
                new Patient{FirstName = "Иван", LastName = "Павловский", Sex = "Мужской", Age = 43, Diagnosis = diagnosisList[4], WardId = DBWards[0].Id},
                new Patient{FirstName = "Евгений", LastName = "Андреев", Sex = "Мужской", Age = 32, Diagnosis = diagnosisList[5], WardId = DBWards[0].Id},
                
                new Patient{FirstName = "Анастасия", LastName = "Петрова", Sex = "Женский", Age = 22, Diagnosis = diagnosisList[12], WardId = DBWards[1].Id},
                new Patient{FirstName = "Екатерина", LastName = "Фролова", Sex = "Женский", Age = 19, Diagnosis = diagnosisList[1], WardId = DBWards[1].Id},
                new Patient{FirstName = "Ксения", LastName = "Виноградова", Sex = "Женский", Age = 23, Diagnosis = diagnosisList[3], WardId = DBWards[1].Id},
                new Patient{FirstName = "Кристина", LastName = "Волкова", Sex = "Женский", Age = 25, Diagnosis = diagnosisList[3], WardId = DBWards[1].Id},
                new Patient{FirstName = "Мария", LastName = "Никитина", Sex = "Женский", Age = 19, Diagnosis = diagnosisList[4], WardId = DBWards[1].Id},
                new Patient{FirstName = "Елена", LastName = "Смирнова", Sex = "Женский", Age = 24, Diagnosis = diagnosisList[13], WardId = DBWards[1].Id},

                new Patient{FirstName = "Леонид", LastName = "Лопатин", Sex = "Мужской", Age = 33, Diagnosis = diagnosisList[9], WardId = DBWards[2].Id},
                new Patient{FirstName = "Валерий", LastName = "Морозов", Sex = "Мужской", Age = 18, Diagnosis = diagnosisList[7], WardId = DBWards[2].Id},
                new Patient{FirstName = "Артем", LastName = "Сидоров", Sex = "Мужской", Age = 24, Diagnosis = diagnosisList[2], WardId = DBWards[2].Id},

                new Patient{FirstName = "Александр", LastName = "Жаров", Sex = "Мужской", Age = 26, Diagnosis = diagnosisList[8], WardId = DBWards[3].Id},
                new Patient{FirstName = "Степан", LastName = "Ларионов", Sex = "Мужской", Age = 21, Diagnosis = diagnosisList[8], WardId = DBWards[3].Id},
                new Patient{FirstName = "Богдан", LastName = "Одинцов", Sex = "Мужской", Age = 19, Diagnosis = diagnosisList[5], WardId = DBWards[3].Id},
                new Patient{FirstName = "Владислав", LastName = "Гринкевич", Sex = "Мужской", Age = 45, Diagnosis = diagnosisList[14], WardId = DBWards[3].Id},
                new Patient{FirstName = "Олег", LastName = "Гончаров", Sex = "Мужской", Age = 36, Diagnosis = diagnosisList[10], WardId = DBWards[3].Id},
                new Patient{FirstName = "Николай", LastName = "Семенов", Sex = "Мужской", Age = 32, Diagnosis = diagnosisList[6], WardId = DBWards[3].Id}
            };
            await _connection.InsertAllAsync(patients);
        }
    }
}
