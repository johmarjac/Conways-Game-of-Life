namespace GameOfLife.Model
{
    public class Cell
    {

        public ECellState CellState;

        public Cell()
        {
            CellState = ECellState.Dead;
        }
    }
}
