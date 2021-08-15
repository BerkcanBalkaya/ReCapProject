using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);// Burada çalışma anında çalışması için reflection kullanmışız.
            var entityType = _validatorType.BaseType.GetGenericArguments()[0]; //Sonra productValidatorun basetypeının generic argümanlarından ilkini bul diyoruz attribute sonrası gelen methodun yani
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType); //Sonra da onun parametrelerini bul diyoruz.
            foreach (var entity in entities)//Burada ise her birini tek tek gez ve validation tool u kullanarak onları doğrula diye bitiriyoruz.
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}