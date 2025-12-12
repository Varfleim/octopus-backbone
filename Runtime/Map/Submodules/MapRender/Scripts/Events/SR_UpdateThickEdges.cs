
namespace GBB.Map.Render
{
    public readonly struct SR_UpdateThickEdges
    {
        public SR_UpdateThickEdges(
            int edgeIndex)
        {
            this.edgeIndex = edgeIndex;
        }

        public readonly int edgeIndex;
    }
}
