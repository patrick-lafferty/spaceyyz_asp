using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceYYZ.Models
{
	public class Performance
   	{
		[Key]
		[ForeignKey("Engine")]
		public int EngineId {get; set;}

		public float Isp {get; set;}
		public float Thrust {get; set;}
	}

	public class Engine
	{
		public int ID {get; set;}

		[MaxLength(50)]
		[Required]
		public string Name {get; set;}

		[Required]
		public Performance SeaLevel {get; set;}

		[Required]
		public Performance Vacuum {get; set;}

	}
}
