using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using GlmNet;

namespace SeaTeaDisplay
{
    class ViewerCamera
    {
        public vec3 cameraPos;
        public vec3 cameraFront;
        public vec3 upVector;
        public double cameraFov;
        public double aspect;
        public double zNear;
        public double zFar;

        private vec3 orbitFront;
        private vec3 orbitUp;
        private vec3 orbitRight;
        private vec3 orbitTarget;
        private double frontLength;
        private double orbitPitch;
        private double orbitYaw;

        private const double zoomSensitivity = 0.1;
        private const double panSensitivity = 200.0;
        private const double orbitSensitivity = 10.0;

        public ViewerCamera()
        {
            cameraPos = new vec3(0f, 0f, -100f);
            cameraFront = new vec3(0f, 0f, 100f);
            upVector = new vec3(0f, 1f, 0f);
            cameraFov = 60.0;
            aspect = 1.0;
            zNear = 0.0;
            zFar = 20.0;
            orbitFront = new vec3();
            orbitUp = new vec3();
            orbitRight = new vec3();
            orbitTarget = new vec3();
        }

        public void CameraZoom(int zoomValue)
        {
            float value = (float)(zoomValue * zoomSensitivity);
            // Change the camera fov, the fov is between 1 degree and 170 degree
            // Not a correct calculation, test for now
            cameraPos.z += value;
            cameraFront.z -= value;
        }

        public void CameraPan(Point curPt, Point prePt)
        {
            double xDev = (curPt.X - prePt.X) * panSensitivity;
            double yDev = (curPt.Y - prePt.Y) * panSensitivity;

            vec3 up = glm.normalize(upVector);
            vec3 front = glm.normalize(cameraFront); // front direction
            vec3 right = glm.cross(up, front); // right direction
                                               // Move x deviation along right direction, move y deviation along up direction. 
            cameraPos.x -= (float)(up.x * yDev + right.x * xDev);
            cameraPos.y -= (float)(up.y * yDev + right.y * xDev);
            cameraPos.z -= (float)(up.z * yDev + right.z * xDev);
            //UpdateBackgroundPts();
        }
    }
}
