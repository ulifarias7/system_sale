using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SistemaStokeo.MODELS;
using SistemStokeo.DTO;
namespace SistemaStokeo.UTILITYS
{
    public class AutoMapperProfile : Profile
    {


        public AutoMapperProfile()
        {
            #region Rol
            CreateMap<Rol, RolDto>().ReverseMap();
            #endregion Rol

            #region Menu
            CreateMap<Menu, MenuDto>().ReverseMap();
            #endregion Menu

            #region Usuario
            CreateMap<Usuario, UsuarioDto>()
                .ForMember(destino =>destino.RolDescripcion,opt => opt.MapFrom(origen => origen.IdRolNavigation.Nombre)
                )
                .ForMember(destino =>
                destino.EsActivo,opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                );

            CreateMap<Usuario, SesionDto>()
                .ForMember(destino =>destino.RolDescripcion,opt => opt.MapFrom(origen =>origen.IdRolNavigation.Nombre)
               );

            CreateMap<UsuarioDto, Usuario>()
                .ForMember(destino =>destino.IdRolNavigation,opt => opt.Ignore()
                 )
                .ForMember(destino =>destino.EsActivo,opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                );

          
       
            #endregion Usuario

            #region Categoria
            CreateMap<Categoria, CategoriaDto>().ReverseMap();
            #endregion Categoria

            #region Producto
            CreateMap<Producto, ProductoDto>()
                .ForMember(destino =>
                destino.CategoriaDescripcion,
                opt => opt.MapFrom(origen => origen.IdCategoriaNavigation.Nombre)
                )
               .ForMember(destino =>
                destino.Precio,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-PE")))
                )
             .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                );


            CreateMap<ProductoDto, Producto>()
            .ForMember(destino =>
            destino.IdCategoriaNavigation,
            opt => opt.Ignore()
            )
           .ForMember(destino =>
            destino.Precio,
            opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("es-PE")))
            )
         .ForMember(destino =>
            destino.EsActivo,
            opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
            );
            #endregion Producto

            #region Venta
            CreateMap<Venta, VentaDto>()
                .ForMember(destino =>
                destino.TotalTexto,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-PE")))
                )
                  .ForMember(destino =>
                destino.FechaRegistro,
                opt => opt.MapFrom(origen => origen.FechaRegistro.Value.ToString("dd/MM/yyyy"))
                );

            CreateMap<VentaDto, Venta>()
                .ForMember(destino =>
                destino.Total,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("es-PE")))
                );

            #endregion Venta

            #region DetalleVenta
            CreateMap<DetalleVenta, DetalleVentaDto>()
             .ForMember(destino =>
                destino.DescripcionProducto,
                opt => opt.MapFrom(origen =>
                origen.IdProductoNavigation.Nombre)
                )
              .ForMember(destino =>
                destino.PrecioTexto,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-PE")))
                )
              .ForMember(destino =>
                destino.TotalTexto,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-PE")))
                );

            CreateMap<DetalleVentaDto, DetalleVenta>()
                .ForMember(destino =>
                destino.Precio,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.PrecioTexto, new CultureInfo("es-PE")))
                )
                   .ForMember(destino =>
                destino.Total,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("es-PE")))
                );




            #endregion DetalleVenta

            #region Reporte
            CreateMap<DetalleVenta, ReporteDto>()
                .ForMember(destino =>
                destino.FechaRegistro,
                opt => opt.MapFrom(origen => origen.IdVentaNavigation.FechaRegistro.Value.ToString("dd/MM/yyyy"))
                )
                .ForMember(destino =>
                destino.NumeroDocumento,
                opt => opt.MapFrom(origen => origen.IdVentaNavigation.NumeroDocumento)
                )
                 .ForMember(destino =>
                destino.TipoPago,
                opt => opt.MapFrom(origen => origen.IdVentaNavigation.TipoPago)
                )
                 .ForMember(destino =>
                destino.TotalVenta,
                opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.Total.Value, new CultureInfo("dd/MM/yyyy")))
                )
                 .ForMember(destino =>
                destino.Producto,
                opt => opt.MapFrom(origen => origen.IdProductoNavigation.Nombre)
                )
                .ForMember(destino =>
                destino.Precio,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("dd/MM/yyyy")))
                )
                .ForMember(destino =>
                destino.Total,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("dd/MM/yyyy")))
                );

            #endregion Reporte 

        }
    }
}
