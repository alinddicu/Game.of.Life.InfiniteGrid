using System;

namespace Game.of.Life.InfiniteGrid
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class Cell
    {
        private readonly List<Cell> _knownNeighbours;

        public Cell(int? x, int? y, CellState state = CellState.Dead)
        {
            X = x;
            Y = y;
            CurrentState = state;
            _knownNeighbours = new List<Cell>();
        }

        public CellState CurrentState { get; private set; }

        public CellState? NextState { get; private set; }

        public int? X { get; private set; }

        public int? Y { get; private set; }

        public IEnumerable<Cell> KnownNeighbours
        {
            get
            {
                return _knownNeighbours;
            }
        }

        public void DiscoverNeighbours(Grid grid)
        {
            var startX = X - 1;
            var startY = Y - 1;
            for (var xNeighbour = startX; xNeighbour <= X + 1; xNeighbour++)
            {
                for (var yNeighbour = startY; yNeighbour <= Y + 1; yNeighbour++)
                {
                    var foundCell = grid.GetKnownCells()
                        .SingleOrDefault(c => c.X == xNeighbour && c.Y == yNeighbour) ??
                                    new Cell(xNeighbour, yNeighbour);

                    if (foundCell != this && !_knownNeighbours.Contains(foundCell))
                    {
                        _knownNeighbours.Add(foundCell);
                    }
                }
            }
        }

        public void Mutate()
        {
            NextState = CurrentState;
            var aliveNeighboursCount = KnownNeighbours.Count(n => n.CurrentState == CellState.Alive);
            if (aliveNeighboursCount < 2 || aliveNeighboursCount > 3)
            {
                NextState = CellState.Dead;
            }

            if (CurrentState == CellState.Dead && aliveNeighboursCount == 3)
            {
                NextState = CellState.Alive;
            }
        }

        public void CompleteMutation()
        {
            if (NextState != null)
            {
                CurrentState = NextState.Value;
            }

            NextState = null;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return !ReferenceEquals(this, null) && Equals(obj as Cell);
        }

        private bool Equals(Cell cell)
        {
            return Equals(X, cell.X) && Equals(Y, cell.Y);
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "X : {0}, Y : {1}, CurrentState : {2}, NextState : {3}", X, Y, CurrentState, NextState);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(Cell left, Cell right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Cell left, Cell right)
        {
            return !Equals(left, right);
        }

        public string Draw()
        {
            return CurrentState == CellState.Alive ? "+" : " ";
        }
    }
}
