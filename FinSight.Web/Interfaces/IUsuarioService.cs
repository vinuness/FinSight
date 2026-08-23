using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinSight.Web.Interfaces
{
    public interface IUsuarioService
    {
        public void Cadastrar();
        public void Login();
        public void FindId();
        public void FindEmail();
        public void SetEmail();
        public void SetNome();
        public void SetPass();
    }
}