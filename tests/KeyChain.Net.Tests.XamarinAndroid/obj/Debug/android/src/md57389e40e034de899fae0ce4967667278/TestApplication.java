package md57389e40e034de899fae0ce4967667278;


public class TestApplication
	extends mono.android.app.Application
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
	}


	public TestApplication () throws java.lang.Throwable
	{
		super ();
	}

	public void onCreate ()
	{
		mono.android.Runtime.register ("KeyChain.Net.Tests.XamarinAndroid.TestApplication, KeyChain.Net.Tests.XamarinAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", TestApplication.class, __md_methods);
		super.onCreate ();
	}

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
