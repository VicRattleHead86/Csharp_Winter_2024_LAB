/*
 * Giezar Panaligan
 * Lab 4
 * 
 *  April 22, 2024
 *  Lab 4 - BallRoom
 * 
 * Pseudo Code
 * 
 * Define diameter sizes and movement states
 * ENUM DiameterSize { Small, Medium, Large }
 * ENUM MovementState { Idle, Moving }
 *
 * Ball structure with attributes
 * STRUCT Ball
 *   DiameterSize Size
 *   Point Position
 *   Color Color
 *   Point Velocity
 *
 * Main Program class
 * CLASS Program
 *   - MovementState currentMovementState (Tracks Idle/Moving state)
 *   - Random rng (Random number generator)
 *   - int ballCount (Counts current balls)
 *
 * Main program entry point
 * METHOD Main
 *   - Create CDrawer canvas (Initialize drawing surface)
 *   - Create Ball array of 100 elements (Holds balls)
 *   - Get screenWidth and screenHeight from canvas
 *   LOOP
 *     - If left mouse click
 *       - Create new ball and set velocity
 *       - Add new ball to array and render it
 *     - If right mouse click
 *       - Toggle currentMovementState (Idle <-> Moving)
 *     - If currentMovementState is Moving
 *       - Update and render all balls
 *     - Sleep for a short duration (Reduce CPU usage)
 *
 * Method to get random diameter size
 * METHOD GetDiameterSize
 *   - Return a random DiameterSize
 *
 * Method to create a new ball
 * METHOD CreateBall
 *   - Create and return Ball with position, size, color, and zero velocity
 *
 * Method to set initial velocity
 * METHOD SetBallVelocity
 *   - Assign random horizontal and minimum downward velocity
 *   - If ball is near the top, enforce stronger downward velocity
 *
 * Method to move the ball and handle boundaries
 * METHOD MoveBall
 *   - Update ball's position based on its velocity
 *   - Reverse direction if hitting side boundaries
 *   - Handle vertical boundary collisions, ensuring downward movement
 *
 * Method to render a ball on the canvas
 * METHOD RenderBall
 *   - Draw the ball at its current position on the canvas
 *
 * Method to update and render all balls
 * METHOD UpdateAndRenderAllBalls
 *   - Clear canvas before rendering
 *   - Loop through all balls
 *   - Move and render each ball
 *   
 */

using System;
using System.Drawing;
using GDIDrawer;

namespace BallRoom
{

    // Main class handling the ball game
    internal class Program
    {

        // Define enum for different diameters of balls
        public enum DiameterSize
        {
            Small = 25,
            Medium = 40,
            Large = 70
        }

        // Define enum for the movement state of balls
        public enum MovementState
        {
            Idle,
            Moving
        }

        // Structure to represent a ball with size, position, color, and velocity
        public struct Ball
        {
            public DiameterSize Size;
            public Point Position;
            public Color Color;
            public Point Velocity;
        }

        // Track current movement state of balls (Idle or Moving)
        private static MovementState currentMovementState = MovementState.Idle;

        // Random generator for various randomization needs
        private static Random rng = new Random();

        // Counter to track the current number of balls in play
        private static int ballCount = 0;

