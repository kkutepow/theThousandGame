using System;
using System.Windows.Forms;

namespace TheThousandGame
{
    interface IUpdatable
    {
        Panel Create(int left, int top, int width, int height);
        void Update(object obj);
    }
}
