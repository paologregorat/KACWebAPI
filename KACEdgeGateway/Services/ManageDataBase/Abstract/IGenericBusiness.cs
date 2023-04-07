using KACGatewayContextLibrary.Domain;
using KVMWebAPI.Domain.Command.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace KACEdgeGateway.Services.ManageDataBase.Abstract
{
    public interface IGenericBusiness<TEntity> where TEntity : EntityBase
    {
        TEntity GetById(Guid id);

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");

        CommandResponse Delete(Guid id);

        CommandResponse Delete(TEntity entityToDelete);

        CommandResponse Insert(TEntity entity);

        CommandResponse Update(TEntity entityToUpdate);
        CommandResponse Save(TEntity entity);
    }
}
