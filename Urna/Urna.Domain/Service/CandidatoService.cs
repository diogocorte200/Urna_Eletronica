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
    public class CandidatoService<Tv, Te> : GenericServiceAsync<Tv, Te>
                                              where Tv : CandidatoModel
                                              where Te : Candidato
    {
        ICandidatoRepository _candidatoRepository;

        public CandidatoService(IUnitOfWork unitOfWork, IMapper mapper,
                             ICandidatoRepository candidatoRepository)
        {
            if (_unitOfWork == null)
                _unitOfWork = unitOfWork;
            if (_mapper == null)
                _mapper = mapper;

            if (_candidatoRepository == null)
                _candidatoRepository = candidatoRepository;
        }

        public async Task<Candidato> ModelarCandidato(CandidatoCreateModel candidato)
        {
            Candidato result = new Candidato()
            {
                Id = Guid.NewGuid(),
                NomeCandidato = candidato.NomeCandidato,
                ViceCandidato = candidato.ViceCandidato,
                DataRegistro = DateTime.Now,
                Legenda = Convert.ToInt32(candidato.Legenda)
            };

            return result;
        }

        public async Task<RetornoControllerViewModel<ExibicaoMensagemViewModel, Guid>> AdicionarCandidato(CandidatoCreateModel candidato)
        {
            var candidatoExistente = await BuscarCandidatoPorLegenda(Convert.ToInt32(candidato.Legenda));

            if (candidatoExistente != null)
            {
                return null;
            }

            RetornoControllerViewModel<ExibicaoMensagemViewModel, Guid> retornoController = new RetornoControllerViewModel<ExibicaoMensagemViewModel, Guid>();
            try
            {
                var candidatoInserir = await ModelarCandidato(candidato);
                var entityCandidato = candidatoInserir;

                _candidatoRepository.Add(entityCandidato);
                _candidatoRepository.Save();


                return retornoController;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<string> DeletarCandidato(string idCandidato)
        {

            var result = _candidatoRepository.GetSingleOrDefault(x => x.Id.ToString() ==  idCandidato);

            if (result == null)
                throw new Exception("Candidato não encontrado.");

            _candidatoRepository.Remove(result);
            _candidatoRepository.Save();

            return idCandidato;
        }


        public async Task<List<CandidatoModel>> ListarCandidatos()
        {
            var CandidatosAtivos = _candidatoRepository.GetAll();

            List<CandidatoModel> candidatos = new List<CandidatoModel>();
            foreach (var elem in CandidatosAtivos)
            {
                var lista = new CandidatoModel();
                lista.Id = elem.Id;
                lista.NomeCandidato = elem.NomeCandidato;
                lista.Legenda = elem.Legenda.ToString();
                lista.ViceCandidato = elem.ViceCandidato;
                candidatos.Add(lista);
            }
            return candidatos.ToList();
        }
        public async Task<Candidato> BuscarCandidatoPorLegenda(int legenda)
        {
            var candidato = _candidatoRepository.Find(c => c.Legenda == legenda).SingleOrDefault();

            return candidato;
        }
    }
}
