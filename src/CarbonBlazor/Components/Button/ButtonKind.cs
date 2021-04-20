using System.ComponentModel;

namespace CarbonBlazor
{
    public enum ButtonKind
    {
        [Description("")]
        Primary,

        [Description("secondary")]
        Secondary,

        [Description("tertiary")]
        Tertiary,

        [Description("ghost")]
        Ghost,

        [Description("danger")]
        Danger,

        [Description("danger--tertiary")]
        DangerTertiary,

        [Description("danger--ghost")]
        DangerGhost
    }
}