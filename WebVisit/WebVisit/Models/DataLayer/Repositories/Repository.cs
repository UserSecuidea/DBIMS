using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebVisit.Models.DomainModels.Passport;

namespace WebVisit.Models
{
    /// <summary>
    /// encapsulate code that interacts with the db.
    /// </summary>
    /// <typeparam name="T">class</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext Context { get; set; }
        // protected BaseSVISITContext Context { get; set; }
        private DbSet<T> Dbset { get; set; }

        public Repository(BaseSVISITContext ctx) {
            Context = ctx;
            Dbset = Context.Set<T>();
        }

        public Repository(BasePASSPORTContext ctx) {
            Context = ctx;
            // DbSet<ViewCardPerson> ViewCardPeople
            Dbset = Context.Set<T>();
        }

        public int TotalCount => Dbset.Count();  // readonly. 테이블 row의 전체 count
        public int Count = 0; // query 조건으로 테이블 row의 전체 count
        public int Max = 0; // query 조건으로 max 값

        // retrieve a list of entities
        public virtual IEnumerable<T> List(QueryOptions<T> options) =>
            BuildQuery(options).ToList();

        public virtual async Task<IEnumerable<T>> ListAsync(QueryOptions<T> options) =>
            await BuildQuery(options).ToListAsync();

        // retrieve a single entity (3 overloads)
        public virtual T? Get(int id) => Dbset.Find(id);
        public virtual T? Get(string id) => Dbset.Find(id);

        public virtual T? GetNT(int id) {
            var entity = Context.Set<T>().Find(id);//dbset.Find(id);
            if (entity != null)
                Context.Entry<T>(entity).State = EntityState.Detached;
            return entity;
            // dbset.AsNoTracking().FirstOrDefaultAsync(id);
        }

        public virtual T? GetNT(string id) {
            var entity = Context.Set<T>().Find(id);//dbset.Find(id);
            if (entity != null)
                Context.Entry<T>(entity).State = EntityState.Detached;
            return entity;
            // dbset.AsNoTracking().FirstOrDefaultAsync(id);
        }

        public virtual T? Get(QueryOptions<T> options) =>
            BuildQuery(options).FirstOrDefault(); // single row

        // insert, update, delete, save
        public virtual T? GetNT(QueryOptions<T> options) {
            try{
                var entity = BuildQuery(options).ToList().FirstOrDefault();
                if (entity != null)
                    Context.Entry<T>(entity).State = EntityState.Detached;
                return entity;
            }catch(Exception e){
                Console.WriteLine(e.ToString());
                return null;
            }
            // dbset.AsNoTracking().FirstOrDefaultAsync(id);
        }

        public virtual void Insert(T entity) => Dbset.Add(entity);
        // public virtual void Insert(T entity){
        //     context.Entry<T>(entity).State = EntityState.Added;
        //     dbset.Add(entity);
        // }
        public virtual void Update(T entity) => Dbset.Update(entity);
        /// <summary>
        ///  부분 업데이트
        /// </summary>
        /// <param name="entity">업데이트 객체</param>
        /// <param name="columns">업데이트 컬럼 명</param>
        public virtual void UpdateP(T entity, List<String> columns) {
            Context.Attach(entity);
            foreach (var p in Context.Entry(entity).Properties){
                if (columns.Contains(p.Metadata.Name)){
                    Context.Entry(entity).Property(p.Metadata.Name).IsModified = true;
                }else{
                    Context.Entry(entity).Property(p.Metadata.Name).IsModified = false;
                }
            }
            Context.SaveChanges();
        } 
        public virtual void Delete(T entity) => Dbset.Remove(entity);
        public virtual void Save() => Context.SaveChanges();

        public virtual async void SaveAsync()
        {
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// private helper method to build query expression
        /// Get(QueryOptions<T> options), List(QueryOptions<T> options) 에서 사용
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        private IQueryable<T> BuildQuery(QueryOptions<T> options)
        {
            IQueryable<T> query = Dbset;
            foreach (string include in options.GetIncludes()) {
                query = query.Include(include);
            }
            if (options.HasWhere) { 
                if (options.Where != null)
                    query = query.Where(options.Where);
                if (options.MultipleWhere.Count > 0) {
                    foreach(var m in options.MultipleWhere) {
                        query = query.Where(m);
                    }
                }
            }
            if(options.HasGroupBy){                
                query.GroupBy(options.GroupBy).SelectMany(c => c);
            }
            if (options.HasOrderBy){
                if (options.OrderBy != null) {
                    // Console.WriteLine("OrderBy:"+options.OrderBy+" OrderByDirection "+options.OrderByDirection);
                    if (options.OrderByDirection == "asc")
                        query = query.OrderBy(options.OrderBy);
                    else
                        query = query.OrderByDescending(options.OrderBy);
                }
                if (options.MultipleOrderBy.Count > 0) {
                    int i = 0;
                    foreach (KeyValuePair<Expression<Func<T, Object>>, string> item in options.MultipleOrderBy)
                    {
                        Console.WriteLine("[BuildQuery>MultipleOrderBy] [{0}:{1}]", item.Key, item.Value);
                        if (i == 0) {
                            if (item.Value == "asc") {
                                query = query.OrderBy(item.Key);
                                
                            } else {
                                query = query.OrderByDescending(item.Key);
                            }
                        } else {
                            if (item.Value == "asc") {
                                query = ((IOrderedQueryable<T>)query).ThenBy(item.Key);
                            } else {
                                query = ((IOrderedQueryable<T>)query).ThenByDescending(item.Key);
                            }
                        }
                        i++;
                    }
                }
            }
            Count = 0;
            try{
                Count = query.Count();
            }catch(InvalidOperationException e){
                Console.WriteLine(e.ToString());
            }
            if(options.HasMax && Count > 0){
                Max = (int)(query.Max(options.Max) ?? 0);
                Console.WriteLine("Max2: "+Max);
            }
            if (options.HasPaging) { 
                query = query.PageBy(options.PageNumber, options.PageSize);
            }
            return query;
        }
    }}