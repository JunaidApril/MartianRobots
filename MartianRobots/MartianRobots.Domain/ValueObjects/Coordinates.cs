namespace MartianRobots.Domain.ValueObjects
{
    public class Coordinates
    {
        private int _x;
        private int _y;
        public Coordinates(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public int X { get { return _x; } }

        public int Y { get { return _y; } }
    }
}
