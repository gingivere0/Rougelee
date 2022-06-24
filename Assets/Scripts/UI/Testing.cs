using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee {
    public class Testing : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
            DamagePopup.Create(Vector3.zero, 200);
        }
    }
}