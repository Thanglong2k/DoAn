
using Core.Entities;
using Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICombinationSubjectRepository : IBaseRepository<CombinationSubject>
    {
        public bool CheckDuplicatePositionCode(string positionCode);

    }
}
