using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using KeyChain.Net.XamarinWinRT;

namespace KeyChain.Net.Tests.XamarinWinPhone
{
    [TestClass]
    public class UnitTestApp
    {
        IKeyChainHelper _keyChainHelper;

        [TestInitialize]
        public void Setup()
        {
            _keyChainHelper = new KeyChainHelper();
        }

        [TestMethod]
        public void SetKey_ShouldStoreKeyInKeyChain_ReturnsTrue()
        {
            var isSaved = _keyChainHelper.SetKey("myKey", "myKeyValue");
            Assert.IsTrue(isSaved);
            Assert.AreEqual("myKeyValue", _keyChainHelper.GetKey("myKey"));
        }

        [TestMethod]
        public void GetKey_WhenKeyExists_ShouldReturnKey()
        {
            var isSaved = _keyChainHelper.SaveKey("myKey", "myKeyValue");
            Assert.IsTrue(isSaved);

            var keyValue = _keyChainHelper.GetKey("myKey");
            Assert.AreEqual("myKeyValue", keyValue);
        }

        [TestMethod]
        public void GetKey_WhenKeyDoesNotExists_ShouldReturnNull()
        {
            var keyValue = _keyChainHelper.GetKey("myKey111");
            Assert.IsTrue(string.IsNullOrEmpty(keyValue));
        }

        [TestMethod]
        public void DeleteKey_ShouldReturnTrue()
        {
            var isSaved = _keyChainHelper.SaveKey("myKey", "myKeyValue");
            Assert.IsTrue(isSaved);

            var isDeleted = _keyChainHelper.DeleteKey("myKey");
            Assert.IsTrue(isDeleted);

            Assert.IsTrue(string.IsNullOrEmpty(_keyChainHelper.GetKey("myKey")));
        }

    }
}
