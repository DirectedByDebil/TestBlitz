using UnityEngine;

namespace Blitz
{
    public interface IDrawable
    {
        void Draw();
        void SetParentAndPosition(Transform parent, Vector2 position);
    }
}