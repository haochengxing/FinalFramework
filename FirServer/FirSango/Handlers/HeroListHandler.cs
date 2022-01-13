using log4net;
using FirServer.Handler;
using FirServer;
using GameLibs.FirSango.Defines;
using GameLibs.FirSango.Model;
using PbUser;
using FirCommon.Utility;
using FirCommon.Define;
using FirCommon.Data;
using System;
using System.Collections.Generic;
using Google.Protobuf.Collections;

namespace GameLibs.FirSango.Handlers
{
    public class HeroListHandler : BaseHandler
    {
        private static readonly ILog logger = LogManager.GetLogger(AppServer.repository.Name, typeof(HeroListHandler));

        public override void OnMessage(ClientPeer peer, byte[] bytes)
        {
            ReqHeroList req = ReqHeroList.Parser.ParseFrom(bytes);

            logger.Info("Get HeroList userid : " + req.Userid);

            ResHeroList res = new ResHeroList();

            //res.Result = PbCommon.ResultCode.Failed;

            HeroModel heroModel = modelMgr.GetModel(ModelNames.Hero) as HeroModel;

            List<Hero> heroList = heroModel?.GetHeroList(req.Userid);

            if (heroList!=null)
            {
                res.Result = PbCommon.ResultCode.Success;

                for (int i = 0; i < heroList.Count; i++)
                {
                    Hero hero = heroList[i];

                    PbCommon.Hero heroData = new PbCommon.Hero
                    {
                        HeroId = hero.hero_id,
                        Entry = hero.entry,
                        Level = hero.level,
                        Exp = hero.exp,
                        Attack = hero.attack,
                        Defense = hero.defense,
                        Speed = hero.speed,
                        Intelligence = hero.intelligence,
                        FreePoint = hero.free_point,
                        Cost = hero.cost,
                        StarLevel = hero.star_level,
                        WakeupState = hero.wakeup_state,
                        LockState = hero.lock_state,
                    };

                    for (int j = 0; j < hero.skills.Count; j++)
                    {
                        Skill skill = hero.skills[j];
                        heroData.Skills.Add(new PbCommon.Skill
                        {
                            Entry = skill.entry,
                            Level = skill.level
                        });
                    }

                    res.HeroList.Add(heroData); 
                }

                logger.Info("返回卡牌个数 : " + res.HeroList.Count);
            }

            netMgr.SendData(peer, ProtoType.LuaProtoMsg, Protocal.ResHeroList, res);
        }
    }
}
