using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

namespace InfallibleCode
{
    public class SectionTrigger : MonoBehaviour
    {
        public GameObject roadSection;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Trigger"))
            {
                Instantiate(roadSection, new Vector3(0, 0, 148), Quaternion.identity);
            }
        }
    }
}
