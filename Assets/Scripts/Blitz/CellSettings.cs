using UnityEngine;
using UnityEngine.UI;

namespace Blitz
{
    public struct CellSettings
    {
        public CellSettings(Vector2 size, ColorBlock colorBlock)
        {
            this.size = size;
            this.colorBlock = colorBlock;
            material = null;
        }

        public Material material;
        public readonly ColorBlock colorBlock;
        public readonly Vector2 size;
    }
}