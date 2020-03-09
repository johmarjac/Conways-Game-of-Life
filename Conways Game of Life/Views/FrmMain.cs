using GameOfLife.Core;
using GameOfLife.Model;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GameOfLife.Views
{
    public partial class FrmMain : Form
    {

        private Playground Playground;

        public FrmMain()
        {
            InitializeComponent();

            Playground = new Playground();
        }

        private void GamePanel_Click(object sender, EventArgs e)
        {
            var mousePos = GamePanel.PointToClient(Cursor.Position);
            var xIndex = mousePos.X / Playground.CELL_SIZE;
            var yIndex = mousePos.Y / Playground.CELL_SIZE;

            if (xIndex < 0 || xIndex > Playground.GRID_SIZE - 1)
                return;

            if (yIndex < 0 || yIndex > Playground.GRID_SIZE - 1)
                return;

            var cell = Playground.CurrentGeneration[xIndex, yIndex];

            if (cell.CellState == ECellState.Alive)
            {
                cell.CellState = ECellState.Dead;
            }
            else
            {
                cell.CellState = ECellState.Alive;
            }
        }

        private void RenderTimer_Tick(object sender, EventArgs e)
        {
            using(var bitmap = new Bitmap(Playground.CELL_SIZE * Playground.GRID_SIZE, Playground.CELL_SIZE * Playground.GRID_SIZE))
            using (var backBufferGraphics = Graphics.FromImage(bitmap))
            using (var frontBufferGraphics = Graphics.FromHwnd(GamePanel.Handle))
            {
                backBufferGraphics.Clear(Color.Black);

                Playground.Render(backBufferGraphics);

                frontBufferGraphics.DrawImage(bitmap, 0, 0);
            }

            if (cbAuto.Checked)
                EvaluateNextGeneration();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            EvaluateNextGeneration();
            lbGeneration.Text = $"Generation: {Playground.Generation}";
        }

        private void EvaluateNextGeneration()
        {
            Playground.EvaluateNextGeneration();
        }

        private void numInterval_ValueChanged(object sender, EventArgs e)
        {
            RenderTimer.Interval = (int)numInterval.Value;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Playground.Generation = 0;
            Playground.InitializeCells();
            lbGeneration.Text = $"Generation: 0";
        }
    }
}
