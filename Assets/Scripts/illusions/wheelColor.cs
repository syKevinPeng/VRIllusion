// using System;
// using UnityEngine;

// public class wheelColor : wheelLength
// {
//     [SerializeField]
//     private float foregroundIntensity = 1.0f;  // Initially white
//     [SerializeField]
//     private float backgroundIntensity = 0.0f;  // Initially black

//     // Property to get/set the foreground color intensity
//     public float ForegroundIntensity
//     {
//         get { return foregroundIntensity; }
//         set
//         {
//             if (!Mathf.Approximately(foregroundIntensity, value))
//             {
//                 foregroundIntensity = value;
//                 UpdatePatternColors();
//             }
//         }
//     }

//     // Property to get/set the background color intensity
//     public float BackgroundIntensity
//     {
//         get { return backgroundIntensity; }
//         set
//         {
//             if (!Mathf.Approximately(backgroundIntensity, value))
//             {
//                 backgroundIntensity = value;
//                 UpdatePatternColors();
//             }
//         }
//     }

//     void Start()
//     {
//         texture = GeneratePattern(); // Generate the initial pattern
//         ApplyTexture(); // Apply texture to a renderer
//     }

//     public wheelColor(float stepSize = 0.01f)
//     {
//         texture = GeneratePattern();
//         this.stepSize = stepSize;
//     }

//     // Generate the pattern with dynamic color based on intensity
//     public override Texture2D GeneratePattern()
//     {
//         Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);

//         // Define colors based on intensity
//         Color foregroundColor1 = new Color(foregroundIntensity, foregroundIntensity, foregroundIntensity, 1f);
//         Color foregroundColor2 = new Color(1f - foregroundIntensity, 1f - foregroundIntensity, 1f - foregroundIntensity, 1f); // The complementary color
//         Color backgroundColor = new Color(backgroundIntensity, backgroundIntensity, backgroundIntensity, 1f);

//         // Fill the background
//         for (int i = 0; i < width; i++)
//         {
//             for (int j = 0; j < height; j++)
//             {
//                 texture.SetPixel(i, j, backgroundColor);
//             }
//         }

//         Vector2 center = new Vector2(width / 2, height / 2);

//         // Draw rotated squares with alternating line colors
//         for (int i = 0; i < numSquares; i++)
//         {
//             float angle = 2 * Mathf.PI * i / numSquares;
//             Vector2 squareCenter = center + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
//             DrawRotatedSquare(texture, squareCenter, squareSize, angle + Mathf.PI / 2, foregroundColor1, foregroundColor2);
//         }

//         // Draw the dot at the center using the first foreground color
//         DrawDot(texture, center, dotDiameter, foregroundColor1);

//         texture.Apply();
//         return texture;
//     }

//     public override float GetCurrentRatio()
//     {
//         Debug.LogWarning("ForegroundColor: " + foregroundColor + " BackgroundColor: " + backgroundColor);
//         if (foregroundColor == 0)
//         {
//             return 10f;
//         }
//         else
//         {
//             return (float)backgroundColor / foregroundColor;
//         }

//     }

//     public override float GetInitRatio()
//     {
//         return 10;
//     }

//     public override void DecreasePatternRatio()
//     {

//         if (backgroundColor > foregroundColor)
//         {
//             foregroundColor += stepSize;
//             backgroundColor -= stepSize;
//         }
//         else
//         {
//             foregroundColor = backgroundColor = 0.5f;
//         }

//         texture = GeneratePattern();
//     }

//     public override void IncreasePatternRatio()
//     {
//         if (foregroundColor - stepSize >= 0.0f)
//         {
//             backgroundColor += stepSize;
//             foregroundColor -= stepSize;
//         }
//         texture = GeneratePattern();
//     }


// }
