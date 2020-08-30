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

        private vec3 up;
        private vec3 front;
        private vec3 right;
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
            cameraFront = new vec3(0f, 0f, 1f);
            upVector = new vec3(0f, 1f, 0f);
            cameraFov = 60.0;
            aspect = 1.0;
            zNear = 0.0;
            zFar = 20.0;
            
            orbitTarget = new vec3();
        }

        public void CameraZoom(int zoomValue)
        {
            // Get zoom value.
            float value = (float)(zoomValue * zoomSensitivity);
            // Normalize the front vector
            cameraFront = glm.normalize(cameraFront);
            // Change the camera position
            cameraPos.x += value * cameraFront.x;
            cameraPos.y += value * cameraFront.y;
            cameraPos.z += value * cameraFront.z;
        }

        public void CameraStartingPan()
        {
            up = glm.normalize(upVector);
            front = glm.normalize(cameraFront); // front direction
            right = glm.cross(up, front); // right direction
                                               
        }

        public void CameraPan(Point curPt, Point prePt)
        {
            double xDev = (curPt.X - prePt.X) * panSensitivity;
            double yDev = (curPt.Y - prePt.Y) * panSensitivity;

            // Move x deviation along right direction, move y deviation along up direction. 
            cameraPos.x -= (float)(up.x * yDev + right.x * xDev);
            cameraPos.y -= (float)(up.y * yDev + right.y * xDev);
            cameraPos.z -= (float)(up.z * yDev + right.z * xDev);
            //UpdateBackgroundPts();
        }

        public void CameraOrbit(Point curPt, Point prePt)
        {

        }
    }
}
