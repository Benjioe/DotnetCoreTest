using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


namespace DotnetCoreTest.ValidationAttributes
{
    /*
     * Des règles de validation différentes selon le niveau de privilèges du compte
     */
    public class PasswordValidationAttribute :  ValidationAttribute, IClientModelValidator
    {
        public PasswordValidationAttribute()
        {
                
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            throw new System.NotImplementedException();
            /*
            
             FormulaireViewModel formulaireViewModel = validationContext.ObjectInstance as FormulaireViewModel;


            string linkPropertyValue = formulaireViewModel.GetType().GetProperty(PropertyName).GetValue(formulaireViewModel).ToString();

            if (formulaireViewModel == null)
                return ValidationResult.Success;

            return new ValidationResult("Message");
            
            
             */
        }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
                yield return new ValidationResult(
                    $"Message",
                    new[] { "ReleaseDate" });
        }
    }
}