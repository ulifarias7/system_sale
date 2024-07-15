﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SistemaStokeo.MODELS;
using SistemStokeo.DTO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SistemaStokeo.UTILITYS
{
    public class Cryptoo
    {
        private readonly IConfiguration _configuration;
        private static readonly List<string> InvalidTokens = new List<string>();

        public Cryptoo(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //metodo para encriptar contraseña
        public string encriptarSHA256(string texto)
        {
            using (SHA256 sHA256 = SHA256.Create())
            {
                //computar el hash
                byte[] bytes = sHA256.ComputeHash(Encoding.UTF8.GetBytes(texto));

                //recorre el array uno por uno  y luego convierte el array de bytes en string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }

        }


        public string generarJWt(SesionDto modelo)//int IDUsuario, string correo  
        {
            //crear la informacion del usuario para el token
            var UserClaims = new[]
            {
              new Claim(ClaimTypes.NameIdentifier,modelo.IdUsuario.ToString()) ,
              new Claim(ClaimTypes.Email,modelo.Correo! )
             };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:key"]!));// convertimos a bytes el token
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);


            //crear detalle del token
            var jwtConfig = new JwtSecurityToken(
                claims: UserClaims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
        }


        public void InvalidarToken(string token)
        {
            InvalidTokens.Add(token);
        }

        public bool EsTokenInvalido(string token)
        {
            return InvalidTokens.Contains(token);
        }





    }

}

