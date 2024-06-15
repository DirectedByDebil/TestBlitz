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
            int index = 0;
            foreach (IDrawable cell in _field.Cells)
            {
                Vector2 position = new Vector2(index % _columns, -index / _columns);
                position *= _settings.CellSize;

                cell.Draw();
                cell.SetParentAndPosition(parent, position);
                index++;
            }
        }

        private void OnDisable()
        {
            _field.UnsetModel();
        }
    }
}