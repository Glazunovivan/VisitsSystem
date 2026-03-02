using VisitsApp.Core.Models;

namespace VisitsApp.Core.Dtos
{
    public class CheckedStudentVisitDto
    {
        public Student Model { get; set; }

        public bool IsChecked { get; set; } 

        public CheckedStudentVisitDto(Student model)
        {
            Model = model;   
        }
    }
}
