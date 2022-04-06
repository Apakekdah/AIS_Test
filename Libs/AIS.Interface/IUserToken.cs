using AIS.Model.Models;
using System.Collections.Generic;

namespace AIS.Interface
{
    public interface IUserToken
    {
        AuthenticateResponse RefreshToken(string oldToken, IDictionary<string, object> properties);
    }
}