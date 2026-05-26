using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace MinimalApi.Dominio.Entidades

{
    public class Veiculo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } = default!;

        [Required]
        [StringLength(255)]
        public string Nome { get; set; } = default!;
        [Required]
        [StringLength(50)]
        public string Marca { get; set; } = default!;
        
        [Required]
        [Range(1900, 2100)]
        public int Ano { get; set; } = default!;
    }
}