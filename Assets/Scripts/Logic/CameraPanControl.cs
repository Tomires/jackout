using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout.Logic {
    public class CameraPanControl : MonoBehaviour
    {
        public Shared.Axis rotationAxis;
        public float minRotation, maxRotation;
        public float minZoom, maxZoom;
        public Camera cameraComponent;

        public void Zoom(float factor) {
            float proposedZoom = Shared.Map(cameraComponent.fieldOfView + -factor * 1.0f, maxZoom, minZoom);
            cameraComponent.fieldOfView = proposedZoom;
        }

        public void Pan(float factor) {
            switch(rotationAxis) {
                case Shared.Axis.X:
                    break;
                case Shared.Axis.Y:
                    float proposedRot = transform.rotation.eulerAngles.y;
                    proposedRot = Shared.Map(proposedRot + factor * 2.0f, minRotation, maxRotation);
                    Quaternion target = Quaternion.Euler(0, proposedRot, 0);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, target, 0.5f);
                    break;
                case Shared.Axis.Z:
                    break;
            }
            
        }
    }
}