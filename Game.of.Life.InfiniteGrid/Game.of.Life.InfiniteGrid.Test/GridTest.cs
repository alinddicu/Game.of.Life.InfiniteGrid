using System.Collections.Generic;

namespace Game.of.Life.InfiniteGrid.Test
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
        public void WhenRefreshThenKnownCellsContainsTheUnknownCellsBeforeBis()
        {
            var cell00 = new Cell(0, 0, CellState.Alive);
            var cell10 = new Cell(-1, 0, CellState.Alive);
            var cell01 = new Cell(0, -1, CellState.Alive);
            var cell11 = new Cell(-1, -1, CellState.Alive);

            var grid = new Grid();
            grid.AddRange(cell00, cell10, cell01, cell11);

            grid.RefreshKnownCells();

            cell00.DiscoverNeighbours(grid);
            cell10.DiscoverNeighbours(grid);
            cell01.DiscoverNeighbours(grid);
            cell11.DiscoverNeighbours(grid);
            var supposedKnown = new List<Cell>();
            supposedKnown.AddRange(cell00.KnownNeighbours);
            supposedKnown.AddRange(cell10.KnownNeighbours);
            supposedKnown.AddRange(cell01.KnownNeighbours);
            supposedKnown.AddRange(cell11.KnownNeighbours);
            supposedKnown = supposedKnown.Distinct().ToList();

            var knownCells = grid.GetKnownCells().ToList();
            Check.That(knownCells).Contains(supposedKnown);

            // 3
            Check.That(knownCells.Where(c => c.X == -2)).HasSize(4);
            Check.That(knownCells.Where(c => c.X == -1)).HasSize(4);
            Check.That(knownCells.Where(c => c.X == 0)).HasSize(4);
            Check.That(knownCells.Where(c => c.X == 1)).HasSize(4);

            Check.That(knownCells.Where(c => c.Y == -2)).HasSize(4);
            var yEgalMinus1 = knownCells.Where(c => c.Y == -1).ToList();
            // 5 -> (2;-1)
            Check.That(knownCells.Where(c => c.Y == -1)).HasSize(4);
            var yEgal0 = knownCells.Where(c => c.Y == 0).ToList();
            // 5 -> (2;0)
            Check.That(knownCells.Where(c => c.Y == 0)).HasSize(4);
            Check.That(knownCells.Where(c => c.Y == 1)).HasSize(4);
            Check.That(knownCells).HasSize(16);
        }

        [TestMethod]
        public void WhenRefreshThenKnownCellsContainsTheUnknownCellsBeforeBisBis()
        {
            var cell11 = new Cell(1, 0, CellState.Alive);

            var grid = new Grid();
            grid.AddRange(cell11);

            grid.RefreshKnownCells();

            cell11.DiscoverNeighbours(grid);
            var supposedKnown = new List<Cell>();
            supposedKnown.AddRange(cell11.KnownNeighbours);
            supposedKnown = supposedKnown.Distinct().ToList();

            Check.That(grid.GetKnownCells()).Contains(supposedKnown);

            Check.That(grid.GetKnownCells()).HasSize(9);
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

        [TestMethod]
        public void WhenGridCompleteMutationThenAllCellCompleteMutation()
        {
            var grid = new Grid();
            var cell00 = new Cell(0, 0);
            var cell10 = new Cell(1, 0, CellState.Alive);
            var cell01 = new Cell(0, 1, CellState.Alive);
            var cell11 = new Cell(1, 1, CellState.Alive);

            grid.AddRange(cell00, cell10, cell01, cell11);

            grid.Mutate();
            grid.CompleteMutation();
            Check.That(cell00.CurrentState).IsEqualTo(CellState.Alive);
        }

        [TestMethod]
        public void Given2By2GridWhenDrawThenDrawingIsCorrect()
        {
            var grid = new Grid();
            var cell00 = new Cell(0, 0);
            var cell10 = new Cell(1, 0, CellState.Alive);
            var cell01 = new Cell(0, 1, CellState.Alive);
            var cell11 = new Cell(1, 1, CellState.Alive);

            grid.AddRange(cell00, cell10, cell01, cell11);

            var lines = grid.Draw().ToList();
            Check.That(lines[0]).IsEqualTo(" +");
            Check.That(lines[1]).IsEqualTo("++");
        }

        [TestMethod]
        public void RunOneCycleTest()
        {
            var grid = new Grid();
            var cell1 = new Cell(-1, 0, CellState.Alive);
            var cell2 = new Cell(0, 1, CellState.Alive);
            var cell3 = new Cell(0, 0, CellState.Alive);
            var cell4 = new Cell(0, -1, CellState.Alive);
            var cell5 = new Cell(1, 1, CellState.Alive);
            var cell6 = new Cell(1, 0, CellState.Alive);
            var cell7 = new Cell(1, -1, CellState.Alive);

            grid.AddRange(cell1, cell2, cell3, cell4, cell5, cell6, cell7);
            grid.RunOneCycle();

            var lines = grid.Draw().ToList();
            Check.That(lines[1]).Contains("+ +");
            Check.That(lines[2]).Contains("+  +");
            Check.That(lines[3]).Contains("+ +");
        }

        [TestMethod]
        public void GivenDeadCellWithAllDead5NeighboursWhenTrimHopelessBorderCellsThenCellDies()
        {
            var grid = new Grid();
            var cell1 = new Cell(-1, 0);
            var cell2 = new Cell(0, 0);
            var cell3 = new Cell(1, 0);
            var cell4 = new Cell(-1, -1);
            var cell5 = new Cell(0, -1);
            var cell6 = new Cell(1, -1);
            var cell7 = new Cell(-1, -2, CellState.Alive);
            var cell8 = new Cell(0, -2, CellState.Alive);
            var cell9 = new Cell(1, -2, CellState.Alive);

            var allCells = new[] {cell1, cell2, cell3, cell4, cell5, cell6, cell7, cell8, cell9};
            grid.AddRange(allCells);
            grid.TrimHopelessBorderCells();

            Check.That(grid.GetKnownCells()).Contains(allCells.Except(new []{cell3}));
        }
    }
}
