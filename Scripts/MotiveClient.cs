﻿/******************************************************************************************************************************************************
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

using System;
using System.Threading;
using UnityEngine;

namespace MotiveStream
{
	public class FrameDataEventArgs : EventArgs
	{
		public FrameData FrameData { get; private set; }

		public FrameDataEventArgs(FrameData pFrame)
		{
			FrameData = pFrame;
		}
	}

	public class MotiveClient : MonoBehaviour
	{
		//public int SleepTime = 10;
		private FrameData _frame, _lastFrame;
		private SlipStream _client;
		private Thread _thread;
		private object _mutex;
		private volatile bool _receive = false;

		public event EventHandler<FrameDataEventArgs> NewFrameReceived;

		#region Private Methods
		private void _ReceptionThread()
		{
			_client = new SlipStream();
			_client.Connect();
			_receive = true;

			while (_receive && (_client != null))
			{
				if(_client.Connected)
				{
					//Debug.Log("Client Connected !");

					if (_client.GetLastFrame())
					{
						lock (_mutex)
						{
							// Copy to avoid thread concurrency.
							_lastFrame = new FrameData(_client.LastFrame);
						}
					}
					//if (SleepTime > 0)
					//{
					//	Thread.Sleep(SleepTime);
					//}
				}
			}
		}
		#endregion

		#region Public Methods
		// Use this for initialization
		void Start()
		{
			try
			{
				_receive = false;
				_frame = new FrameData();
				_lastFrame = new FrameData();

				_mutex = new object();
				_thread = new Thread(new ThreadStart(_ReceptionThread));
				_thread.Start();
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.Message);
			}
		}

		void OnDestroy()
		{
			try
			{
				//Debug.Log("Killing Thread...");
				_receive = false;
				if(_thread != null)
				{
					//Debug.Log("Thread Join !");
					if(!_thread.Join(100))
					{
						_thread.Interrupt();
						//Debug.Log("Thread Interrupted !");
					}
				}
				//Debug.Log("Thread Killed !");

				//Debug.Log("Closing Client...");
				if (_client != null)
				{
					_client.Close();
					//Debug.Log("Client Closed !");
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.Message);
			}
		}

		// Update is called once per frame
		void Update()
		{
			if ( (_client != null) && _client.Connected)
			{
				//Debug.Log("Client connected !");
				lock (_mutex)
				{
					_frame = _lastFrame;
                }

				if (NewFrameReceived != null)
				{
					NewFrameReceived(this, new FrameDataEventArgs(_frame));
				}
			}
			//else
			//{
			//    Debug.Log("Client Disconnected !");
			//}
		}
		#endregion
	}
}