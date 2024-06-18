using UnityEngine;

namespace Blitz
{
    public sealed class FieldDrawer : MonoBehaviour
    {
        [SerializeField] private FieldSettings _settings;
        [Space, SerializeField] private RectTransform _parent;

        [Space, SerializeField, Range(1, 30)] private int _columns;
        [SerializeField, Range(1, 90)] private int _rows;

        private Field _field;
        
        private void Start()
        {
            _field = new Field(_rows, _columns, _settings);

            _field.CountField();

            //#TODO onButtonClicked += DrawField
            DrawField(_parent);
        }

        public void DrawField(Transform parent)
        {
            for(int j = 0; j < _rows; j++)
            {
                for(int i = 0; i < _columns; i++)
                {
                    IDrawable cell = _field.Cells[i, j]; 
                    Vector2 position = new Vector2(i, -j);
                    position *= _settings.CellSize;

                    cell.Draw();
                    cell.SetParentAndPosition(parent, position);
                }
            }
        }

        private void OnDisable()
        {
            _field.UnsetModel();
        }
    }
}