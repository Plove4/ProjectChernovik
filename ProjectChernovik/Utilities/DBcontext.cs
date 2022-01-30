using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectChernovik.Entities;

namespace ProjectChernovik.Utilities
{
    internal class DBcontext
    {
        private static ChernovikEntities1 _context { get; set; }

        public static ChernovikEntities1 Context
        {
            get
            {
                if (_context == null)
                    _context = new ChernovikEntities1();
                return _context;
            }
        }
    }
}
