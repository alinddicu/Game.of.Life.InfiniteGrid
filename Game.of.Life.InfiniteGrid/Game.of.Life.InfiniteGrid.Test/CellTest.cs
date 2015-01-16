namespace Game.of.Life.V2.Test
{
    using System.Linq;
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
        public void WhenGetNeighboursThenReturn8CoordinatesValidNeighbours()
        {
            var cell = new Cell(0, 0);

            var neighbours = cell.GetNeighbours().ToList();

            Check.That(neighbours).Contains(new Cell(-1, 1));
            Check.That(neighbours).Contains(new Cell(0, 1));
            Check.That(neighbours).Contains(new Cell(1, 1));
            Check.That(neighbours).Contains(new Cell(-1, 0));
            Check.That(neighbours).Contains(new Cell(1, 0));
            Check.That(neighbours).Contains(new Cell(-1, -1));
            Check.That(neighbours).Contains(new Cell(0, -1));
            Check.That(neighbours).Contains(new Cell(1, -1));

            Check.That(cell.KnownNeighbours).ContainsExactly(neighbours);
        }
    }
}