using UnityEngine;
using System.Collections;

[AddComponentMenu( "Utilities/HUDFPS")]
public class HUDFPS : MonoBehaviour
{
	// Attach this to any object to make a frames/second indicator.
	//
	// It calculates frames/second over each updateInterval,
	// so the display does not keep changing wildly.
	//
	// It is also fairly accurate at very low FPS counts (<10).
	// We do this not by simply counting frames per interval, but
	// by accumulating FPS for each frame. This way we end up with
	// corstartRect overall FPS even if the interval renders something like
	// 5.5 frames.

	private Rect startRect = new Rect( 0, 0, 60, 20 ); // The rect the window is initially displayed at.
	public bool updateColor = true; // Do you want the color to change if the FPS gets low
	public bool allowDrag = true; // Do you want to allow the dragging of the FPS window
	public  float frequency = 0.5F; // The update frequency of the fps
	public int nbDecimal = 1; // How many decimal do you want to display

	private float accum   = 0f; // FPS accumulated over the interval
	private int   frames  = 0; // Frames drawn over the interval
	private Color color = Color.white; // The color of the GUI, depending of the FPS ( R < 10, Y < 30, G >= 30 )
	private string sFPS = ""; // The fps formatted into a string.
	private GUIStyle style; // The style the text will be displayed at, based en defaultSkin.label.

	void Start(){
		StartCoroutine( FPS() );
	}

	void Update(){
		accum += Time.timeScale/ Time.deltaTime;
		++frames;
	}

	IEnumerator FPS(){
		// Infinite loop executed every "frenquency" seconds.
		while( true )
		{
			// Update the FPS
			float fps = accum/frames;
			sFPS = fps.ToString( "f" + Mathf.Clamp( nbDecimal, 0, 10 ) );

			//Update the color
//			color = (fps >= 30) ? Color.clear : ((fps > 10) ? Color.red : Color.yellow);
			color = (fps >= 30) ? Color.green : ((fps > 10) ? Color.red : Color.yellow);

			accum = 0.0F;
			frames = 0;

			yield return new WaitForSeconds( frequency );
		}
	}

	void OnGUI() {

		float screenWidth = Screen.width;
		float screenHeight = Screen.height;
		float startWidth = startRect.width;
		float startHeight = startRect.height;
		float x = screenWidth - startWidth;
		float y = screenHeight - startHeight;
//		float foo = 0.0f;
//		float bar = 0.0f;

		startRect.x = x;
		startRect.y = y;


		// Copy the default label skin, change the color and the alignement
		if( style == null ){
			style = new GUIStyle( GUI.skin.label );
//			style.normal.textColor = Color.white;
			style.alignment = TextAnchor.LowerRight;
//			style.alignment = TextAnchor.MiddleCenter;

//			Vector2 myVector = new Vector2(0f, 0f);
//			style.contentOffset = myVector;
		}

		GUI.color = updateColor ? color : Color.white;
//		GUI.backgroundColor = Color.clear;
//		startRect = GUI.Window(0, startRect, DoMyWindow, "");
		startRect = GUI.Window(0, startRect, DoMyWindow, "");
	}

	void DoMyWindow(int windowID) {
//		Rect rect = new Rect(50,50, startRect.width, startRect.height);



		float screenWidth = Screen.width;
		float screenHeight = Screen.height;
//		float startWidth = startRect.width;
//		float startHeight = startRect.height;
////		float foo = screenWidth - startWidth;
////		float bar = screenHeight - startHeight;
//		float foo = 0.0f;
//		float bar = 0.0f;


//		Debug.Log("DoMyWindow screenWidth " + screenWidth);
//		Debug.Log("DoMyWindow screenHeight " + screenHeight);



//		Rect rect = new Rect(foo,bar, startWidth, startHeight);
//		Rect rect = new Rect((float)Screen.width - startRect.width, (float)Screen.height - startRect.height, startRect.width, startRect.height);
		Rect rect = new Rect(0, 0, startRect.width, startRect.height);
		GUI.Label(rect, sFPS + " FPS", style );

		if( allowDrag ) GUI.DragWindow(new Rect(0, 0, screenWidth, screenHeight));
	}
}
