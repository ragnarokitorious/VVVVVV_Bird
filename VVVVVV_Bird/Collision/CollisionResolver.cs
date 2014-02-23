using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace VVVVVV_Bird
{
    public class CollisionResolver
    {
        private CollisionManager collisionManager;

        /// <summary>
        /// 
        /// </summary>
        public CollisionResolver()
        {
            collisionManager = new CollisionManager();
        }


        /// <summary>
        /// 
        /// </summary>
        public void Add(ICollidableObject item)
        {
            collisionManager.Add(item);
        }

        public void Remove(ICollidableObject item)
        {
            collisionManager.Remove(item);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Resolve()
        {
            List<CollisionObject> collList = collisionManager.GetCurrentCollision();

            foreach (CollisionObject obj in collList)
            {
                Type type1 = obj.Item1.GetType();
                Type type2 = obj.Item2.GetType();

                #region UGLY

                if (type1 == typeof(Player) && type2 == typeof(Wall))
                    HandlePlayer(obj.Item1 as Player, obj.Item2 as Wall);
                #endregion UGLY
            }
        }


        public void HandlePlayer(Player p1, Wall w1)
        {
            if (p1 != null && w1 != null)
            {
                Rectangle p1Rect = p1.CollisionRectangle;
                Rectangle w1Rect = w1.CollisionRectangle;

                Vector2 p1Pos = p1.Position;
                Vector2 p2Pos = w1.Position;

                Vector2 penetrationDepth = RectangleExtensions.GetIntersectionDepth(p1Rect, w1Rect);
                penetrationDepth.X = Math.Abs(penetrationDepth.X);
                penetrationDepth.Y = Math.Abs(penetrationDepth.Y);

                Vector2 p1MovementDirection, p1Vel;
                p1MovementDirection = p1Vel = p1.Velocity;
                p1MovementDirection.X /= (p1MovementDirection.X != 0 ? Math.Abs(p1MovementDirection.X) : 1);
                p1MovementDirection.Y /= (p1MovementDirection.Y != 0 ? Math.Abs(p1MovementDirection.Y) : 1);

                Vector2 p2MovementDirection, p2Vel;
                p2MovementDirection = p2Vel = new Vector2(0,0);
                p2MovementDirection.X /= (p2MovementDirection.X != 0 ? Math.Abs(p2MovementDirection.X) : 1);
                p2MovementDirection.Y /= (p2MovementDirection.Y != 0 ? Math.Abs(p2MovementDirection.Y) : 1);

                if (penetrationDepth.X < penetrationDepth.Y || (p1MovementDirection.Y == 0 && p2MovementDirection.Y == 0))
                {
                    float valToMove = penetrationDepth.X;
                    if (p1MovementDirection.X != 0 && p2MovementDirection.X != 0)
                        valToMove = (float)Math.Ceiling(valToMove / 2);

                    p1Pos.X += valToMove * p1MovementDirection.X * -1;
                    p2Pos.X += valToMove * p2MovementDirection.X * -1;

                    p1Vel.X = 0;
                    p2Vel.X = 0;
                }
                else if (penetrationDepth.X >= penetrationDepth.Y || (p1MovementDirection.X == 0 && p2MovementDirection.X == 0))
                {
                    float valToMove = penetrationDepth.Y;
                    if (p1MovementDirection.Y != 0 && p2MovementDirection.Y != 0)
                        valToMove = (float)Math.Ceiling(valToMove / 2);

                    p1Pos.Y += valToMove * p1MovementDirection.Y * -1;
                    p2Pos.Y += valToMove * p2MovementDirection.Y * -1;

                    p1Vel.Y = 0;
                    p2Vel.Y = 0;
                }


                p1.KillPlayer();
                p1.Velocity=new Vector2(0,0);

            }
        }
    }
}
