using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Entregable2_PD.Models.DBO.DTO
{
    /// <summary>
    /// 
    /// </summary>
    public class FormDto
    {
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "The 'name' parameter is required.")]
        [MinLength(2)]
        [MaxLength(30)]
        [RegularExpression(@"^([A-Za-zÑñÁáÉéÍíÓóÚú]+['\-]{0,1}[A-Za-zÑñÁáÉéÍíÓóÚú]+)(\s+([A-Za-zÑñÁáÉéÍíÓóÚú]+['\-]{0,1}[A-Za-zÑñÁáÉéÍíÓóÚú]+))*$", ErrorMessage = "Invalid Name")]
        public string? Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "The 'FirstName' parameter is required.")]
        [MinLength(2)]
        [MaxLength(20)]
        [RegularExpression(@"^([A-Za-zÑñÁáÉéÍíÓóÚú]+['\-]{0,1}[A-Za-zÑñÁáÉéÍíÓóÚú]+)(\s+([A-Za-zÑñÁáÉéÍíÓóÚú]+['\-]{0,1}[A-Za-zÑñÁáÉéÍíÓóÚú]+))*$", ErrorMessage = "Invalid FirstName")]
        public string? FirstName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "The 'LastName' parameter is required.")]
        [MinLength(2)]
        [MaxLength(20)]
        [RegularExpression(@"^([A-Za-zÑñÁáÉéÍíÓóÚú]+['\-]{0,1}[A-Za-zÑñÁáÉéÍíÓóÚú]+)(\s+([A-Za-zÑñÁáÉéÍíÓóÚú]+['\-]{0,1}[A-Za-zÑñÁáÉéÍíÓóÚú]+))*$", ErrorMessage = "Invalid LastName")]
        public string? LastName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "The 'name' parameter is required.")]
        [DataType(DataType.MultilineText, ErrorMessage = "Invalid Description")]
        public string? Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "The 'BirthDay' parameter is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date type")]
        //[RegularExpression(@"^(?:(?:19|20|21)[0-9]{2})-(((0[13578]|(10|12))-(0[1-9]|[1-2][0-9]|3[0-1]))|(02-(0[1-9]|[1-2][0-9]))|((0[469]|11)-(0[1-9]|[1-2][0-9]|30)))$", ErrorMessage = "Invalid date")]
        public DateTime BirthDay { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "The 'Message' parameter is required.")]
        [DataType(DataType.Text, ErrorMessage = "Invalid text type")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Invalid name format")]
        public string? Message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "The 'Address' parameter is required.")]
        public string? Address { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "The 'ZipCode' parameter is required.")]
        [StringLength(5)]
        [DataType(DataType.PostalCode, ErrorMessage = "Invalid cp")]
        [RegularExpression(@"^\d{5}(?:[-\s]\d{4})?$", ErrorMessage = "Invalid cp")]
        public string? ZipCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "The 'Telephone' parameter is required.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid telephone type")]
        [RegularExpression(@"^\+?[1-9][0-9]{7,14}$", ErrorMessage = "Invalid telephone")]
        public string? Telephone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "The 'Website' parameter is required.")]
        [DataType(DataType.Url, ErrorMessage = "Invalid url type")]
        public string? Website { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataType(DataType.Upload, ErrorMessage = "Invalid upload type")]
        public string? File { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "The 'PhotoUrl' parameter is required.")]
        [DataType(DataType.ImageUrl, ErrorMessage = "Invalid imgurl type")]
        public string? PhotoUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "The 'Nss' parameter is required.")]
        [RegularExpression(@"^(\d{2})(\d{2})(\d{2})\d{5}$", ErrorMessage = "Invalid nss")]
        public string? Nss { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "The 'Curp' parameter is required.")]
        [RegularExpression(@"^([A-Z&]|[a-z&]{1})([AEIOU]|[aeiou]{1})([A-Z&]|[a-z&]{1})([A-Z&]|[a-z&]{1})([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1])([HM]|[hm]{1})([AS|as|BC|bc|BS|bs|CC|cc|CS|cs|CH|ch|CL|cl|CM|cm|DF|df|DG|dg|GT|gt|GR|gr|HG|hg|JC|jc|MC|mc|MN|mn|MS|ms|NT|nt|NL|nl|OC|oc|PL|pl|QT|qt|QR|qr|SP|sp|SL|sl|SR|sr|TC|tc|TS|ts|TL|tl|VZ|vz|YN|yn|ZS|zs|NE|ne]{2})([^A|a|E|e|I|i|O|o|U|u]{1})([^A|a|E|e|I|i|O|o|U|u]{1})([^A|a|E|e|I|i|O|o|U|u]{1})([0-9]{2})$", ErrorMessage = "Invalid curp")]
        public string? Curp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "The 'Rfc' parameter is required.")]
        //[A-Z&Ñ]{3,4}[0-9]{2}(0[1-9]|1[012])(0[1-9]|[12][0-9]|3[01])[A-Z0-9]{2}[0-9A]$
        [RegularExpression(@"^(((?!(([CcKk][Aa][CcKkGg][AaOo])|([Bb][Uu][Ee][YyIi])|([Kk][Oo](([Gg][Ee])|([Jj][Oo])))|([Cc][Oo](([Gg][Ee])|([Jj][AaEeIiOo])))|([QqCcKk][Uu][Ll][Oo])|((([Ff][Ee])|([Jj][Oo])|([Pp][Uu]))[Tt][Oo])|([Rr][Uu][Ii][Nn])|([Gg][Uu][Ee][Yy])|((([Pp][Uu])|([Rr][Aa]))[Tt][Aa])|([Pp][Ee](([Dd][Oo])|([Dd][Aa])|([Nn][Ee])))|([Mm](([Aa][Mm][OoEe])|([Ee][Aa][SsRr])|([Ii][Oo][Nn])|([Uu][Ll][Aa])|([Ee][Oo][Nn])|([Oo][Cc][Oo])))))[A-Za-zñÑ&][aeiouAEIOUxX]?[A-Za-zñÑ&]{2}(((([02468][048])|([13579][26]))0229)|(\d{2})((02((0[1-9])|1\d|2[0-8]))|((((0[13456789])|1[012]))((0[1-9])|((1|2)\d)|30))|(((0[13578])|(1[02]))31)))[a-zA-Z1-9]{2}[\dAa])|([Xx][AaEe][Xx]{2}010101000))$", ErrorMessage = "Invalid rfc")]
        public string? Rfc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "The 'Ine' parameter is required.")]
        [RegularExpression(@"[A-Z]{6}[0-9]{8}[A-Z]{1}[0-9]{3}", ErrorMessage = "Invalid ine")]
        public string? Ine { get; set; }




    }
}
