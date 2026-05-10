using System.ComponentModel.DataAnnotations;

public class RegisterViewModel
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "Digite um e-mail válido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória.")]
    [DataType(DataType.Password)]
    [RegularExpression(
        @"^(?=.{6,16}$)(?=.*[A-Za-z])(?=.*\d)(?=.*[^A-Za-z0-9]).*$",
        ErrorMessage = "A senha deve ter entre 6 e 16 caracteres e conter letras, números e caracteres especiais.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Confirme sua senha.")]
    [Compare("Password", ErrorMessage = "As senhas não conferem.")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
}