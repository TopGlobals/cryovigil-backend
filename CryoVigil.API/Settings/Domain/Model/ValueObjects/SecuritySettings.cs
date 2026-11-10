namespace CryoVigil.API.Settings.Domain.Model.ValueObjects;

public class SecuritySettings
{
    private SecuritySettings()
    {
    }

    public SecuritySettings(bool twoFactorEnabled)
    {
        TwoFactorEnabled = twoFactorEnabled;
    }

    public bool TwoFactorEnabled { get; private set; }

    public void Update(bool twoFactorEnabled)
    {
        TwoFactorEnabled = twoFactorEnabled;
    }
}
