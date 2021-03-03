using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Shared.ViewModels
{
    public class UserAuthorizeView
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
