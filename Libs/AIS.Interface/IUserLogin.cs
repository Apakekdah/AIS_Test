using AIS.Model.Models;
using System.Collections.Generic;

namespace AIS.Interface
{
    public interface IUserLogin
    {
        AuthenticateResponse Login(AuthenticateUser authenticate, IDictionary<string, string> properties);
    }
}