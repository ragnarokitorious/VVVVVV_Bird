using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VVVVVV_Bird
{
    public interface ICollidableObject
    {
        Rectangle AddBoundingBox(Vector2 position, Vector2 origin, int width, int height);
        Rectangle CollisionRectangle { get; }
    }
}
