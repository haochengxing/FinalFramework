using FirCommon.Utility;
using FirServer.Model;
using GameLibs.FirSango.Defines;
using MongoDB.Bson.Serialization;

namespace GameLibs.FirSango.Model
{
    public class HeroModel : BaseModel
    {
        public HeroModel() : base("Hero")
        {
            BsonClassMap.RegisterClassMap<Hero>();
        }
    }
}
