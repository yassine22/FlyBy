using System;
using System.Threading.Tasks;

namespace FlyBy
{
    public abstract class Drone
    {
        protected Connector connector;
        public virtual Task Connect(String addr) {return Task.FromResult(default(object));}
        public abstract void Disconnect();
        public virtual Task Takeoff() {return Task.FromResult(default(object));}
        public virtual Task Land() {return Task.FromResult(default(object));}
        public virtual Task Up(int value) {return Task.FromResult(default(object));}
        public virtual Task Down(int value) {return Task.FromResult(default(object));}
        public virtual Task Pitch(int value) {return Task.FromResult(default(object));}
        public virtual Task Roll(int value) {return Task.FromResult(default(object));}
        public virtual Task Yaw(int value) {return Task.FromResult(default(object));}

    }
}
