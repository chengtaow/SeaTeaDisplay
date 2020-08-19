using SeaTeaImageApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using SharpGL;
using SharpGL.WPF;
using SharpGL.SceneGraph.Primitives;
using GlmNet;
using Microsoft.Win32;

namespace SeaTeaDisplay
{
    class MainWindowModel : WithNotification
    {
        private DataModel dataModel;
        private OpenGL gl;
        public MainWindowModel()
        {
            dataModel = new DataModel();
            _OpenGLInitializeCommand = new DelegateCommand(OpenGLInitialize, CanExecute);
            _OpenGLDrawCommand = new DelegateCommand(OpenGLDraw, CanExecute);
            _ImportCommand = new DelegateCommand(ImportPointClouds, CanExecute);
            BottomMessage = "SeaTea Display initialized.";
        }

        private void OpenGLInitialize(object param)
        {
            gl = (OpenGL)param;
            gl.Enable(OpenGL.GL_DEPTH_TEST);

            float[] global_ambient = new float[] { 0.5f, 0.5f, 0.5f, 1.0f };
            float[] light0pos = new float[] { 0.0f, 5.0f, 10.0f, 1.0f };
            float[] light0ambient = new float[] { 0.2f, 0.2f, 0.2f, 1.0f };
            float[] light0diffuse = new float[] { 0.3f, 0.3f, 0.3f, 1.0f };
            float[] light0specular = new float[] { 0.8f, 0.8f, 0.8f, 1.0f };

            float[] lmodel_ambient = new float[] { 0.2f, 0.2f, 0.2f, 1.0f };
            gl.LightModel(OpenGL.GL_LIGHT_MODEL_AMBIENT, lmodel_ambient);

            gl.LightModel(OpenGL.GL_LIGHT_MODEL_AMBIENT, global_ambient);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, light0pos);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_AMBIENT, light0ambient);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_DIFFUSE, light0diffuse);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_SPECULAR, light0specular);
            gl.Enable(OpenGL.GL_LIGHTING);
            gl.Enable(OpenGL.GL_LIGHT0);

            gl.ShadeModel(OpenGL.GL_SMOOTH);
        }

        private void OpenGLDraw(object param)
        {
            // Clear The Screen And The Depth Buffer
            gl.ClearColor(0.5f, 1f, 1f, 1f); // Set the background color
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            // Move Left And Into The Screen
            gl.LoadIdentity();

            gl.PointSize(2f);

            List <vec3> points = dataModel.GetPointCloud();
            if (points != null)
            {
                gl.Begin(SharpGL.OpenGL.GL_LINES);
                // Draw coordinates
                foreach (var pt in points)
                {
                    gl.Vertex(pt.x, pt.y, pt.z);
                }
                gl.End();
            }
        }

        /// <summary>
        /// Import point clouds
        /// </summary>
        /// <param name="param"></param>
        public void ImportPointClouds(object param)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Title = "Import Point Cloud Files";
            openDlg.Filter = "Text File|*.txt|XYZ File|*.xyz|All Files|*.*";
            openDlg.RestoreDirectory = true;
            if (openDlg.ShowDialog() == true)
            {
                int ptNum = dataModel.ImportPointCloudFile(openDlg.FileName);
                BottomMessage = string.Format("{0} points imported from file: {1}.", ptNum, openDlg.FileName);
            }
        }

        private bool CanExecute(object param)
        {
            return true;
        }

        #region Commands
        private DelegateCommand _ImportCommand;
        public ICommand ImportCommand
        {
            get { return _ImportCommand; }
        }

        private DelegateCommand _OpenGLDrawCommand;
        public ICommand OpenGLDrawCommand
        {
            get { return _OpenGLDrawCommand; }
        }

        private DelegateCommand _OpenGLInitializeCommand;
        public ICommand OpenGLInitializeCommand
        {
            get { return _OpenGLInitializeCommand; }
        }
        #endregion

        #region Attritubes
        private string _BottomMessage;
        public string BottomMessage
        {
            get { return _BottomMessage; }
            set
            {
                _BottomMessage = value;
                RaisedPropertyChanged(nameof(BottomMessage));
            }
        }
        #endregion
    }
}
