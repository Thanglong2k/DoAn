using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace doantotnghiep.Controllers
{
    [Route("api/v1/combinationSubjects")]
    [ApiController]
    public class CombinationSubjectController : BaseEntityController<CombinationSubject>
    {
        #region Declare
        ICombinationSubjectService _combinationSubjectService;
        #endregion
        #region Constructor
        public CombinationSubjectController(ICombinationSubjectService combinationSubjectService) : base(combinationSubjectService)
        {
            _combinationSubjectService = combinationSubjectService;
        }
        #endregion
    }
}
