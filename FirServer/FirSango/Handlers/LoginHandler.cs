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
                    //返回已用账号

                    resData.Result = PbCommon.ResultCode.Success;
                    UserInfo userInfo = userModel.GetUser(userid);


                    userInfo.lasttime = DateTime.Now.ToString();
                    userInfo.lasttimestamp = DateTimeUtil.DateTimeToLongTimeStamp(DateTime.Now);


                    userModel.SetLastTime(userid, userInfo.lasttime);
                    userModel.SetLastTimeStamp(userid, userInfo.lasttimestamp);


                    resData.Userinfo = new PbCommon.UserInfo {
                        Userid = userInfo.uid.ToString(),
                        Name = userInfo.username,
                        Money = userInfo.money,
                        Lasttimestamp = userInfo.lasttimestamp,
                    };
                }
                else
                {
                    //创建账号

                    var user = new UserInfo()
                    {
                        username = person.Name,
                        money = 10000L,
                        lasttime = DateTime.Now.ToString(),
                        lasttimestamp = DateTimeUtil.DateTimeToLongTimeStamp(DateTime.Now),
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
                        Lasttimestamp = user.lasttimestamp,
                    };

                    logger.Info("创建新账号 : " + resData.Userinfo.Name);
                }

                
            }
            netMgr.SendData(peer, ProtoType.LuaProtoMsg, Protocal.ResLogin, resData);

            logger.Info(resData.Userinfo.Name+ " timestamp : " + resData.Userinfo.Lasttimestamp);
        }
    }
}
