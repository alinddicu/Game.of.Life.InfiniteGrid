using System.Collections.Generic;

namespace Game.of.Life.V2.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NFluent;

    [TestClass]
    // 1. Any live cell with less than two live neighbours dies, as if caused by under-population.
    // 2. Any live cell with two or three live neighbours lives on to the next generation.
    // 3. Any live cell with more than three live neighbours dies, as if by overcrowding.
    // 4. Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
    public class CellTest
    {
        [TestMethod]
        // TDD - half 1st rule -> 1st test
        public void Given1AliveCellWith0AliveNeighboursWhenMutateThenCellDies()
        {
            var cell = new Cell(1, 1, CellState.Alive);
            var grid = new Grid();
            cell.DiscoverNeighbours(grid);
            cell.Mutate();

            Check.That(cell.NextState).Equals(CellState.Dead);
        }

        [TestMethod]
        // TDD - half 1st rule -> 2nd test
        public void Given1AliveCellWith1AliveNeighboursWhenMutateThenCellDies()
        {
            var cell = new Cell(1, 1, CellState.Alive);
            var grid = new Grid();
            grid.Add(new Cell(0, 0, CellState.Alive));
            cell.DiscoverNeighbours(grid);
            cell.Mutate();

            Check.That(cell.NextState).Equals(CellState.Dead);
        }

        [TestMethod]
        // 2. Any live cell with two or three live neighbours lives on to the next generation.
        public void Given1AliveCellWith2AliveNeighboursWhenMutateTheCellStaysAlive()
        {
            var cell = new Cell(1, 1, CellState.Alive);
            var grid = new Grid();
            grid.Add(cell);
            grid.Add(new Cell(0, 0, CellState.Alive));
            grid.Add(new Cell(2, 2, CellState.Alive));
            cell.DiscoverNeighbours(grid);
            cell.Mutate();

            Check.That(cell.NextState).Equals(CellState.Alive);
        }

        [TestMethod]
        // 3. Any live cell with more than three live neighbours dies, as if by overcrowding.
        public void Given1AliveCellWith4AliveNeighboursWhenMutateTheCellDies()
        {
            var cell = new Cell(1, 1, CellState.Alive);
            var grid = new Grid();
            grid.Add(cell);
            grid.Add(new Cell(0, 0, CellState.Alive));
            grid.Add(new Cell(2, 2, CellState.Alive));
            grid.Add(new Cell(1, 0, CellState.Alive));
            grid.Add(new Cell(2, 0, CellState.Alive));
            cell.DiscoverNeighbours(grid);
            cell.Mutate();

            Check.That(cell.NextState).Equals(CellState.Dead);
        }

        [TestMethod]
        // 4. Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
        public void Given1DeadCellWith3AliveNeighboursWhenMutateTheCellBecomesAlive()
        {
            var cell = new Cell(1, 1);
            var grid = new Grid();
            grid.Add(cell);
            grid.Add(new Cell(2, 2, CellState.Alive));
            grid.Add(new Cell(1, 0, CellState.Alive));
            grid.Add(new Cell(2, 0, CellState.Alive));
            cell.DiscoverNeighbours(grid);
            cell.Mutate();

            Check.That(cell.NextState).Equals(CellState.Alive);
        }

        [TestMethod]
        public void WhenCellMutatesThenNextStateBecomesCurrentState()
        {
            var cell = new Cell(1, 1, CellState.Alive);
            var grid = new Grid();
            cell.DiscoverNeighbours(grid);
            cell.Mutate();
            var nextState = cell.NextState;
            cell.CompleteMutation();

            Check.That(cell.CurrentState).Equals(nextState);
            Check.That(cell.NextState).IsNull();
        }

        [TestMethod]
        public void CheckCellEquality()
        {
            var cell11 = new Cell(1, 1);
            var cell00 = new Cell(0, 0);
            Check.That(Equals(cell11, cell00)).IsFalse();
            Check.That(cell11 == cell00).IsFalse();
            Check.That(Equals(cell00, cell00)).IsTrue();
            Check.That(cell00 == cell00).IsTrue();
            Check.That(Equals(cell11, null)).IsFalse();
            Check.That(Equals(null, cell11)).IsFalse();
        }

        [TestMethod]
        public void GivenNewCellWhenGetStateThenReturnDead()
        {
            Check.That(new Cell(0, 1).CurrentState).IsEqualTo(CellState.Dead);
        }

        [TestMethod]
        public void GivenNewCellWithCoordinatesWhenGetCoordinatesThenCoordinatesAreCorrect()
        {
            var cell = new Cell(1, 1);

            Check.That(cell.X).Equals(1);
            Check.That(cell.Y).Equals(1);
        }

        [TestMethod]
        public void WhenDiscoverNeighboursThenReturn8CoordinatesValidNeighbours()
        {
            var cell = new Cell(0, 0);

            cell.DiscoverNeighbours(new Grid());

            var supposedKnownNeighbours = new List<Cell>
            {
                new Cell(-1, 1),
                new Cell(0, 1),
                new Cell(1, 1),
                new Cell(-1, 0),
                new Cell(1, 0),
                new Cell(-1, -1),
                new Cell(0, -1),
                new Cell(1, -1)
            };

            Check.That(cell.KnownNeighbours).HasSize(8);
            Check.That(cell.KnownNeighbours).Contains(supposedKnownNeighbours);
        }
    }
}