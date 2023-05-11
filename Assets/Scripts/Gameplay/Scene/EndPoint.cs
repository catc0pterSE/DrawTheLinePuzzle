using Gameplay.Player;
using UnityEngine;

namespace Gameplay.Scene
{
    public class EndPoint : MonoBehaviour
    {
        [SerializeField] private PlayerType _type;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public bool IsOccupied { get; private set; } = false;

        public PlayerType Type => _type;

        public void Initialize(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }

        public void Occupy() =>
            IsOccupied = true;

        public void Free() =>
            IsOccupied = false;
    }
}