using System.Threading.Tasks;
using UnityEngine;

namespace HOT
{
    public class CompositeRoot : MonoBehaviour
    {
        public virtual void Compose() {}
        
        public virtual async Task ComposeAsync() {}
    }
}