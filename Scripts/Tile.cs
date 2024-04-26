using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    public class Tile<TTileState> where TTileState : Enum
    {
        public TTileState state { get; set; }

        public Tile(TTileState state)
        {
            this.state = state;
        }
    }
}
