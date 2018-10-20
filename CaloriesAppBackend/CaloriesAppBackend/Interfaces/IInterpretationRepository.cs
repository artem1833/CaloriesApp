using CaloriesAppBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaloriesAppBackend.Interfaces
{
    public interface IInterpretationRepository
    {
        Task<IEnumerable<InterpretationDto>> GetInterpretationAsync<T>() where T : Interpretation;
        T FindInterpretationByType<T>(int type) where T : Interpretation;
    }
}
