using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Common
{
    public static class DbSetExtensions
    {
        public static EntityEntry<TEnt> AddIfNotExists<TEnt, TKey>(this DbSet<TEnt> dbSet, TEnt entity, Func<TEnt, TKey> predicate) where TEnt : class
        {
            var exists = dbSet.Any(c => predicate(entity).Equals(predicate(c)));
            return exists
                ? null
                : dbSet.Add(entity);
        }

        public static EntityEntry<TEnt> AddOrUpdate<TEnt, TKey>(this DbSet<TEnt> dbSet, TEnt entity, Func<TEnt, TKey> predicate) where TEnt : class
        {
            var exists = dbSet.Any(c => predicate(entity).Equals(predicate(c)));
            return exists
                ? dbSet.Update(entity)
                : dbSet.Add(entity);
        }

        public static void AddIfNotExists<T>(this DbSet<T> dbSet, Func<T, object> predicate, params T[] entities) where T : class, new()
        {
            foreach (var entity in entities)
            {
                var newValues = predicate.Invoke(entity);
                Expression<Func<T, bool>> compare = arg => predicate(arg).Equals(newValues);
                var compiled = compare.Compile();
                var existing = dbSet.FirstOrDefault(compiled);
                if (existing == null)
                {
                    dbSet.Add(entity);
                }
            }
        }


        public static void AddRangeIfNotExists<TEnt, TKey>(this DbSet<TEnt> dbSet, IEnumerable<TEnt> entities, Func<TEnt, TKey> predicate) where TEnt : class
        {
            var entitiesExist = from ent in dbSet
                                where entities.Any(add => predicate(ent).Equals(predicate(add)))
                                select ent;

            dbSet.AddRange(entities.Except(entitiesExist));
        }
    }
}
