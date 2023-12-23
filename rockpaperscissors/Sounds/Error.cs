﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable CA1416

namespace rockpaperscissors.Sounds
{
    internal class Error : Sound
    {
        protected override void play()
        {
            PlayNote("D#", 350, 0.5f);
            PlayNote("C");
        }
    }
}
