using UnityEngine;
using System.Collections.Generic;
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

            _cells = new List<Cell>(_rows * _columns);
        }

        public IEnumerable<IDrawable> Cells { get => _cells; }

        private Random _random;
        private IContainer _selectedCell;

        private CellSettings _cellSettings;
        private List<Cell> _cells;

        private readonly FieldSettings _settings;
        private readonly int _rows, _columns;

        public void CountField()
        {
            int amountOfCells = _rows * _columns;

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
                    
                    _cells.Add(cell);
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
            if (Vector2Int.Distance(_selectedCell.Position, cell.Position) < 2)
            {
                CellContent data = cell.Content;
                cell.Content = _selectedCell.Content;
                _selectedCell.Content = data;
            }
            _selectedCell = null;
        }

        public void UnsetModel()
        {
            foreach(Cell cell in _cells)
            {
                cell.Exchanged -= OnCellPressed;
                cell.UnsetModel();
            }
        }
    }
}