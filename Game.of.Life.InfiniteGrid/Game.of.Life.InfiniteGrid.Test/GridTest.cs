namespace Game.of.Life.V2.Test
{
    using System.Linq;
    using NFluent;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GridTest
    {
        [TestMethod]
        public void WhenRefreshThenKnownCellsContainsTheUnknownCellsBefore()
        {
            var cell = new Cell(1, 1, CellState.Alive);
            var grid = new Grid();
            grid.Add(cell);

            grid.RefreshKnownCells();
            var supposedRefreshedKnownCells = grid.GetKnownCells().ToList();
            supposedRefreshedKnownCells.Add(cell);
            var refreshedKnownCells = grid.GetKnownCells().ToList();

            Check.That(refreshedKnownCells).HasSize(9);
            Check.That(refreshedKnownCells).Contains(supposedRefreshedKnownCells);
        }

        [TestMethod]
        public void WhenAddCellThenCellIsAddedToTheKnownCells()
        {
            var grid = new Grid();
            Check.That(grid.GetKnownCells()).HasSize(0);
            var cell = new Cell(1, 1);

            grid.Add(cell);
            Check.That(grid.GetKnownCells()).ContainsExactly(cell);
        }
    }
}
