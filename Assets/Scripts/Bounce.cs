using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Bounce : MonoBehaviour
{
    private RectTransform canvas;

    private Vector3 position;

    private float FinalX;
    private float FinalY;
    
    private float OffScreenX;
    private float OffScreenY;

    private Vector3 FinalPosition;

    private float screenWidth;
    private float screenHeight;

    private float actualScreenWidth;
    private float actualScreenHeight;

    private float buttonWidth;
    private float buttonHeight;

    private float actualButtonWidth;
    private float actualButtonHeight;

    private float change;

    public float duration = 100;

    public enum Options { Bottom, Top, Left, Right };

    public Options Side;

    // Used to increase the springiness/urgency of the animation
    public float padding = 0;

    // Use this for initialization
    void Start()
    {
        // Find the canvas (the UI element we're animating is a child of it)
        canvas = transform.parent.GetComponent<RectTransform>();

        // Get the final position of the button before we start moving things around
        FinalPosition = this.transform.position;

        GetScreenDimensions();
        GetAdjustedScreenDimensions();
        GetButtonDimensions();
        GetAdjustedButtonDimensions();
        GetButtonPosition();
        CalculateNewPosition();
        CalculateTravelDistance();
        MoveToFinalPosition();
    }

    // Get the dimensions of the canvas
    void GetScreenDimensions()
    {
        screenWidth = canvas.rect.width;
        screenHeight = canvas.rect.height;
    }
    
    // Adjust the dimensions of the canvas to account for canvas scaling
    void GetAdjustedScreenDimensions()
    {
        actualScreenWidth = screenWidth * canvas.localScale.x;

        actualScreenHeight = screenHeight * canvas.localScale.y;
    }

    // Get the dimensions of the button
    void GetButtonDimensions()
    {
        buttonWidth = gameObject.GetComponent<RectTransform>().rect.width;
        buttonHeight = gameObject.GetComponent<RectTransform>().rect.height;
    }

    // Adjust the dimensions of the buttons to account for canvas scaling
    void GetAdjustedButtonDimensions()
    {
        actualButtonWidth = gameObject.GetComponent<RectTransform>().rect.width * canvas.localScale.x;
        actualButtonHeight = gameObject.GetComponent<RectTransform>().rect.height * canvas.localScale.y;
    }

    // Save the final X and Y position for the button before moving it
    void GetButtonPosition()
    {
        FinalX = gameObject.GetComponent<RectTransform>().position.x;
        FinalY = gameObject.GetComponent<RectTransform>().position.y;
    }

    // Calculate the button's new position off the canvas based on what side is chosen
    void CalculateNewPosition()
    {
        if (Side == Options.Bottom)
        {
            OffScreenY = (0 - actualButtonHeight/2) - padding;
        }

        if (Side == Options.Top)
        {
            OffScreenY = (actualScreenHeight + actualButtonHeight/2) + padding;
        }

        if (Side == Options.Left)
        {
            OffScreenX = (0 - actualButtonWidth/2) - padding;
        }

        if (Side == Options.Right)
        {
            OffScreenX = (actualScreenWidth + actualButtonWidth/2) + padding;
        }
    }

    // Calculate the travel distance of the button's animation
    void CalculateTravelDistance()
    {
        if (Side == Options.Bottom)
        {
            change = Math.Abs(OffScreenY) + Math.Abs(FinalY);
        }

        if (Side == Options.Top)
        {
            change = OffScreenY - Math.Abs(FinalY);
        }

        if (Side == Options.Left)
        {
            change = Math.Abs(OffScreenX) + Math.Abs(FinalX);
        }

        if (Side == Options.Right)
        {
            change = -(Math.Abs(OffScreenX) - Math.Abs(FinalX));
        }
    }

    // Move the button outside the canvas
    void MoveToFinalPosition()
    {
        if (Side == Options.Bottom || Side == Options.Top)
        {
            transform.position = new Vector3(transform.position.x, OffScreenY, transform.position.z);
        }

        if (Side == Options.Left || Side == Options.Right)
        {
            transform.position = new Vector3(OffScreenX, transform.position.y, transform.position.z);
        }
    }

    void Update()
    {
        StartCoroutine("AnimateIn");

        // Find the back button
        GameObject backbutton = GameObject.Find("Back");

        // If the back button is pressed, animate out of the canvas
        if (backbutton.GetComponent<NextScene>().exit == true)
        {
            StopCoroutine("AnimateIn");
            StartCoroutine("AnimateOut");
        }
    }

    // Animates the button from outside the canvas into the canvas
    IEnumerator AnimateIn()
    {
        if (Side == Options.Bottom)
        {
            for (float t = 0; t < duration; t++)
            {
                position.x = transform.position.x;
                position.y = (float)(Easing.BackEaseOut(t, OffScreenY, change, duration));
                transform.position = position;
                yield return null;
            }
        }

        if (Side == Options.Top)
        {
            for (float t = 0; t < duration; t++)
            {
                position.x = transform.position.x;
                position.y = (float)(Easing.BackEaseOut(t, OffScreenY, -change, duration));
                transform.position = position;
                yield return null;
            }
        }

        if (Side == Options.Left)
        {
            for (float t = 0; t < duration; t++)
            {
                position.y = transform.position.y;
                position.x = (float)(Easing.BackEaseOut(t, OffScreenX, change, duration));
                transform.position = position;
                yield return null;
            }
        }

        if (Side == Options.Right)
        {
            for (float t = 0; t < duration; t++)
            {
                position.y = transform.position.y;
                position.x = (float)(Easing.BackEaseOut(t, OffScreenX, change, duration));
                transform.position = position;
                yield return null;
            }
        }
    }
    
    // Animates the button from inside the canvas to outside the canvas
    IEnumerator AnimateOut()
    {
        if (Side == Options.Bottom)
        {
            for (float t = 0; t < duration; t++)
            {
                position.x = transform.position.x;
                position.y = (float)(Easing.BackEaseInOut(t, FinalY, -change, duration));
                transform.position = position;
                yield return null;
            }
        }

        if (Side == Options.Top)
        {
            for (float t = 0; t < duration; t++)
            {
                position.x = transform.position.x;
                position.y = (float)(Easing.BackEaseInOut(t, FinalY, change, duration));
                transform.position = position;
                yield return null;
            }
        }

        if (Side == Options.Left)
        {
            for (float t = 0; t < duration; t++)
            {
                position.y = transform.position.y;
                position.x = (float)(Easing.BackEaseInOut(t, FinalX, -change, duration));
                transform.position = position;
                yield return null;
            }
        }

        if (Side == Options.Right)
        {
            for (float t = 0; t < duration; t++)
            {
                position.y = transform.position.y;
                position.x = (float)(Easing.BackEaseInOut(t, FinalX, -change, duration));
                transform.position = position;
                yield return null;
            }
        }

    }
}
