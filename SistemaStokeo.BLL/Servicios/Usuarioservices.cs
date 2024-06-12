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
using Microsoft.EntityFrameworkCore;

namespace SistemaStokeo.BLL.Servicios
{
    public class Usuarioservices : IUsuarioServices
    {
        private readonly IGenericRepository<Usuario> _usuariorepositorio;
        private readonly IMapper _mapper;

        public Usuarioservices(IGenericRepository<Usuario> usuariorepositorio, IMapper mapper)
        {
            _usuariorepositorio = usuariorepositorio;
            _mapper = mapper;
        }

        public async Task<List<UsuarioDto>> Lista()
        {
            try
            {
                //creamos una variable ; vamos a trabajar con mi interfaz de repositprio generico y con mi metodo consultar(el cual nos devuelve un Iqueryble de usuario)
                var queryUsuario = await _usuariorepositorio.Consultar();
                //creamos una lista y acedemos a quaryusuario,y vamos a incluir la descripcion del rol de cada usuario
                var listadeUsuario = queryUsuario.Include(rol=>rol.IdRolNavigation).ToList();


                return _mapper.Map<List<UsuarioDto>>(listadeUsuario);
            }
            catch
            {
                throw;
            }
        }


         

        public async Task<SesionDto> ValidarCredenciales(string correo, string clave)
        {
            try
            {
                var queryUsuario = await _usuariorepositorio.Consultar(u =>
                u.Correo == correo &&
                u.Clave == clave
                );

                if (queryUsuario.FirstOrDefault() == null)
                    throw new TaskCanceledException("el usuario no existe");


                Usuario devorverUsuario = queryUsuario.Include(rol=> rol.IdRolNavigation).First();
                return _mapper.Map<SesionDto>(devorverUsuario);
            }
            catch
            {
                throw;//devuelve error
            }
        }


        public async Task<UsuarioDto> Crear(UsuarioDto modelo)
        {
            try
            {
                var usuarioCreado = await _usuariorepositorio.Crear(_mapper.Map<Usuario>(modelo));
                if(usuarioCreado.IdUsuario==0)
                    throw new TaskCanceledException("el usuario no pudo ser creado");
                var query = await _usuariorepositorio.Consultar(u => u.IdUsuario == usuarioCreado.IdUsuario);

                usuarioCreado = query.Include(rol => rol.IdRolNavigation).First();
                return _mapper.Map<UsuarioDto>(usuarioCreado);
                
            }
            catch
            {
                throw;
            }

        }

        public async Task<bool> Editar(UsuarioDto modelo)
        {
            try
            {
                var usuarioModelo = _mapper.Map<Usuario>(modelo);
                var usuarioEncontrado = await _usuariorepositorio.Obtener(u=>u.IdUsuario==usuarioModelo.IdUsuario);

                if(usuarioEncontrado == null)
                    throw new TaskCanceledException("el usuario no existe");


                usuarioEncontrado.NombreCompleto = usuarioModelo.NombreCompleto;
                usuarioEncontrado.Correo=usuarioModelo.Correo;
                usuarioEncontrado.IdRol=usuarioModelo.IdRol;
                usuarioEncontrado.Clave=usuarioModelo.Clave;
                usuarioEncontrado.EsActivo=usuarioModelo.EsActivo;

                bool respuesta = await _usuariorepositorio.Editar(usuarioEncontrado);
                if(respuesta)
                    throw new TaskCanceledException("no se pudo editar");

                return respuesta;



            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var usuarioEncontrado = await _usuariorepositorio.Obtener(u => u.IdUsuario == id);

                if(usuarioEncontrado == null )
                    throw new TaskCanceledException("el usuario no existe ");

                bool respuesta = await _usuariorepositorio.Delete(usuarioEncontrado);
                if (respuesta)
                    throw new TaskCanceledException("no se pudo editar");

                return respuesta;
            }
            catch
            {
                throw;
            }
        }

      
        
    }
}
