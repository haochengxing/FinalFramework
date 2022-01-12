﻿using FirCommon.Utility;
using FirServer.Model;
using GameLibs.FirSango.Defines;
using MongoDB.Bson.Serialization;

namespace GameLibs.FirSango.Model
{
    public class UserModel : BaseModel
    {
        public UserModel() : base("UserInfo")
        {
            BsonClassMap.RegisterClassMap<UserInfo>();
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        public long AddUser(UserInfo user)
        {
            user.uid = AppUtil.NewGuidId();
            if (!Add<UserInfo>(user))
            {
                return 0L;
            }
            return user.uid;
        }

        public string GetUserName(long uid)
        {
            var result = Get<UserInfo>("username", u => u.uid == uid);
            return result == null ? null : result.username;
        }

        public void SetUserName(long uid, string value)
        {
            var filter = Builder.Update<UserInfo>("username", value);
            Set<UserInfo>(filter, u => u.uid == uid);
        }

        public long GetMoney(long uid)
        {
            var result = Get<UserInfo>("money", u => u.uid == uid);
            return result == null ? 0L : result.money;
        }

        public void SetMoney(long uid, long value)
        {
            var filter = Builder.Update<UserInfo>("money", value);
            Set<UserInfo>(filter, u => u.uid == uid);
        }

        public string GetLastTime(long uid)
        {
            var result = Get<UserInfo>("lasttime", u => u.uid == uid);
            return result == null ? null : result.lasttime;
        }

        public void SetLastTime(long uid, string value)
        {
            var filter = Builder.Update<UserInfo>("lasttime", value);
            Set<UserInfo>(filter, u => u.uid == uid);
        }

        public void SetLastTimeStamp(long uid, long value)
        {
            var filter = Builder.Update<UserInfo>("lasttimestamp", value);
            Set<UserInfo>(filter, u => u.uid == uid);
        }

        /// <summary>
        /// 用户是否存在
        /// </summary>
        public long ExistUser(string username, string password)
        {
            var result = Exist<UserInfo>(u => u.username == username && u.password == password);
            if (result != null)
            {
                return result.uid;
            }
            return 0L;
        }

        /// <summary>
        /// 用户是否存在
        /// </summary>
        public long ExistUser(string username)
        {
            var result = Exist<UserInfo>(u => u.username == username);
            if (result != null)
            {
                return result.uid;
            }
            return 0L;
        }

        public UserInfo GetUser(long uid)
        {
            var result = GetDoc<UserInfo>(u => u.uid == uid);
            return result;
        }
    }
}
