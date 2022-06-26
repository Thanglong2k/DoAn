using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces.Service
{
    public interface IBaseService<TEntity>
    {
        ServiceResult GetAll();
        ServiceResult GetAllPaging(int pageIndex, int pageSize, string queryText);
        ServiceResult GetById(Guid entityId);
        ServiceResult Add(TEntity entity);
        ServiceResult Update(TEntity entity, Guid entityId);
        ServiceResult Delete(Guid entityId);
        ServiceResult Delete(List<Guid> entityIds);
        ServiceResult GetNewCode();
        
    }
}
