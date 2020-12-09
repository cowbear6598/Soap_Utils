using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Soap.Utils
{
	public static class SoapUtils
	{
		#region ParticleSystem

		public static void EnableEmission(ParticleSystem particleSystem, bool enabled)
		{
			ParticleSystem.EmissionModule emission = particleSystem.emission;
			emission.enabled = enabled;
		}

		public static float GetEmissionRate(ParticleSystem particleSystem)
		{
			return particleSystem.emission.rateOverTime.constantMax;
		}

		public static void SetEmissionRate(float emissionRate, params ParticleSystem[] particleSystem)
		{
			for (int i = 0; i < particleSystem.Length; i++)
			{
				ParticleSystem.EmissionModule emission = particleSystem[i].emission;
				ParticleSystem.MinMaxCurve rate = emission.rateOverTime.constantMax;
				rate.constantMax = emissionRate;
				emission.rateOverTime = rate;
			}
		}

		#endregion

		#region Hash

		public static string EncodeToSha256(string data)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(data);
			byte[] hash = SHA256Managed.Create().ComputeHash(bytes);

			StringBuilder builder = new StringBuilder();
			for (int i = 0; i < hash.Length; i++)
			{
				builder.Append(hash[i].ToString("X2"));
			}

			return builder.ToString();
		}

		public static string EncodeToHMAC_SHA1(string input, byte[] key)
		{
			HMACSHA1 myhmacsha1 = new HMACSHA1(key);
			byte[] byteArray = Encoding.ASCII.GetBytes(input);
			MemoryStream stream = new MemoryStream(byteArray);
			return myhmacsha1.ComputeHash(stream).Aggregate("", (s, e) => s + String.Format("{0:x2}", e), s => s);
		}

		#endregion

		#region UI

		public static void SetCanvasGroup(CanvasGroup[] _canvasGroups, int _index)
		{
			for (int i = 0; i < _canvasGroups.Length; i++)
			{
				_canvasGroups[i].alpha = (i == _index) ? 1 : 0;
				_canvasGroups[i].interactable = (i == _index);
				_canvasGroups[i].blocksRaycasts = (i == _index);
			}
		}

		public static void SetCanvasGroup(CanvasGroup _canvasGroup, bool _IsEnable)
		{
			_canvasGroup.alpha = _IsEnable ? 1 : 0;
			_canvasGroup.interactable = _IsEnable;
			_canvasGroup.blocksRaycasts = _IsEnable;
		}

		public static void SetColorAlpha(Graphic _graphic, float _alpha)
		{
			Color _color = _graphic.color;
			_color.a = _alpha;
			_graphic.color = _color;
		}

		#endregion

		public static Vector3 CalculateParabolaWithTwoDots(Vector3 _pos, Vector3 _targetPos, float _height, float _gravity, bool _drawPath = false)
		{
			// 計算位移量
			float displacementY = _targetPos.y - _pos.y;
			Vector3 displacementXZ = new Vector3(_targetPos.x - _pos.x, 0, _targetPos.z - _pos.z);

			// 計算時間
			float time = (Mathf.Sqrt(-2 * _height / _gravity) + Mathf.Sqrt(2 * (displacementY - _height) / _gravity));
			
			// 計算速度
			Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * _gravity * _height);
			Vector3 velocityXZ = displacementXZ / time;

			Vector3 initVelocity = velocityY * -Mathf.Sign(_gravity) + velocityXZ;
			
			if (_drawPath)
			{
				int resolution = 30;

				Vector3 previousDrawPoint = _pos;
				
				for (int i = 0; i < resolution; i++)
				{
					float simulationTime = i / (float) resolution * time;
					Vector3 displacement = initVelocity * simulationTime + Vector3.up * (_gravity * simulationTime * simulationTime) / 2;
					Vector3 drawPoint = _pos + displacement;

					Debug.DrawLine(previousDrawPoint, drawPoint, Color.green, 10);
					previousDrawPoint = drawPoint;
				}
			}
			
			return initVelocity;
		}

		public static Vector3[] GetTrajectoryPoints(Vector3 _startPos, Vector2 _velocity,float _gravity, float _time, int _resolution)
		{
			// 註解為分解詳細版
			
			Vector3[] outputPos = new Vector3[_resolution];
			outputPos[0] = _startPos;
			
			// 算發射的力道（x力、y力相等 _velocity）
			// float force = Mathf.Sqrt(_velocity.x * _velocity.x + _velocity.y * _velocity.y);
			
			// 算發射角度
			// float angle = Mathf.Atan2(_velocity.y, _velocity.x) * Mathf.Rad2Deg;

			for (int i = 1; i < _resolution; i++)
			{
				float simulationTime = i / (float)_resolution * _time;

				Vector3 displacement = _velocity * simulationTime + Vector2.up * (_gravity * simulationTime * simulationTime) / 2;

				outputPos[i] = _startPos + displacement;

				// s = v0t + at^2/2、Fx = F * cos、Fy = F * sin
				// 計算分力時，把角度換成弧度（相同十進制好算）
				// float displacementX = force * Mathf.Cos(angle * Mathf.Deg2Rad) * simulationTime;
				// float displacementY = force * Mathf.Sin(angle * Mathf.Deg2Rad) * simulationTime + _gravity * simulationTime * simulationTime / 2;

				// outputPos[i] = new Vector3(_startPos.x + displacementX, _startPos.y + displacementY, 0);
			}

			return outputPos;
		}
		
		public static Vector3 FindClosetPointOnLine(Vector3 _linePoint1, Vector3 _linePoint2, Vector3 _pos)
		{
			Vector3 lineVector = _linePoint2 - _linePoint1;

			float lineVectorSqrMag = lineVector.sqrMagnitude;

			float dot = Vector3.Dot(lineVector, _linePoint1 - _pos);
			float t = -dot / lineVectorSqrMag;

			return _linePoint1 + Mathf.Clamp01(t) * lineVector;
		}

		public static Vector2 FindClosetPointOnLine(Vector2 _linePoint1, Vector2 _linePoint2, Vector2 _pos)
		{
			Vector2 lineVector = _linePoint2 - _linePoint1;

			float lineVectorSqrMag = lineVector.sqrMagnitude;

			float dot = Vector2.Dot(lineVector, _linePoint1 - _pos);
			float t = -dot / lineVectorSqrMag;

			return _linePoint1 + Mathf.Clamp01(t) * lineVector;
		}
	}
}