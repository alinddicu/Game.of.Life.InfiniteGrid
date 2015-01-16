namespace Game.of.Life.V2
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class Program
    {
        public static void Main(string[] args)
        {
            var gridWidth = 50;
            var gridHeigth = 50;
            //var startX = 9 + gridWidth / 2;
            //var startY = startX + 2 + gridHeigth / 2;
            var startX = gridWidth / 2;
            var startY = startX + 2;
            var cells = new List<Cell>
            {
                new Cell(startX, startY), new Cell(startX, startY+1), new Cell(startX, startY+2), new Cell(startX, startY+3),
                                        new Cell(startX+1, startY+1), new Cell(startX+2, startY+1), new Cell(startX+3, startY+1),
                                        new Cell(startX+1, startY+2), new Cell(startX+2, startY+2), new Cell(startX+3, startY+2),
                                        new Cell(startX+1, startY+3), new Cell(startX+2, startY+3), new Cell(startX+3, startY+3)
            };

            var grid = new Grid((uint)gridWidth, (uint)gridHeigth);
            grid.Init(cells.ToArray());

            PrintToConsole(grid.Print());
            Console.ReadLine();
            Console.SetWindowPosition(0, 0);
            while (true)
            {
                grid.MutateAndCompleteAllCellsMutation();
                PrintToConsole(grid.Print());
                Thread.Sleep(1000);

                Console.Clear();
            }
        }

        private static void PrintToConsole(IEnumerable<string> boardLines)
        {
            foreach (var line in boardLines)
            {
                Console.WriteLine(line);
            }
        }
    }
}
