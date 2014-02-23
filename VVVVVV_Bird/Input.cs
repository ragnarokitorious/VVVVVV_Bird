using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VVVVVV_Bird
{
    /// <summary>
    /// Provides an API for accessing input devices GamePad, Keyboard and Mouse
    /// </summary>
    public static class Input
    {
        #region PROPERTIES

        /// <summary>
        /// Number of gamepads to track by this class
        /// </summary>
        private static int gamePadNum = 4;
        public static int NumOfGamepad
        {
            get { return gamePadNum; }
        }

        /// <summary>
        /// Stores the current and previous keyboard states
        /// </summary>
        private static KeyboardState currentKeyboardState;
        private static KeyboardState prevKeyboardState;

        /// <summary>
        /// Stores the current and previous mouse states
        /// </summary>
        private static MouseState currentMouseState;
        private static MouseState prevMouseState;

        /// <summary>
        /// Stores the current and previous gamepad states
        /// </summary>
        private static GamePadState[] currentGamepadState;
        private static GamePadState[] prevGamepadState;

        /// <summary>
        /// Position of the mouse stored as a rect for intersection purposes
        /// </summary>
        private static Rectangle mousePos;

        #endregion PROPERTIES

        #region METHODS

        /// <summary>
        /// Generate an instance of the Input class
        /// </summary>
        public static void Initialize()
        {
            //Get the current state of the keyboard and initialize the previous state with the same information
            currentKeyboardState = Keyboard.GetState();
            prevKeyboardState = currentKeyboardState;

            //Get the current state of the mouse and initialize the previous state with the same information
            currentMouseState = Mouse.GetState();
            prevMouseState = currentMouseState;

            //Generate a number of GamePadStates and store the current state of each gamepad
            currentGamepadState = new GamePadState[gamePadNum];
            for (int i = 0; i < currentGamepadState.Length; i++)
                currentGamepadState[i] = GamePad.GetState((PlayerIndex)i);
            prevGamepadState = currentGamepadState;

            //Initialize the mouse location rect
            mousePos = new Rectangle(currentMouseState.X, currentMouseState.Y, 1, 1);
        }

        /// <summary>
        /// Generate an instance of the Input class
        /// </summary>
        /// <param name="gamePadNum">Number of gamepads to track (default 4)</param>
        public static void Initialize(int gamePadNum)
        {
            //Store the non-default number of Gamepads to track, if it isn't more than 4 (max supported by GamePadState)
            if (gamePadNum > 0 && gamePadNum < 5)
                Input.gamePadNum = gamePadNum;
            else
                throw new ArgumentOutOfRangeException("Number of game pads not supported.");

            //Get the current state of the keyboard and initialize the previous state with the same information
            currentKeyboardState = Keyboard.GetState();
            prevKeyboardState = currentKeyboardState;

            //Get the current state of the mouse and initialize the previous state with the same information
            currentMouseState = Mouse.GetState();
            prevMouseState = currentMouseState;

            //Generate a number of GamePadStates and store the current state of each gamepad
            currentGamepadState = new GamePadState[gamePadNum];
            for (int i = 0; i < currentGamepadState.Length; i++)
                currentGamepadState[i] = GamePad.GetState((PlayerIndex)i);
            prevGamepadState = currentGamepadState;

            //Initialize the mouse location rect
            mousePos = new Rectangle(currentMouseState.X, currentMouseState.Y, 1, 1);
        }

        /// <summary>
        /// Update all the current states of the input devices, as well as keeping the previous states
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of the game time</param>
        public static void Update(GameTime gameTime)
        {
            //Move the previous current states to the previous states
            prevKeyboardState = currentKeyboardState;
            prevMouseState = currentMouseState;
            prevGamepadState = (GamePadState[])currentGamepadState.Clone();

            //Update the current states to the updated states
            currentKeyboardState = Keyboard.GetState();
            currentMouseState = Mouse.GetState();
            for (int i = 0; i < gamePadNum; i++)
                currentGamepadState[i] = GamePad.GetState((PlayerIndex)i);

            //Update the position of the muse location rect
            mousePos.X = currentMouseState.X;
            mousePos.Y = currentMouseState.Y;
        }

        #endregion METHODS

        #region MOUSE METHODS

        /// <summary>
        /// Get the current position of the mouse
        /// </summary>
        /// <returns>Current position of the mouse</returns>
        public static Vector2 GetMousePosition()
        {
            return new Vector2(currentMouseState.X, currentMouseState.Y);
        }

        /// <summary>
        /// Check if the mouse is intersecting a rectangle at a certain location
        /// </summary>
        /// <param name="checkRect">Rectangle we are checking intersection with</param>
        /// <returns>Result from intersection check</returns>
        public static bool IsMouseIntersect(Rectangle checkRect)
        {
            return mousePos.Intersects(checkRect);
        }

        /// <summary>
        /// Check if the left mouse button is down
        /// </summary>
        /// <returns>Return if the left mouse button is down</returns>
        public static bool IsLeftMouseDown()
        {
            return currentMouseState.LeftButton == ButtonState.Pressed;
        }

        /// <summary>
        /// Check if the left mouse button is up
        /// </summary>
        /// <returns>Return if the left mouse button is up</returns>
        public static bool IsLeftMouseUp()
        {
            return currentMouseState.LeftButton == ButtonState.Released;
        }

        /// <summary>
        /// Check if the left mouse button is pressed 
        /// (Currently pressed but unpressed last update)
        /// </summary>
        /// <returns>Return if the left mouse button is pressed</returns>
        public static bool IsLeftMousePressed()
        {
            return (currentMouseState.LeftButton == ButtonState.Pressed) && (prevMouseState.LeftButton == ButtonState.Released);
        }

        /// <summary>
        /// Check if the left mouse button is released 
        /// (Currently unpressed but pressed last update)
        /// </summary>
        /// <returns>Return if the left mouse button is released</returns>
        public static bool IsLeftMouseReleased()
        {
            return (currentMouseState.LeftButton == ButtonState.Pressed) && (prevMouseState.LeftButton == ButtonState.Released);
        }

        /// <summary>
        /// Check if the right mouse button is down
        /// </summary>
        /// <returns>Return if the right mouse button is down</returns>
        public static bool IsRightMouseDown()
        {
            return currentMouseState.RightButton == ButtonState.Pressed;
        }

        /// <summary>
        /// Check if the right mouse button is up
        /// </summary>
        /// <returns>Return if the right mouse button is up</returns>
        public static bool IsRightMouseUp()
        {
            return currentMouseState.RightButton == ButtonState.Released;
        }

        /// <summary>
        /// Check if the right mouse button is pressed 
        /// (Currently pressed but unpressed last update)
        /// </summary>
        /// <returns>Return if the right mouse button is pressed</returns>
        public static bool IsRightMousePressed()
        {
            return (currentMouseState.RightButton == ButtonState.Pressed) && (prevMouseState.RightButton == ButtonState.Released);
        }

        /// <summary>
        /// Check if the right mouse button is released 
        /// (Currently unpressed but pressed last update)
        /// </summary>
        /// <returns>Return if the right mouse button is released</returns>
        public static bool IsRightMouseReleased()
        {
            return (currentMouseState.RightButton == ButtonState.Released) && (prevMouseState.RightButton == ButtonState.Pressed);
        }

        #endregion MOUSE METHODS

        #region KEYBOARD METHODS

        /// <summary>
        /// Check if a keyboard key is down
        /// </summary>
        /// <returns>Return if a keyboard key is down</returns>
        public static bool IsKeyDown(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// Check if a keyboard key is up
        /// </summary>
        /// <returns>Return if a keyboard key is up</returns>
        public static bool IsKeyUp(Keys key)
        {
            return currentKeyboardState.IsKeyUp(key);
        }

        /// <summary>
        /// Check if a keyboard key is pressed 
        /// (Currently pressed but unpressed last update)
        /// </summary>
        /// <returns>Return if a keyboard key is pressed</returns>
        public static bool IsKeyPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && prevKeyboardState.IsKeyUp(key);
        }

        /// <summary>
        /// Check if a keyboard key is released 
        /// (Currently unpressed but pressed last update)
        /// </summary>
        /// <returns>Return if a keyboard key is pressed</returns>
        public static bool IsKeyReleased(Keys key)
        {
            return currentKeyboardState.IsKeyUp(key) && prevKeyboardState.IsKeyDown(key);
        }

        #endregion KEYBOARD METHODS

        #region GAMEPAD METHODS

        /// <summary>
        /// Get the current position of a joystick on the gamepad for a certain player
        /// </summary>
        /// <param name="stick">Stick to check</param>
        /// <param name="playerNum">Player to be checked</param>
        /// <returns>Current position of the joystick inputted</returns>
        public static Vector2 GetStickPosition(Stick stick, PlayerIndex playerNum)
        {
            if (stick == Stick.Left)
                return currentGamepadState[(int)playerNum].ThumbSticks.Left;
            else
                return currentGamepadState[(int)playerNum].ThumbSticks.Right;
        }

        /// <summary>
        /// Check if a stick on the gamepad is pressed down in a particular direction, similar to the DPad directional buttons
        /// </summary>
        /// <param name="dir">Direction of the stick we are checking</param>
        /// <param name="playerNum">Player to be checked</param>
        /// <returns>Return of the stick is pressed in thats direction or not</returns>
        public static bool IsStickDown(Stick stick, Direction dir, PlayerIndex playerNum)
        {
            //1. Get the stick in question
            Vector2 stickPos;
            if (stick == Stick.Left)
                stickPos = currentGamepadState[(int)playerNum].ThumbSticks.Left;
            else
                stickPos = currentGamepadState[(int)playerNum].ThumbSticks.Right;

            //2. Run the IsStickDown check
            switch (dir)
            {
                case Direction.Up:
                    return (stickPos.Y >= 0.5);
                case Direction.Left:
                    return (stickPos.X <= -0.5);
                case Direction.Down:
                    return (stickPos.Y <= -0.5);
                case Direction.Right:
                    return (stickPos.X >= 0.5);
                default:
                    throw new ArgumentOutOfRangeException("Direction inputted is not a viable direction.");
            }
        }

        /// <summary>
        /// Check if a stick on the gamepad is not pressed down in a particular direction, similar to the DPad directional buttons
        /// </summary>
        /// <param name="dir">Direction of the stick we are checking</param>
        /// <param name="playerNum">Player to be checked</param>
        /// <returns>Return of the stick is not pressed in thats direction or not</returns>
        public static bool IsStickUp(Stick stick, Direction dir, PlayerIndex playerNum)
        {
            //1. Get the stick in question
            Vector2 stickPos;
            if (stick == Stick.Left)
                stickPos = currentGamepadState[(int)playerNum].ThumbSticks.Left;
            else
                stickPos = currentGamepadState[(int)playerNum].ThumbSticks.Right;

            //2. Run the IsStickUp check
            switch (dir)
            {
                case Direction.Up:
                    return (stickPos.Y < 0.5);
                case Direction.Left:
                    return (stickPos.X > -0.5);
                case Direction.Down:
                    return (stickPos.Y > -0.5);
                case Direction.Right:
                    return (stickPos.X < 0.5);
                default:
                    throw new ArgumentOutOfRangeException("Direction inputted is not a viable direction.");
            }
        }

        /// <summary>
        /// Check if a stick on the gamepad is pressed in a particular direction, similar to the DPad directional buttons
        /// (Currently pressed but unpressed last update)
        /// </summary>
        /// <param name="dir">Direction of the stick we are checking</param>
        /// <param name="playerNum">Player to be checked</param>
        /// <returns>Return of the stick is pressed in thats direction or not</returns>
        public static bool IsStickPressed(Stick stick, Direction dir, PlayerIndex playerNum)
        {
            //1. Get the sticks in question
            Vector2 currentStickPos, prevStickPos;
            if (stick == Stick.Left)
            {
                currentStickPos = currentGamepadState[(int)playerNum].ThumbSticks.Left;
                prevStickPos = prevGamepadState[(int)playerNum].ThumbSticks.Left;
            }
            else
            {
                currentStickPos = currentGamepadState[(int)playerNum].ThumbSticks.Right;
                prevStickPos = prevGamepadState[(int)playerNum].ThumbSticks.Right;
            }

            //2. Run the IsStickPressed check
            switch (dir)
            {
                case Direction.Up:
                    return (currentStickPos.Y >= 0.5) && (prevStickPos.Y < 0.5);
                case Direction.Left:
                    return (currentStickPos.X <= -0.5) && (prevStickPos.X > -0.5);
                case Direction.Down:
                    return (currentStickPos.Y <= -0.5) && (prevStickPos.Y > -0.5);
                case Direction.Right:
                    return (currentStickPos.X >= 0.5) && (prevStickPos.X < 0.5);
                default:
                    throw new ArgumentOutOfRangeException("Direction inputted is not a viable direction.");
            }
        }

        /// <summary>
        /// Check if a stick on the gamepad is released in a particular direction, similar to the DPad directional buttons
        /// (Currently unpressed but pressed last update)
        /// </summary>
        /// <param name="dir">Direction of the stick we are checking</param>
        /// <param name="playerNum">Player to be checked</param>
        /// <returns>Return of the stick is released in thats direction or not</returns>
        public static bool IsStickReleased(Stick stick, Direction dir, PlayerIndex playerNum)
        {
            //1. Get the sticks in question
            Vector2 currentStickPos, prevStickPos;
            if (stick == Stick.Left)
            {
                currentStickPos = currentGamepadState[(int)playerNum].ThumbSticks.Left;
                prevStickPos = prevGamepadState[(int)playerNum].ThumbSticks.Left;
            }
            else
            {
                currentStickPos = currentGamepadState[(int)playerNum].ThumbSticks.Right;
                prevStickPos = prevGamepadState[(int)playerNum].ThumbSticks.Right;
            }

            //2. Run the IsStickPressed check
            switch (dir)
            {
                case Direction.Up:
                    return (currentStickPos.Y < 0.5) && (prevStickPos.Y >= 0.5);
                case Direction.Left:
                    return (currentStickPos.X > -0.5) && (prevStickPos.X <= -0.5);
                case Direction.Down:
                    return (currentStickPos.Y > -0.5) && (prevStickPos.Y <= -0.5);
                case Direction.Right:
                    return (currentStickPos.X < 0.5) && (prevStickPos.X >= 0.5);
                default:
                    throw new ArgumentOutOfRangeException("Direction inputted is not a viable direction.");
            }
        }

        /// <summary>
        /// Check if a button on the controller is down
        /// </summary>
        /// <param name="button">Button to be checked</param>
        /// <param name="playerNum">Player to be checked</param>
        /// <returns>Return if the button checked is down</returns>
        public static bool IsButtonDown(Buttons button, PlayerIndex playerNum)
        {
            return currentGamepadState[(int)playerNum].IsButtonDown(button);
        }

        /// <summary>
        /// Check if a button on the controller is up
        /// </summary>
        /// <param name="button">Button to be checked</param>
        /// <param name="playerNum">Player to be checked</param>
        /// <returns>Return if the button checked is up</returns>
        public static bool IsButtonUp(Buttons button, PlayerIndex playerNum)
        {
            return currentGamepadState[(int)playerNum].IsButtonUp(button);
        }

        /// <summary>
        /// Check if a button on the controller is pressed
        /// (Currently pressed but unpressed last update)
        /// </summary>
        /// <param name="button">Button to be checked</param>
        /// <param name="playerNum">Player to be checked</param>
        /// <returns>Return if the button checked is pressed</returns>
        public static bool IsButtonPressed(Buttons button, PlayerIndex playerNum)
        {
            return currentGamepadState[(int)playerNum].IsButtonDown(button) && prevGamepadState[(int)playerNum].IsButtonUp(button);
        }

        /// <summary>
        /// Check if a button on the controller is released
        /// (Currently unpressed but pressed last update)
        /// </summary>
        /// <param name="button">Button to be checked</param>
        /// <param name="playerNum">Player to be checked</param>
        /// <returns>Return if the button checked is released</returns>
        public static bool IsButtonReleased(Buttons button, PlayerIndex playerNum)
        {
            return currentGamepadState[(int)playerNum].IsButtonUp(button) && prevGamepadState[(int)playerNum].IsButtonDown(button);
        }

        /// <summary>
        /// Check if the gamepad of a certain player is connected
        /// </summary>
        /// <param name="playerNum">Player to be checked</param>
        /// <returns>If the gamepad of a certain player is connected</returns>
        public static bool IsGamepadConnected(PlayerIndex playerNum)
        {
            return currentGamepadState[(int)playerNum].IsConnected;
        }

        #endregion GAMEPAD METHODS
    }

    /// <summary>
    /// Representation of the 4 directions on a DPad
    /// </summary>
    public enum Direction
    {
        Up = 0,
        Left = 1,
        Down = 2,
        Right = 3
    };

    /// <summary>
    /// Representation of the two sticks on a Gamepad
    /// </summary>
    public enum Stick
    {
        Left = 0,
        Right = 1
    };
}

