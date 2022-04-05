using System;

namespace AIS.Model.Models
{
    public class AuthenticateResponse
    {
        public string Session { get; set; }
        public string User { get; set; }
        public DateTime Create { get; set; }
    }
}