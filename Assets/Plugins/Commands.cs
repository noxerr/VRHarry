using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.Timers;

// This is boilerplate code to start coding using the Ispikit Unity plugin
// On iOS, it assumes it belongs to a "GameObject" game object

[RequireComponent (typeof (AudioSource))]

public class Commands : MonoBehaviour {

// Below are the available plugin calls for all supported platforms

#if UNITY_ANDROID
	public delegate void initCallbackDelegate(int n);
	public delegate void resultCallbackDelegate(int score, int speed, string words);

	[DllImport("upal")]
	private static extern int startInitialization(initCallbackDelegate icb, string path);
	[DllImport("upal")]
	private static extern int setResultCallback(resultCallbackDelegate rcb);
	[DllImport("upal")]
	private static extern int startRecording(string sentences);
	[DllImport("upal")]
	private static extern int stopRecording();
#endif
    public GameObject go;
	private static System.Timers.Timer timer;
    private static string phrase = "";
	// We need to keep this information for desktop platforms 
    // otherwise the timer keep running in the background
	private static bool stopped;
	void Awake()
	{
		stopped = false;
#if UNITY_ANDROID || UNITY_STANDALONE_OSX
        startInitialization(new initCallbackDelegate(this.initCallback), Application.persistentDataPath);
#endif
	}
	void StopAll()
	{
		stopped = true;
		stopRecording ();
	}
	void OnApplicationQuit()
	{
		StopAll ();
	}
	void Start () {
	}
	void Update () {
	}

#if UNITY_ANDROID || UNITY_STANDALONE_OSX
	public void initCallback(int status) {
#endif
		// This is for when plugin is initialized, status should be "0"
		Debug.Log ("HOLA ESTATUS: " + status);
#if UNITY_ANDROID || UNITY_STANDALONE_OSX
		setResultCallback (new resultCallbackDelegate(Ispikit.resultCallback));
#endif
    }

    public void startRecCommand(string phrases){
		// We then start recording, with three possible inputs: "first", "second", and "third"
        Debug.Log("HOLA START: ");
        phrase = phrases;
		startRecording (phrase);
		// Three seconds later, we wiĺl stop recording in the provided onRecordingDone function
		if (stopped)
			return;
		timer = new System.Timers.Timer (1000);
		timer.Elapsed += onRecordingDone;
		timer.AutoReset = false;
		timer.Enabled = true;
	}


	private static void onRecordingDone(object source, ElapsedEventArgs e) {
		// This just stops recording. In the background, analysis will start
		// and result callback will be called once done.
		stopRecording ();
	}

#if UNITY_ANDROID || UNITY_STANDALONE_OSX
	public static void resultCallback(int score, int speed, string words) {
#endif
		// Callback when result is available, a few seconds after stopRecording, typically.
		// See docs on how to parse the result.
		Debug.Log ("HOLA Result");
#if UNITY_ANDROID || UNITY_STANDALONE_OSX
		Debug.Log (score);
		Debug.Log (speed);
		Debug.Log (words);
#endif
		// Starts replaying the userś voice after one second
		if (stopped)
			return;
		timer = new System.Timers.Timer (1000);
        timer.Elapsed += onRestarting;
		timer.AutoReset = false;
		timer.Enabled = true;
	}

	private static void onRestarting(object source, ElapsedEventArgs e) {
		// This is a function to be called by a timer to start another recognition
		if (stopped)
			return;
		startRecording (phrase);
		timer = new System.Timers.Timer (1000);
		timer.Elapsed += onRecordingDone;
		timer.AutoReset = false;
		timer.Enabled = true;
	}

	public static void newWordsCallback(string words) {
		// This callback comes during recording, it gives the words recognized
		Debug.Log ("New words");
		Debug.Log (words);
	}

}
