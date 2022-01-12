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

namespace GameLibs.FirSango.Handlers
{
    public class LoginHandler : BaseHandler
    {
        private static readonly ILog logger = LogManager.GetLogger(AppServer.repository.Name, typeof(LoginHandler));

        public override void OnMessage(ClientPeer peer, byte[] bytes)
        {
            var person = ReqLogin.Parser.ParseFrom(bytes);

            logger.Info("login user : "+person.Name);

            var resData = new ResLogin();
            resData.Result = PbCommon.ResultCode.Failed;
            var userModel = modelMgr.GetModel(ModelNames.User) as UserModel;
            if (userModel != null)
            {
                long userid = 0L;
                if ((userid = userModel.ExistUser(person.Name)) != 0)
                {
                    resData.Result = PbCommon.ResultCode.Success;
                    UserInfo userInfo = userModel.GetUser(userid);

                    userInfo.lasttime = DateTime.Now.ToShortDateString();

                    userModel.SetLastTime(userid, userInfo.lasttime);

                    resData.Userinfo = new PbCommon.UserInfo {
                        Userid = userInfo.uid.ToString(),
                        Name = userInfo.username,
                        Money = 10000,
                    };
                }
                else
                {
                    var user = new UserInfo()
                    {
                        username = person.Name,
                        money = 10000L,
                        lasttime = DateTime.Now.ToShortDateString()
                    };
                    var uid = userModel.AddUser(user);


                    //var uid = AppUtil.NewGuidId();
                    //var uid = userModel.ExistUser(person.Name, person.Pass);
                    resData.Result = PbCommon.ResultCode.Success;
                    resData.Userinfo = new PbCommon.UserInfo()
                    {
                        Name = person.Name,
                        Money = 10000,
                        Userid = uid.ToString(),
                    };
                }

                
            }
            netMgr.SendData(peer, ProtoType.LuaProtoMsg, Protocal.ResLogin, resData);

            logger.Info(person.Name + " " + person.Pass);
        }
    }
}
