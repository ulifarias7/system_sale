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
    public class CategoriaServices : ICategoriaServices
    {
        private readonly IGenericRepository<Categoria> _Categoriarepositorio;
        private readonly IMapper _mapper;

        public CategoriaServices(IGenericRepository<Categoria> rOLrepositorio, IMapper mapper)
        {
            _Categoriarepositorio = rOLrepositorio;
            _mapper = mapper;
        }

        public async Task<List<CategoriaDto>> List()
        {
            try
            {
               var Listacategorias = await _Categoriarepositorio.Consultar();//creamos una variable para cargar los datos que nos va a traer repogeneric 
               return _mapper.Map<List<CategoriaDto>>(Listacategorias.ToList());
            }
            catch
            {
                throw;
            }
        }
    }
}
