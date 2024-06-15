using UnityEngine;

namespace Blitz
{
    public interface IContainer
    {
        CellContent Content { get; set; }
        Vector2Int Position { get; }

        delegate void ExchangedHandler(IContainer exchangeable);
        event ExchangedHandler Exchanged;
    }
}