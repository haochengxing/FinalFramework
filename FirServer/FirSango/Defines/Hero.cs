using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace GameLibs.FirSango.Defines
{
	public class Skill {
		public int entry { get; set; }
		public int level { get; set; }
	}

	public class Hero
    {
        [BsonId]
        public ObjectId Id { get; set; }
		public long hero_id { get; set; }
		public int entry { get; set; }
		public int level { get; set; }
		public int exp { get; set; }
		public int attack { get; set; }
		public int defense { get; set; }
		public int speed { get; set; }
		public int intelligence { get; set; }
		public int free_point { get; set; }
		public int cost { get; set; }
		public int star_level { get; set; }
		public bool wakeup_state { get; set; }
		public bool lock_state { get; set; }
		public List<Skill> skills { get; set; }
	}
}
