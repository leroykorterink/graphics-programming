using System;
using System.ComponentModel;
using System.Drawing;
using System.Dynamic;
using System.Windows.Forms;

namespace graphics_programming
{
    class CubeControlValues {
        public float CameraTheta = 25;
        public float CameraPhi = 40;
        public float CameraDistance = 800;

        // Cube properties
        public float X = 0;
        public float Y = 0;
        public float Z = 0;

        public float ThetaX = 0;
        public float ThetaY = 0;
        public float ThetaZ = 0;

        public float Scale = 0;

        public bool IsOrthogonal = false;
    }

    /// <summary>
    /// Starting parameters
    /// - d = 800
    /// - r = 10
    /// - theta = -100
    /// - phi = -10
    /// 
    /// Handle keys
    /// - Cursor keys: change x/y [Done]
    /// - PgUp / PgDn : decrease / increase z [Done]
    /// - x/X, y/Y, z/Z : rotate around x-axis, y-axis, z-axis [Done]
    /// - s/S : scale(increase / decrease) [Done]
    /// 
    /// All stepsizes are 0.1
    /// 
    /// When “C” is pressed, all variables are reset to default
    /// 
    /// When “A” is pressed, start animation
    ///     Phases
    ///     - Phase 1: Scale until 1.5x and shrink(stepsize 0.01)
    ///     - Phase 2: Rotate 45° over X-axis and back
    ///     - Phase 3: Rotate 45° over Y-axis and back
    ///     - During phase 1-2, decrease theta
    ///     - During phase 3, increase phi
    ///     - After phase 3, increase theta and decrease phi until starting values. Then start with phase 1
    ///     
    ///     If no stepsize is mentioned, use stepsize 1
    /// 
    /// Hints to get more insight:
    /// - Display all parameters in the window
    /// - Make more events to change all parameters manually (e.g.r/R for r, t/T for theta, etc.)
    /// </summary>
    class CubeControls
    {
        public CubeControlValues Values = new CubeControlValues();

        public delegate void OnChangeHandler();
        public OnChangeHandler OnChange;

        Brush _brush = new SolidBrush(Color.Black);
        Font _font = new Font("Arial", 12, FontStyle.Bold);
        Point _position = new Point(0, 0);

        public void KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                // Translate
                case Keys.Right:
                    Values.X -= .1F;
                    break;

                case Keys.Left:
                    Values.X += .1F;
                    break;

                case Keys.Up:
                    Values.Y += .1F;
                    break;

                case Keys.Down:
                    Values.Y -= .1F;
                    break;

                case Keys.PageUp:
                    Values.Z += .1F;
                    break;

                case Keys.PageDown:
                    Values.Z -= .1F;
                    break;

                // Rotate
                case Keys.X:
                    Values.ThetaX += e.Shift ? -1F : 1F;
                    break;

                case Keys.Y:
                    Values.ThetaY += e.Shift ? -1F : 1F;
                    break;

                case Keys.Z:
                    Values.ThetaZ += e.Shift ? -1F : 1F;
                    break;

                // Scale
                case Keys.S:
                    Values.Scale += e.Shift ? -1F : 1F;
                    break;

                case Keys.O:
                    Values.IsOrthogonal = !Values.IsOrthogonal;
                    break;

                case Keys.C:
                    Values = new CubeControlValues();
                    break;

                default:
                    Console.WriteLine("Invalid command");
                break;
            }

            OnChange();
        }

        public void Draw(Graphics graphics)
        {
            graphics.DrawString(
                $"Distance: {Values.CameraDistance} \n\n\n" +
                $"X: {Values.X} \n" +
                $"Y: {Values.Y} \n" +
                $"Z: {Values.Z} \n\n" +
                $"ThetaX: {Values.ThetaX} \n" +
                $"ThetaY: {Values.ThetaY} \n" +
                $"ThetaZ: {Values.ThetaZ} \n",
                _font, 
                _brush, 
                _position
            );
        }
    }
}
