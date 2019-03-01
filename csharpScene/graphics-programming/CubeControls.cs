using System;
using System.ComponentModel;
using System.Drawing;
using System.Dynamic;
using System.Windows.Forms;

namespace graphics_programming
{
    class CubeControlValues {
        // Rendering properties
        public bool IsOrthogonal = false;

        // Camera properties
        public float CameraR = 10;            // R / r
        public float CameraDistance = 800;    // D / d
        public float CameraThetaX = -10;         // P / p
        public float CameraThetaY = -100;      // T / t

        // Cube properties
        public float CubeScale = 1;           // S / s

        public float CubeX = 0;               // X / x
        public float CubeY = 0;               // X / x
        public float CubeZ = 0;               // X / x

        public float CubeThetaX = 0;          // ? / ?
        public float CubeThetaY = 0;          // ? / ?
        public float CubeThetaZ = 0;          // ? / ?
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

        private readonly Brush _brush = new SolidBrush(Color.Black);
        private readonly Font _font = new Font("Arial", 12, FontStyle.Bold);
        private readonly Point _position = new Point(0, 0);

        private readonly Timer _timer;
        private readonly AnimationControl _animationControl = new AnimationControl();

        public CubeControls()
        {
            _timer = new Timer();
            _timer.Interval = 50;
            _timer.Tick += OnTick;
        }

        private void OnTick(object sender, EventArgs e)
        {
            _animationControl.Update(Values);

            OnChange();
        }

        public void KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                #region Camera properties

                case Keys.R:
                    Values.CameraR += e.Shift ? -1F : 1F;
                    break;

                case Keys.D:
                    Values.CameraDistance += e.Shift ? -5F : 5F;
                    break;

                #endregion

                #region Translate cube

                case Keys.Right:
                    Values.CubeX -= .1F;
                    break;

                case Keys.Left:
                    Values.CubeX += .1F;
                    break;

                case Keys.Up:
                    Values.CubeY += .1F;
                    break;

                case Keys.Down:
                    Values.CubeY -= .1F;
                    break;

                case Keys.PageUp:
                    Values.CubeZ += .1F;
                    break;

                case Keys.PageDown:
                    Values.CubeZ -= .1F;
                    break;

                #endregion

                #region Rotate cube

                case Keys.X:
                    Values.CubeThetaX += e.Shift ? -1F : 1F;
                    break;

                case Keys.Y:
                    Values.CubeThetaY += e.Shift ? -1F : 1F;
                    break;

                case Keys.Z:
                    Values.CubeThetaZ += e.Shift ? -1F : 1F;
                    break;

                #endregion

                #region Scale cube

                // Scale cube
                case Keys.S:
                    Values.CubeScale += e.Shift ? -1F : 1F;
                    break;

                #endregion

                #region Rendering properties

                // Toggle animation
                case Keys.A:
                    _timer.Enabled = !_timer.Enabled;
                    break;

                // Toggle orthogonal rendering
                case Keys.O:
                    Values.IsOrthogonal = !Values.IsOrthogonal;
                    break;

                // Reset values
                case Keys.C:
                    _timer.Enabled = false;
                    Values = new CubeControlValues();
                    break;

                #endregion

                default:
                    Console.WriteLine("Invalid command");
                break;
            }

            OnChange();
        }

        public void Draw(Graphics graphics)
        {
            graphics.DrawString(
                $"IsOrthogonal   = {Values.IsOrthogonal}\n" +
                $"CameraR        = {Values.CameraR}\n" +
                $"CameraDistance = {Values.CameraDistance}\n" +
                $"CameraThetaX   = {Values.CameraThetaX}\n" +
                $"CameraThetaY   = {Values.CameraThetaY}\n" +
                $"CubeScale      = {Values.CubeScale}\n" +
                $"CubeX          = {Values.CubeX}\n" +
                $"CubeY          = {Values.CubeY}\n" +
                $"CubeZ          = {Values.CubeZ}\n" +
                $"CubeThetaX     = {Values.CubeThetaX}\n" +
                $"CubeThetaY     = {Values.CubeThetaY}\n" +
                $"CubeThetaZ     = {Values.CubeThetaZ}\n",
                _font, 
                _brush, 
                _position
            );
        }
    }
}
