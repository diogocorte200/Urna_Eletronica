using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Urna.Domain.Domain;
using Urna.Domain.Service.Generic;
using Urna.Entity.Entity;
using Urna.Entity.Repositories.Interfaces;
using Urna.Entity.UnitofWork;

namespace Urna.Domain.Service
{
    public class VotoService<Tv, Te> : GenericServiceAsync<Tv, Te>
                                              where Tv : VotoModel
                                              where Te : Voto
    {
        IVotoRepository _votoRepository;
        ICandidatoRepository _candidatoRepository;

        public VotoService(IUnitOfWork unitOfWork, IMapper mapper,
                             IVotoRepository votoRepository, ICandidatoRepository candidatoRepository)
        {
            if (_unitOfWork == null)
                _unitOfWork = unitOfWork;
            if (_mapper == null)
                _mapper = mapper;

            if (_votoRepository == null)
                _votoRepository = votoRepository;

            if (_candidatoRepository == null)
                _candidatoRepository = candidatoRepository;
        }

        public async Task<Voto> ModelarVoto(VotoModel voto)
        {
            Voto result = new Voto()
            {
                DataVoto = DateTime.Now,
                IdCandidato = voto.IdCandidato
            };

            return result;
        }

        public async Task<List<VotoModel>> ListarVotos()
        {
            var votosAtivos = _votoRepository.GetAll();

            List<VotoModel> votos = new List<VotoModel>();
            foreach (var elem in votosAtivos)
            {
                var lista = new VotoModel();
                lista.Id = elem.Id;

                votos.Add(lista);
            }

            return votos.OrderBy(x => x.DataVoto).ToList();
        }

        public async Task<RetornoControllerViewModel<ExibicaoMensagemViewModel, Guid>> AdicionarVoto(VotoModel voto)
        {
            RetornoControllerViewModel<ExibicaoMensagemViewModel, Guid> retornoController = new RetornoControllerViewModel<ExibicaoMensagemViewModel, Guid>();
            try
            {
                var votoInserir = await ModelarVoto(voto);
                var entityVoto = votoInserir;

                _votoRepository.Add(entityVoto);
                _votoRepository.Save();


                return retornoController;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<ComputarVotoModel>> ComputarVotos()
        {
            var votosComputados = new List<ComputarVotoModel>();
            var candidatos = _candidatoRepository.GetAll();

            try
            {
                foreach (var candidato in candidatos)
                {
                    var votosCandidato = _votoRepository.Find(v => v.IdCandidato == candidato.Id);

                    var votoComputado = new ComputarVotoModel
                    {
                        NomeCompleto = candidato.NomeCandidato,
                        ViceCandidato = candidato.ViceCandidato,
                        Legenda = candidato.Legenda.ToString(),
                        QtdVotos = votosCandidato.Count()
                    };

                    votosComputados.Add(votoComputado);
                }

                return votosComputados;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}