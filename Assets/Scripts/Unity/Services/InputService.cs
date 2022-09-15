using System;
using UnityEngine;

namespace Unity.Services
{
    public class InputService : MonoBehaviour
    {
        private Camera _camera;
        public event Action OnRestart;

        public event Action<WorldPosition> OnTap;

        public void Construct(Camera camera)
        {
            _camera = camera;
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
                OnTap?.Invoke(WorldPosition.FromScreen(Input.mousePosition, _camera));
            
            if(Input.GetKeyDown(KeyCode.R))
                OnRestart?.Invoke();
        }
    }
    
    public class WorldPosition
    {
        public float X => Position.x;
        public float Y => Position.y;
        public float Z => Position.z;
        private Vector3 Position { get; }

        private WorldPosition(Vector3 position) => 
            Position = position;

        public static WorldPosition FromScreen(Vector3 position, Camera camera) => 
            new WorldPosition(camera.ScreenToWorldPoint(position));

        public static implicit operator Vector3(WorldPosition pos) => pos.Position;
    }
}