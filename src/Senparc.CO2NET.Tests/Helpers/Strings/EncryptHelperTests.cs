using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Helpers;
using System;
using System.Text;

namespace Senparc.CO2NET.Tests.Helpers
{
    [TestClass]
    public class EncryptHelperTests
    {
        string encypStr = "Senparc";

        #region SHA相关

        [TestMethod]
        public void GetSha1Test()
        {
            var result = EncryptHelper.GetSha1("jsapi_ticket=sM4AOVdWfPE4DxkXGEs8VMCPGGVi4C3VM0P37wVUCFvkVAy_90u5h9nbSlYy3-Sl-HhTdfl2fzFy1AOcHKP7qg&noncestr=Wm3WZYTPz0wzccnW&timestamp=1414587457&url=http://mp.weixin.qq.com?params=value");
            Assert.AreEqual("0f9de62fce790f9a083d5c99e95740ceb90c27ed", result);

            result = EncryptHelper.GetSha1(encypStr);
            Assert.AreEqual("6d5c5d6c9d36c88cf47e1b97b813b74c78111cb2", result);
        }

        [TestMethod]
        public void GetHmacSha256Test()
        {
            var msg = "{\"foo\":\"bar\"}";
            var sessionKey = "o0q0otL8aEzpcZL/FT9WsQ==";
            var result = EncryptHelper.GetHmacSha256(msg, sessionKey);
            Assert.AreEqual("654571f79995b2ce1e149e53c0a33dc39c0a74090db514261454e8dbe432aa0b", result);
        }

        #endregion

        #region MD5

        [TestMethod()]
        public void GetMD5Test()
        {
            string exceptMD5Result = "8C715F421744218AB5B9C8E8D7E64AC6";

            //常规方法
            var result = EncryptHelper.GetMD5(encypStr, Encoding.UTF8);
            Assert.AreEqual(exceptMD5Result, result);

            //重写方法
            result = EncryptHelper.GetMD5(encypStr);
            Assert.AreEqual(exceptMD5Result, result);

            //小写
            result = EncryptHelper.GetLowerMD5(encypStr, Encoding.UTF8);
            Assert.AreEqual(exceptMD5Result.ToLower(), result);
        }

        #endregion

        #region AES

        [TestMethod]
        public void AESEncryptTest()
        {
            //加密
            var inputBytes = Encoding.UTF8.GetBytes(encypStr);
            var iv = Encoding.UTF8.GetBytes("SENPARC_IV;SENPA");//16字节
            var key = "SENPARC_KEY";
            //var inputString = Convert.ToBase64String(inputBytes);
            //Console.WriteLine($"inputString：{inputString}");

            var encryptResult = Convert.ToBase64String(EncryptHelper.AESEncrypt(inputBytes, iv, key));
            Console.WriteLine("Result:" + encryptResult);
            Assert.AreEqual("Q0l9E//huAwYXzYmxMWusw==", encryptResult);

            //解密
            inputBytes = Convert.FromBase64String(encryptResult);
            var decryptResult = Encoding.UTF8.GetString(EncryptHelper.AESDecrypt(inputBytes, iv, key));
            Assert.AreEqual(encypStr, decryptResult);


            //Assert.AreEqual("U2FsdGVkX1/ZyXf7WU3LYGTAwpx0U3UvGxAtS8I+EUs=", result);
        }


        #endregion

    }
}
