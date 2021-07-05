using System;
using System.Collections.Generic;
using System.Text;
using Urna.Entity.Entity;
using Urna.Entity.Repositories.Interfaces;
using Urna.Entity.Context;

namespace Urna.Entity.Repositories
{
    public class VotoRepository : Repository<Voto>, IVotoRepository
    {
        private UrnaContext _appContext => (UrnaContext)_context;

        public VotoRepository(UrnaContext context) : base(context)
        { }
    }
}
