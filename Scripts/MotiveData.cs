/******************************************************************************************************************************************************
* MIT License																																		  *
*																																					  *
* Copyright (c) 2015																																  *
* Emmanuel Badier <emmanuel.badier@gmail.com>																										  *
* 																																					  *
* Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"),  *
* to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,  *
* and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:          *
* 																																					  *
* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.					  *
* 																																					  *
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, *
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. 																							  *
* IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, 		  *
* TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.							  *
******************************************************************************************************************************************************/

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