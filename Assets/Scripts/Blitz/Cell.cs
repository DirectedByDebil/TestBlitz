using UnityEngine;
using UnityEngine.UI;

namespace Blitz
{
    public sealed class Cell : IDrawable, IContainer
    {
        public Cell(CellContent data, CellSettings settings, Vector2Int position)
        {
            _data = data;
            _settings = settings;
            _position = position;
        }

        public CellContent Content
        {
            get => _data;
            set
            {
                _data = value;
                _image.sprite = _data.sprite;
            }
        }
        public Vector2Int Position
        {
            get => _position;
        }

        public event IContainer.ExchangedHandler Exchanged;

        private GameObject _gameObject;
        private RectTransform _rectTransform;
        private Button _button;
        private Image _image;

        private CellSettings _settings;
        private CellContent _data;
        private readonly Vector2Int _position;
        
        public void Draw()
        {
            _gameObject = new GameObject("Cell", typeof(Button), typeof(Image));

            _button = _gameObject.GetComponent<Button>();
            _image = _gameObject.GetComponent<Image>();

            _rectTransform = _image.rectTransform;
            _rectTransform.sizeDelta = _settings.size;

            _image.preserveAspect = true;
            _image.sprite = Content.sprite;
            _image.material = _settings.material;

            _button.targetGraphic = _image;
            _button.colors = _settings.colorBlock;
            _button.onClick.AddListener(OnButtonClicked);
        }
        private void OnButtonClicked() => Exchanged.Invoke(this);

        public void SetParentAndPosition(Transform parent, Vector2 position)
        {
            _rectTransform.SetParent(parent, false);
            _rectTransform.localPosition = position;
        }
        public void UnsetModel() => _button.onClick.RemoveAllListeners();
    }
}