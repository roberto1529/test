public class  Creditos_Model 
{
   public int id_credito  { get; set; }     
   public decimal cedula  { get; set; }= 0m;              
   public string nombre  { get; set; } = string.Empty;               
   public decimal valor_credito  { get; set; } = 0m;
   public int plazo  { get; set; }                
   public decimal valor_cuota { get; set; } = 0m;
   public decimal tasa_interes  { get; set; } = 0m;     
}