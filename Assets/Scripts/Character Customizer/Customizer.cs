using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Customizer : MonoBehaviour {

	// Arrays
	private Sprite[] bodies;
	private Sprite[] faces;
	private Object[] effects;
	
	// Currently selected part
	private int currentBody;
	private int currentFace;
	private int currentEffect;

	private int category = 1;

	public Image Body;
	public Image Face;
	public Image Effect;

	public Text editingText;

	public Sprite activeBody;
	public Sprite inactiveBody;
	public Sprite activeFace;
	public Sprite inactiveFace;
	public Sprite activeEffect;
	public Sprite inactiveEffect;

	public Button bodyButton;
	public Button faceButton;
	public Button effectButton;

	// Use this for initialization
	void Start () 
	{
		LoadImages();
		LoadCharacter();
	}
	
	// Update is called once per frame
	void Update () 
	{
		DisplayCurrentImage();
	}

	void LateUpdate () 
	{
		UpdateText();

		Body.SetNativeSize();
		Face.SetNativeSize();
	}

	void LoadImages () 
	{
		//Debug.Log("Loading Images...");

		// Load body images from resources
		bodies = Resources.LoadAll<Sprite>("Player/Body");

		if (bodies.Length > 0)
		{
			for (int x = 0; x < bodies.Length; x++)
			{
				//Debug.Log("Loaded " + bodies[x].name);
				
				if (x == bodies.Length - 1)
				{
					//Debug.Log("Bodies Loaded: " + bodies.Length);
				}
			}
		}

		// Load face images from resources
		faces = Resources.LoadAll<Sprite>("Player/Face");

		if (faces.Length > 0)
		{
			for (int x = 0; x < faces.Length; x++)
			{
				//Debug.Log("Loaded " + faces[x].name);
				
				if (x == faces.Length - 1)
				{
					//Debug.Log("Faces Loaded: " + faces.Length);
				}
			}
		}
	}

	void LoadCharacter ()
	{
		//Debug.Log("Loading Character...");

		// Load the player's character
		currentBody = Globals.PLAYER_BODY;
		//Debug.Log("Currently Equipped Body: " + bodies[currentBody].name);

		currentFace = Globals.PLAYER_FACE;
		//Debug.Log("Currently Equipped Face: " + faces[currentFace].name);
	}

	void DisplayCurrentImage()
	{
		// Display the currently selected features
		Body.sprite = bodies[currentBody];
		Face.sprite = faces[currentFace];
		//effect.something = effects[currentEffect];
	}

	public void NextPart ()
	{
		if (category == 1)
		{
			if (currentBody + 1 < bodies.Length)
			{
				currentBody++;
			}
			else
			{
				currentBody = 0;
			}
		}

		if (category == 2)
		{
			if (currentFace + 1 < faces.Length)
			{
				currentFace++;
			}
			else
			{
				currentFace = 0;
			}
		}
	}

	public void PreviousPart()
	{
		if (category == 1)
		{
			if (currentBody != 0)
			{
				currentBody--;
			}
			else
			{
				currentBody = bodies.Length - 1;
			}
		}
		
		if (category == 2)
		{
			if (currentFace != 0)
			{
				currentFace--;
			}
			else
			{
				currentFace = faces.Length - 1;
			}
		}
	}

	public void SelectBody()
	{
		if (category != 1)
		{
			category = 1;
			bodyButton.image.sprite = activeBody;

			faceButton.image.sprite = inactiveFace;
			effectButton.image.sprite = inactiveEffect;
		}
	}

	public void SelectFace()
	{
		if (category != 2)
		{
			category = 2;
			faceButton.image.sprite = activeFace;

			bodyButton.image.sprite = inactiveBody;
			effectButton.image.sprite = inactiveEffect;
		}
	}

	public void SelectEffect()
	{
		if (category != 3)
		{
			category = 3;

			effectButton.image.sprite = activeEffect;

			bodyButton.image.sprite = inactiveBody;
			faceButton.image.sprite = inactiveFace;
		}
	}

	void UpdateText ()
	{
		if (category == 1)
		{
			editingText.text = bodies[currentBody].name;
		}

		if (category == 2)
		{
			editingText.text = faces[currentFace].name;
		}

		if (category == 3)
		{
			//editingText.text = effects[currentEffect].name;
			editingText.text = "Sorry, no effects available!";
		}
	}

	void Equip ()
	{
		// Check if part is available and equip currently selected part

	}

	public void SaveCharacter ()
	{
		// Save character settings
		Globals.PLAYER_BODY = currentBody;
		Globals.PLAYER_FACE = currentFace;
	}
}
