using Scene;
using SerializableDictionary.Scripts;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "ScriptableObject/Config/Sprites", order = 51)]
    public class PlayerTypeSpriteConfig : ScriptableObject
    {
        [SerializeField] private SerializableDictionary<PlayerType, Sprite> _typedSprites;

        public Sprite Get(PlayerType playerType) =>
            _typedSprites.Get(playerType);
    }
}