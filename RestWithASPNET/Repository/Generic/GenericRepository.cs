using System.Data;
using System.Collections.Generic;
using RestWithASPNET.Context;
using RestWithASPNET.Model.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace RestWithASPNET.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        // private MySqlContext _context;
        protected MySqlContext _context;
        private DbSet<T> dataSet;

        public GenericRepository(MySqlContext context)
        {
            _context = context;
            dataSet = _context.Set<T>();
        }

        public List<T> FindAll()
        {
            return dataSet.ToList();
        }

        public T FindById(int id)
        {
            return dataSet.SingleOrDefault(d => d.Id.Equals(id));
        }

        public T Create(T item)
        {
            try
            {
                dataSet.Add(item);
                _context.SaveChanges();
                return item;
            }
            catch (System.Exception)
            {
                throw;
            };

        }

        public T Update(T item)
        {
            var result = dataSet.SingleOrDefault( d => d.Id.Equals(item.Id));

            if(result is not null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(item);
                    _context.SaveChanges();
                    return item;
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
            else
            {
                return null;
            }
        }

        public void Delete(int id)
        {
            var result = dataSet.SingleOrDefault( d => d.Id.Equals(id));

            if(result is not null)
            {
                try
                {
                    dataSet.Remove(result);
                    _context.SaveChanges();
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
        }

        public bool Exists(int id)
        {
            return dataSet.Any(d => d.Id.Equals(id));
        }
    }
}