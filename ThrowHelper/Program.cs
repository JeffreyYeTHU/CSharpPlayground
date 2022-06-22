using ThrowHelper;

Console.WriteLine("Hello, World!");
ArgumentNullException.ThrowIfNull(null);
ThrowUtil.ThrowIfNullOrEmpty(string.Empty);