using System.Collections.Generic;
using UnityEngine;

namespace MotiveStream
{
	[RequireComponent(typeof(MotiveClient))]
	public class MotiveClientTest : MonoBehaviour
	{
		public GameObject rigidBodyPrefab;
		public GameObject bonePrefab;
        public int uselayer = 0; // Specify a layer for created GameObjects (if you need to).

        private MotiveClient _motiveClient;
		private Dictionary<int, GameObject> _debugRigidBodies;
		private Dictionary<int, GameObject> _debugBones;

		// Use this for initialization
		void Awake()
		{
			_debugRigidBodies = new Dictionary<int, GameObject>();
			_debugBones = new Dictionary<int, GameObject>();

			_motiveClient = GetComponent<MotiveClient>();
        }

        void OnEnable()
        {
            _motiveClient.NewFrameReceived += OnNewFrameReceived;
        }

        void OnDisable()
        {
            _motiveClient.NewFrameReceived -= OnNewFrameReceived;
        }

		void OnNewFrameReceived(object sender, FrameDataEventArgs args)
		{
			FrameData newFrame = args.FrameData;
			//Debug.Log("New Frame received : " + newFrame.FrameNum);

			HashSet<int> toRemove = new HashSet<int>();
 
			// Update RigidBodies
			//// Update RigidBodies if needed
            foreach (var rb in newFrame.RigidBodies)
            {
				if (!_debugRigidBodies.ContainsKey(rb.Key))
				{
					GameObject newRb = GameObject.Instantiate(rigidBodyPrefab);
					newRb.name = rb.Value.Name;
                    newRb.layer = uselayer;
					_debugRigidBodies.Add(rb.Key, newRb);
				}
				GameObject debugRb = _debugRigidBodies[rb.Key];
				debugRb.transform.position = rb.Value.Position;
				debugRb.transform.rotation = rb.Value.Orientation;
			}
			//// Destroy RigidBodies if needed
			foreach(var debugRb in _debugRigidBodies)
			{
				if(!newFrame.RigidBodies.ContainsKey(debugRb.Key))
				{
					GameObject.Destroy(debugRb.Value);
					toRemove.Add(debugRb.Key);
				}
			}
			foreach(int key in toRemove)
			{
				_debugRigidBodies.Remove(key);
            }
			toRemove.Clear();

			// Update Bones
			//// Update Bones if needed
			foreach (var bone in newFrame.Bones)
			{
				if (!_debugBones.ContainsKey(bone.Key))
				{
					GameObject newBone = GameObject.Instantiate(bonePrefab);
					newBone.name = bone.Value.Name;
                    newBone.layer = uselayer;
                    _debugBones.Add(bone.Key, newBone);
				}
				GameObject debugBone = _debugBones[bone.Key];
				debugBone.transform.position = bone.Value.Position;
				debugBone.transform.rotation = bone.Value.Orientation;
			}
			//// Destroy Bones if needed
			foreach (var debugBone in _debugBones)
			{
				if (!newFrame.Bones.ContainsKey(debugBone.Key))
				{
					GameObject.Destroy(debugBone.Value);
					toRemove.Add(debugBone.Key);
				}
			}
			foreach(int key in toRemove)
			{
				_debugBones.Remove(key);
			}
		}
	}
}