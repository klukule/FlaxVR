﻿using FlaxEngine;
using System;

namespace FlaxVR
{
    public struct HmdPoseState // TODO: Add readonly when switch to C# 7.2
    {
        public readonly Quaternion DeviceRotation;
        public readonly Vector3 DevicePosition;
        public readonly Matrix LeftEyeProjection;
        public readonly Matrix RightEyeProjection;
        public readonly Vector3 LeftEyePosition;
        public readonly Vector3 RightEyePosition;
        public readonly Quaternion LeftEyeRotation;
        public readonly Quaternion RightEyeRotation;
        public HmdPoseState(
            Vector3 devicePosition,
            Quaternion deviceRotation,
            Matrix leftEyeProjection,
            Matrix rightEyeProjection,
            Vector3 leftEyePosition,
            Vector3 rightEyePosition,
            Quaternion leftEyeRotation,
            Quaternion rightEyeRotation)
        {
            DeviceRotation = deviceRotation;
            DevicePosition = devicePosition;
            LeftEyeProjection = leftEyeProjection;
            RightEyeProjection = rightEyeProjection;
            LeftEyePosition = leftEyePosition;
            RightEyePosition = rightEyePosition;
            LeftEyeRotation = leftEyeRotation;
            RightEyeRotation = rightEyeRotation;
        }

        public Vector3 GetEyePosition(VREye eye)
        {
            switch (eye)
            {
            case VREye.Left: return LeftEyePosition;
            case VREye.Right: return RightEyePosition;
            default: throw new Exception($"Invalid {nameof(VREye)}: {eye}.");
            }
        }

        public Quaternion GetEyeRotation(VREye eye)
        {
            switch (eye)
            {
            case VREye.Left: return LeftEyeRotation;
            case VREye.Right: return RightEyeRotation;
            default: throw new Exception($"Invalid {nameof(VREye)}: {eye}.");
            }
        }

        public Matrix CreateView(VREye eye, Vector3 positionOffset, Vector3 forward, Vector3 up, out Vector3 eyePos, out Vector3 target)
        {
            eyePos = GetEyePosition(eye) + positionOffset;
            Quaternion eyeQuat = GetEyeRotation(eye);
            Vector3 forwardTransformed = Vector3.Transform(forward, eyeQuat);
            Vector3 upTransformed = Vector3.Transform(up, eyeQuat);
            target = eyePos + forwardTransformed;
            return Matrix.LookAt(eyePos, target, upTransformed);
        }
    }
}
