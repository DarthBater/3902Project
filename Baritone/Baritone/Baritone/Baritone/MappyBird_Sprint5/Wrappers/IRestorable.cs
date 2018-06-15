using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.MappyBird_Sprint5.Wrappers
{
    /*
        An IRestorable is meant to represent anything that has a
        "default state" that it can return to. This can include
        position, sprite states, etc...
    */
    public interface IRestorable
    {

        void Restore();

    }
}
