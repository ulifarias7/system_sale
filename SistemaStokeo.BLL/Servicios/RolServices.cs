using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SistemaStokeo.BLL.Servicios.Contrato;
using SistemaStokeo.DAL.Repositorios.Contratos;
using SistemStokeo.DTO;
using SistemaStokeo.MODELS;

namespace SistemaStokeo.BLL.Servicios
{
    public class RolServices: IRolServices

    {
        private readonly IGenericRepository<Rol> _ROLrepositorio;
        private readonly IMapper _mapper;

        public RolServices(IGenericRepository<Rol> ROLrepositorio, IMapper mapper)
        {
            _ROLrepositorio = ROLrepositorio;
            _mapper = mapper;
        }

        public async Task<List<RolDto>> List()
        {
            try
            {
                var ListaRoles = await _ROLrepositorio.Consultar();
                return _mapper.Map<List<RolDto>>(ListaRoles.ToList());

            }
            catch
            {
                throw;
            }
        }
    }  
}
