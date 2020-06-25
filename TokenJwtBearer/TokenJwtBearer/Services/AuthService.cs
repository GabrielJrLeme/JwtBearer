using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.Linq;
using TokenJwtBearer.Authentication;
using TokenJwtBearer.Models;

namespace TokenJwtBearer.Services
{
    public class AuthService
    {

        private readonly CacheService _cache;


        public AuthService(CacheService cache)
        {
            _cache = cache;
        }


        /// <summary>
        /// Listar usuarios contidos no cache
        /// </summary>
        public List<Usuario> ListUsers()
            => _cache.Cache["key"].Usuarios.ToList();



        /// <summary>
        /// Retorna um usuario expecífico
        /// </summary>
        public Usuario GetUser(Usuario usuario)
            => _cache.Cache["key"].Usuarios.Where(x => x.Password.Equals(usuario.Password) && 
                                                       x.Email.Equals(usuario.Email))
                                           .FirstOrDefault();

        /// <summary>
        /// retorna uma token para o usuario logado
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public string GetTokenUserLogin(Usuario usuario)
            => AuthenticationHeaderToken.GenerationToken(usuario);

    }
}
