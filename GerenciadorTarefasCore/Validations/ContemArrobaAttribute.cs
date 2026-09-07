using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GerenciadorTarefasCore.Validations
{
    public class ContemArrobaAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            var texto = value as string;
            
            if(texto == null || !texto.ToString().Contains("@"))
            {
                return new ValidationResult("O e-mail deve conter o caractere '@'.");
            }

            return ValidationResult.Success;
        }

    }
}
