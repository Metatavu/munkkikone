using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gazeController : MonoBehaviour {

  public Canvas textCanvas;
  public Text partTitleText;
  public Text partText;
  public float maxInfoDelay;
  public float gazeDelay;
  public float hideDelay;
  public float cancelShowDelay;

  private class PartDescription {

    public PartDescription(string title, string description) {
      Title = title;
      Description = description;
    }

    public string Description {
      get {
        return description;
      }

      set {
        description = value;
      }
    }

    public string Title {
      get {
        return title;
      }

      set {
        title = value;
      }
    }

    private string title;
    private string description;
  }

  private Dictionary<string, PartDescription> partDescriptions = new Dictionary<string, PartDescription> {
    { "annostelija", new PartDescription("Syöttötorvi ja -rumpu", "Esikypsennetyt munkit laitetaan syöttörumpuun, josta munkit menevät uuniin syöttörummun kautta. Yhdellä syöttörummun pyörähdyksellä syötetään 5 munkkia kerrallaan uuniin. Syöttötorveen mahtuu kerrallaan varastoon noin 40-60 munkkia.") },
    { "uuni", new PartDescription("Lämmitysuuni", "Uuni lämmittää munkit 100 asteessa 6 minuutin aikana noin 60-80 asteeseen. Syöttörumpu syöttää kerran minuutissa uuniin uuden rivin munkkeja ja uunin kuljetin siirtää munkkeja samalla syklityksellä askeleen eteenpäin. Uunissa mahtuu kerrallaan olemaan 30 munkkia lämmityksessä.") },
    { "sokerirumpu", new PartDescription("Sokerointirumpu", "Uunista lämmitetyt munkit tipahtavat kourua myöden sokerointirumpuun. Sokerointirumpu pyörittää munkkeja siten, että sokeri pyrkii nousemaan ylämäkeen ja munkit etenevät alamäkeen.") },
    { "hihna", new PartDescription("Siirtokuljetin", "Sokeroinnista munkit tippuvat siirtokuljettimelle, jossa suurin irtosokeri karisee kouruun. Siirtokuljetin siirtää munkit tarjoilukouruun koneen ulkopuolelle.") }
  };

  private string prevTag;
  private enum TextState { TEXT_HIDDEN, SHOWING_TEXT, TEXT_VISIBLE, HIDING_TEXT };
  private float textShowTimer;
  private float textHideTimer;
  private float showCancelTimer;
  private float maxInfoTimer;
  private TextState state;

  void Start() {
    hideText();
  }

  void Update() {
    RaycastHit hitInfo;
    if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, 20.0f, Physics.DefaultRaycastLayers)) {
      string partTag = hitInfo.collider.gameObject.tag;
      if (partDescriptions.ContainsKey(partTag)) {
        handleTagFound(partTag);
      } else {
        handleTagNotFound();
      }
    } else {
      handleTagNotFound();
    }
  }

  void handleTagNotFound() {
    if (state.Equals(TextState.SHOWING_TEXT)) {
      showCancelTimer += Time.deltaTime * 1000;
      if (showCancelTimer >= cancelShowDelay) {
        hideText();
      }
    } else if (state.Equals(TextState.TEXT_VISIBLE)) {
      textHideTimer = 0;
      state = TextState.HIDING_TEXT;
    } else if (state.Equals(TextState.HIDING_TEXT)) {
      textHideTimer += Time.deltaTime * 1000;
      if (textHideTimer >= hideDelay) {
        hideText();
      }
    }
  }

  void handleTagFound(string partTag) {
    if (state.Equals(TextState.TEXT_HIDDEN)) {
      state = TextState.SHOWING_TEXT;
      textShowTimer = 0;
    } else if (state.Equals(TextState.SHOWING_TEXT)) {
      if (prevTag.Equals(partTag)) {
        textShowTimer += Time.deltaTime * 1000;
        if (textShowTimer >= gazeDelay) {
          showText(partTag);
        }
      } else {
        textShowTimer = 0;
      }
    } else if (state.Equals(TextState.TEXT_VISIBLE)) {
      if (!partTag.Equals(prevTag)) {
        textHideTimer = 0;
        state = TextState.HIDING_TEXT;
      } else {
        maxInfoTimer += Time.deltaTime * 1000;
        if (maxInfoTimer >= maxInfoDelay) {
          textCanvas.enabled = false;
        }
      }
    } else if (state.Equals(TextState.HIDING_TEXT)) {
      textHideTimer += Time.deltaTime * 1000;
      if (textHideTimer >= hideDelay) {
        hideText();
      }
    }
    prevTag = partTag;
  }

  void showText(string tag) {
    showCancelTimer = 0;
    maxInfoTimer = 0;
    PartDescription partDescription;
    if (partDescriptions.TryGetValue(tag, out partDescription)) {
      textCanvas.enabled = true;
      partText.text = partDescription.Description;
      partTitleText.text = partDescription.Title;
      state = TextState.TEXT_VISIBLE;
    }
  }

  void hideText() {
    showCancelTimer = 0;
    textCanvas.enabled = false;
    state = TextState.TEXT_HIDDEN;
  }
}

