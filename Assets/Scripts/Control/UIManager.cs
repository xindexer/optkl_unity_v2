using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Optkl.Control;

namespace Optkl.UI
{
    public class UIManager : MonoBehaviour
    {

        [SerializeField]
        private Canvas opening;

        [SerializeField]
        private Canvas loading;

        [SerializeField]
        private Canvas running;

        public void StartCanvas()
        {
            opening.gameObject.SetActive(true);
            loading.gameObject.SetActive(false);
            running.gameObject.SetActive(false);

        }
    }
}