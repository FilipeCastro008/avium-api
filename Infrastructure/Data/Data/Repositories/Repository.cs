using Data.Context;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories {
    public class Repository<TEntity> where TEntity: BaseEntity {

        #region Atributtes
        
        protected readonly AviumContext Db;
        
        protected readonly DbSet<TEntity> DbSet;

        #endregion

        #region Constructor

        public Repository(AviumContext aviumContext) {
            Db = aviumContext;
            DbSet = Db.Set<TEntity>();   
        }

        #endregion

        #region Methods

        public virtual async Task<TEntity> AddAsync(TEntity entity) {
            await DbSet.AddAsync(entity);
            await Db.SaveChangesAsync();    

            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity) {
            DbSet.Update(entity);
            await Db.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<TEntity?> GetByIdAsync(int id) {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> GetAllAsync() {
            return await DbSet.ToListAsync();
        }

        public virtual async Task DeleteAsync(TEntity entity) {
            DbSet.Remove(entity);
            await Db.SaveChangesAsync();
        }

        #endregion

    }
}
