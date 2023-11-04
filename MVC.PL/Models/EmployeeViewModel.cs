using MVC.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace MVC.PL.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(10)]
        public string Name { get; set; }
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        [Range(2000,10000)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public DateTime HireDate { get; set; }
        public string ImageName { get; set; }
        public IFormFile Image { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;
        public int DepartmentId { get; set; }
    }
}
