using UnityEngine;

namespace Default
{
    public class EntityView : MonoBehaviour
    {
        [SerializeField] 
        private EntityType _type;
        
        public int Idx { get; private set; }
        public EntityType Type => _type;

        public void SetIndex(int idx)
        {
            Idx = idx;
        }

        public void SetPos(Vector2 coord)
        {
            transform.localPosition = coord;
        }
    }
}