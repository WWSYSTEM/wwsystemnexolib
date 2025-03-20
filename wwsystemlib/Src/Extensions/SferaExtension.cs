using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using InsERT.Moria.ModelDanych;
using InsERT.Moria.Narzedzia.PolaWlasne2;
using InsERT.Moria.PolaWlasne2;
using InsERT.Moria.Rozszerzanie;

namespace WWsystemLib
{
    public static class SferaExtension
    {
        public static List<T> SferaWhere<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate)
            where T : class
        {
            var result = query.Where(predicate)
                .ResolveExtensionProperties()
                .ToList();
            return result;
        }

        public static ActionResult<T> SferaFirstActionResult<T>(this IQueryable<T> query,
            Expression<Func<T, bool>> predicate)
            where T : class
        {
            var result = query.Where(predicate)
                .ResolveExtensionProperties()
                .FirstOrDefault();
            if (result == null)
            {
                return ActionResult<T>.Fail("Not found");
            }

            return ActionResult<T>.Success(result);
        }


        public static bool SferaAny<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate) where T : class
        {
            return query.Where(predicate)
                .ResolveExtensionProperties()
                .Any();
        }

        public static T SferaFirstOrDefault<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate)
            where T : class
        {
            return query.Where(predicate)
                .ResolveExtensionProperties()
                .FirstOrDefault();
        }


        public static string WypiszBledy(this InsERT.Mox.ObiektyBiznesowe.IObiektBiznesowy obiektBiznesowy)
        {
            var sb = new StringBuilder();
            sb.AppendLine("");
            var result = WypiszBledy((InsERT.Mox.BusinessObjects.IBusinessObject)obiektBiznesowy);
            sb.AppendLine(result);
            var uow = ((InsERT.Mox.BusinessObjects.IGetUnitOfWork)obiektBiznesowy).UnitOfWork;
            foreach (var innyObiektBiznesowy in uow.Participants.OfType<InsERT.Mox.BusinessObjects.IBusinessObject>()
                         .Where(bo => bo != obiektBiznesowy))
            {
                result = WypiszBledy(innyObiektBiznesowy);
                sb.AppendLine(result);
            }

            sb.AppendLine("");
            return sb.ToString();
        }

        public static string WypiszBledy(this InsERT.Mox.BusinessObjects.IBusinessObject obiektBiznesowy)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"==== Błędy dla obiektu {obiektBiznesowy.ToString()}");
            foreach (var encjaZBledami in obiektBiznesowy.InvalidData)
            {
                foreach (var bladNaCalejEncji in encjaZBledami.Errors)
                {
                    sb.AppendLine($"{encjaZBledami.GetType().Name} {bladNaCalejEncji}");
                }

                foreach (var bladNaKonkretnychPolach in encjaZBledami.MemberErrors)
                {
                    var bladNaPolach = string.Join(", ",
                        bladNaKonkretnychPolach.Select(b => b));
                    sb.AppendLine($"Pole {bladNaPolach} {bladNaKonkretnychPolach.Key}");
                }
            }

            sb.AppendLine($"=========================================================");
            return sb.ToString();
        }
    }
}