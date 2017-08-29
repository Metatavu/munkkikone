using HoloToolkit.Unity.InputModule;
using UnityEngine;
using UnityEngine.Events;

namespace HoloToolkit.Examples.ColorPicker
{
    public class CloseDialog : MonoBehaviour, IInputClickHandler
    {
        public GameObject dialogObject;

        void Update()
        {
        }

        public void OnInputClicked(InputClickedEventData eventData)
        {
            Debug.Log("klikattu");
            GameObject[] gameObjects;

            gameObjects = GameObject.FindGameObjectsWithTag("DialogElementti");

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.GetComponent<TapController>().dialogOpen = false;
            }
        }
    }
}