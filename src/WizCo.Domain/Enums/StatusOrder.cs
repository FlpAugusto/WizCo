using System.ComponentModel.DataAnnotations;

namespace WizCo.Domain.Enums
{
    public enum StatusOrder
    {
        [Display(Name = "Novo")]
        New,

        [Display(Name = "Pago")]
        Paid,

        [Display(Name = "Cancelado")]
        Canceled
    }
}
