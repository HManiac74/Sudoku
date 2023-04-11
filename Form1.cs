using System;
using System.Windows.Forms;


namespace Sudoku
{
    public partial class Form1 : Form
    {
        Sudoku S;

        private Communicator C;

        public Form1()
        {
            InitializeComponent();

            S = new Sudoku();

            Application.DoEvents();

            S.GenerateGame();

            S.RequestRepaint += new Sudoku.SudokuEvent2(S_RequestRepaint);

            C = new Communicator();

            C.StatusChange += new Communicator.MethodDelegate(C_StatusChange);

        }

        void C_StatusChange()
        {
            this.Text = C.Status;
        }

        void S_RequestRepaint()
        {
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            S.Draw(e.Graphics, 0);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                S.SetLocation(e.Location);

                pictureBox1.Invalidate();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            S.KeyPress((char)e.KeyValue);

            if ((int)e.KeyValue == 27) S.Deselect();

            if ((int)e.KeyValue == 46) S.DeleteCurrentSquare();

            pictureBox1.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //S.RenderMessage("Sudoku 1.0", "Die Zahlen macht Michael", false);
            S.GenerateGame();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            S.RenderMessage("Sudoku 1.0", "Michelle Fevre (fevrem81@hotmail.com)", false);

        }


        private void solveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (S.checkSolvable(Sudoku.SolveMethods.All) == false)
            {
                Application.DoEvents();

                MessageBox.Show("This puzzle cannot be solved!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                S.SolvePuzzle(Sudoku.SolveMethods.All);

                pictureBox1.Invalidate();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            S.SolveStep(Sudoku.SolveMethods.All);

            pictureBox1.Invalidate();
        }

        private void checkSolvabilityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (S.isSolved()) return;

            int error_count = S.ComputeErrors();

            if (error_count == 0)
            {
                S.RenderMessage("Everything's cool dude!", "Press any key to continue", false);

                pictureBox1.Invalidate();

                Application.DoEvents();

            }
            else
            {
                Application.DoEvents();

                S.ShowErrors = true;

                pictureBox1.Invalidate();
            }

        }

        private void checkSolutionsCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (S.isSolved()) return;

            Sudoku.SolutionStepList L = S.ComputePossibleSteps(Sudoku.SolveMethods.All);

            S.RenderMessage(L.Count().ToString() + " possible deductions!", "Press any key to continue", false);
        }

        private void Form1_Leave(object sender, EventArgs e)
        {

        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            pictureBox1.Invalidate();
        }

        private void menuStrip1_MouseHover(object sender, EventArgs e)
        {

        }

        private void menuStrip1_MouseEnter(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void menuStrip1_MouseLeave(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            pictureBox1.Invalidate();
        }

    }
}
