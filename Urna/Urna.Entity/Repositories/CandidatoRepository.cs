using System;
using System.Collections.Generic;
using System.Text;
using Urna.Entity.Entity;
using Urna.Entity.Repositories.Interfaces;
using Urna.Entity.Context;
using Urna.Entity.Repository;

namespace Urna.Entity.Repositories
{
    public class CandidatoRepository : Repository<Candidato>, ICandidatoRepository
    {
        private UrnaContext _appContext => (UrnaContext)_context;

        public CandidatoRepository(UrnaContext context) : base(context)
        { }
    }
}
