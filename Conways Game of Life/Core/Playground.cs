using GameOfLife.Model;
using System.Drawing;

namespace GameOfLife.Core
{
    public class Playground
    {
        public const int GRID_SIZE = 25;
        public const int CELL_SIZE = 20;

        public Cell[,] CurrentGeneration;
        public Cell[,] NextGeneration;

        public int Generation;

        public Playground()
        {
            InitializeCells();
        }

        public void InitializeCells()
        {
            CurrentGeneration = new Cell[GRID_SIZE, GRID_SIZE];
            NextGeneration = new Cell[GRID_SIZE, GRID_SIZE];

            for (var y = 0; y < GRID_SIZE; y++)
            {
                for (var x = 0; x < GRID_SIZE; x++)
                {
                    CurrentGeneration[x, y] = new Cell();
                    NextGeneration[x, y] = new Cell();
                }
            }

        }
        public void EvaluateNextGeneration()
        {
            Generation++;

            // Read CurrentGeneration into NextGeneration
            for (var y = 0; y < GRID_SIZE; y++)
            {
                for (var x = 0; x < GRID_SIZE; x++)
                {
                    NextGeneration[x, y].CellState = CurrentGeneration[x, y].CellState;
                }
            }

            // Apply Rules to Next Generation
            for (var y = 0; y < GRID_SIZE; y++)
            {
                for (var x = 0; x < GRID_SIZE; x++)
                {
                    var cell = CurrentGeneration[x, y];
                    var nextCell = NextGeneration[x, y];
                    var neighbours = EvaluateAliveNeighbours(x, y);

                    // Rule 1
                    if (cell.CellState == ECellState.Dead && neighbours == 3)
                    {
                        nextCell.CellState = ECellState.Alive;
                    }

                    // Rule 2
                    if (cell.CellState == ECellState.Alive && neighbours < 2)
                    {
                        nextCell.CellState = ECellState.Dead;
                    }

                    // Rule 3
                    if (cell.CellState == ECellState.Alive && (neighbours != 2 && neighbours != 3))
                    {
                        nextCell.CellState = ECellState.Dead;
                    }

                    // Rule 4
                    if (cell.CellState == ECellState.Alive && neighbours > 3)
                    {
                        nextCell.CellState = ECellState.Dead;
                    }
                }
            }

            // Apply Next Generation
            for (var y = 0; y < GRID_SIZE; y++)
            {
                for (var x = 0; x < GRID_SIZE; x++)
                {
                    CurrentGeneration[x, y].CellState = NextGeneration[x, y].CellState;
                }
            }
        }

        private int EvaluateAliveNeighbours(int x, int y)
        {
            int amount = 0;

            // TL
            if(x > 0 && y > 0)
            {
                if (CurrentGeneration[x - 1, y - 1].CellState == ECellState.Alive)
                    amount++;
            }

            // L
            if (x > 0)
            {
                if (CurrentGeneration[x - 1, y].CellState == ECellState.Alive)
                    amount++;
            }

            // BL
            if (x > 0 && y < GRID_SIZE - 1)
            {
                if (CurrentGeneration[x - 1, y + 1].CellState == ECellState.Alive)
                    amount++;
            }

            // B
            if (y < GRID_SIZE - 1)
            {
                if (CurrentGeneration[x, y + 1].CellState == ECellState.Alive)
                    amount++;
            }

            // BR
            if (x < GRID_SIZE - 1 && y < GRID_SIZE - 1)
            {
                if (CurrentGeneration[x + 1, y + 1].CellState == ECellState.Alive)
                    amount++;
            }

            // R
            if (x < GRID_SIZE - 1)
            {
                if (CurrentGeneration[x + 1, y].CellState == ECellState.Alive)
                    amount++;
            }

            // TR
            if (x < GRID_SIZE - 1 && y > 0)
            {
                if (CurrentGeneration[x + 1, y - 1].CellState == ECellState.Alive)
                    amount++;
            }

            // T
            if (y > 0)
            {
                if (CurrentGeneration[x, y - 1].CellState == ECellState.Alive)
                    amount++;
            }

            return amount;
        }

        public void Render(Graphics graphics)
        {
            for (var y = 0; y < GRID_SIZE; y++)
            {
                for (var x = 0; x < GRID_SIZE; x++)
                {
                    var color = CurrentGeneration[x, y].CellState switch
                    {
                        ECellState.Alive => Brushes.LightGreen,
                        ECellState.Dead => Brushes.Black,
                        _ => Brushes.Black
                    };

                    // Cell State
                    graphics.FillRectangle(color, x * CELL_SIZE, y * CELL_SIZE, CELL_SIZE, CELL_SIZE);
                    
                    // White Grid
                    graphics.DrawRectangle(Pens.White, x * CELL_SIZE, y * CELL_SIZE, CELL_SIZE, CELL_SIZE);
                }
            }
        }

    }
}
