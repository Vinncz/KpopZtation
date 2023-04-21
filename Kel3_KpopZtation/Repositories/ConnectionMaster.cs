using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kel3_KpopZtation.Models;

namespace Kel3_KpopZtation.Repositories {
    public class ConnectionMaster {
        private static KZDBEntities db = new KZDBEntities();
        public static KZDBEntities CopyInstance () {
            return db;
        }
    }
}