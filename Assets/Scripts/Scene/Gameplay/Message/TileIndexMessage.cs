namespace PaintAstic.Module.Message
{
    public struct TileIndexMessage
    {
        public int tileIndexX;
        public int tileIndexZ;

        public TileIndexMessage(int indexX, int indexZ)
        {
            tileIndexX = indexX;
            tileIndexZ = indexZ;
        }
    }
}