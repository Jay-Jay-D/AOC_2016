namespace DayEight
{
    public class AuthenticationScreen
    {
        private int Rows;
        private int Columns;

        private int[,] _screen;

        public AuthenticationScreen(int rows, int columns)
        {
            this.Rows = rows;
            this.Columns = columns;
            _screen = new int[rows, columns];
        }

        public int LitPixels()
        {
            var lit = 0;
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    lit += _screen[row, col];
                }
            }
            return lit;
        }

        public void Proccess(string instruction)
        {
            var parts = instruction.Split(' ');
            if (parts.Length == 2)
            {
                var rectSize = parts[1].Split('x');
                var wide = int.Parse(rectSize[0]);
                var tall = int.Parse(rectSize[1]);
                RectInstrustion(wide, tall);
            }
            else
            {
                var isRotateRow = parts[1] == "row";
                var position = int.Parse(parts[2].Split("=")[1]);
                var shift = int.Parse(parts.Last());
                RotateInstruction(isRotateRow, position, shift);
            }
        }

        private void RotateInstruction(bool isRotateRow, int position, int shift)
        {
            var arraySize = isRotateRow ? Columns : Rows;
            var originalArray = Enumerable.Range(0, arraySize)
                                          .Select(idx => isRotateRow ? _screen[position, idx] : _screen[idx, position])
                                          .ToArray();
            for (var idx = 0; idx < arraySize; idx++)
            {
                var shiftedIdx = (idx + shift) % arraySize;
                if (isRotateRow)
                {
                    _screen[position, shiftedIdx] = originalArray[idx];
                }
                else
                {
                    _screen[shiftedIdx, position] = originalArray[idx];

                }
            }
        }

        private void RectInstrustion(int wide, int tall)
        {
            for (int row = 0; row < tall; row++)
            {
                for (int col = 0; col < wide; col++)
                {
                    _screen[row, col] = 1;
                }
            }
        }

        public void ShowScreen()
        {
            var separator = new string('=', Columns);
            Console.Write($"{separator}\n");
            for (var row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    var asd = this[row, col] ? "#" : ".";
                    Console.Write($"{asd}");
                }
                Console.Write("\n");
            }

            Console.Write($"{separator}\n");
        }

        public bool this[int row, int column]
        {
            get { return _screen[row, column] == 1; }
        }
    }
}