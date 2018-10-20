using CaloriesAppBackend.Interfaces;
using CaloriesAppBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaloriesAppBackend.Data
{
    public class InterpretationRepository : IInterpretationRepository
    {
        protected readonly CaloriesContext db;

        public InterpretationRepository(CaloriesContext context)
        {
            db = context;
        }

        public async Task<IEnumerable<InterpretationDto>> GetInterpretationAsync<T>() where T : Interpretation
        {
            return await db.Set<T>().Select(x => new InterpretationDto
            {
                Name = x.Name,
                Type = x.Type
            }).ToListAsync();
        }

        public T FindInterpretationByType<T>(int type) where T : Interpretation
        {
            return db.Set<T>().FirstOrDefault(x => x.Type == type);
        }
    }
}
