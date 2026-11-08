using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FinSight.Metas.Infrastructure.Utilidades
{
    public class JWTHandler : DelegatingHandler
    {   
        private readonly IHttpContextAccessor _accessor;

        public JWTHandler(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        //pega o token JWT automaticamente armazenado no cookie do navegador
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var auth = _accessor.HttpContext?.Request.Headers["Authorization"].ToString();
            Console.WriteLine($"AUTH NO GOAL: [{auth}]");
            if(!string.IsNullOrEmpty(auth)) 
                request.Headers.TryAddWithoutValidation("Authorization", auth);

            return base.SendAsync(request, cancellationToken);
        } 
    }
}