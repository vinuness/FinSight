using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FinSight.Web.DelegateAuth
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
            var token = _accessor.HttpContext?.Request.Cookies["jwt"];
            if(!string.IsNullOrEmpty(token)) 
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return base.SendAsync(request, cancellationToken);
        } 
    }
}