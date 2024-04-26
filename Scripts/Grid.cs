#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace Grid
{
    public class Grid<TTileState, TPawnType> : MonoBehaviour where TTileState : Enum where TPawnType : Pawn
    {
        public Grid(Vector2Int startSize)
        {
            ResizeGrid(startSize);
        }

        private readonly Dictionary<Vector2Int, Tile<TTileState>> _tiles = new();
        private readonly List<TPawnType> _pawns = new();
        private Vector2Int _size = Vector2Int.zero;

        public void ResizeGrid(Vector2Int newSize)
        {
            foreach (var pos in EnumerateTo(newSize))
            {
                if (!_tiles.ContainsKey(pos))
                {
                    _tiles.Add(pos, new Tile<TTileState>((TTileState) (object) 0));
                }
            }

            _size = newSize;

            TruncateGrid();
        }

        private void TruncateGrid()
        {
            var outsideTiles = _tiles.Keys.Where(pos => !IsInsideGrid(pos)).ToList();
            foreach (var pos in outsideTiles) _tiles.Remove(pos);
        }

        public bool IsInsideGrid(Vector2Int pos)
        {
            return pos.x >= 0 && pos.y >= 0 && pos.x < _size.x && pos.y < _size.y;
        }

        public Tile<TTileState> GetTile(Vector2Int pos)
        {
            return _tiles.TryGetValue(pos, out var value) ?
                value :
                new Tile<TTileState>((TTileState) (object) -1);
        }

        public IEnumerable<TPawnType> GetPawns(Vector2Int pos)
        {
            return _pawns.Where(p => p.Position == pos);
        }

        public void AddPawn(TPawnType pawn)
        {
            if (!_pawns.Contains(pawn))
            {
                _pawns.Add(pawn);
            }
        }

        public void RemovePawn(TPawnType pawn)
        {
            _pawns.Remove(pawn);
        }

        public void MovePawn(TPawnType pawn, Vector2Int newPos)
        {
            if (!_pawns.Contains(pawn)) return;
            pawn.Position = newPos;
        }

        private static IEnumerable<Vector2Int> EnumerateTo(Vector2Int self, bool exclusive = true)
        {
            var xMax = exclusive ? self.x - 1 : self.x;
            var yMax = exclusive ? self.y - 1 : self.y;

            for (var i = 0; i <= xMax; i++)
            {
                for (var j = 0; j <= yMax; j++)
                {
                    yield return new Vector2Int(i, j);
                }
            }
        }

        public (Tile<TTileState>, TPawnType?) GetTileInfo(Vector2Int position)
        {
            var tile = GetTile(position);
            var pawn = GetPawns(position).FirstOrDefault();
            return (tile, pawn);
        }
    }
}
