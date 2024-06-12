using System.ComponentModel.DataAnnotations;

namespace AP1_P1_StevenCandelario.Models;

public class Articulos
{
	[Key]
	public int ArticuloId {  get; set; }

	[Required(ErrorMessage = "El campo Descripcion es obligatorio")]
	[RegularExpression(@"^[a-zA-ZñÑ\s]+$", ErrorMessage ="La descripcion no debe contener caracteres especiales")]
	public string? Descripcion { get; set; }

	[Required(ErrorMessage = "El campo PctItbis es obligatorio")]
	[Range (0, 18, ErrorMessage = "Ingrese un numero entre 0 y 18")]
	public decimal PctItbis { get; set; }
	
	[Required(ErrorMessage = "El campo Costo es obligatorio")]
	[Range (0.01, 20000000, ErrorMessage = "Ingrese un numero mayor a 0")]
	public decimal Costo { get; set; }
	public decimal Itbis { get; set; }
}
