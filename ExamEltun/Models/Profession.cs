using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ExamEltun.Models
{
    public class Profession
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
