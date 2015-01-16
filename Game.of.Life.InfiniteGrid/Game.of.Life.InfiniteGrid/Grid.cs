namespace Game.of.Life.V2
{
    using System.Linq;
    using System.Collections.Generic;

    public class Grid
    {
        public Grid(uint width, uint heigth)
        {
            Width = width;
            Height = heigth;
            Cells = new List<Cell>();

            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    Cells.Add(new Cell(x, y, CellState.Dead));
                }
            }

            Cells.ForEach(SetCellNeighbours);
        }

        public uint Width { get; private set; }

        public uint Height { get; private set; }

        public List<Cell> Cells { get; private set; }

        public void Init(params Cell[] aliveCells)
        {
            aliveCells.ToList().ForEach(aliveCell => { Cells.Single(cell => cell == aliveCell).CurrentState = aliveCell.CurrentState; });
        }

        public void MutateAndCompleteAllCellsMutation()
        {
            Cells.ForEach(cell => cell.Mutate());
            Cells.ForEach(cell => cell.CompleteMutation());
        }

        public IEnumerable<string> Print()
        {
            var line = string.Empty;
            for (var yGrid = 0; yGrid < Height; yGrid++)
            {
                line = string.Empty;
                for (var xGrid = 0; xGrid < Width; xGrid++)
                {
                    var cell = Cells.Single(c => c.X == xGrid && c.Y == yGrid);
                    line += cell.CurrentState == CellState.Alive ? "+" : " ";
                }
                yield return line;
            }
        }

        private void SetCellNeighbours(Cell currentCell)
        {
            var startX = currentCell.X - 1 < 0 ? 0 : currentCell.X - 1;
            var startY = currentCell.Y - 1 < 0 ? 0 : currentCell.Y - 1;
            for (var xNeighbour = startX; IsXInNeighbourRange(currentCell, xNeighbour); xNeighbour++)
            {
                for (var yNeighbour = startY; IsYInNeighbourRange(currentCell, yNeighbour); yNeighbour++)
                {
                    AddNeighbourToCell(currentCell, xNeighbour, yNeighbour);
                }
            }
        }

        private void AddNeighbourToCell(Cell cell, int xNeighbour, int yNeighbour)
        {
            var neighbour = Cells.Single(c => c.X == xNeighbour && c.Y == yNeighbour);
            if (neighbour != cell)
            {
                cell.AddNeighbours(neighbour);
            }
        }

        private bool IsYInNeighbourRange(Cell cell, int yGrid)
        {
            return yGrid >= 0 && yGrid < Height && yGrid <= cell.Y + 1;
        }

        private bool IsXInNeighbourRange(Cell cell, int xGrid)
        {
            return xGrid >= 0 && xGrid < Width && xGrid <= cell.X + 1;
        }
    }
}
