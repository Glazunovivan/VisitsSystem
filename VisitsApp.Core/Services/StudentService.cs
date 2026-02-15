using Microsoft.Extensions.Logging;
using VisitsApp.Core.Models;
using VisitsApp.Core.Repositories;

namespace VisitsApp.Core.Services
{
    public class StudentService
    {
        private readonly IStudentRepository _repo;
        private readonly GroupService _groupService;
        private readonly ILogger _logger;

        public StudentService(IStudentRepository repository, GroupService groupService, ILogger<StudentService> logger) 
        {
            _repo = repository;
            _groupService = groupService;
            _logger = logger;
        }

        /// <summary>
        /// Получить ученика
        /// </summary>
        /// <param name="id">Id ученика</param>
        /// <returns></returns>
        public async Task<Student> Get(int id)
        {
            try
            {
                return await _repo.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}. Inner Exception {ex.InnerException?.Message}", "Ошибка при получении данных из БД.");
                throw ex;
            }
        }

        public async Task<Student> AddStudentAsync(string name, string surename, string lastname, int groupId, int categoryId)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(lastname))
            {
                _logger.LogInformation("Имя и фамилия не должны быть пустыми", "Ошибка при попытке сохранить данные об ученике");
                throw new ArgumentNullException("Имя и фамилия не должны быть пустыми");
            }

            try
            {
                _logger.LogInformation($"Сохранение ученика в БД: {name}, {surename}, {lastname}, {groupId}, {categoryId}");
                var s = new Student
                {
                    Name = name,
                    Surename = surename ?? "",
                    Lastname = lastname,
                    GroupId = groupId == 0 ? null : groupId,
                    StudentCategoryId = categoryId == 0 ? null : categoryId
                };

                return await _repo.AddAsync(s);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}. Inner Exception: {ex.InnerException?.Message}");
                throw ex;
            }
           
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

            try 
            { 
                await _repo.UpdateAsync(student);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}. Inner Exception: {ex.InnerException?.Message}");
                throw ex;
            }
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
                _logger.LogError($"{ex.Message}. Inner Exception: {ex.InnerException?.Message}");
            }

            return result;   
        }


        public async Task DeleteStudent(int id)
        {
            try
            {
                _logger.LogInformation($"Удаление ученика с Id = {id}...");
                await _repo.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}. Inner Exception: {ex.InnerException?.Message}");
                throw ex;
            }
        }
    }
}
