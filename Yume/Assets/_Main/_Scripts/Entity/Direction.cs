using UnityEngine;

namespace Euphelia
{
    public class Direction
    {
        public Vector2 Axis { get; private set; }

        private readonly Animations _animations;

        public Direction(Animations animations)
        {
            _animations = animations;
        }

        public void Set(Vector2 direction)
        {
            Axis = direction;
            _animations.SetAxis(direction);
        }
    }
}
