using Ecommerce_G02.Domain.Contacts.ISpecifications;
using Ecommerce_G02.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Services.Specifications
{
    public abstract class BaseSpecification<Tentity, Tkey> : ISpecification<Tentity, Tkey> where Tentity : BaseEntity<Tkey>
    {
       
      
        
        #region Where Expression Step 1
        public Expression<Func<Tentity, bool>> Ceretria { get; private set; }
        protected BaseSpecification(Expression<Func<Tentity, bool>> _Ceretria)

        {
            Ceretria = _Ceretria;
        }
        #endregion


        #region Add Includes Step 2
        public List<Expression<Func<Tentity, object>>> Include { get; private set; } = [];


        protected void AddInclude(Expression<Func<Tentity, object>> IncludeExpression)
        {
            Include.Add(IncludeExpression);
        }
        #endregion


        #region Ordering Step 3
        public Expression<Func<Tentity, object>> orderBy { get; private set; }

        public Expression<Func<Tentity, object>> orderByDesc { get; private set; } 

        protected void AddOrderBy(Expression<Func<Tentity, object>>? _orderBy)

        {
            orderBy = _orderBy;
        }

        protected void AddOrderByDesc(Expression<Func<Tentity, object>>? _orderByDesc)

        {
            orderByDesc = _orderByDesc;
        }
        #endregion





        #region Pagination Step 4 
        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPaginted { get ; set ; }

        public void ApplayMigination (int pagesize,int pageinedx)
        {
            IsPaginted = true;
            {
                Take = pagesize;
                Skip = (pageinedx-1)*pagesize;
                
            }
        }

        #endregion

    }
}
