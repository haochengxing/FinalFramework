using FirCommon.Data;
using FirCommon.Utility;
using FirServer.Model;
using GameLibs.FirSango.Defines;
using MongoDB.Bson.Serialization;
using System.Collections.Generic;

namespace GameLibs.FirSango.Model
{
    public class HeroModel : BaseModel
    {
        public HeroModel() : base("Hero")
        {
            BsonClassMap.RegisterClassMap<Hero>();
        }

        /// <summary>
        /// 添加卡牌
        /// </summary>
        public long AddHero(Hero hero)
        {
            hero.hero_id = AppUtil.NewGuidId();
            if (!Add<Hero>(hero))
            {
                return 0L;
            }
            return hero.hero_id;
        }

        /// <summary>
        /// 添加卡牌
        /// </summary>
        public long AddHero(long userid,int entry)
        {
            Hero hero = new Hero();

            hero.userid = userid;

            hero.entry = entry;

            hero.skills = new List<Skill>();

            hero.skills.Add(new Skill { entry=1,level=1 });

            return AddHero(hero);
        }

        public void AddHeroList(long userid)
        {
            for (int i = 0; i < TableManager.Create().npcTable.Items.Count; i++)
            {
                NpcTableItem heroTable = TableManager.Create().npcTable.Items[i];

                AddHero(userid,heroTable.id);
            }
        }


        public List<Hero> GetHeroList(long userid)
        {
            var result = Query<Hero>(u => u.userid == userid);
            return result;
        }

    }
}
