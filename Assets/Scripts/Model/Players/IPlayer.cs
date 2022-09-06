using System.Threading.Tasks;
using Model.Actions;

namespace Model
{
    public interface IPlayer
    {
        string Name { get; }
        Task<GameAction> GetInput();
        bool KingCaptured();
    }
}