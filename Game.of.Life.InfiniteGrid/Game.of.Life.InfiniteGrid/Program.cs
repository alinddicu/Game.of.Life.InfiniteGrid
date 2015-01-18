using System.Linq;

namespace Game.of.Life.InfiniteGrid
{
    using System;
    using System.Threading;
    using System.Collections.Generic;

    public static class Program
    {
        public static void Main()
        {
            //var cells = new List<Cell>
            //{
            //    new Cell(0, 0, CellState.Alive), 
            //    new Cell(0, -1, CellState.Alive), new Cell(1, -1, CellState.Alive), new Cell(2, -1, CellState.Alive),new Cell(3, -1, CellState.Alive), 
            //    new Cell(0, -2, CellState.Alive), new Cell(1, -2, CellState.Alive), new Cell(2, -2, CellState.Alive),new Cell(3, -2, CellState.Alive), 
            //    new Cell(0, -3, CellState.Alive), new Cell(1, -3, CellState.Alive), new Cell(2, -3, CellState.Alive),new Cell(3, -3, CellState.Alive)
            //};

            var cells = new List<Cell>
            {
                new Cell(0, 0), new Cell(-1, 0, CellState.Alive),
                new Cell(0, -1, CellState.Alive), new Cell(-1, -1, CellState.Alive)
            };

            //var cells = new List<Cell>
            //{
            //        new Cell(0, -1, CellState.Alive), new Cell(-1, -1, CellState.Alive)
            //};

            var grid = new Grid();
            grid.AddRange(cells.ToArray());
            grid.RefreshKnownCells();

            PrintToConsole(grid.Draw());
            Console.ReadLine();
            Console.SetWindowPosition(0, 0);
            while (true)
            {
                grid.RunOneCycle();

                PrintToConsole(grid.Draw());
                Thread.Sleep(1000);

                Console.Clear();
            }
        }

        private static void PrintToConsole(IEnumerable<string> boardLines)
        {
            foreach (var line in boardLines.Reverse())
            {
                Console.WriteLine(line);
            }
        }
    }
}
