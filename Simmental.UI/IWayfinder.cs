namespace Simmental.UI
{
    public interface IWayfinder
    {
        int Height { get; }
        
        int Width { get; }

        void Initialize(int width, int height);
        
        ITile[][] Tiles { get; }

        ITile this[int i, int j] { get; }
        ITile this[Position position] { get; }
        int CameraI { get; set; }
        int CameraJ { get; set; }
        int CameraWidth { get; set; }
        int CameraHeight { get; set; }
        int TilePixelWidth { get; set; }
        int TilePixelHeight { get; set; }

        bool IsVisible(Position from, Position to);
        bool IsVisible(Position from, Position to, int maxDistance);
        bool CanSee(Position from, Position to, int maxDistance);
        void Move(ICharacter character, Position moveTo);
        void ApplyLightSources(IGame game);
    }
}