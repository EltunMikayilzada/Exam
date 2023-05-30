using System.ComponentModel.DataAnnotations.Schema;

namespace ExamEltun.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Image { get; set; }
        public string Mesagge { get; set; }
        public Profession Profession { get; set; }
        public int ProfessionId { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
