using Gameplay.Player;
using UnityEngine;

namespace Gameplay.Scene
{
    public class StartPoint: MonoBehaviour
    {
        [SerializeField] private PlayerType _type;

        public PlayerType Type => _type;
    }
}