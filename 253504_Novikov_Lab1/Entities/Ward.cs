using SQLite;

namespace _253504_Novikov_Lab1.Entities
{
    [Table("Wards")]
    public class Ward
    {
        [PrimaryKey, AutoIncrement, Indexed]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Capacity { get; set; }
    }
}
