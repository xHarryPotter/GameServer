using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Interfaces
{
    public interface IResource
    {
        Task OnStart();
        Task OnStop();
    }
}
