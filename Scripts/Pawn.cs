#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    public class Pawn
    {
        public Pawn(Vector2Int position)
        {
            Position = position;
        }

        public Vector2Int Position { get; set; }
    }
}
