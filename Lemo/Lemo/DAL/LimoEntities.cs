using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects;
//using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Data;
//using System.Data.Entity.Validation;

namespace Lemo.DAL
{
    public partial class LimoEntitiesEntityFremwork 
    {
        public int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            //catch (DbEntityValidationException ex)
            //{
            //    foreach (var error in ex.EntityValidationErrors)
            //    {
            //        Console.WriteLine("====================");
            //        Console.WriteLine(
            //            "Entity {0} in state {1} has validation errors:",
            //            error.Entry.Entity.GetType().Name,
            //            error.Entry.State);
            //        foreach (var ve in error.ValidationErrors)
            //        {
            //            Console.WriteLine("\tProperty: {0}, Error: {1}", ve.PropertyName, ve.ErrorMessage);
            //        }
            //        Console.WriteLine();
            //    }
            //    throw;
            //}
            catch (UpdateException ex)
            {
                var xx = ex.StateEntries;
                throw ex;
            }
        }
    }
}