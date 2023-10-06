using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using WebApplicationKinoAPI0510.Models;


namespace WebApplicationKinoAPI0510
{
    public class CommonOperations
    {
        public readonly KinoDb0410Context context;
        string errorMessage = string.Empty;

        public CommonOperations(KinoDb0410Context context)
        {
            this.context = context;
        }
        public CommonOperations()
        {
            this.context = new KinoDb0410Context();
        }



        public async Task<T> GetByIdAsync<T>(object id) where T : class
        {
            try
            {
                return await context.Set<T>().FindAsync(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        public async Task<T> AddEntityAsync<T>(T entity) where T : class
        {
            try
            {
                context.Set<T>().Add(entity);
                await context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<T> RemoveEntityAsync<T>(T entity) where T : class
        {
            try
            {

                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
                return null;
            }
            catch (Exception ex)
            {

                return entity;

            }
        }
        public async Task<T> UpdateEntityAsync<T>(T entity) where T : class
        {
            try
            {
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<IEnumerable<T>> GetAllByFieldAsync<T>(Expression<Func<T, object>> field, object value) where T : class
        {
            try
            {
                var propertyName = GetPropertyName(field);

                // Формируем выражение для фильтрации
                var parameter = Expression.Parameter(typeof(T), "x");
                var property = Expression.Property(parameter, propertyName);
                var equalExpression = Expression.Equal(property, Expression.Constant(value));
                var lambda = Expression.Lambda<Func<T, bool>>(equalExpression, parameter);

                var entities = await context.Set<T>().Where(lambda).ToListAsync();

                return entities;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<T>> GetAllByFieldContainsAsync<T>(Expression<Func<T, string>> field, string value) where T : class
        {
            try
            {
                var parameter = field.Parameters.Single();
                var property = field.Body as MemberExpression;

                if (property == null)
                {
                    throw new ArgumentException("Expression must be a member expression.");
                }

                var propertyInfo = property.Member as PropertyInfo;

                if (propertyInfo == null)
                {
                    throw new ArgumentException("Expression must be a property expression.");
                }

                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var propertyAccess = Expression.MakeMemberAccess(parameter, propertyInfo);
                var searchExpression = Expression.Constant(value, typeof(string));
                var searchValue = Expression.Call(propertyAccess, containsMethod, searchExpression);
                var lambda = Expression.Lambda<Func<T, bool>>(searchValue, parameter);

                var entities = await context.Set<T>().Where(lambda).ToListAsync();

                return entities;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private string GetPropertyName<T>(Expression<Func<T, object>> expression) where T : class
        {
            var member = expression.Body as MemberExpression;
            if (member == null)
            {
                throw new ArgumentException("Expression must be a member expression.");
            }

            return member.Member.Name;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>() where T : class
        {
            try
            {

                var entities = await context.Set<T>().ToListAsync();

                return entities;
            }
            catch (Exception ex)
            {
                return null;
            }
        }




        //public IEnumerable<T> GetAllByField(Expression<Func<T, object>> field, object value)
        //{
        //    try
        //    {
        //        // Получаем имя свойства, по которому нужно фильтровать
        //        var propertyName = GetPropertyName(field);

        //        // Формируем выражение для фильтрации
        //        var parameter = Expression.Parameter(typeof(T), "x");
        //        var property = Expression.Property(parameter, propertyName);
        //        var equalExpression = Expression.Equal(property, Expression.Constant(value));
        //        var lambda = Expression.Lambda<Func<T, bool>>(equalExpression, parameter);

        //        // Выполняем запрос к базе данных с фильтрацией по полю и значению
        //        var entities = this.Entities.Where(lambda);

        //        return entities.ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Обработка ошибок
        //        throw new Exception("Error while fetching entities by field.", ex);
        //    }
        //}


        //private string GetPropertyName(Expression<Func<T, object>> expression)
        //{
        //    var member = expression.Body as MemberExpression;
        //    if (member == null)
        //    {
        //        throw new ArgumentException("Expression must be a member expression.");
        //    }

        //    return member.Member.Name;
        //}


        //public T GetById(object id)
        //{
        //    try
        //    {
        //        return this.Entities.Find(id);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Обработка ошибки
        //        throw new Exception("Error while fetching entity by ID.", ex);
        //    }
        //}



        //public void Update(T entity)
        //{
        //    try
        //    {
        //        if (entity == null)
        //        {
        //            throw new ArgumentNullException("entity");
        //        }

        //        this.context.Update(entity);
        //        this.context.SaveChanges();

        //    }
        //    //catch (DbEntityValidationException dbEx)
        //    //{
        //    //    throw new Exception(errorMessage, dbEx);
        //    //}
        //    catch (Exception ex)
        //    {
        //        // Обработка ошибки
        //        throw new Exception("Error while updating entity.", ex);
        //    }
        //}










        //public void Insert(T entity)
        //{
        //    try
        //    {
        //        if (entity == null)
        //        {
        //            throw new ArgumentNullException("entity");
        //        }

        //        this.Entities.Add(entity);
        //        this.context.SaveChanges();

        //    }
        //    //catch (DbEntityValidationException dbEx)
        //    //{
        //    //    throw new Exception(errorMessage, dbEx);
        //    //}
        //    catch (Exception ex)
        //    {
        //        // Обработка ошибки
        //        throw new Exception("Error while inserting entity.", ex);
        //    }
        //}


        //public void Delete(T entity)
        //{
        //    try
        //    {
        //        if (entity == null)
        //        {
        //            throw new ArgumentNullException("entity");

        //        }

        //        this.Entities.Remove(entity);
        //        this.context.SaveChanges();
        //    }
        //    //catch (DbEntityValidationException dbEx)
        //    //{
        //    //    throw new Exception(errorMessage, dbEx);
        //    //}
        //    catch (Exception ex)
        //    {
        //        // Обработка ошибки
        //        throw new Exception("Error while deleting entity.", ex);
        //    }
        //}

        //public virtual IQueryable<T> Table
        //{
        //    get
        //    {
        //        try
        //        {
        //            return this.Entities;
        //        }
        //        catch (Exception ex)
        //        {
        //            // Обработка ошибки
        //            throw new Exception("Error while fetching entities.", ex);
        //        }
        //    }
        //}




        //// ...


        //public IEnumerable<T> GetAllByField(Expression<Func<T, object>> field, object value)
        //{
        //    try
        //    {
        //        // Получаем имя свойства, по которому нужно фильтровать
        //        var propertyName = GetPropertyName(field);

        //        // Формируем выражение для фильтрации
        //        var parameter = Expression.Parameter(typeof(T), "x");
        //        var property = Expression.Property(parameter, propertyName);
        //        var equalExpression = Expression.Equal(property, Expression.Constant(value));
        //        var lambda = Expression.Lambda<Func<T, bool>>(equalExpression, parameter);

        //        // Выполняем запрос к базе данных с фильтрацией по полю и значению
        //        var entities = this.Entities.Where(lambda);

        //        return entities.ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Обработка ошибок
        //        throw new Exception("Error while fetching entities by field.", ex);
        //    }
        //}


    }

}
