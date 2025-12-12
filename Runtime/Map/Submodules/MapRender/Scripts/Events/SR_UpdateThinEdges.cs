

namespace GBB.Map.Render
{
    public readonly struct SR_UpdateThinEdges
    {
        public SR_UpdateThinEdges(
            int edgeIndex)
        {
            this.edgeIndex = edgeIndex;
        }

        public readonly int edgeIndex;
    }
}
