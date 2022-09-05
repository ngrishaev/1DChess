using System;
using UnityEngine;

namespace Unity.Services
{
    public class InputService : MonoBehaviour
    {
        private Camera _camera;
        public event Action OnRestart;

        public event Action<Coordinate> OnTap;

        public void Construct(Camera camera)
        {
            _camera = camera;
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
                OnTap?.Invoke(Coordinate.FromScreen(Input.mousePosition, _camera));
            
            if(Input.GetKeyDown(KeyCode.R))
                OnRestart?.Invoke();
        }
    }

    public class Coordinate
    {
        public Vector3 World { get; }
        public Vector2 Screen { get; }

        private Coordinate(Vector3 world, Vector2 screen)
        {
            World = world;
            Screen = screen;
        }

        public static Coordinate FromWorld(Vector3 position, Camera camera)
        {
            return new Coordinate(position, camera.WorldToScreenPoint(position));
        }  
        
        public static Coordinate FromScreen(Vector3 position, Camera camera)
        {
            return new Coordinate(camera.ScreenToWorldPoint(position), position);
        }  
    }
}