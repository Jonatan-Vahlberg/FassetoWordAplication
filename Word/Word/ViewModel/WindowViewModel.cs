using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Word
{
    /// <summary>
    /// ViewModel for flast custom window
    /// </summary>
    class WindowViewModel : baseViewModel
    {
        #region private members
        /// <summary>
        /// The Window This model controlls
        /// </summary>
        private Window mWindow;

        /// <summary>
        /// Outer window margin
        /// </summary>
        private int mOuterMarginSize = 10;

        /// <summary>
        /// Border corner Radius
        /// </summary>
        private int mWindowRadius = 10;
        #endregion

        #region public Members
        /// <summary>
        /// Window Minimum Width
        /// </summary>
        public double WindowMinWidth { get; set; } = 400;
        /// <summary>
        /// Window Minimum height
        /// </summary>
        public double WindowMinHeight { get; set; } = 400;

        /// <summary>
        /// The size of the window border for size handles
        /// </summary>
        public int ResizeBorder { get; set; } = 6;


        /// <summary>
        /// Size of the resize border around the window taking into acount the margin
        /// </summary>
        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder + OutermarginSize); } }

        /// <summary>
        /// The padding of the inner content of main window
        /// </summary>
        public Thickness InnerContentPadding { get { return new Thickness(ResizeBorder); } }

        /// <summary>
        /// The marin around the window for a drop shadow
        /// </summary>
        public int OutermarginSize
        {
            get
            {
                return mWindow.WindowState == WindowState.Maximized ? 0 : mOuterMarginSize;

            }
            set
            {
                mOuterMarginSize = value;
            }
        }

        /// <summary>
        /// The marin around the window for a drop shadow
        /// </summary>
        public Thickness OutermarginSizeThickness { get { return new Thickness(OutermarginSize); } }
        
        /// <summary>
        /// The radius around the corners
        /// </summary>
        public int WindowRadius
        {
            get
            {
                return mWindow.WindowState == WindowState.Maximized ? 0 : mWindowRadius;

            }
            set
            {
                mWindowRadius = value;
            }
        }

        /// <summary>
        /// The Corner Radius for the window
        /// </summary>
        public CornerRadius WindowCornerRadius { get { return new CornerRadius(WindowRadius); } }

        /// <summary>
        /// The HEight of the Title bar of window
        /// </summary>
        public int TitleHeight { get; set; } = 42;

        /// <summary>
        /// The HEight of the Title bar of window
        /// </summary>
        public GridLength TitleHeightGridLength { get { return new GridLength(TitleHeight + ResizeBorder); } }


        #endregion

        #region Commands
        /// <summary>
        /// Comand for window has been minimized
        /// </summary>
        public ICommand MinimizeCommand { get; set; }

        /// <summary>
        /// Comand for window has been Maximized
        /// </summary>
        public ICommand MaximizeCommand { get; set; }
        
        /// <summary>
        /// Comand for window has been Closed
        /// </summary>
        public ICommand CloseCommand { get; set; }

        /// <summary>
        /// Comand for System Icon Button pressed
        /// </summary>
        public ICommand MenuCommand { get; set; }
        #endregion

        #region constructor
        /// <summary>
        /// basic Constructor for custom window model
        /// </summary>
        /// <param name="window"></param>
        public WindowViewModel(Window window)
        {
            mWindow = window;

            mWindow.StateChanged += (sender, e) =>
            {
                //fire off all events afected by resiszing window
                OnPropertyChanged(nameof(ResizeBorderThickness));
                OnPropertyChanged(nameof(OutermarginSize));
                OnPropertyChanged(nameof(OutermarginSizeThickness));
                OnPropertyChanged(nameof(WindowRadius));
                OnPropertyChanged(nameof(WindowCornerRadius));
            };

            //CREATE COMMANDS
            MinimizeCommand = new relayCommand(() => mWindow.WindowState = WindowState.Minimized);

            //XOR FOR SWITCHING ENUM VALUE 0 => 2 or 2 => 0
            MaximizeCommand = new relayCommand(() => mWindow.WindowState ^= WindowState.Maximized);

            CloseCommand = new relayCommand(() => mWindow.Close());


            MenuCommand = new relayCommand(() => SystemCommands.ShowSystemMenu(mWindow,GetMousePosition()));


            //Fix Window Resize issue
            var Resizer = new WindowResizer(mWindow);

        }
        #endregion

        #region Helpers
        /// <summary>
        /// Gets the current mouse position on the screen
        /// </summary>
        /// <returns></returns>
        private Point GetMousePosition()
        {
            // Position of the mouse relative to the window
            var position = Mouse.GetPosition(mWindow);

            // Add the window position so its a "ToScreen"
            return new Point(position.X + mWindow.Left, position.Y + mWindow.Top);
        }

        #endregion
    }
}
