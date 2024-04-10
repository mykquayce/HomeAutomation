namespace System;

public static class UriExtensions
{
	public static Uri GetBaseAddress(this Uri uri)
	{
		var builder = new UriBuilder(uri.Scheme, uri.Host, uri.Port);
		return builder.Uri;
	}
}
