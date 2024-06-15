using UnityEngine;

namespace Blitz
{
    public struct CellContent
    {
        public CellContent (int index, Sprite sprite)
        {
            this.index = index;
            this.sprite = sprite;
        }

        public int index;
        public Sprite sprite;
    }
}