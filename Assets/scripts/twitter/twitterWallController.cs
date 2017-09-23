using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Twitter;

public class twitterWallController : MonoBehaviour {

  public string TwitterConsumerKey;
  public string TwitterConsumerSecret;
  public string TwitterAccessToken;
  public string TwitterAccessTokenSecret;
  public Text tweets;

  private Stream stream;
  private List<string> visibleTweets;
  private Coroutine twitterListener;

  void Start () {
    Twitter.Oauth.consumerKey = TwitterConsumerKey;
    Twitter.Oauth.consumerSecret = TwitterConsumerSecret;
    Twitter.Oauth.accessToken = TwitterAccessToken;
    Twitter.Oauth.accessTokenSecret = TwitterAccessTokenSecret;
    visibleTweets = new List<string>();

    stream = new Stream(StreamType.PublicFilter);
    Dictionary<string, string> streamParameters = new Dictionary<string, string>();

    List<string> tracks = new List<string>();
    tracks.Add("hololens");
    tracks.Add("HoloLens");
    tracks.Add("metatavu");
    tracks.Add("hakko");
    tracks.Add("alihankintamessut");
    tracks.Add("alihankinta-messut");
    tracks.Add("augmented");
    tracks.Add("hologram");
    Twitter.FilterTrack filterTrack = new Twitter.FilterTrack(tracks);
    streamParameters.Add(filterTrack.GetKey(), filterTrack.GetValue());
    twitterListener = StartCoroutine(stream.On(streamParameters, OnStream));
  }

  void OnApplicationQuit() {
    stream.Off();
    StopCoroutine(twitterListener);
  }

  void DisplayTweets() {
    string textToDisplay = "";
    visibleTweets.ForEach(delegate (string tweet) {
      textToDisplay += "\n\n" + tweet;
    });
    tweets.text = textToDisplay;
  }

  void OnStream(string response, StreamMessageType messageType) {
    try {
      if (messageType == StreamMessageType.Tweet) {
        Tweet tweet = JsonUtility.FromJson<Tweet>(response);
        visibleTweets.Add(tweet.text);
        if (visibleTweets.Count > 5) {
          visibleTweets.RemoveAt(0);
        }
        DisplayTweets();
      }
    } catch (System.Exception e) {
      Debug.Log(e);
    }
  }
}
