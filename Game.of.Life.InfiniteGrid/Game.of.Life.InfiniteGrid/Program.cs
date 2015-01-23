namespace Game.of.Life.InfiniteGrid
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

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
                new Cell(0, 1, CellState.Alive),
                new Cell(0, 0, CellState.Alive),new Cell(1, 0, CellState.Alive),
                new Cell(0, -1, CellState.Alive)
            };

            #region other models

            //var cells = new List<Cell>
            //{
            //    new Cell(0, 0), new Cell(-1, 0, CellState.Alive),
            //    new Cell(0, -1, CellState.Alive), new Cell(-1, -1, CellState.Alive)
            //};

            //var cells = new List<Cell>
            //{
            //    new Cell(0, 0, CellState.Alive),
            //    new Cell(-1, 0, CellState.Alive), new Cell(0, 1, CellState.Alive),
            //    new Cell(1, 0, CellState.Alive), new Cell(0, -1, CellState.Alive),
            //    new Cell(1, 1, CellState.Alive)
            //};

            // Toad    GRID(5,4:6)=1;GRID(6,3:5)=1;
            //var cells = new List<Cell>
            //{
            //    new Cell(5, 6, CellState.Alive),new Cell(4, 6, CellState.Alive),
            //    new Cell(6, 5, CellState.Alive), new Cell(3, 5, CellState.Alive)
            //};

            // Glider	GRID(3,1:3)=1;GRID(2,3)=1;GRID(1,2)=1;
            //var cells = new List<Cell>
            //{
            //    new Cell(3, 3, CellState.Alive),new Cell(3, 1, CellState.Alive),
            //    new Cell(2, 3, CellState.Alive), new Cell(3, 5, CellState.Alive)
            //};

            // Diehard	GRID(10,10:11)=1;GRID(11,11)=1; GRID(11,15:17)=1;GRID(9,16)=1;
            //var cells = new List<Cell>
            //{
            //    new Cell(10, 10, CellState.Alive),new Cell(10, 11, CellState.Alive),
            //    new Cell(11, 11, CellState.Alive), 
            //    new Cell(11, 15, CellState.Alive),new Cell(11, 16, CellState.Alive),new Cell(11, 17, CellState.Alive),
            //    new Cell(9, 16, CellState.Alive)
            //};

            //var cells = new List<Cell>
            //{
            //    new Cell(0, 0, CellState.Alive),
            //    new Cell(-1, 0, CellState.Alive), new Cell(0, 1, CellState.Alive),
            //    new Cell(1, 0, CellState.Alive), new Cell(0, -1, CellState.Alive)
            //};

            //var cells = new List<Cell>
            //{
            //    new Cell(0, 0, CellState.Alive),
            //    new Cell(-1, 0, CellState.Alive), new Cell(0, 1, CellState.Alive),
            //    new Cell(1, 0, CellState.Alive), new Cell(0, -1, CellState.Alive),
            //    new Cell(1, 1, CellState.Alive), new Cell(-1, -1, CellState.Alive),
            //};

            //var cells = new List<Cell>
            //{
            //        new Cell(0, -1, CellState.Alive), new Cell(-1, -1, CellState.Alive)
            //};

            #endregion

            var grid = new Grid();
            grid.AddRange(cells.ToArray());

            PrintToConsole(grid.Draw());
            Console.ReadLine();
            while (true)
            {
                grid.RunOneCycle();

                PrintToConsole(grid.Draw());
                Thread.Sleep(1000);
                //Console.ReadLine();

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
