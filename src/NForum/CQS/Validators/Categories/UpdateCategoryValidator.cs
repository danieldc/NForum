﻿using CreativeMinds.CQS.Validators;
using NForum.CQS.Commands.Categories;
using System;

namespace NForum.CQS.Validators.Categories {

	public class UpdateCategoryValidator : IValidator<UpdateCategoryCommand> {

		public ValidationResult Validate(UpdateCategoryCommand command) {
			ValidationResult result = new ValidationResult();

			if (String.IsNullOrWhiteSpace(command.Id)) {
				result.AddError("TODO; missing id", -1);
			}

			if (String.IsNullOrWhiteSpace(command.Name)) {
				result.AddError("TODO; missing name", -1);
			}

			return result;
		}
	}
}
