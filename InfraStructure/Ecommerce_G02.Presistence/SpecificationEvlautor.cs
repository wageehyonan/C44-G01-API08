using Ecommerce_G02.Domain.Contacts.ISpecifications;
using Ecommerce_G02.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Presistence
{
    public static class SpecificationEvlautor
    {
        public static IQueryable<Tentity> CreateQuery<Tentity,Tkey>(IQueryable<Tentity> BaseQuery ,ISpecification<Tentity,Tkey> Specificstion) where Tentity : BaseEntity<Tkey>
        {
            var Query=BaseQuery;

            if( Specificstion.Ceretria != null )

            {
                Query = Query.Where(Specificstion.Ceretria);
            }


            if(Specificstion.orderBy != null )
            {
                Query=Query.OrderBy(Specificstion.orderBy);
            }

            if (Specificstion.orderByDesc != null)
            {
                Query = Query.OrderBy(Specificstion.orderByDesc);
            }

            if (Specificstion.IsPaginted)
            {
                Query = Query.Skip(Specificstion.Skip).Take(Specificstion.Take);
            }


            if (Specificstion.Include is not null && Specificstion.Include.Any())
            {
                Query = Specificstion.Include.Aggregate(Query, (CurrentQuery, expression) => CurrentQuery.Include(expression));
            }
            return Query;
        }
    }
}
