﻿using CreativeMinds.CQS.Validators;
using NForum.Core.Dtos;
using NForum.CQS.Commands.Categories;
using NForum.CQS.Validators.Categories;
using NForum.Datastores;
using NForum.Infrastructure;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace NForum.Tests.Core.HandlerTests {

	[TestFixture]
	public class CategoryTests {

		[Test(Author = "Steen F. Tøttrup", Description = "Make sure that Create mthod gets called on the datastore, when a valid CreateCategoryCommand is provided")]
		public void CreateCategory() {
			var inputParameter = new Domain.Category("name", 1, "description");
			var command = new CreateCategoryCommand { Name = inputParameter.Name, SortOrder = inputParameter.SortOrder, Description = inputParameter.Description };
			var dto = Substitute.For<ICategoryDto>();

			var datastore = Substitute.For<ICategoryDatastore>();
			datastore.Create(inputParameter).Returns<ICategoryDto>(dto);
			var taskDatastore = Substitute.For<ITaskDatastore>();

			CreateCategoryCommandHandler handler = new CreateCategoryCommandHandler(datastore, taskDatastore);
			GenericValidationCommandHandlerDecorator<CreateCategoryCommand> val =
					new GenericValidationCommandHandlerDecorator<CreateCategoryCommand>(
						handler,
						new List<IValidator<NForum.CQS.Commands.Categories.CreateCategoryCommand>> { new NForum.CQS.Validators.Categories.CreateCategoryValidator() }
					);

			val.Execute(command);

			datastore.ReceivedWithAnyArgs(1).Create(inputParameter);
		}

		[Test(Author = "Steen F. Tøttrup", Description = "Make sure that Update method gets called on the datastore, when a valid UpdateCategoryCommand is provided")]
		public void UpdateCategory() {
			var inputParameter = new Domain.Category("name", 1, "description");
			var command = new UpdateCategoryCommand { Id = "1", Name = inputParameter.Name, SortOrder = inputParameter.SortOrder, Description = inputParameter.Description };
			var dto = Substitute.For<ICategoryDto>();

			var datastore = Substitute.For<ICategoryDatastore>();
			datastore.Create(inputParameter).Returns<ICategoryDto>(dto);
			var taskDatastore = Substitute.For<ITaskDatastore>();

			UpdateCategoryCommandHandler handler = new UpdateCategoryCommandHandler(datastore, taskDatastore);
			GenericValidationCommandHandlerDecorator<UpdateCategoryCommand> val =
					new GenericValidationCommandHandlerDecorator<UpdateCategoryCommand>(
						handler,
						new List<IValidator<UpdateCategoryCommand>> { new UpdateCategoryValidator(TestUtils.GetInt32IdValidator()) }
					);

			val.Execute(command);

			datastore.ReceivedWithAnyArgs(1).Update(inputParameter);
		}
	}
}
