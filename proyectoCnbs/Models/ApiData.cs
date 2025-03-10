﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyectoCnbs.Models
{
    public class ApiData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        public string NroTransaccion { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        public string FechaHoraTrn { get; set; }
        
        [Required(ErrorMessage = "El campo es obligatorio")]
        public DateTime FechaAConsultar { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        public int CuentaDeclaraciones { get; set; }
    
        //public string DatosComprimidos { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Column(TypeName = "nvarchar(max)")] 
        public string JsonDatos { get; set; }


    }
}
