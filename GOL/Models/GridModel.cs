using System.Threading.Tasks;

namespace GOL.Models
{
    public class GridModel
    {
        public int Columns { get; private set; }

        public int Rows { get; private set; }

        public int[,] Cells;

        public GridModel(int columns, int rows)
        {
            Columns = columns;
            Rows = rows;

            Cells = new int[Columns, Rows];

            for (int i = 0; i < Columns; i++)
                for (int j = 0; j < Rows; j++)
                    Cells[i, j] = 0;
        }

        public GridModel(int[,] cells)
        {
            Columns = cells.GetLength(0);
            Rows = cells.GetLength(1);
            Cells = cells;
        }

        public void NextGeneration()
        {
            var previousGenerationCells = (int[,])Cells.Clone();

            Parallel.For(0, Columns, columns =>
            {
                Parallel.For(0, Rows, row =>
                {
                    // Is cell alive?
                    bool currentCellAlive = previousGenerationCells[columns, row] == 1;
                    int count = LivingNeighborsCount(columns, row, previousGenerationCells);
                    bool result = false;

                    // Rules
                    if (currentCellAlive && count < 2)
                        result = false;
                    if (currentCellAlive && (count == 2 || count == 3))
                        result = true;
                    if (currentCellAlive && count > 3)
                        result = false;
                    if (!currentCellAlive && count == 3)
                        result = true;

                    Cells[columns, row] = result ? 1 : 0;
                });
            });
        }

        public int LivingNeighborsCount(int column, int row,int[,] previousGenerationCells)
        {
			int alive = 0;

			// R
			if (column != Columns - 1)
				if (previousGenerationCells[column + 1, row] == 1)
					alive++;

			// BR
			if (column != Columns - 1 && row != Rows - 1)
				if (previousGenerationCells[column + 1, row + 1] == 1)
					alive++;

			// B
			if (row != Rows - 1)
				if (previousGenerationCells[column, row + 1] == 1)
					alive++;

			// BL
			if (column != 0 && row != Rows - 1)
				if (previousGenerationCells[column - 1, row + 1] == 1)
					alive++;

			// L
			if (column != 0)
				if (previousGenerationCells[column - 1, row] == 1)
					alive++;

			// TL
			if (column != 0 && row != 0)
				if (previousGenerationCells[column - 1, row - 1] == 1)
					alive++;

			// T
			if (row != 0)
				if (previousGenerationCells[column, row - 1] == 1)
					alive++;

			// TR
			if (column != Columns - 1 && row != 0)
				if (previousGenerationCells[column + 1, row - 1] == 1)
					alive++;

			return alive;
        }

    }
}
