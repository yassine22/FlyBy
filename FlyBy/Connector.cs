using System;
using System.Threading.Tasks;

namespace FlyBy
{
    public abstract class Connector
    {
        public virtual Task Connect(String addr) {return Task.FromResult(default(object));}
        public virtual void Disconnect() {}
    }
}
