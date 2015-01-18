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
            Check.That(grid.GetKnownCells()).IsEmpty();
            var cell = new Cell(1, 1);

            grid.Add(cell);
            Check.That(grid.GetKnownCells()).ContainsExactly(cell);
        }

        [TestMethod]
        public void WhenAddRangeThenCellsAreAddedToTheKnownCells()
        {
            var grid = new Grid();
            var cell11 = new Cell(1, 1);
            var cell00 = new Cell(0, 0);

            grid.AddRange(cell00, cell11);
            Check.That(grid.GetKnownCells()).ContainsExactly(cell00, cell11);
        }

        [TestMethod]
        public void WhenGridMutatesThenAllCellMutate()
        {
            var grid = new Grid();
            var cell00 = new Cell(0, 0);
            var cell10 = new Cell(1, 0, CellState.Alive);
            var cell01 = new Cell(0, 1, CellState.Alive);
            var cell11 = new Cell(1, 1, CellState.Alive);

            grid.AddRange(cell00, cell10, cell01, cell11);

            grid.Mutate();
            Check.That(cell00.NextState).IsEqualTo(CellState.Alive);
        }
    }
}
