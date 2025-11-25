using Ecommerce_G02.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Domain.Contacts.ISpecifications
{
    public interface ISpecification<Tentity,Tkey>where Tentity : BaseEntity<Tkey>
    {
        Expression<Func<Tentity,bool>> Ceretria {  get; }
        List<Expression<Func<Tentity,object>>> Include { get; }

        Expression<Func<Tentity,object>> orderBy { get; }
        Expression<Func<Tentity,object>> orderByDesc    { get; }

        int Take { get; }
        int Skip {  get; }
        bool IsPaginted { get; set; }
         
    }
}
