using VisitsApp.Core.Models;

namespace VisitsApp.Core.Repositories
{
    public class InMemoryGroupRepository : IGroupRepository
    {
        private List<Group> _groups;

        public InMemoryGroupRepository()
        {
            _groups = new List<Group>
            {
                new Group
                {
                    Id = 1,
                    Name = "Младшая"
                },
                new Group {
                    Id = 2,
                    Name = "Старшая"
                }
            };
        }

        public Task Add(string name)
        {
            int id = _groups.Count + 1;
            _groups.Add(new Group 
            {
                Id = id, 
                Name = name 
            });

            return Task.CompletedTask;
        }

        public Task Delete(int id)
        {
            var g = _groups.FirstOrDefault(g => g.Id == id);
            if (g != null)
            {
                _groups.Remove(g);
            }

            return Task.CompletedTask;
        }

        public Task<List<Group>> GetAllGroupsAsync()
        {
            return Task.FromResult(_groups);
        }

        public Task Update(int id, string name)
        {
            var g = _groups.FirstOrDefault(x=>x.Id == id);
            g.Name = name;

            return Task.CompletedTask;
        }
    }
}
