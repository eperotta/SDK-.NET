using System.ComponentModel.DataAnnotations;

namespace TPEjemploHibrido.Models
{
    public class InfoModel
    {
        [Required (ErrorMessage="Debe completar el campo Security")]
        [StringLength (32)]
        public string Security { get; set; }

        [Required(ErrorMessage = "Debe completar el campo Merchant Id")]
        [StringLength(10)]
        public string MerchantId { get; set; }

        [Required(ErrorMessage = "Debe completar el campo Api Key")]
        [StringLength(50)]
        public string ApiKey { get; set; }

        [Required(ErrorMessage = "Debe completar el campo monto")]
        [StringLength(3)]
        public string Monto { get; set; }

        public string PublicRequestKey { get; set; }

        public string json { get; set; }

        public string RequestKey { get; set; }

        public string AnswerKey { get; set; }

        public string StatusCode { get; set; }

        public string StatusMessage { get; set; }

        public string ResultadoGAA { get; set; }
    }
}