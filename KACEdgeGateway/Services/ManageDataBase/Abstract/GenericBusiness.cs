using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using KACGatewayContextLibrary.Domain;
using KVMWebAPI.Domain.Command.Commands;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Xml.Linq;

namespace KACEdgeGateway.Services.ManageDataBase.Abstract
{
    public abstract class GenericBusiness<TEntity> : IGenericBusiness<TEntity> where TEntity : EntityBase
    {
        private readonly KACGatewayContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public KACGatewayContext Context    // the Name property
        {
            get => _context;
        }


        public GenericBusiness()
        {
            _context = KACGatewayContext.GetInstance();
            _dbSet = _context.Set<TEntity>();

        }

        public CommandResponse Delete(Guid id)
        {
            var response = new CommandResponse()
            {
                Success = false
            };

            try
            {
                if (id == null) throw new ArgumentNullException("id");

                var entityToDelete = GetById(id);

                if (entityToDelete == null) throw new Exception("Entity not found");

                _dbSet.Remove(entityToDelete);
                _context.SaveChanges();

                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message.ToString();
                return response;
            }
        }

        public CommandResponse Delete(TEntity entity)
        {
            var response = new CommandResponse()
            {
                Success = false
            };
            try
            {
                if (entity == null) throw new ArgumentNullException("entity");

                if (Context.Entry(entity).State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                }

                _dbSet.Remove(entity);
                _context.SaveChanges();

                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message.ToString();
                return response;
            }
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (!string.IsNullOrEmpty(includeProperties) && !string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query).AsEnumerable();
            }
            else
            {
                return query.AsEnumerable();
            }
        }

        public TEntity GetById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public CommandResponse Insert(TEntity entity)
        {
            var response = new CommandResponse()
            {
                Success = false
            };

            try
            {
                var dateTime = DateTime.Now;
                entity.CreationDate = dateTime;
                entity.LastEditDate = dateTime;
                _dbSet.Add(entity);
                _context.SaveChanges();

                response.Success = true;
                response.ID = entity.ID;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message.ToString();
                return response;
            }
        }

        public CommandResponse Update(TEntity entity)
        {
            var response = new CommandResponse()
            {
                Success = false
            };
            try
            {
                var toUpdate = _dbSet.FirstOrDefault(e => e.ID == entity.ID);
                entity.LastEditDate = DateTime.Now;
                _context.Entry(toUpdate).CurrentValues.SetValues(entity);
                _context.SaveChanges();

                response.Success = true;
                response.ID = entity.ID;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message.ToString();
                return response;
            }
        }

        public CommandResponse Save(TEntity entity)
        {
            var response = new CommandResponse()
            {
                Success = false
            };
            try
            {
                var toUpdate = GetById(entity.ID);
                if (toUpdate == default)
                {
                    return Insert(entity);
                }
                else
                {
                    return Update(entity);
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message.ToString();
                return response;
            }
        }
    }
}
