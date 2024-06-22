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
    public class MenuServices : IMenuServices
    {
        private readonly IGenericRepository<Usuario> _UsuarioRepository;
        private readonly IGenericRepository<MenuRol> _MenuRolRepository;
        private readonly IGenericRepository<Menu> _MenuRepository;
        private readonly IMapper _mapper;

        public MenuServices(IGenericRepository<Usuario> usuarioRepository, IGenericRepository<MenuRol> menuRolRepository, IGenericRepository<Menu> menuRepository, IMapper mapper)
        {
            _UsuarioRepository = usuarioRepository;
            _MenuRolRepository = menuRolRepository;
            _MenuRepository = menuRepository;
            _mapper = mapper;
        }

        public async Task<List<MenuDto>> Lista(int IdUsuario)
        {
          IQueryable<Usuario> usuario = await _UsuarioRepository.Consultar(u=>u.IdUsuario== IdUsuario);
          IQueryable<MenuRol> Menurol = await _MenuRolRepository.Consultar();
          IQueryable<Menu>Menu =await _MenuRepository.Consultar();

            try
            {   
                IQueryable<Menu> resultado = (from u in usuario
                                              join mr in Menurol on u.IdRol equals mr.IdRol
                                              join m in Menu on mr.IdMenu equals m.IdMenu
                                              select m).AsQueryable();
                    var listamenu = resultado.ToList();
                return _mapper.Map<List<MenuDto>>(listamenu);
            }
            catch
            {
                throw;
            }
        }
    }
}
