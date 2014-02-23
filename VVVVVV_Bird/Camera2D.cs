#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion


namespace VVVVVV_Bird
{
    public static class Camera2D
    {
        private static Vector2 cameraPosition;
        private static Matrix cameraTranslationMatrix;

        public static void UpdatePosition(Vector2 cameraPos, Vector2 cameraOffset)
        {
            cameraPosition = cameraPos;
            cameraTranslationMatrix=Matrix.CreateTranslation(-cameraPosition.X + cameraOffset.X, -cameraPosition.Y + cameraOffset.Y, 0);
        }

        public static Vector2 CameraPosition
        {
            get
            {
                return cameraPosition;
            }
        }

        public static Matrix CameraTranslationMatrix
        {
           get{
               return cameraTranslationMatrix;
           }
        }
    }
}
