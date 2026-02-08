using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitSchool.Models;

namespace VisitSchool.Repositories
{
    public class InMemoryStudentRepository : IStudentRepository
    {
        private readonly ConcurrentDictionary<int, Student> _store = new();
        private int _idCounter = 0;

        public InMemoryStudentRepository()
        {
            _store.Clear();
            _store.TryAdd(1, new Student()
            {
                Name = "Петр", 
                Surename = "Петрович",
                Lastname = "Петров",
                Id = 1,
                GroupId = 1,
                Group = new Group()
                {
                    Id = 1,
                    Name = "Младшая"
                },
                StudentCategoryId = 0,
                StudentCategory = new DiscountCategory() { Id = 1, DscountPercent = 0d }
            });
            _store.TryAdd(2, new Student()
            {
                Name = "Иван",
                Surename = "Иванович",
                Lastname = "Иванов",
                Id = 2,
                GroupId = 2,
                Group = new Group()
                {
                    Id = 2,
                    Name = "Старшая"
                },
                StudentCategoryId = 1,
                StudentCategory = new DiscountCategory() { Id = 2, DscountPercent = 0.15d }
            });
            _idCounter = 2;
        }

        public Task<Student> AddAsync(Student student)
        {
            var id = Interlocked.Increment(ref _idCounter);
            student.Id = id;
            _store[id] = student;
            return Task.FromResult(student);
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Student>> GetAllAsync()
        {
            var list = _store.Values.OrderByDescending(s => s.Lastname)
                                    .ThenBy(x=>x.Name)
                                    .ThenBy(x=>x.Surename)
                                    .ToList();
            return Task.FromResult(list);
        }

        public Task<List<DiscountCategory>> GetAllDiscountCategories()
        {
            throw new NotImplementedException();
        }

        public Task<Student> GetByIdAsync(int id)
        {
            _store.TryGetValue(id, out var s);
            return Task.FromResult(s);
        }

        public Task UpdateAsync(Student student)
        {
            if (_store.ContainsKey(student.Id))
            {
                _store[student.Id] = student;
            }
            return Task.CompletedTask;
        }
    }
}
