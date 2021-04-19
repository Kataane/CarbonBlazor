using System.ComponentModel;

namespace CarbonBlazor {

    public enum ButtonSize
    {
        [Description("")]
        Primary,

        [Description("cb--btn--field")]
        Field,

        [Description("cb--btn--sm")]
        Sm,

        [Description("cb--btn--lg")]
        Lg,

        [Description("cb--btn--xl")]
        Xl,
    }

}