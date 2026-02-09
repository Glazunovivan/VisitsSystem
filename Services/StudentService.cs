using VisitSchool.Models;
using VisitSchool.Repositories;

namespace VisitSchool.Services
{
    public class StudentService
    {
        private readonly IStudentRepository _repo;
        private readonly GroupService _groupService;

        public StudentService(IStudentRepository repository, GroupService groupService) 
        {
            _repo = repository;
            _groupService = groupService;
        }

        /// <summary>
        /// Получить ученика
        /// </summary>
        /// <param name="id">Id ученика</param>
        /// <returns></returns>
        public async Task<Student> Get(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<Student> AddStudentAsync(string name, string surename, string lastname, int groupId, int categoryId)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(lastname))
                throw new ArgumentNullException("Имя и фамилия не должны быть пустыми");

            var s = new Student
            {
                Name = name,
                Surename = surename,
                Lastname = lastname,
                GroupId = groupId == 0 ? null : groupId,  
                StudentCategoryId = categoryId == 0 ? null : categoryId
            };

            return await _repo.AddAsync(s);
        }
   
        /// <summary>
        /// Обновить информацию об ученике
        /// </summary>
        /// <returns></returns>
        public async Task UpdateStudentAsync(int id, string name = null, string surename = null, string lastname = null, int? groupId = null, int? categoryId = null)
        {
            var student = await _repo.GetByIdAsync(id);
            if (name != null && string.IsNullOrEmpty(name) == false)
            {
                student.Name = name;
            }
            if (lastname != null && string.IsNullOrEmpty(lastname) == false)
            {
                student.Lastname = lastname;
            }
            if (surename != null)
            {
                student.Surename = surename;
            }
            student.GroupId = groupId == 0 ? null : groupId;
            student.StudentCategoryId = categoryId == 0 ? null : categoryId; 

            await _repo.UpdateAsync(student);

        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            List<Student> result = new();
            try
            {
                result = await _repo.GetAllAsync();
            }
            catch (Exception ex)
            {

            }
            
            return result;   
        }


        public async Task DeleteStudent(int id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
