namespace Game.of.Life.V2
{
    using System.Collections.Generic;

    public class Grid
    {
        private readonly List<Cell> _knownCells = new List<Cell>(); 

        public IEnumerable<Cell> GetKnownCells()
        {
            return _knownCells;
        }

        public void Add(Cell cell)
        {
            if (!_knownCells.Contains(cell))
            {
                _knownCells.Add(cell);
            }
        }
    }
}
