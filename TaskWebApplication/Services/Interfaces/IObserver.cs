using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskWebApplication.Models;

namespace TaskWebApplication.Services.Interfaces
{
    public interface IObserver
    {
        void Update(ATask task);
    }
}
