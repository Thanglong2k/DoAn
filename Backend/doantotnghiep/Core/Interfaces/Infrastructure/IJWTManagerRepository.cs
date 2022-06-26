using Core.Entities;
using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(Account account);
    }
}
