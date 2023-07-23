using UnityEngine;

namespace MelonJam4.Factory
{
    [ExecuteAlways]
    public sealed class ConveyorScroller : MonoBehaviour
    {
        [SerializeField]
        private float _speed;
        
        [SerializeField]
        private Material _conveyorMaterial;

        private void Update()
        {
            _conveyorMaterial.mainTextureOffset = new Vector2(0, Time.time * _speed);
        }
    }
}
