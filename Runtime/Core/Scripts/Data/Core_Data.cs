
using UnityEngine;

namespace GBB.Core
{
    public class Core_Data : MonoBehaviour
    {
        public int Seed
        {
            get
            {
                return seed;
            }
        }
        [SerializeField]
        private int seed;
    }
}
