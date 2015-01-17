namespace Game.of.Life.V2
{
    using System.Collections.Generic;
    using System.Linq;

    public class Grid
    {
        public IEnumerable<Cell> GetKnownCells()
        {
            return Enumerable.Empty<Cell>();
        }
    }
}
