using UnityEngine;
using Random = System.Random;

namespace Blitz
{
    public sealed class Field
    {
        public Field(int rows, int columns, FieldSettings fieldSettings)
        {
            _rows = rows;
            _columns = columns;
            
            _settings = fieldSettings;

            _random = new Random();
            _cellSettings = new CellSettings(_settings.CellSize, _settings.ColorBlock);
            _cells = new Cell[_columns, _rows];
        }

        public IDrawable[,] Cells { get => _cells; }

        private delegate int FuncHandler(int index);

        private Random _random;
        private IContainer _selectedCell;

        private CellSettings _cellSettings;
        private Cell[,] _cells;

        private readonly FieldSettings _settings;
        private readonly int _rows, _columns;

        public void CountField()
        {
            for(int j = 0; j < _rows; j++)
            {
                for(int i = 0; i < _columns; i++)
                {
                    int index = _random.Next(_settings.SpritesAmount);
                    Sprite sprite = _settings.GetSprite(index);
                    CellContent content = new CellContent(index, sprite);

                    Vector2Int matrixPos = new Vector2Int(i, j);

                    Cell cell = new Cell(content, _cellSettings, matrixPos);
                    cell.Exchanged += OnCellPressed;

                    _cells[i, j] = cell;
                }
            }
        }
        private void OnCellPressed(IContainer cell)
        {
            if (_selectedCell == null)
            {
                _selectedCell = cell;
                return;
            }

            //#TODO set max step
            if (Vector2Int.Distance(_selectedCell.Position, cell.Position) < 2
                && IsInRow(cell.Position))
            {
                CellContent data = cell.Content;
                cell.Content = _selectedCell.Content;
                _selectedCell.Content = data;
            }
            _selectedCell = null;
        }

        private bool IsInRow(Vector2Int position)
        {
            int x = position.x,
                y = position.y;

            ClampCells(out int xStart, out int xFinal, x, _columns - 1);
            ClampCells(out int yStart, out int yFinal, y, _rows - 1);

            int GetCellX(int incrementX) => _cells[incrementX, y].Content.index;
            int GetCellY(int incrementY) => _cells[x, incrementY].Content.index;

            if (CountCellsInRow(xStart, xFinal, x, GetCellX) >= 3)
                return true;

            if (CountCellsInRow(yStart, yFinal, y, GetCellY) >= 3)
                return true;

            return false;
        }
        private void ClampCells(out int start, out int final, int cellPosition, int border)
        {
            start = cellPosition >= 2 ? cellPosition - 2 : 0;

            final = cellPosition <= border - 2 ? cellPosition + 2 : border;
        }
        private int CountCellsInRow(int startValue, int finalValue, int cellPosition, FuncHandler func)
        {
            int inRow = 0;
            int needIndex = _selectedCell.Content.index;

            while (startValue <= finalValue)
            {
                if (func(startValue) == needIndex || startValue == cellPosition)
                    inRow++;
                else
                    inRow = 0;

                if (inRow >= 3)
                    return inRow;

                startValue++;
            }

            return inRow;
        }

        public void UnsetModel()
        {
            for(int j = 0; j < _rows; j++)
            {
                for(int i = 0; i < _columns; i++)
                {
                    Cell cell = _cells[i, j];
                    cell.Exchanged -= OnCellPressed;
                    cell.UnsetModel();
                }
            }
        }
    }
}