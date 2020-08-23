using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using SharpGL.WPF;


namespace SeaTeaDisplay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowModel mainWindowModel;
        public MainWindow()
        {
            InitializeComponent();
            mainWindowModel = new MainWindowModel();
            this.DataContext = mainWindowModel;
        }

        /// <summary>
        /// Handles the OpenGLDraw event of the OpenGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="OpenGLRoutedEventArgs"/> instance containing the event data.</param>
        private void OpenGLControl_OpenGLDraw(object sender, OpenGLRoutedEventArgs args)
        {
            mainWindowModel.OpenGLDrawCommand.Execute(args.OpenGL);
        }

        /// <summary>
        /// Handles the OpenGLInitialized event of the OpenGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="OpenGLRoutedEventArgs"/> instance containing the event data.</param>
        private void OpenGLControl_OpenGLInitialized(object sender, OpenGLRoutedEventArgs args)
        {
            mainWindowModel.OpenGLInitializeCommand.Execute(args.OpenGL);
        }

        private void openGLControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            mainWindowModel.ViewerZoomCommand.Execute(e.Delta);
        }

        private void openGLControl_MouseLeave(object sender, MouseEventArgs e)
        {
            mainWindowModel.FinishViewerChangeCommand.Execute(null);
        }

        private void openGLControl_MouseMove(object sender, MouseEventArgs e)
        {
            Point pt = e.GetPosition(openGLControl);
            pt.X = pt.X / openGLControl.ActualWidth;
            pt.Y = pt.Y / openGLControl.ActualHeight;
            mainWindowModel.ViewerChangeCommand.Execute(pt);
        }

        private void openGLControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point pt = e.GetPosition(openGLControl);
            pt.X = pt.X / openGLControl.ActualWidth;
            pt.Y = pt.Y / openGLControl.ActualHeight;
            if (e.ChangedButton == MouseButton.Left)
            {
                mainWindowModel.StartViewerOrbitCommand.Execute(pt);
            }
            else if (e.ChangedButton == MouseButton.Middle)
            {
                mainWindowModel.StartViewerPanCommand.Execute(pt);
            }
        }

        private void openGLControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mainWindowModel.FinishViewerChangeCommand.Execute(null);
        }
    }
}
