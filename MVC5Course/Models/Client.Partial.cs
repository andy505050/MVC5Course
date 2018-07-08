namespace MVC5Course.Models
{
    using MVC5Course.Models.InputValidations;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(ClientMetaData))]
    public partial class Client : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ClientId == 0)
            {
                //pk為0代表新增
            }
            //if ((Longitude!=null)!= (Latitude!=null))
            if (Longitude.HasValue != Latitude.HasValue)
            {
                yield return new ValidationResult("經緯度要一起設定!", new[] { "Longitude", "Latitude" });
            }
        }
    }

    public partial class ClientMetaData
    {
        [Required]
        public int ClientId { get; set; }

        [StringLength(40, ErrorMessage = "欄位長度不得大於 40 個字元")]
        [Required]
        public string FirstName { get; set; }

        [StringLength(40, ErrorMessage = "欄位長度不得大於 40 個字元")]
        [Required]
        public string MiddleName { get; set; }

        [StringLength(40, ErrorMessage = "欄位長度不得大於 40 個字元")]
        [Required]
        public string LastName { get; set; }

        [StringLength(1, ErrorMessage = "欄位長度不得大於 1 個字元")]
        [Required]
        public string Gender { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        [Required]
        public Nullable<double> CreditRating { get; set; }

        [StringLength(7, ErrorMessage = "欄位長度不得大於 7 個字元")]
        public string XCode { get; set; }
        public Nullable<int> OccupationId { get; set; }

        [StringLength(20, ErrorMessage = "欄位長度不得大於 20 個字元")]
        public string TelephoneNumber { get; set; }

        [StringLength(100, ErrorMessage = "欄位長度不得大於 100 個字元")]
        public string Street1 { get; set; }

        [StringLength(100, ErrorMessage = "欄位長度不得大於 100 個字元")]
        public string Street2 { get; set; }

        [StringLength(100, ErrorMessage = "欄位長度不得大於 100 個字元")]
        public string City { get; set; }

        [StringLength(15, ErrorMessage = "欄位長度不得大於 15 個字元")]
        public string ZipCode { get; set; }
        public Nullable<double> Longitude { get; set; }
        public Nullable<double> Latitude { get; set; }
        public string Notes { get; set; }

        [StringLength(10, ErrorMessage = "欄位長度不得大於 10 個字元")]
        [IDNumber]
        public string IDnumber { get; set; }

        public virtual Occupation Occupation { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
