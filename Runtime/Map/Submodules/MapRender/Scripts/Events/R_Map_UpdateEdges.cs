
namespace GBB.Map.Render
{
    public readonly struct R_Map_UpdateEdges
    {
        public R_Map_UpdateEdges(
            bool isThinUpdated, bool isThickUpdated, bool isCurrentHighlightUpdated)
        {
            this.isThinUpdated = isThinUpdated;
            this.isThickUpdated = isThickUpdated;
            this.isCurrentHighlightUpdated = isCurrentHighlightUpdated;
        }

        public readonly bool isThinUpdated;
        public readonly bool isThickUpdated;
        public readonly bool isCurrentHighlightUpdated;
    }
}
