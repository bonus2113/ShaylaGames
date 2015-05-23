using UnityEngine;
using System.Collections;

namespace Atomtwist
{
	public static class ATValueSmoother {
		
		static float _velocity = 0.0F;
		static float _smoothedValue;
		public static float SmoothValue(float value, float smoothTime)
		{
			_smoothedValue = Mathf.SmoothDamp(_smoothedValue,value,ref _velocity,smoothTime);
			return _smoothedValue;
		}

		public static float ATSmoothValue(this float value, float smoothTime)
		{
			_smoothedValue = Mathf.SmoothDamp(_smoothedValue,value,ref _velocity,smoothTime);
			return _smoothedValue;
		}
	}
	

}