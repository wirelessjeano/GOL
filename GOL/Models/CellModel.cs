namespace GOL.Models
{
    public class CellModel
    {
        public int X { get; set; }

        public int Y { get; set; }

        public bool IsAlive { get; set; }

        public int Size { get; set; }

        public CellModel(int x, int y)
        {
            X = x;
            Y = y;
            IsAlive = false;
        }

        public void Update(bool isAlive)
        {
            IsAlive = isAlive;
        }

    }
}
