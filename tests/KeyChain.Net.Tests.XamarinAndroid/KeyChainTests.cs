using System;
using NUnit.Framework;

namespace KeyChain.Net.Tests
{
	[TestFixture]
	public class KeyChainTests
	{
		IKeyChainHelper _keyChainHelper;

		[SetUp]
		public void Setup ()
		{
			#if __IOS
				_keyChainHelper = new KeyChain.Net.XamarinIOS.KeyChainHelper("myServiceId", false, Security.SecAccessible.Always);
			#elif __Android
			_keyChainHelper = new  KeyChain.Net.XamarinAndroid.KeyChainHelper(() => KeyChain.Net.Tests.XamarinAndroid.TestApplication.Context, "myKeyProtectionPassword");
			#endif
		}

		[Test]
		public void SetKey_ShouldStoreKeyInKeyChain_ReturnsTrue ()
		{
			var isSaved = _keyChainHelper.SetKey("myKey", "myKeyValue");
			Assert.IsTrue(isSaved);
			Assert.AreEqual("myKeyValue", _keyChainHelper.GetKey("myKey"));
		}

		[Test]
		public void GetKey_WhenKeyExists_ShouldReturnKey ()
		{
			var isSaved = _keyChainHelper.SaveKey("myKey", "myKeyValue");
			Assert.IsTrue(isSaved);

			var keyValue = _keyChainHelper.GetKey("myKey");
			Assert.AreEqual("myKeyValue", keyValue);
		}

		[Test]
		public void GetKey_WhenKeyDoesNotExists_ShouldReturnNull ()
		{
			var keyValue = _keyChainHelper.GetKey("myKey111");
			Assert.IsTrue(string.IsNullOrEmpty(keyValue));
		}

		[Test]
		public void DeleteKey_ShouldReturnTrue ()
		{
			var isSaved = _keyChainHelper.SaveKey("myKey", "myKeyValue");
			Assert.IsTrue(isSaved);

			var isDeleted = _keyChainHelper.DeleteKey("myKey");
			Assert.IsTrue(isDeleted);

			Assert.IsTrue(string.IsNullOrEmpty(_keyChainHelper.GetKey("myKey")));
		}

		[TearDown]
		public void Tear ()
		{
			_keyChainHelper = null;
		}

	}
}

