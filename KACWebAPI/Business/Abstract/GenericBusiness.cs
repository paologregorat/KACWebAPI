using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using KVMWebAPI.Domain.Command.Commands;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Xml.Linq;
using KACCloudContextLibrary.Domain;
using KACWebAPI.Domain.Query.Abstract;
using KACCloudContextLibrary.DomainDTO.EntityDTO;
using KVMContext.DomainDTO;
using KVMWebAPI.Infrastructure.Utility.RequestFilters;
using KACCloudContextLibrary.Domain.Entity;
using System.Linq.Expressions;

namespace KACWebAPI.Business.Abstract
{
    public abstract class GenericBusiness<TEntity, TSerializer, TEntityDTO> : IGenericBusiness<TEntity> 
            where TEntity : EntityBase 
            where TSerializer : IGenericSerializer
            where TEntityDTO : EntityDTOBase
    {
        private readonly KACCloudContext _context;
        private readonly DbSet<TEntity> _dbSet;
        private readonly IGenericSerializer _serializer;
        private IQueryable<TEntity> _entityIEnumerable;

        public KACCloudContext Context    // the Name property
        {
            get => _context;
        }


        public GenericBusiness(KACCloudContext context, IGenericSerializer serializer, string[] includes)
        {
            //_context = KACCloudContext.GetInstance();
            _context = context;
            var a_dbSet = _context.Set<TEntity>();
            _entityIEnumerable = _context.Set<TEntity>().IncludeMany(includes);
            _serializer = (TSerializer)serializer;
        }

        public List<TEntityDTO> GetAllDTO() 
        {
            var result = new List<TEntityDTO>();
            var toSerializeList = _entityIEnumerable.ToList();
            foreach (var toSerialize in toSerializeList)
            {
                result.Add((TEntityDTO)_serializer.SerializeSingle(toSerialize));
            }
            return result;
        }

        public List<TEntity> Search(Expression<Func<TEntity, bool>> whereClause)
        {
            return _entityIEnumerable.Where(whereClause).ToList();
        }

        public List<TEntityDTO> SearchDTO(Expression<Func<TEntity, bool>> whereClause)
        {
            var toSerializeList = Search(whereClause);
            var result = new List<TEntityDTO>();
            foreach (var toSerialize in toSerializeList)
            {
                result.Add((TEntityDTO)_serializer.SerializeSingle(toSerialize));
            }
            return result;
        }
           

        public TEntityDTO GetByIdDTO(Guid id)
        {
            var toSerialize = _entityIEnumerable.FirstOrDefault(e => e.ID == id);
            
            return (TEntityDTO)_serializer.SerializeSingle(toSerialize);
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
                if (entity.ID == Guid.Empty)
                {
                    entity.ID = Guid.NewGuid();
                    return Insert(entity);
                }
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
