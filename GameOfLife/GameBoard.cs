namespace GameOfLife
{
    public class GameBoard
    {
        private readonly int _width;
        private readonly int _height;
        private bool[,] _board;
        public bool[,] Board => _board;
        
        public GameBoard(int width, int height)
        {
            _width = width;
            _height = height;
            _board = new bool[_width, _height];
        }

        public void Update()
        {
            _board = CheckBoard();
        }

        private bool[,] CheckBoard()
        {
            var newBoard = new bool[_width, _height];

            for (var x = 0; x < _width; x++)
            {
                for (var y = 0; y < _height; y++)
                {
                    newBoard[x, y] = IsCellAlive(x, y);
                }
            }
            
            return newBoard;
        }

        private bool IsCellAlive(int x, int y)
        {
            var count = 0;
            for (var i = -1; i <= 1; i++)
            {
                for (var j = -1; j <= 1; j++)
                {
                    var ix = x + i;
                    var iy = y + j;
                    if((i == 0 && j == 0) || ix < 0 || iy < 0 || ix >= _width || iy >= _height) continue;
                    if (Board[ix, iy]) ++count;
                }
            }

            return Board[x, y] && (count == 2 || count == 3) || Board[x, y] == false && count == 3;
        }
    }
}