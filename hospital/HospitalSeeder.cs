using System;
using System.Collections.Generic;
using System.Linq;
using hospital.Entities;

namespace hospital
{
    public class HospitalSeeder
    {
        private readonly HospitalDbContext _dbContext;

        public HospitalSeeder(HospitalDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }

            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "Doctor"
                },
                new Role()
                {
                Name = "Patient"
            },
            };

            return roles;
        }
    }
}
