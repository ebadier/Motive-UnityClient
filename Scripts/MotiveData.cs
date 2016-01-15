using UnityEngine;
using System.Collections.Generic;

namespace MotiveStream
{
	public class RigidBodyData
	{
		public RigidBodyData(string pName, Vector3 pPosition, Quaternion pOrientation)
		{
			Name = pName;
			Position = pPosition;
			Orientation = pOrientation;
		}

		public string Name { get; set; }
		public Vector3 Position { get; set; }
		public Quaternion Orientation { get; set; }
	}

	public class BoneData
	{
		public BoneData(string pName, Vector3 pPosition, Quaternion pOrientation)
		{
			Name = pName;
			Position = pPosition;
			Orientation = pOrientation;
		}

		public string Name { get; set; }
		public Vector3 Position { get; set; }
		public Quaternion Orientation { get; set; }
	}

	public class FrameData
	{
		public FrameData()
		{
			FrameNum = -1;
			Time = 0.0f;
			RigidBodies = new Dictionary<int, RigidBodyData>();
			Bones = new Dictionary<int, BoneData>();
        }

		public FrameData(FrameData pSrc)
		{
			FrameNum = pSrc.FrameNum;
			Time = pSrc.Time;
			RigidBodies = new Dictionary<int, RigidBodyData>(pSrc.RigidBodies);
			Bones = new Dictionary<int, BoneData>(pSrc.Bones);
		}

		public void Clear()
		{
			FrameNum = -1;
			Time = 0.0f;
			RigidBodies.Clear();
			Bones.Clear();
		}

		public int FrameNum { get; set; }
		public double Time { get; set; }
		public Dictionary<int, RigidBodyData> RigidBodies { get; private set; }
		public Dictionary<int, BoneData> Bones { get; private set; }
	}
}