        // Entry point of the application
        static void Main(string[] args)
        {
            // Create a graphical canvas to draw on
            CDrawer canvas = new CDrawer();

            // Array to store up to 100 balls
            Ball[] balls = new Ball[100];

            // Retrieve screen dimensions from the canvas
            int screenWidth = canvas.m_ciWidth;
            int screenHeight = canvas.m_ciHeight;

            // Main application loop
            while (true)
            {
                // Check for left mouse click to create a new ball
                if (canvas.GetLastMouseLeftClick(out Point mouseClickPosition))
                {
                    // Randomly select a size for the new ball
                    DiameterSize selectedSize = GetDiameterSize();

                    // Create a new ball at the clicked position
                    Ball newBall = CreateBall(mouseClickPosition, selectedSize);

                    // Set the velocity of the new ball
                    SetBallVelocity(ref newBall, screenHeight);

                    // Store the new ball in the array
                    balls[ballCount++] = newBall;

                    // Draw the new ball on the canvas
                    RenderBall(canvas, newBall);
                }

                // Check for right mouse click to toggle the movement state of all balls
                if (canvas.GetLastMouseRightClick(out _))
                {
                    currentMovementState = currentMovementState == MovementState.Idle ? MovementState.Moving : MovementState.Idle;
                }

                // If balls are set to move, update and render all balls
                if (currentMovementState == MovementState.Moving)
                {
                    UpdateAndRenderAllBalls(canvas, balls, ballCount, screenWidth, screenHeight);
                }

                // Introduce a small delay to reduce CPU usage
                System.Threading.Thread.Sleep(20);
            }
        }

        // Method to randomly select a diameter size for a new ball
        static DiameterSize GetDiameterSize()
        {
            int index = rng.Next(3); // Randomly select an index for the size
            return (DiameterSize)(25 + 15 * index); // Map index to actual size value
        }

        // Method to create a new ball at a given position with a given size
        static Ball CreateBall(Point position, DiameterSize size)
        {
            return new Ball
            {
                Size = size,
                Position = position,
                Color = Color.FromArgb(rng.Next(256), rng.Next(256), rng.Next(256)), // Assign a random color
                Velocity = new Point(0, 0) // Initialize velocity to zero
            };
        }

        // Method to set the initial velocity of a ball based on its vertical position
        static void SetBallVelocity(ref Ball ball, int screenHeight)
        {
            ball.Velocity.X = rng.Next(-1, 2); // Randomize horizontal movement
            ball.Velocity.Y = rng.Next(3, 6); // Set a minimum downward velocity

            // If the ball is very close to the top, ensure it moves downwards
            if (ball.Position.Y <= ((int)ball.Size / 2))
            {
                ball.Velocity.Y = Math.Max(ball.Velocity.Y, 3); // Ensure at least some downward velocity
            }
        }

        // Method to move a ball and handle boundary collisions
        static void MoveBall(ref Ball ball, int screenWidth, int screenHeight)
        {
            int radius = (int)ball.Size / 2;

            // Update ball position based on current velocity
            ball.Position.X += ball.Velocity.X;
            ball.Position.Y += ball.Velocity.Y;

            // Handle collisions with the left and right walls
            if (ball.Position.X - radius <= 0 || ball.Position.X + radius >= screenWidth)
            {
                ball.Velocity.X *= -1; // Reverse horizontal direction upon hitting a wall
            }

            // Handle collisions with the top and bottom walls
            if (ball.Position.Y - radius <= 0)
            {
                ball.Position.Y = radius; // Prevent ball from sticking to the top
                ball.Velocity.Y = Math.Max(ball.Velocity.Y, 3); // Ensure it moves downwards
            }
            else if (ball.Position.Y + radius >= screenHeight)
            {
                ball.Position.Y = screenHeight - radius; // Prevent ball from moving below the bottom
                ball.Velocity.Y *= -1; // Reverse vertical direction upon hitting the bottom
            }
        }

        // Method to draw a ball on the canvas
        static void RenderBall(CDrawer canvas, Ball ball)
        {
            canvas.AddCenteredEllipse(ball.Position.X, ball.Position.Y, (int)ball.Size, (int)ball.Size, ball.Color);
        }

        // Method to update and render all balls on the canvas
        static void UpdateAndRenderAllBalls(CDrawer canvas, Ball[] balls, int count, int screenWidth, int screenHeight)
        {
            canvas.Clear(); // Clear the canvas before redrawing
            for (int i = 0; i < count; i++)
            {
                MoveBall(ref balls[i], screenWidth, screenHeight); // Move each ball and handle boundaries
                RenderBall(canvas, balls[i]); // Render each ball on the canvas
            }
        }
    }
}

