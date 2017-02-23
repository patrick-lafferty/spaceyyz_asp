using System;

namespace SpaceYYZ.Models.DesignViewModels
{

	public class Performance
	{
		public float Thrust {get; set;}
		public float Isp {get ;set;}
	}

	public class EngineViewModel
	{
		public string Name {get; set;}
		public Performance SeaLevel {get; set;}
		public Performance Vacuum {get; set;}

	}
}
