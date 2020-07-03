namespace MiraThree.Base
{
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using CoHAExceptions;
    using Microsoft.EntityFrameworkCore;

    public class EntityRepository<T> : IRepository<T> where T : class, IModel
    {
        public DbContext DbContext { get; set; }

        public EntityRepository(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<T> Create(T item)
        {
            //We create an empty container for our product

            //Open connection to the database
            await using (var transaction = await DbContext.Database.BeginTransactionAsync())
            {
                //Add our item to the database
                await DbContext.AddAsync(item);

                //Save the in-memory local changes.
                await DbContext.SaveChangesAsync();

                //Overwrite db with local changes
                transaction.Commit();
            }

            var product = await Read(item.Id);
            //Return product.
            return product;
        }

        public async Task<T> Read(string id)
        {
            //Declaring container for our finished product.
            T product;

            //Opening connection to the database
            await using (await DbContext.Database.BeginTransactionAsync())
            {
                product = await
                    DbContext.Set<T>().AsQueryable().SingleOrDefaultAsync(t =>
                        t.Id.ToLower() == id.ToLower());
            }

            return product;
        }

        public async Task<T> Update(T item)
        {
            T product;
            await using (var transaction = await DbContext.Database.BeginTransactionAsync())
            {
                product = await DbContext.Set<T>().SingleOrDefaultAsync(t =>
                    t.Id.ToLower() == item.Id.ToLower());
                if (product is null) throw new NotFoundException();
                SyncObjects(product, item);

                await DbContext.SaveChangesAsync();
                transaction.Commit();
            }

            return product;
        }

        public async Task<T> Delete(string id)
        {
            T product;
            using (var transaction = await DbContext.Database.BeginTransactionAsync())
            {
                //Fetch Item from Database (if it exists)
                product = await DbContext.Set<T>().SingleOrDefaultAsync(t =>
                    t.Id.ToLower() == id.ToLower());

                //Remove Item from Database (if we found one).
                if (product != null) DbContext.Remove(product); //Calling this will throw an exception if its null
                //so we check first.

                await DbContext.SaveChangesAsync(); //Save changes to the Ts in memory.

                transaction.Commit(); //Overwrite database to make it match what is in memory.
            }

            return product;
        }

        public async Task<List<T>> ReadAll(Dictionary<string, string> parameters)
        {
            var model = $"{typeof(T).Name}s";
            var sql = new StringBuilder($"SELECT * FROM {model} ");

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    sql.Append($"WHERE {parameter.Key} = '{parameter.Value}'");
                }
            }
            
            var product = await DbContext.Set<T>().FromSqlRaw(sql.ToString()).ToListAsync();

            return product;
        }

        private void SyncObjects<T>(T entity, T update)
        {
            foreach (var property in typeof(T).GetProperties())
            {
                var eValue = property.GetValue(entity);
                var uValue = property.GetValue(update);
                if (eValue != uValue) property.SetValue(entity, uValue);
            }
        }
    }
}