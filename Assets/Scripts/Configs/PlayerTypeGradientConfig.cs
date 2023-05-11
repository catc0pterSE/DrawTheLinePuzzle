using Scene;
using SerializableDictionary.Scripts;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "ScriptableObject/Config/Gradients", order = 51)]
    
    public class PlayerTypeGradientConfig: ScriptableObject
    {
        [SerializeField] private SerializableDictionary<PlayerType, Gradient> _typedGradients;

        public Gradient Get(PlayerType playerType) =>
            _typedGradients.Get(playerType);
    }
}