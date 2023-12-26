namespace HR_System.Models.Interfaces
{
    public interface IPosition
    {
        Task<Position> PostPosition(Position position);
    }
}
