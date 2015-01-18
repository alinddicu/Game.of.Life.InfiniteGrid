namespace Game.of.Life.InfiniteGrid
{
    using System.Linq;
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

            _knownCells.AddRange(refreshedKnownCells.Distinct().Except(_knownCells));
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

        public void CompleteMutation()
        {
            foreach (var cell in _knownCells)
            {
                cell.CompleteMutation();
            }
        }

        public IEnumerable<string> Draw()
        {
            OrderCells();
            var yCellGroups = _knownCells.GroupBy(c => c.Y).ToDictionary(g => g.Key);

            foreach (var yKey in yCellGroups.Keys.OrderBy(k => k))
            {
                var line = string.Empty;
                foreach (var cell in yCellGroups[yKey])
                {
                    line += cell.Draw();
                }

                yield return line;
            }
        }

        private void OrderCells()
        {
            var yCellGroups = _knownCells.GroupBy(c => c.Y).ToDictionary(g => g.Key);

            var orderedCells = new List<Cell>();
            foreach (var yKey in yCellGroups.Keys.OrderBy(k => k))
            {
                orderedCells.AddRange(yCellGroups[yKey].OrderBy(c => c.X));
            }

            _knownCells.Clear();
            _knownCells.AddRange(orderedCells);
        }

        public void RunOneCycle()
        {
            Mutate();
            CompleteMutation();
            RefreshKnownCells();
        }
    }
}
