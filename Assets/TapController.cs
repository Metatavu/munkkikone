using HoloToolkit.Unity.InputModule;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace HoloToolkit.Examples.ColorPicker {
    public class TapController : MonoBehaviour, IInputClickHandler {
        public bool dialogOpen = false;
        public string dialogMessage;
        public GameObject selectedObject;
        public GameObject textObject;

        void Update() {
            if (dialogOpen)
            {
                textObject.GetComponent<UnityEngine.UI.Text>().text = dialogMessage;
                selectedObject.SetActive(true);
            } else {
                selectedObject.SetActive(false);
            }
        }

        public void OnInputClicked(InputClickedEventData eventData) {
            Debug.Log("click");
            dialogOpen = true;
        }
    }
}