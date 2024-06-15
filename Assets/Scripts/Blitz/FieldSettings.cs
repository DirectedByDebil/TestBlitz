using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Blitz
{
    [CreateAssetMenu(fileName ="New Field Settings", menuName = "Settings/Field", order = 50)]
    public sealed class FieldSettings : ScriptableObject
    {
        public int SpritesAmount { get => _sprites.Count; }
        public ColorBlock ColorBlock { get => _colorBlock; }
        public Vector2Int CellSize { get => _cellSize; }

        [SerializeField] private List<Sprite> _sprites = new ();
        [SerializeField] private ColorBlock _colorBlock;
        [SerializeField] private Vector2Int _cellSize;

        public Sprite GetSprite(int index) => _sprites[index];
    }
}