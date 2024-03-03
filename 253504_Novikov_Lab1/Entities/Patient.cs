using SQLite;

namespace _253504_Novikov_Lab1.Entities
{
    [Table("Patients")]
    public class Patient
    {
        [PrimaryKey, AutoIncrement, Indexed]
        [Column("Id")]
        public int PatientId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public string? Diagnosis { get; set; }
        [Indexed]
        public int WardId { get; set; }
    }
}
