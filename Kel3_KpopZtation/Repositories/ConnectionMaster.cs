using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Models;

namespace Kel3_KpopZtation.Repositories
{
    public class ConnectionMaster
    {
        private static KZEntities db = new KZEntities();
        public static KZEntities CopyInstance () {
            return db;
        }
    }
}