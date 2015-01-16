namespace Game.of.Life.V2
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class Cell
    {
        public Cell(int x, int y, CellState state = CellState.Alive)
        {
            X = x;
            Y = y;
            CurrentState = state;
            Neighbours = new List<Cell>();
        }

        public CellState CurrentState { get; set; }

        public CellState? NextState { get; private set; }

        public int X { get; private set; }

        public int Y { get; private set; }

        public List<Cell> Neighbours { get; private set; }

        public void AddNeighbours(params Cell[] cells)
        {
            if (Neighbours.Count() > 8)
            {
                throw new Exception("The cell can't have more than 8 neighbours");
            }

            foreach (var cell in cells)
            {
                Neighbours.Remove(cell);
                Neighbours.Add(cell);
            }
        }

        public void Mutate()
        {
            NextState = CurrentState;
            var aliveNeighboursCount = Neighbours.Count(n => n.CurrentState == CellState.Alive);
            if (aliveNeighboursCount < 2 || aliveNeighboursCount > 3)
            {
                NextState = CellState.Dead;
            }

            if (CurrentState == CellState.Dead && aliveNeighboursCount == 3)
            {
                NextState = CellState.Alive;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(this, null))
            {
                return false;
            }

            return this.Equals(obj as Cell);
        }

        private bool Equals(Cell cell)
        {
            return Equals(X, cell.X) && Equals(Y, cell.Y);
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "X : {0}, Y : {1}, State : {2}", X, Y, CurrentState);
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

        public void CompleteMutation()
        {
            CurrentState = NextState.Value;
            NextState = null;
        }
    }
}
