using System;
using System.Collections;
using UnityEngine;

namespace Block
{
    public class BlockController : MonoBehaviour
    {
        
        // Update is called once per frame
        private void OnBecameInvisible()
        {
            try
            {
                StartCoroutine(Destroy());
            }
            catch (Exception exception)
            {
                // ignored
            }
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        IEnumerator Destroy()
        {
            yield return new WaitForSeconds(2f);

            Destroy(gameObject);
        }
    }
}