using UnityEngine;

namespace Scene
{
    public class StartPoint: MonoBehaviour
    {
        [SerializeField] private PlayerType _type;

        public PlayerType Type => _type;
    }
}