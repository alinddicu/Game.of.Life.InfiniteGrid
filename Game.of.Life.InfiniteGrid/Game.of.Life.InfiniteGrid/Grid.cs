using System.Linq;

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

        public void RefreshKnownCells()
        {
            var refreshedKnownCells = new List<Cell>();

            foreach (var knownCell in _knownCells)
            {
                knownCell.DiscoverNeighbours(this);
                refreshedKnownCells.AddRange(knownCell.KnownNeighbours);
                refreshedKnownCells.Add(knownCell);
            }

            _knownCells.Clear();
            _knownCells.AddRange(refreshedKnownCells.Distinct());
        }

        public void AddRange(params Cell[] cells)
        {
            _knownCells.AddRange(cells);
        }

        public void Mutate()
        {
            foreach (var cellToMutate in _knownCells)
            {
                cellToMutate.DiscoverNeighbours(this);
                cellToMutate.Mutate();
            }
        }
    }
}
