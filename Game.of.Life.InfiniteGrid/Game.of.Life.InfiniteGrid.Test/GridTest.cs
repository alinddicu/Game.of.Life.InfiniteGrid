namespace Game.of.Life.V2.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NFluent;

    [TestClass]
    public class GridTest
    {
        [TestMethod]
        public void WhenCreateGridThenAllCellsAreDead()
        {
            var grid = new Grid(2, 2);

            Check.That(grid.Cells.Count(c => c.CurrentState == CellState.Dead)).Equals((int)grid.Width * (int)grid.Height);
        }

        [TestMethod]
        public void WhenInitAliveCellsOnGridThenOnlyTheseCellAreAlive()
        {
            var grid = new Grid(3, 3);
            var aliveCells = new[] { new Cell(0, 0), new Cell(1, 1), new Cell(2, 2) };
            grid.Init(aliveCells);

            Check.That(grid.Cells.Where(c => c.CurrentState == CellState.Alive)).ContainsExactly(aliveCells);
        }

        [TestMethod]
        public void WhenCreateGridOf3by3ThenTheCenterCellHas8Neighbours()
        {
            var grid = new Grid(3, 3);
            var cell11 = grid.Cells.Single(c => c.X == 1 && c.Y == 1);
            var cell11Neighbours = grid.Cells.Except(new[] { cell11 });

            Check.That(cell11Neighbours).HasSize(8);
            Check.That(cell11.Neighbours).ContainsExactly(cell11Neighbours);
        }

        [TestMethod]
        public void WhenCreateGridOf2by2ThenACornerCellHas3Neighbours()
        {
            var grid = new Grid(2, 2);
            var cell00 = grid.Cells.Single(c => c.X == 0 && c.Y == 0);

            Check.That(cell00.Neighbours).HasSize(3);
        }

        [TestMethod]
        public void WhenCreateGridOf3by3ThenACornerCellHas3Neighbours()
        {
            var grid = new Grid(3, 3);
            var cell01 = grid.Cells.Single(c => c.X == 0 && c.Y == 1);

            Check.That(cell01.Neighbours).HasSize(5);
        }

        [TestMethod]
        public void TestGridToString()
        {
            var grid = new Grid(2, 2);
            var aliveCells = new[] { new Cell(0, 0), new Cell(1, 1), new Cell(1, 0) };
            grid.Init(aliveCells);
            grid.MutateAndCompleteAllCellsMutation();
        }

        [TestMethod]
        public void Given2By2GridWithAliveCellsAt00And11And10WhenMutateAndCompleteAllCellsMutationAllNextStatesAreAlive()
        {
            var grid = new Grid(2, 2);
            var aliveCells = new[] { new Cell(0, 0), new Cell(1, 1), new Cell(1, 0) };
            grid.Init(aliveCells);

            Check.That(grid.Print()).ContainsExactly("++", " +");
            grid.MutateAndCompleteAllCellsMutation();
            Check.That(grid.Print()).ContainsExactly("++", "++");
        }
    }
}
