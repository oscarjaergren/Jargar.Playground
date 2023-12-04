using System.ComponentModel;
using NLog.Config;
using NLog.LayoutRenderers;
using NLog.LayoutRenderers.Wrappers;

namespace Jargar.Playgrounds.Loggers.NLog;

[LayoutRenderer("Encrypt")]
[AmbientProperty("Encrypt")]
[ThreadAgnostic]
internal sealed class EncryptLayoutRendererWrapper : WrapperLayoutRendererBase
{
    /// <summary>
    ///     This stores the private key in plain text in code - this does not provide security, but obscurity.
    ///     Without being able to refer to custom hardware or a third party service (i.e. the internet), this is
    ///     the best we can do on a single client machine. The code as a whole should be obfuscated to support this.
    /// </summary>
    private const string PrivateKey = "KEY";

    internal EncryptLayoutRendererWrapper()
    {
        Encrypt = true;
    }

    [DefaultValue(value: true)]
    internal bool Encrypt { get; set; }

    protected override string Transform(string text)
    {
        //string encryptedMessage = EncryptionHelper.Encrypt(text, PrivateKey);

        return text;
    }
}