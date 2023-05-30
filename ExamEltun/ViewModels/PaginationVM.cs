using ExamEltun.Models;

namespace ExamEltun.ViewModels
{
    public class PaginationVM<T>
    {
        public decimal TotalPage { get; set; }
        public decimal CurrentPage { get; set; }
        public PaginationVM<T> items { get; set; }

      
    }
}
