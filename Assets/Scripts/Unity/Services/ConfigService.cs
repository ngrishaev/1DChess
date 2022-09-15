using UnityEngine;

namespace Unity.Services
{
    public class ConfigService : MonoBehaviour, IConfigService
    {
        [SerializeField] private int _boardSize;
        
        public int BoardSize => _boardSize;
    }

    public interface IConfigService
    {
        int BoardSize { get; }
    }
}