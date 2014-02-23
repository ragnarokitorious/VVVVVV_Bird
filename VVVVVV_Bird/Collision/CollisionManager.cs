using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VVVVVV_Bird
{
    class CollisionManager
    {
        /// <summary>
        /// List of collision objects
        /// </summary>
        private List<ICollidableObject> objectList;

        /// <summary>
        /// Initialize the collision manager
        /// </summary>
        public CollisionManager()
        {
            objectList = new List<ICollidableObject>();
        }

        /// <summary>
        /// Check collision and get the current list of collisions
        /// </summary>
        public List<CollisionObject> GetCurrentCollision()
        {
            List<CollisionObject> collisionList = new List<CollisionObject>();

            for (int i = 0; i < objectList.Count; i++)
            {
                for (int j = i + 1; j < objectList.Count; j++)
                {
                    Rectangle rect1 = objectList[i].CollisionRectangle;
                    Rectangle rect2 = objectList[j].CollisionRectangle;
                    if (rect1.Intersects(rect2))
                        collisionList.Add(new CollisionObject(objectList[i], objectList[j]));
                }
            }

            return collisionList;
        }

        /// <summary>
        /// Add item to the list of objects to check for collision
        /// </summary>
        public void Add(ICollidableObject item)
        {
            objectList.Add(item);
        }

        /// <summary>
        /// Remove item from the list of objects
        /// </summary>
        public void Remove(ICollidableObject item)
        {
            objectList.Remove(item);
        }
    }

    public struct CollisionObject
    {
        public ICollidableObject Item1;
        public ICollidableObject Item2;

        public CollisionObject(ICollidableObject item1, ICollidableObject item2)
        {
            Item1 = item1;
            Item2 = item2;
        }
    }
}
