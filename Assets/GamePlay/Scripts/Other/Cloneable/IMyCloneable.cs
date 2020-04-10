using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GamePlay.Scripts.Other.Cloneable
{
    public interface IMyCloneable<T>
    {
        void Clone(T t);
    }
}
